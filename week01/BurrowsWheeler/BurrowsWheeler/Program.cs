// <copyright file="Program.cs" company="SPBU">
// Copyright (c) Alexander Bugaev 2024. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using BurrowsWheeler;

static string GetInputString()
{
    var inputString = Console.ReadLine();
    if (inputString is null)
    {
        throw new Exception("Failed to read a string from console");
    }

    return inputString;
}

static int GetCommand()
{
    try
    {
        return int.Parse(GetInputString());
    }
    catch (FormatException)
    {
        return -1;
    }
}

static int GetReverseBWTPosition(int length)
{
    var position = int.Parse(GetInputString());
    if (position >= 0 && position < length)
    {
        return position;
    }

    throw new IndexOutOfRangeException();
}

Console.WriteLine("----- Burrows-Wheeler -----");
Console.WriteLine("\n0 - Exit" +
    "\n1 - Burrows-Wheeler Transformation" +
    "\n2 - Reverse Transformation");

Console.WriteLine("\nEnter a command: ");
var command = GetCommand();
while (command != 0)
{
    switch (command)
    {
        case 0:
            continue;
        case 1:
            Console.WriteLine("\nEnter a string to transform: ");
            var inputString = GetInputString();
            var result = BWT.Transform(inputString);
            Console.WriteLine($"\nResult: {result.Item1}" +
                $"\nPosition: {result.Item2}");
            break;
        case 2:
            Console.WriteLine("\nEnter a string to transform: ");
            inputString = GetInputString();
            Console.WriteLine("\nEnter the result position (integer): ");
            try
            {
                var position = GetReverseBWTPosition(inputString.Length);
                Console.WriteLine($"\nResult: {BWT.ReverseTransform(inputString, position)}");
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("\nIndex out of range");
            }
            catch (FormatException)
            {
                Console.WriteLine("\nWrong format");
            }

            break;
        default:
            Console.WriteLine("\nUnknown command");
            break;
    }

    Console.WriteLine("\nEnter a command: ");
    command = GetCommand();
}
