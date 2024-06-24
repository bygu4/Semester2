// <copyright file="CodeWriter.cs" company="SPBU">
// Copyright (c) Alexander Bugaev 2024. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace CodeIO;

using Utility;

/// <summary>
/// Class for writing codes of specific length to the file stream.
/// </summary>
public class CodeWriter
{
    private FileStream stream;
    private int buffer;
    private int shift;

    /// <summary>
    /// Initializes a new instance of the <see cref="CodeWriter"/> class.
    /// </summary>
    /// <param name="stream">File stream to write codes to.</param>
    /// <param name="lenghtOfCode">The bit length of codes to write.</param>
    public CodeWriter(FileStream stream, int lenghtOfCode)
    {
        this.stream = stream;
        this.LengthOfCode = lenghtOfCode;
    }

    /// <summary>
    /// Gets or sets the current bit length of codes to write.
    /// </summary>
    public int LengthOfCode { get; set; }

    /// <summary>
    /// Gets the length of stream in bytes.
    /// </summary>
    public long LengthOfStream { get => this.stream.Length; }

    /// <summary>
    /// Write the given integer code of current length to the file stream.
    /// </summary>
    /// <param name="code">Integer code to write.</param>
    public void WriteCode(int code)
    {
        var numberOfUnwrittenBits = this.LengthOfCode;
        while (numberOfUnwrittenBits > 0)
        {
            var bitsToAdd = Utility.LeftBitShift(
                code, Utility.LengthOfByte - numberOfUnwrittenBits - this.shift);
            var sum = this.buffer | bitsToAdd;
            var addedBits = Utility.RightBitShift(
                sum - this.buffer, Utility.LengthOfByte - numberOfUnwrittenBits - this.shift);
            this.buffer = sum;
            this.shift += numberOfUnwrittenBits;
            numberOfUnwrittenBits = this.shift - Utility.LengthOfByte;
            if (numberOfUnwrittenBits >= 0)
            {
                this.WriteCurrentBuffer();
            }

            code -= addedBits;
        }
    }

    /// <summary>
    /// If the current buffer is not empty, write it to the stream and set to null.
    /// </summary>
    public void EmptyBuffer()
    {
        if (this.shift > 0)
        {
            this.WriteCurrentBuffer();
        }
    }

    /// <summary>
    /// Convert given string to integer codes and write to the file stream.
    /// </summary>
    /// <param name="data">String to convert to codes.</param>
    /// <param name="lengthOfLastCode">The length of the last code to write.</param>
    public void GetBytesAndWrite(string data, int lengthOfLastCode)
    {
        for (int i = 0; i < data.Length - 1; ++i)
        {
            this.WriteCode(data[i]);
        }

        if (data.Length > 0)
        {
            this.LengthOfCode = lengthOfLastCode;
            this.WriteCode(data[^1]);
        }

        this.EmptyBuffer();
    }

    /// <summary>
    /// Write an Int32 number to the file stream.
    /// </summary>
    /// <param name="value">A number to write.</param>
    public void WriteNumber(int value)
        => this.stream.Write(BitConverter.GetBytes(value));

    /// <summary>
    /// Close the file stream.
    /// </summary>
    public void CloseStream()
        => this.stream.Close();

    private void WriteCurrentBuffer()
    {
        this.stream.WriteByte(Convert.ToByte(this.buffer));
        this.buffer = 0;
        this.shift = 0;
    }
}
