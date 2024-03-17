namespace LZWEncoder;

using Trie;
using BurrowsWheeler;
using CodeIO;
using Utility;

using System;

public static class Encoder
{
    private const int lengthOfEncoding = 16;

    private enum Offsets
    {
        BWTPositionOffset = 12,
        NumberOfUniqueCharactersOffset = 8,
        LengthOfEncodedDataOffset = 4
    }

    private class CompressionInfo
    {
        public bool lastByteCutOff;
        public int BWTPosition;
        public int numberOfUniqueCharacters;
        public int lengthOfEncodedData;

        public CompressionInfo()
        {
        }

        public void Write(CodeWriter writer)
        {
            writer.LengthOfCode = 1;
            writer.WriteCode(lastByteCutOff ? 1 : 0);
            writer.EmptyBuffer();

            writer.WriteNumber(BWTPosition);
            writer.WriteNumber(numberOfUniqueCharacters);
            writer.WriteNumber(lengthOfEncodedData);
        }

        public void Read(byte[] bytes)
        {
            lengthOfEncodedData = BitConverter.ToInt32(bytes, 
                bytes.Length - (int)Offsets.LengthOfEncodedDataOffset);
            numberOfUniqueCharacters = BitConverter.ToInt32(bytes,
                bytes.Length - (int)Offsets.NumberOfUniqueCharactersOffset);
            BWTPosition = BitConverter.ToInt32(bytes, bytes.Length - (int)Offsets.BWTPositionOffset);

            if (lengthOfEncodedData < 0 || lengthOfEncoding < 0 || BWTPosition < 0)
            {
                throw new InvalidDataException("Invalid file format");
            }
        }

        public void ReadLastByteCutOffInfo(CodeReader reader)
        {
            reader.LengthOfCode = 1;
            lastByteCutOff = reader.ReadCode() == 1;
        }
    }

    private static void WriteAndAddUniqueCharacters(CodeWriter writer, Trie<int> dictionary, string data)
    {
        foreach (char character in data)
        {
            if (dictionary.Add(character, dictionary.Size))
            {
                writer.WriteCode(character);
            }
        }
    }

    private static (string, long, CompressionInfo, CodeWriter, Trie<int>) Compress_Setup(string filePath)
    {
        FileStream inputStream = File.OpenRead(filePath);
        string resultDirectory = Path.Join(Path.GetDirectoryName(filePath), "LZWCompression");
        Directory.CreateDirectory(resultDirectory);
        FileStream resultStream = File.OpenWrite(
            Path.Join(resultDirectory, Path.GetFileName(filePath)) + ".zipped");

        long inputFileLength = inputStream.Length;
        byte[] inputBytes = Utility.GetBytes(inputStream);
        CodeReader reader = new CodeReader(inputBytes, lengthOfEncoding);
        string inputData = reader.GetString();
        CompressionInfo info = new CompressionInfo();
        info.lastByteCutOff = reader.LastByteCutOff;
        info.lengthOfEncodedData = inputData.Length;
        (inputData, info.BWTPosition) = BWT.Transform(inputData);

        CodeWriter writer = new CodeWriter(resultStream, lengthOfEncoding);
        Trie<int> dictionary = new Trie<int>();
        WriteAndAddUniqueCharacters(writer, dictionary, inputData);
        return (inputData, inputFileLength, info, writer, dictionary);
    }

    public static float Compress(string filePath)
    {
        var (inputData, inputFileLength, compressionInfo, writer, words) =
            Compress_Setup(filePath);
        compressionInfo.numberOfUniqueCharacters = words.Size;

        writer.LengthOfCode = Utility.GetLengthOfCode(words.Size);
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
                writer.LengthOfCode = int.Max(
                    writer.LengthOfCode, Utility.GetLengthOfCode(words.Size));
                currentWord = next;
            }
        }
        if (words.Size > 0)
        {
            writer.WriteCode(words.Value(currentWord));
        }
        compressionInfo.Write(writer);

        long resultFileLength = writer.GetLengthOfStream();
        writer.CloseStream();
        return (float)inputFileLength / resultFileLength;
    }

    private static void ReadAndAddUniqueCharacters(CodeReader reader, 
        Dictionary<int, string> dictionary, int numberOfUniqueCharacters)
    {
        for (int i = 0; i < numberOfUniqueCharacters; ++i)
        {
            char character = (char)reader.ReadCode();
            dictionary.Add(dictionary.Count, character.ToString());
        }
        dictionary.TryAdd(0, "");
    }

    private static (CompressionInfo, CodeReader, Dictionary<int, string>) Decompress_Setup(string filePath)
    {
        if (Path.GetExtension(filePath) != ".zipped")
        {
            throw new InvalidDataException("Incorrect file extension. Expected: '.zipped'");
        }
        try
        {
            FileStream inputStream = File.OpenRead(filePath);
            byte[] inputBytes = Utility.GetBytes(inputStream);
            CompressionInfo info = new CompressionInfo();
            info.Read(inputBytes);

            CodeReader reader = new CodeReader(inputBytes, lengthOfEncoding);
            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            ReadAndAddUniqueCharacters(reader, dictionary, info.numberOfUniqueCharacters);
            return (info, reader, dictionary);
        }
        catch (ArgumentOutOfRangeException e)
        {
            throw new InvalidDataException("Invalid file format", e);
        }
    }

    public static void Decompress(string filePath)
    {
        var (compressionInfo, reader, words) = 
            Decompress_Setup(filePath);

        reader.LengthOfCode = Utility.GetLengthOfCode(words.Count);
        int currentCode = reader.ReadCode();
        try
        {
            char[] encodedData = new char[compressionInfo.lengthOfEncodedData];
            int encodedDataIndex = 0;
            words[currentCode].CopyTo(encodedData);
            encodedDataIndex += words[currentCode].Length;
            while (encodedDataIndex < compressionInfo.lengthOfEncodedData)
            {
                reader.LengthOfCode = int.Max(
                    reader.LengthOfCode, Utility.GetLengthOfCode(words.Count + 1));
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
                output.CopyTo(0, encodedData, encodedDataIndex, output.Length);
                encodedDataIndex += output.Length;
                words.Add(words.Count, wordToAdd);
                currentCode = next;
            }
            compressionInfo.ReadLastByteCutOffInfo(reader);
            string encodedString = new string(encodedData);
            string result = BWT.ReverseTransform(encodedString, compressionInfo.BWTPosition);

            FileStream resultStream = File.OpenWrite(
                Path.Join(Path.GetDirectoryName(filePath), Path.GetFileNameWithoutExtension(filePath)));
            CodeWriter writer = new CodeWriter(resultStream, lengthOfEncoding);
            writer.GetBytesAndWrite(result, compressionInfo.lastByteCutOff);
            writer.CloseStream();
        }
        catch (Exception e) when (e is KeyNotFoundException || e is IndexOutOfRangeException)
        {
            throw new InvalidDataException("Invalid file format", e);
        }
    }
}
