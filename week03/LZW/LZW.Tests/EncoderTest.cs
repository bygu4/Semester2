// Copyright (c) 2024
//
// Use of this source code is governed by an MIT license
// that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

namespace LZW.Tests;

using LZWEncoder;
using System;

static class EncoderTest
{
    private static string testFilesDirectory = Path.Join("../../..", "TestFiles");

    [TestCase("EmptyFile.txt")]
    [TestCase("SingleCharacter.txt")]
    [TestCase("SmallTextFile.txt")]
    public static void TestForEncoder_SmallTextFiles_FilesHaveNotChanged(string fileName)
    {
        var compressionRatio = Encoder.Compress(GetOriginalFilePath(fileName));
        AssertThatCompressionRatioIsValid(fileName, compressionRatio);
        Encoder.Decompress(GetCompressedFilePath(fileName));
        AssertThatFileHasNotChangedAfterDecompression(fileName);
    }

    [Test]
    public static void TestForEncoder_TryToCompressUnexistingFile_ThrowException()
    {
        Assert.Throws<FileNotFoundException>(
            () => { Encoder.Compress("ololo.txt"); });
    }

    [Test]
    public static void TestForEncoder_TryToDecompressUnexistingFile_ThrowException()
    {
        Assert.Throws<FileNotFoundException>(
            () => { Encoder.Decompress("ololo.txt.zipped"); });
    }

    [TestCase("Executable.exe")]
    [TestCase("LargeTextFile.txt")]
    public static void TestForEncoder_TryToDecompressUnzippedFile_ThrowException(string fileName)
    {
        Assert.Throws<InvalidDataException>(
            () => { Encoder.Decompress(GetOriginalFilePath(fileName)); });
    }

    [TestCase("EmptyFile.txt.zipped")]
    [TestCase("InvalidData.txt.zipped")]
    public static void TestForEncoder_TryToDecompressInvalidData_ThrowException(string fileName)
    {
        Assert.Throws<InvalidDataException>(
            () => { Encoder.Decompress(GetOriginalFilePath(fileName)); });
    }

    [Test, MaxTime(100)]
    public static void TestForEncoder_TextFileWithRepeatingSequence_HighCompressionAndFileHasNotChanged()
    {
        var fileName = "TextFileWithRepeatingSequence.txt";
        var compressionRatio = Encoder.Compress(GetOriginalFilePath(fileName));
        AssertThatCompressionRatioIsValid(fileName, compressionRatio);
        Assert.That(compressionRatio, Is.GreaterThan(15));
        Encoder.Decompress(GetCompressedFilePath(fileName));
        AssertThatFileHasNotChangedAfterDecompression(fileName);
    }

    [Test, MaxTime(5000)]
    public static void TestForEncoder_Image_FileHasNotChanged()
    {
        var fileName = "Image.jpg";
        var compressionRatio = Encoder.Compress(GetOriginalFilePath(fileName));
        AssertThatCompressionRatioIsValid(fileName, compressionRatio);
        Encoder.Decompress(GetCompressedFilePath(fileName));
        AssertThatFileHasNotChangedAfterDecompression(fileName);
    }

    [Test, MaxTime(20000)]
    public static void TestForEncoder_BigBinaryFile_MediumCompressionAndFileHasNotChanged()
    {
        var fileName = "Executable.exe";
        var compressionRatio = Encoder.Compress(GetOriginalFilePath(fileName));
        AssertThatCompressionRatioIsValid(fileName, compressionRatio);
        Assert.That(compressionRatio, Is.GreaterThan(3));
        Encoder.Decompress(GetCompressedFilePath(fileName));
        AssertThatFileHasNotChangedAfterDecompression(fileName);
    }

    [Test, MaxTime(20000)]
    public static void TestForEncoder_LargeTextFile_MediumCompressionAndFileHasNotChanged()
    {
        var fileName = "LargeTextFile.txt";
        var compressionRatio = Encoder.Compress(GetOriginalFilePath(fileName));
        AssertThatCompressionRatioIsValid(fileName, compressionRatio);
        Assert.That(compressionRatio, Is.GreaterThan(3));
        Encoder.Decompress(GetCompressedFilePath(fileName));
        AssertThatFileHasNotChangedAfterDecompression(fileName);
    }

    private static string GetOriginalFilePath(string fileName)
        => Path.Join(testFilesDirectory, fileName);

    private static string GetCompressedFilePath(string fileName)
        => Path.Join(testFilesDirectory, "LZWCompression", fileName) + ".zipped";

    private static string GetDecompressedFilePath(string fileName)
        => Path.Join(testFilesDirectory, "LZWCompression", fileName);

    private static bool AreEqual(float number1, float number2)
        => Math.Abs(number1 - number2) < float.Epsilon;

    private static void AssertThatCompressionRatioIsValid(string fileName, float encoderOutput)
    {
        var originalFile = File.OpenRead(GetOriginalFilePath(fileName));
        var compressedFile = File.OpenRead(GetCompressedFilePath(fileName));

        var comprassionRatio = (float)originalFile.Length / compressedFile.Length;
        originalFile.Close();
        compressedFile.Close();
        Assert.That(AreEqual(encoderOutput, comprassionRatio), Is.True);
    }

    private static void AssertThatFileHasNotChangedAfterDecompression(string fileName)
    {
        var originalFile = File.OpenRead(GetOriginalFilePath(fileName));
        var fileAfterDecompression = File.OpenRead(GetDecompressedFilePath(fileName));

        Assert.That(originalFile.Length, Is.EqualTo(fileAfterDecompression.Length));
        for (int i = 0; i < originalFile.Length; ++i)
        {
            Assert.That(originalFile.ReadByte(), Is.EqualTo(fileAfterDecompression.ReadByte()));
        }

        originalFile.Close();
        fileAfterDecompression.Close();
    }
}
