﻿// Copyright 2024 Alexander Bugaev
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

namespace BurrowsWheeler;

/// <summary>
/// Class implementing Burrows-Wheeler transformation.
/// </summary>
public static class BWT
{
    private static string transformingString = string.Empty;

/// <summary>
/// Transforms an input string by the Burrows-Wheeler algorithm.
/// </summary>
/// <param name="inputString">String to transform.</param>
/// <returns>Transformed string and
/// the position of initial string in the rotations matrix.</returns>
    public static (string, int) Transform(string inputString)
    {
        transformingString = inputString;
        int[] shifts = Enumerable.Range(0, inputString.Length).ToArray();
        Array.Sort(shifts, new RotationsComparer());
        int position = Array.IndexOf(shifts, 0);
        return (GetResultString(inputString, shifts), (position >= 0) ? position : 0);
    }

/// <summary>
/// Reverse Burrows-Wheeler transformation.
/// </summary>
/// <param name="inputString">String transformed by BWT algorithm.</param>
/// <param name="position">Position of string in the rotations matrix.</param>
/// <returns>String after reverse transformation.</returns>
/// <exception cref="IndexOutOfRangeException">
/// Position is not in range of the input string length.</exception>
    public static string ReverseTransform(string inputString, int position)
    {
        if (inputString.Length != 0 && (position < 0 || position >= inputString.Length))
        {
            throw new IndexOutOfRangeException();
        }

        char[] result = new char[inputString.Length];
        int[] shifts = GetShiftsArray(inputString);
        for (int i = 0; i < inputString.Length; ++i)
        {
            result[inputString.Length - i - 1] = inputString[position];
            position = shifts[position];
        }

        return new string(result);
    }

    private static string GetResultString(string inputString, int[] shifts)
    {
        char[] result = new char[inputString.Length];
        for (int i = 0; i < inputString.Length; ++i)
        {
            result[i] = inputString[(shifts[i] + inputString.Length - 1)
                % inputString.Length];
        }

        return new string(result);
    }

    private static int[] GetShiftsArray(string inputString)
    {
        int[] result = new int[inputString.Length];
        char[] sortedInput = inputString.ToArray();
        Array.Sort(sortedInput);
        Dictionary<char, List<int>> indices = new Dictionary<char, List<int>>();
        for (int i = 0; i < sortedInput.Length; ++i)
        {
            if (!indices.ContainsKey(sortedInput[i]))
            {
                indices.Add(sortedInput[i], new List<int>());
            }

            indices[sortedInput[i]].Add(i);
        }

        for (int i = 0; i < inputString.Length; ++i)
        {
            result[i] = indices[inputString[i]].First();
            indices[inputString[i]].RemoveAt(0);
        }

        return result;
    }

    private static int CompareRotations(string inputString, int shift1, int shift2)
    {
        for (int i = 0; i < inputString.Length; ++i)
        {
            if (inputString[(shift1 + i) % inputString.Length] <
                inputString[(shift2 + i) % inputString.Length])
            {
                return -1;
            }

            if (inputString[(shift1 + i) % inputString.Length] >
                inputString[(shift2 + i) % inputString.Length])
            {
                return 1;
            }
        }

        return 0;
    }

    private class RotationsComparer : IComparer<int>
    {
        int IComparer<int>.Compare(int number1, int number2)
        {
            return CompareRotations(transformingString, number1, number2);
        }
    }
}