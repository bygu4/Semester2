namespace LZWEncoder;

using Trie;
using BurrowsWheeler;
using System;

public static class Encoder
{
    private const int lastByteIndexOffset = 10;
    private const int lastByteShiftOffset = 9;
    private const int BWTPositionOffset = 8;
    private const int numberOfUniqueCharactersOffset = 4;

    private static byte[] GetBytes(FileStream stream)
    {
        byte[] buffer = new byte[stream.Length];
        stream.Read(buffer, 0, buffer.Length);
        stream.Close();
        return buffer;
    }

    private static int GetLengthInBits(int number)
    {
        int log = (int)Math.Floor(Math.Log2(number)) + 1;
        return log > 0 ? log : 0;
    }

    private static int LeftBitShift(int number, int shift)
    {
        if (shift < 0)
        {
            return number >> -shift;
        }
        return number << shift;
    }

    private static int RightBitShift(int number, int shift)
    {
        return LeftBitShift(number, -shift);
    }

    private static void WriteCode(FileStream stream, 
        int code, int lengthOfCode, ref int buffer, ref int shift)
    {
        int lengthOfByte = GetLengthInBits(byte.MaxValue);
        while (lengthOfCode > 0)
        {
            int bitsToAdd = LeftBitShift(code, lengthOfByte - lengthOfCode - shift);
            int sum = buffer | bitsToAdd;
            int addedBits = RightBitShift(sum - buffer, lengthOfByte - lengthOfCode - shift);
            buffer = sum;
            shift += lengthOfCode;
            lengthOfCode = shift - lengthOfByte;
            if (lengthOfCode >= 0)
            {
                stream.WriteByte(Convert.ToByte(buffer));
                buffer = 0;
                shift = 0;
            }
            code -= addedBits;
        }
    }

    private static (string, long, int, Trie<int>, FileStream) Compress_Setup(string filePath)
    {
        FileStream inputStream = File.OpenRead(filePath);
        string resultDirectory = Path.Join(Path.GetDirectoryName(filePath), "LZWCompression");
        Directory.CreateDirectory(resultDirectory);
        FileStream resultStream = File.OpenWrite(
            Path.Join(resultDirectory, Path.GetFileName(filePath)) + ".zipped");

        long inputFileLength = inputStream.Length;
        byte[] inputBytes = GetBytes(inputStream);
        string inputData = System.Text.Encoding.UTF8.GetString(inputBytes);
        (inputData, int BWTPosition) = BWT.Transform(inputData);

        Trie<int> dictionary = new Trie<int>();
        foreach (char character in inputData)
        {
            if (dictionary.Add(character, dictionary.Size))
            {
                resultStream.Write(BitConverter.GetBytes(character));
            }
        }
        return (inputData, inputFileLength, BWTPosition, dictionary, resultStream);
    }

    public static float Compress(string filePath)
    {
        var (inputData, inputFileLength, BWTPosition, words, resultStream) =
            Compress_Setup(filePath);

        int numberOfUniqueCharacters = words.Size;
        int lengthOfCode = GetLengthInBits(numberOfUniqueCharacters);
        int buffer = 0;
        int shift = 0;

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
                int code = words.Value(currentWord);
                WriteCode(resultStream, code, lengthOfCode, ref buffer, ref shift);
                words.Add(temp, words.Size);
                lengthOfCode = int.Max(lengthOfCode, GetLengthInBits(words.Size));
                currentWord = next;
            }
        }
        if (lengthOfCode > 0)
        {
            WriteCode(resultStream, words.Value(currentWord), lengthOfCode, ref buffer, ref shift);
        }
        if (shift > 0)
        {
            resultStream.WriteByte(Convert.ToByte(buffer));
        }
        resultStream.WriteByte(Convert.ToByte(shift));
        resultStream.Write(BitConverter.GetBytes(BWTPosition));
        resultStream.Write(BitConverter.GetBytes(numberOfUniqueCharacters));

        long resultFileLength = resultStream.Length;
        resultStream.Close();
        return (float)inputFileLength / resultFileLength;
    }

    private static int ReadCode(byte[] inputBytes, int lengthOfCode, 
        ref int currentIndex, ref int shift)
    {
        int lengthOfByte = GetLengthInBits(byte.MaxValue);
        int code = 0;
        while (lengthOfCode > 0)
        {
            int currentByte = inputBytes[currentIndex];
            int bitsToAdd = (currentByte & (byte.MaxValue >> shift)) -
                (currentByte & (byte.MaxValue >> (shift + lengthOfCode)));
            int addedBits = RightBitShift(bitsToAdd, lengthOfByte - lengthOfCode - shift);
            shift += lengthOfCode;
            lengthOfCode = shift - lengthOfByte;
            if (lengthOfCode >= 0)
            {
                ++currentIndex;
                shift = 0;
            }
            code += addedBits;
        }
        return code;
    }

    private static (byte[], int, int, Dictionary<int, string>, FileStream) Decompress_Setup(string filePath)
    {
        try
        {
            FileStream inputStream = File.OpenRead(filePath);
            FileStream resultStream = File.OpenWrite(
                Path.Join(Path.GetDirectoryName(filePath), Path.GetFileNameWithoutExtension(filePath)));

            byte[] inputBytes = GetBytes(inputStream);
            int numberOfUniqueCharacters = BitConverter.ToInt32(inputBytes,
                inputBytes.Length - numberOfUniqueCharactersOffset);
            int BWTPosition = BitConverter.ToInt32(inputBytes, inputBytes.Length - BWTPositionOffset);
            int lastByteShift = inputBytes[inputBytes.Length - lastByteShiftOffset];

            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            for (int i = 0; i < numberOfUniqueCharacters; ++i)
            {
                dictionary.Add(dictionary.Count, BitConverter.ToChar(inputBytes, i * 2).ToString());
            }
            dictionary.TryAdd(0, "");
            return (inputBytes, BWTPosition, lastByteShift, dictionary, resultStream);
        }
        catch (ArgumentOutOfRangeException e)
        {
            throw new InvalidDataException(null, e);
        }
    }

    public static void Decompress(string filePath)
    {
        var (inputBytes, BWTPosition, lastByteShift, words, resultStream) = 
            Decompress_Setup(filePath);

        int lengthOfCode = GetLengthInBits(words.Count);
        int currentIndex = words.Count * 2;
        int shift = 0;
        int currentCode = ReadCode(inputBytes, lengthOfCode, ref currentIndex, ref shift);

        string encodedData = words[currentCode];
        while (currentIndex <= inputBytes.Length - lastByteIndexOffset)
        {
            lengthOfCode = int.Max(lengthOfCode, GetLengthInBits(words.Count + 1));
            int next = ReadCode(inputBytes, lengthOfCode, ref currentIndex, ref shift);
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
            if (currentIndex == inputBytes.Length - lastByteIndexOffset && shift >= lastByteShift)
            {
                break;
            }
        }
        try
        {
            string result = BWT.ReverseTransform(encodedData, BWTPosition);
            resultStream.Write(System.Text.Encoding.UTF8.GetBytes(result));
            resultStream.Close();
        }
        catch (IndexOutOfRangeException e)
        {
            throw new InvalidDataException(null, e);
        }
    }
}
