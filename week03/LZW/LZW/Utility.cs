// <copyright file="Utility.cs" company="SPBU">
// Copyright (c) Alexander Bugaev 2024. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Utility;

/// <summary>
/// Class containing bitwise utility methods.
/// </summary>
public static class Utility
{
    /// <summary>
    /// Number of bits in the byte.
    /// </summary>
    public const int LengthOfByte = 8;

    /// <summary>
    /// Read the bytes from the given file stream.
    /// </summary>
    /// <param name="stream">File stream to read from.</param>
    /// <returns>An array of red bytes.</returns>
    public static byte[] GetBytes(FileStream stream)
    {
        var buffer = new byte[stream.Length];
        stream.Read(buffer, 0, buffer.Length);
        stream.Close();
        return buffer;
    }

    /// <summary>
    /// Left bitwise shift.
    /// </summary>
    /// <param name="number">Number to apply shift to.</param>
    /// <param name="shift">The shift in bits.
    /// If negative, the right bitwise shift is applied.</param>
    /// <returns>The number after shift.</returns>
    public static int LeftBitShift(int number, int shift)
    {
        if (shift < 0)
        {
            return number >> -shift;
        }

        return number << shift;
    }

    /// <summary>
    /// Right bitwise shift.
    /// </summary>
    /// <param name="number">Number to apply shift to.</param>
    /// <param name="shift">The shift in bits.
    /// If negative, the left bitwise shift is applied.</param>
    /// <returns>The number after shift.</returns>
    public static int RightBitShift(int number, int shift)
        => LeftBitShift(number, -shift);

    /// <summary>
    /// Get the length of code for the given number of possible outcomes.
    /// </summary>
    /// <param name="numberOfOutcomes">The number of possible outcomes.</param>
    /// <returns>The length of code.</returns>
    public static int GetLengthOfCode(int numberOfOutcomes)
    {
        if (numberOfOutcomes < 1)
        {
            return 0;
        }

        if (numberOfOutcomes == 1)
        {
            return 1;
        }

        return (int)Math.Ceiling(Math.Log2(numberOfOutcomes));
    }
}
