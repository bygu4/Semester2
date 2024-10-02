// Copyright (c) 2024
//
// Use of this source code is governed by an MIT license
// that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

namespace LZWEncoder;

using Trie;
using BurrowsWheeler;
using CodeIO;
using Utility;

using System;

/// <summary>
/// Class implementing a file archiver.
/// </summary>
public static class Encoder
{
    private const int LengthOfEncoding = 16;

    private enum Offsets
    {
        BWTPositionOffset = 12,
        NumberOfUniqueCharactersOffset = 8,
        LengthOfEncodedDataOffset = 4,
    }

    /// <summary>
    /// Compress the file at the given path.
    /// Compressed files are stored in the LZWCompression directory relatively
    /// to the original file. The '.zipped' extension is added to the compressed file.
    /// </summary>
    /// <param name="filePath">The path of the file to compress.</param>
    /// <returns>The compression ratio that is the length of the original file
    /// divided by the length of compressed file.</returns>
    public static float Compress(string filePath)
    {
        var (inputData, inputFileLength, compressionInfo, writer, words) =
            Compress_Setup(filePath);
        compressionInfo.NumberOfUniqueCharacters = words.Size;

        writer.LengthOfCode = Utility.GetLengthOfCode(words.Size);
        var currentWord = string.Empty;
        for (int i = 0; i < inputData.Length; ++i)
        {
            var next = inputData[i].ToString();
            var newWord = currentWord + next;
            if (words.Contains(newWord))
            {
                currentWord = newWord;
            }
            else
            {
                writer.WriteCode(words.Value(currentWord));
                words.Add(newWord, words.Size);
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
        var resultFileLength = writer.LengthOfStream;
        writer.CloseStream();
        return (float)inputFileLength / resultFileLength;
    }

    /// <summary>
    /// Decompress the file with '.zipped' extension at the given path.
    /// The decompressed file is stored in the same directory as the given one.
    /// The '.zipped' extension is deleted after the decompression.
    /// </summary>
    /// <param name="filePath">The path of the file to decompress.</param>
    /// <exception cref="InvalidDataException">The data in the
    /// given file is invalid and unable to decompress.</exception>
    public static void Decompress(string filePath)
    {
        var (compressionInfo, reader, words) = Decompress_Setup(filePath);

        reader.LengthOfCode = Utility.GetLengthOfCode(words.Count);
        var currentCode = reader.ReadCode();
        try
        {
            var encodedData = new char[compressionInfo.LengthOfEncodedData];
            int encodedDataIndex = 0;
            words[currentCode].CopyTo(encodedData);
            encodedDataIndex += words[currentCode].Length;
            while (encodedDataIndex < compressionInfo.LengthOfEncodedData)
            {
                reader.LengthOfCode = int.Max(
                    reader.LengthOfCode, Utility.GetLengthOfCode(words.Count + 1));
                var next = reader.ReadCode();
                string output;
                string wordToAdd;
                if (words.TryGetValue(next, out string? value))
                {
                    output = value;
                    wordToAdd = words[currentCode] + value[0];
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

            compressionInfo.ReadLastByteTrimmedInfo(reader);
            var encodedString = new string(encodedData);
            var result = BWT.ReverseTransform(encodedString, compressionInfo.BWTPosition);

            var resultStream = File.OpenWrite(
                Path.Join(Path.GetDirectoryName(filePath), Path.GetFileNameWithoutExtension(filePath)));
            var writer = new CodeWriter(resultStream, LengthOfEncoding);
            var lengthOfLastCode = compressionInfo.LastByteTrimmed ? LengthOfEncoding / 2 : LengthOfEncoding;
            writer.GetBytesAndWrite(result, lengthOfLastCode);
            writer.CloseStream();
        }
        catch (Exception e) when (e is KeyNotFoundException || e is IndexOutOfRangeException)
        {
            throw new InvalidDataException("Invalid file format", e);
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

    private static void ReadAndAddUniqueCharacters(
        CodeReader reader, Dictionary<int, string> dictionary, int numberOfUniqueCharacters)
    {
        for (int i = 0; i < numberOfUniqueCharacters; ++i)
        {
            var character = (char)reader.ReadCode();
            dictionary.Add(dictionary.Count, character.ToString());
        }

        dictionary.TryAdd(0, string.Empty);
    }

    private static (string, long, CompressionInfo, CodeWriter, Trie<int>) Compress_Setup(string filePath)
    {
        var inputStream = File.OpenRead(filePath);
        var resultDirectory = Path.Join(Path.GetDirectoryName(filePath), "LZWCompression");
        Directory.CreateDirectory(resultDirectory);
        var resultStream = File.OpenWrite(
            Path.Join(resultDirectory, Path.GetFileName(filePath)) + ".zipped");

        var inputFileLength = inputStream.Length;
        var inputBytes = Utility.GetBytes(inputStream);
        var reader = new CodeReader(inputBytes, LengthOfEncoding);
        var inputData = reader.GetString();

        var info = new CompressionInfo();
        info.LastByteTrimmed = reader.LengthOfLastCode % LengthOfEncoding != 0;
        info.LengthOfEncodedData = inputData.Length;
        (inputData, info.BWTPosition) = BWT.Transform(inputData);

        var writer = new CodeWriter(resultStream, LengthOfEncoding);
        var dictionary = new Trie<int>();
        WriteAndAddUniqueCharacters(writer, dictionary, inputData);
        return (inputData, inputFileLength, info, writer, dictionary);
    }

    private static (CompressionInfo, CodeReader, Dictionary<int, string>) Decompress_Setup(string filePath)
    {
        if (Path.GetExtension(filePath) != ".zipped")
        {
            throw new InvalidDataException("Incorrect file extension. Expected: '.zipped'");
        }

        try
        {
            var inputStream = File.OpenRead(filePath);
            var inputBytes = Utility.GetBytes(inputStream);
            var info = new CompressionInfo();
            info.Read(inputBytes);

            var reader = new CodeReader(inputBytes, LengthOfEncoding);
            var dictionary = new Dictionary<int, string>();
            ReadAndAddUniqueCharacters(reader, dictionary, info.NumberOfUniqueCharacters);
            return (info, reader, dictionary);
        }
        catch (ArgumentOutOfRangeException e)
        {
            throw new InvalidDataException("Invalid file format", e);
        }
    }

    private class CompressionInfo
    {
        public bool LastByteTrimmed { get; set; }

        public int BWTPosition { get; set; }

        public int NumberOfUniqueCharacters { get; set; }

        public int LengthOfEncodedData { get; set; }

        public void Write(CodeWriter writer)
        {
            writer.LengthOfCode = 1;
            writer.WriteCode(this.LastByteTrimmed ? 1 : 0);
            writer.EmptyBuffer();

            writer.WriteNumber(this.BWTPosition);
            writer.WriteNumber(this.NumberOfUniqueCharacters);
            writer.WriteNumber(this.LengthOfEncodedData);
        }

        public void Read(byte[] bytes)
        {
            this.LengthOfEncodedData = BitConverter.ToInt32(
                bytes, bytes.Length - (int)Offsets.LengthOfEncodedDataOffset);
            this.NumberOfUniqueCharacters = BitConverter.ToInt32(
                bytes, bytes.Length - (int)Offsets.NumberOfUniqueCharactersOffset);
            this.BWTPosition = BitConverter.ToInt32(bytes, bytes.Length - (int)Offsets.BWTPositionOffset);

            if (this.LengthOfEncodedData < 0 || this.NumberOfUniqueCharacters < 0 || this.BWTPosition < 0)
            {
                throw new InvalidDataException("Invalid file format");
            }
        }

        public void ReadLastByteTrimmedInfo(CodeReader reader)
        {
            reader.LengthOfCode = 1;
            this.LastByteTrimmed = reader.ReadCode() == 1;
        }
    }
}
