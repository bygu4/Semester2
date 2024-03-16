namespace LZWEncoder;

using Trie;
using BurrowsWheeler;
using ByteIO;

using System;

public static class Encoder
{
    private const int numberOfEncodingBits = 12;

    private const int lastByteIndexOffset = 10;
    private const int lastByteInfoOffset = 9;
    private const int BWTPositionOffset = 8;
    private const int numberOfUniqueCharactersOffset = 4;

    private class CompressionInfo
    {
        public int lastByteIndex;
        public int lastByteShift;
        public bool lastByteCutOff;
        public int BWTPosition;
        public int numberOfUniqueCharacters;

        public CompressionInfo()
        {
        }

        public void Write(ByteWriter writer)
        {
            int lastByteInfo = (lastByteShift << 1) + (lastByteCutOff ? 1 : 0);
            writer.WriteByte(Convert.ToByte(lastByteInfo));
            writer.WriteNumber(BWTPosition);
            writer.WriteNumber(numberOfUniqueCharacters);
        }

        public void Read(byte[] bytes)
        {
            numberOfUniqueCharacters = BitConverter.ToInt32(bytes,
                bytes.Length - numberOfUniqueCharactersOffset);
            BWTPosition = BitConverter.ToInt32(bytes, bytes.Length - BWTPositionOffset);
            int lastByteInfo = bytes[bytes.Length - lastByteInfoOffset];
            lastByteShift = lastByteInfo >> 1;
            lastByteCutOff = (lastByteInfo & 1) == 1;
            lastByteIndex = bytes.Length - lastByteIndexOffset;
        }
    }

    private static byte[] GetBytes(FileStream stream)
    {
        byte[] buffer = new byte[stream.Length];
        stream.Read(buffer, 0, buffer.Length);
        stream.Close();
        return buffer;
    }

    private static int GetLengthOfCode(int sizeOfDictionary)
    {
        if (sizeOfDictionary < 1)
        {
            return 0;
        }
        if (sizeOfDictionary == 1)
        {
            return 1;
        }
        return (int)Math.Ceiling(Math.Log2(sizeOfDictionary));
    }

    private static void WriteAndAddUniqueCharacters(ByteWriter writer, Trie<int> dictionary, string data)
    {
        foreach (char character in data)
        {
            if (dictionary.Add(character, dictionary.Size))
            {
                writer.WriteCode(character);
            }
        }
    }

    private static (string, long, CompressionInfo, ByteWriter, Trie<int>) Compress_Setup(string filePath)
    {
        FileStream inputStream = File.OpenRead(filePath);
        string resultDirectory = Path.Join(Path.GetDirectoryName(filePath), "LZWCompression");
        Directory.CreateDirectory(resultDirectory);
        FileStream resultStream = File.OpenWrite(
            Path.Join(resultDirectory, Path.GetFileName(filePath)) + ".zipped");

        long inputFileLength = inputStream.Length;
        byte[] inputBytes = GetBytes(inputStream);
        ByteReader reader = new ByteReader(inputBytes, numberOfEncodingBits);
        string inputData = reader.GetString();
        CompressionInfo info = new CompressionInfo();
        info.lastByteCutOff = reader.LastByteCutOff;
        (inputData, info.BWTPosition) = BWT.Transform(inputData);

        ByteWriter writer = new ByteWriter(resultStream, numberOfEncodingBits);
        Trie<int> dictionary = new Trie<int>();
        WriteAndAddUniqueCharacters(writer, dictionary, inputData);
        return (inputData, inputFileLength, info, writer, dictionary);
    }

    public static float Compress(string filePath)
    {
        var (inputData, inputFileLength, compressionInfo, writer, words) =
            Compress_Setup(filePath);
        compressionInfo.numberOfUniqueCharacters = words.Size;

        writer.LengthOfCode = GetLengthOfCode(words.Size);
        string currentWord = "";
        for (int i = 0; i < inputData.Length; ++i)
        {
            string next = inputData[i].ToString();
            string temp = currentWord + next;
            if (words.Contains(temp))
            {
                currentWord = temp;
            }
            else
            {
                writer.WriteCode(words.Value(currentWord));
                words.Add(temp, words.Size);
                writer.LengthOfCode = int.Max(writer.LengthOfCode, GetLengthOfCode(words.Size));
                currentWord = next;
            }
        }
        if (words.Size > 0)
        {
            writer.WriteCode(words.Value(currentWord));
        }
        writer.EmptyBuffer();
        compressionInfo.lastByteShift = writer.Shift;
        compressionInfo.Write(writer);

        long resultFileLength = writer.GetLengthOfStream();
        writer.CloseStream();
        return (float)inputFileLength / resultFileLength;
    }

    private static void ReadAndAddUniqueCharacters(ByteReader reader, 
        Dictionary<int, string> dictionary, int numberOfUniqueCharacters)
    {
        for (int i = 0; i < numberOfUniqueCharacters; ++i)
        {
            char character = (char)reader.ReadCode();
            dictionary.Add(dictionary.Count, character.ToString());
        }
        dictionary.TryAdd(0, "");
    }

    private static (CompressionInfo, ByteReader, Dictionary<int, string>) Decompress_Setup(string filePath)
    {
        try
        {
            FileStream inputStream = File.OpenRead(filePath);
            byte[] inputBytes = GetBytes(inputStream);
            CompressionInfo info = new CompressionInfo();
            info.Read(inputBytes);

            ByteReader reader = new ByteReader(inputBytes, numberOfEncodingBits);
            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            ReadAndAddUniqueCharacters(reader, dictionary, info.numberOfUniqueCharacters);
            return (info, reader, dictionary);
        }
        catch (ArgumentOutOfRangeException e)
        {
            throw new InvalidDataException(null, e);
        }
    }

    public static void Decompress(string filePath)
    {
        var (compressionInfo, reader, words) = 
            Decompress_Setup(filePath);

        reader.LengthOfCode = GetLengthOfCode(words.Count);
        int currentCode = reader.ReadCode();
        try
        {
            string encodedData = words[currentCode];
            while (reader.CurrentIndex < compressionInfo.lastByteIndex || 
                reader.Shift < compressionInfo.lastByteShift)
            {
                reader.LengthOfCode = int.Max(reader.LengthOfCode, GetLengthOfCode(words.Count + 1));
                int next = reader.ReadCode();
                string output;
                string wordToAdd;
                if (words.ContainsKey(next))
                {
                    output = words[next];
                    wordToAdd = words[currentCode] + words[next][0];
                }
                else
                {
                    output = words[currentCode] + words[currentCode][0];
                    wordToAdd = output;
                }
                encodedData += output;
                words.Add(words.Count, wordToAdd);
                currentCode = next;
            }
            string result = BWT.ReverseTransform(encodedData, compressionInfo.BWTPosition);

            FileStream resultStream = File.OpenWrite(
                Path.Join(Path.GetDirectoryName(filePath), Path.GetFileNameWithoutExtension(filePath)));
            ByteWriter writer = new ByteWriter(resultStream, numberOfEncodingBits);
            writer.GetBytesAndWrite(result, compressionInfo.lastByteCutOff);
            writer.CloseStream();
        }
        catch (Exception e) when (e is KeyNotFoundException || e is IndexOutOfRangeException)
        {
            throw new InvalidDataException(null, e);
        }
    }
}
