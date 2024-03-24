// Copyright 2024 Alexander Bugaev
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

using BurrowsWheeler;

static string GetInputString()
{
    string? inputString = Console.ReadLine();
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
    int position = int.Parse(GetInputString());
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
int command = GetCommand();
while (command != 0)
{
    switch (command)
    {
        case 0:
            continue;
        case 1:
            Console.WriteLine("\nEnter a string to transform: ");
            string inputString = GetInputString();
            (string, int) result = BWT.Transform(inputString);
            Console.WriteLine($"\nResult: {result.Item1}" +
                $"\nPosition: {result.Item2}");
            break;
        case 2:
            Console.WriteLine("\nEnter a string to transform: ");
            inputString = GetInputString();
            Console.WriteLine("\nEnter the result position (integer): ");
            try
            {
                int position = GetReverseBWTPosition(inputString.Length);
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
