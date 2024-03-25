﻿// <copyright file="EncoderTest.cs" company="SPBU">
// Copyright (c) Alexander Bugaev 2024. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace LZW.Tests;

using LZWEncoder;
using System;

class EncoderTest
{
    private static string testFilesDirectory = Path.Join("../../..", "TestFiles");

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
    [TestCase("SingleCharacter.txt")]
    [TestCase("SmallTextFile.txt")]
    public void TestForEncoder_SmallTextFiles_FilesHaveNotChanged(string fileName)
    {
        float compressionRatio = Encoder.Compress(GetOriginalFilePath(fileName));
        AssertThatCompressionRatioIsValid(fileName, compressionRatio);
        Encoder.Decompress(GetCompressedFilePath(fileName));
        AssertThatFileHasNotChangedAfterDecompression(fileName);
    }

    [Test]
    public void TestForEncoder_TryToCompressUnexistingFile_ThrowException()
    {
        Assert.Throws<FileNotFoundException>(
            () => { Encoder.Compress("ololo.txt"); });
    }

    [Test]
    public void TestForEncoder_TryToDecompressUnexistingFile_ThrowException()
    {
        Assert.Throws<FileNotFoundException>(
            () => { Encoder.Decompress("ololo.txt.zipped"); });
    }

    [TestCase("Executable.exe")]
    [TestCase("LargeTextFile.txt")]
    public void TestForEncoder_TryToDecompressUnzippedFile_ThrowException(string fileName)
    {
        Assert.Throws<InvalidDataException>(
            () => { Encoder.Decompress(GetOriginalFilePath(fileName)); });
    }

    [TestCase("EmptyFile.txt.zipped")]
    [TestCase("InvalidData.txt.zipped")]
    public void TestForEncoder_TryToDecompressInvalidData_ThrowException(string fileName)
    {
        Assert.Throws<InvalidDataException>(
            () => { Encoder.Decompress(GetOriginalFilePath(fileName)); });
    }

    [Test, MaxTime(100)]
    public void TestForEncoder_TextFileWithRepeatingSequence_HighCompressionAndFileHasNotChanged()
    {
        string fileName = "TextFileWithRepeatingSequence.txt";
        float compressionRatio = Encoder.Compress(GetOriginalFilePath(fileName));
        AssertThatCompressionRatioIsValid(fileName, compressionRatio);
        Assert.That(compressionRatio, Is.GreaterThan(15));
        Encoder.Decompress(GetCompressedFilePath(fileName));
        AssertThatFileHasNotChangedAfterDecompression(fileName);
    }

    [Test, MaxTime(5000)]
    public void TestForEncoder_Image_FileHasNotChanged()
    {
        string fileName = "Image.jpg";
        float compressionRatio = Encoder.Compress(GetOriginalFilePath(fileName));
        AssertThatCompressionRatioIsValid(fileName, compressionRatio);
        Encoder.Decompress(GetCompressedFilePath(fileName));
        AssertThatFileHasNotChangedAfterDecompression(fileName);
    }

    [Test, MaxTime(20000)]
    public void TestForEncoder_BigBinaryFile_MediumCompressionAndFileHasNotChanged()
    {
        string fileName = "Executable.exe";
        float compressionRatio = Encoder.Compress(GetOriginalFilePath(fileName));
        AssertThatCompressionRatioIsValid(fileName, compressionRatio);
        Assert.That(compressionRatio, Is.GreaterThan(3));
        Encoder.Decompress(GetCompressedFilePath(fileName));
        AssertThatFileHasNotChangedAfterDecompression(fileName);
    }

    [Test, MaxTime(20000)]
    public void TestForEncoder_LargeTextFile_MediumCompressionAndFileHasNotChanged()
    {
        string fileName = "LargeTextFile.txt";
        float compressionRatio = Encoder.Compress(GetOriginalFilePath(fileName));
        AssertThatCompressionRatioIsValid(fileName, compressionRatio);
        Assert.That(compressionRatio, Is.GreaterThan(3));
        Encoder.Decompress(GetCompressedFilePath(fileName));
        AssertThatFileHasNotChangedAfterDecompression(fileName);
    }
}