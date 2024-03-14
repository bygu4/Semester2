namespace LZW.Tests;

using LZWEncoder;
using System;

class EncoderTest
{
    private static string testFilesDirectory = Path.Join("..\\..\\..", "TestFiles");

    private static string GetOriginalFilePath(string fileName)
    {
        return Path.Join(testFilesDirectory, fileName);
    }
    private static string GetCompressedFilePath(string fileName)
    {
        return Path.Join(testFilesDirectory, "LZWCompression", fileName) + ".zipped";
    }

    private static string GetDecompressedFilePath(string fileName)
    {
        return Path.Join(testFilesDirectory, "LZWCompression", fileName);
    }

    private static bool AreEqual(float number1, float number2)
    {
        return Math.Abs(number1 - number2) < float.Epsilon;
    }

    private void AssertThatCompressionRatioIsValid(string fileName, float encoderOutput)
    {
        FileStream originalFile = File.OpenRead(GetOriginalFilePath(fileName));
        FileStream compressedFile = File.OpenRead(GetCompressedFilePath(fileName));

        float comprassionRatio = (float)originalFile.Length / compressedFile.Length;
        originalFile.Close();
        compressedFile.Close();
        Assert.That(AreEqual(encoderOutput, comprassionRatio), Is.True);
    }

    private void AssertThatFileHasNotChangedAfterDecompression(string fileName)
    {
        FileStream originalFile = File.OpenRead(GetOriginalFilePath(fileName));
        FileStream fileAfterDecompression = File.OpenRead(GetDecompressedFilePath(fileName));

        Assert.That(originalFile.Length, Is.EqualTo(fileAfterDecompression.Length));
        for (int i = 0; i < originalFile.Length; ++i)
        {
            Assert.That(originalFile.ReadByte(), Is.EqualTo(fileAfterDecompression.ReadByte()));
        }
        originalFile.Close();
        fileAfterDecompression.Close();
    }

    [SetUp]
    public void Setup()
    {
    }

    [TestCase("EmptyFile.txt")]
    [TestCase("SmallTextFile.txt")]
    public void TestForEncoder_SmallFiles_LowCompressionAndFilesHaveNotChanged(string fileName)
    {
        float compressionRatio = Encoder.Compress(GetOriginalFilePath(fileName));
        AssertThatCompressionRatioIsValid(fileName, compressionRatio);
        Encoder.Decompress(GetCompressedFilePath(fileName));
        AssertThatFileHasNotChangedAfterDecompression(fileName);
    }

    [Test]
    public void TestForEncoder_FileWithRepeatingSequence_HighCompressionAndFileHasNotChanged()
    {
        string fileName = "TextFileWithRepeatingSequence.txt";
        float compressionRatio = Encoder.Compress(GetOriginalFilePath(fileName));
        AssertThatCompressionRatioIsValid(fileName, compressionRatio);
        Assert.That(compressionRatio, Is.GreaterThan(10));
        Encoder.Decompress(GetCompressedFilePath(fileName));
        AssertThatFileHasNotChangedAfterDecompression(fileName);
    }

    [Test]
    public void TestForEncoder_LargeFile_MediumComprassionAndFileHasNotChanged()
    {
        string fileName = "LargeTextFile.txt";
        float compressionRatio = Encoder.Compress(GetOriginalFilePath(fileName));
        AssertThatCompressionRatioIsValid(fileName, compressionRatio);
        Assert.That(compressionRatio, Is.GreaterThan(3));
        Encoder.Decompress(GetCompressedFilePath(fileName));
        AssertThatFileHasNotChangedAfterDecompression(fileName);
    }

    [TestCase("Executable.exe")]
    [TestCase("Image.jpg")]
    public void TestForEncoder_NonTextFiles_FilesHaveNotChanged(string fileName)
    {
        float compressionRatio = Encoder.Compress(GetOriginalFilePath(fileName));
        AssertThatCompressionRatioIsValid(fileName, compressionRatio);
        Encoder.Decompress(GetCompressedFilePath(fileName));
        AssertThatFileHasNotChangedAfterDecompression(fileName);
    }

    [TestCase("EmptyFile.txt")]
    [TestCase("InvalidData.txt")]
    public void TestForEncoder_TryToDecompressInvalidData_ThrowException(string fileName)
    {
        Assert.Throws<InvalidDataException>(
            delegate { Encoder.Decompress(GetOriginalFilePath(fileName)); });
    }
}
