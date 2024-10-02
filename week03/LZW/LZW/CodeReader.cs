// Copyright (c) 2024
//
// Use of this source code is governed by an MIT license
// that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

namespace CodeIO;

using Utility;

/// <summary>
/// Class for reading codes of specific length from an array of bytes.
/// </summary>
public class CodeReader
{
    private byte[] bytes;
    private int currentIndex;
    private int shift;

    /// <summary>
    /// Initializes a new instance of the <see cref="CodeReader"/> class.
    /// </summary>
    /// <param name="bytes">Array of bytes to read code from.</param>
    /// <param name="lengthOfCode">The bit length of codes to read.</param>
    public CodeReader(byte[] bytes, int lengthOfCode)
    {
        this.bytes = bytes;
        this.LengthOfCode = lengthOfCode;
    }

    /// <summary>
    /// Gets or sets the current bit length of codes to read.
    /// </summary>
    public int LengthOfCode { get; set; }

    /// <summary>
    /// Gets the length of the last code red from the bytes array.
    /// </summary>
    public int LengthOfLastCode { get; private set; }

    /// <summary>
    /// Read the next code of current length from the bytes array.
    /// </summary>
    /// <returns>Integer code that was red.</returns>
    public int ReadCode()
    {
        var numberOfUnreadBits = this.LengthOfCode;
        int code = 0;
        while (numberOfUnreadBits > 0)
        {
            if (this.currentIndex >= this.bytes.Length)
            {
                this.LengthOfLastCode = this.LengthOfCode - numberOfUnreadBits;
                return code >> numberOfUnreadBits;
            }

            var currentByte = this.bytes[this.currentIndex];
            var unreadBitsShift = int.Min(numberOfUnreadBits, Utility.LengthOfByte);
            var bitsToAdd = (currentByte & (byte.MaxValue >> this.shift)) -
                (currentByte & (byte.MaxValue >> (this.shift + unreadBitsShift)));
            var addedBits = Utility.RightBitShift(
                bitsToAdd, Utility.LengthOfByte - numberOfUnreadBits - this.shift);
            this.shift += numberOfUnreadBits;
            numberOfUnreadBits = this.shift - Utility.LengthOfByte;
            if (numberOfUnreadBits >= 0)
            {
                ++this.currentIndex;
                this.shift = 0;
            }

            code += addedBits;
        }

        this.LengthOfLastCode = this.LengthOfCode;
        return code;
    }

    /// <summary>
    /// Read codes from a start to an end of the array and convert codes to a string.
    /// </summary>
    /// <returns>String after converting codes.</returns>
    public string GetString()
    {
        this.currentIndex = 0;
        this.shift = 0;
        var encodedData = new char[(int)Math.Ceiling(
            (float)this.bytes.Length * Utility.LengthOfByte / this.LengthOfCode)];
        for (int i = 0; this.currentIndex < this.bytes.Length; ++i)
        {
            encodedData[i] = (char)this.ReadCode();
        }

        return new string(encodedData);
    }
}
