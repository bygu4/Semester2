namespace LZWEncoder;
using Trie;
using BurrowsWheeler;
using System;

public static class Encoder
{
    private static byte[] GetBytes(FileStream stream)
    {
        byte[] buffer = new byte[stream.Length];
        stream.Read(buffer, 0, buffer.Length);
        stream.Close();
        return buffer;
    }

    private static (string, int, int) Compress_Setup(Trie<int> dictionary,
        FileStream inputStream, FileStream resultStream)
    {
        byte[] inputBytes = GetBytes(inputStream);
        string inputData = System.Text.Encoding.UTF8.GetString(inputBytes);
        (inputData, int BWTPosition) = BWT.Transform(inputData);
        int numberOfSpecialCharacters = 0;
        foreach (char character in inputData)
        {
            if (dictionary.Add(character, dictionary.Size))
            {
                resultStream.WriteByte(Convert.ToByte(character));
                ++numberOfSpecialCharacters;
            }
        }
        return (inputData, BWTPosition, numberOfSpecialCharacters);
    }

    private static (string, int) Decompress_Setup(Dictionary<int, string> dictionary, 
        FileStream inputStream, FileStream resultStream)
    {
        byte[] inputBytes = GetBytes(inputStream);
        int numberOfSpecialCharacters = inputBytes[inputBytes.Length - 1];
        int BWTPosition = inputBytes[inputBytes.Length - 2];
        while (dictionary.Count < numberOfSpecialCharacters)
        {
            dictionary.Add(dictionary.Count, Convert.ToChar(inputBytes[dictionary.Count]).ToString());
        } 
        dictionary.TryAdd(0, "");
        string inputData = System.Text.Encoding.UTF8.GetString(inputBytes).Substring(
            numberOfSpecialCharacters, inputBytes.Length - numberOfSpecialCharacters - 1);
        return (inputData, BWTPosition);
    }

    public static float Compress(string filePath)
    {
        FileStream inputStream = File.OpenRead(filePath);
        FileStream resultStream = File.OpenWrite(filePath + ".zipped");
        long inputFileLength = inputStream.Length;
        Trie<int> words = new Trie<int>();
        (string inputData, int BWTPosition, int numberOfSpecialCharacters) = 
            Compress_Setup(words, inputStream, resultStream);

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
                resultStream.WriteByte(Convert.ToByte(words.Value(currentWord)));
                words.Add(temp, words.Size);
                currentWord = next;
            }
        }
        resultStream.WriteByte(Convert.ToByte(words.Value(currentWord)));
        resultStream.WriteByte(Convert.ToByte(BWTPosition));
        resultStream.WriteByte(Convert.ToByte(numberOfSpecialCharacters));
        long resultFileLength = resultStream.Length;
        resultStream.Close();
        return (float)inputFileLength / resultFileLength;
    }

    public static void Decompress(string filePath)
    {
        FileStream inputStream = File.OpenRead(filePath);
        FileStream resultStream = File.OpenWrite(
            Path.Join(Path.GetDirectoryName(filePath), Path.GetFileNameWithoutExtension(filePath)));
        Dictionary<int, string> words = new Dictionary<int, string>();
        (string inputData, int BWTPosition) = Decompress_Setup(words, inputStream, resultStream);

        char currentCode = inputData[0];
        string encodedData = words[currentCode];
        for (int i = 1; i < inputData.Length - 1; ++i)
        {
            char next = inputData[i];
            string output;
            string addition;
            if (words.ContainsKey(next))
            {
                output = words[next];
                addition = words[currentCode] + words[next][0];
            }
            else
            {
                output = words[currentCode] + words[currentCode][0];
                addition = output;
            }
            encodedData += output;
            words.Add(words.Count, addition);
            currentCode = next;
        }
        string result = BWT.ReverseTransform(encodedData, BWTPosition);
        resultStream.Write(System.Text.Encoding.UTF8.GetBytes(result));
        resultStream.Close();
    }
}
