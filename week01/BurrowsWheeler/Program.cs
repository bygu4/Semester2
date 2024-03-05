using System;

namespace BurrowsWheeler
{
    static class Program
    {
        private static int CompareRotations(string inputString, int shift1, int shift2)
        {
            for (int i = 0; i < inputString.Length; ++i)
            {
                if (inputString[(shift1 + i) % inputString.Length] > 
                    inputString[(shift2 + i) % inputString.Length])
                {
                    return -1;
                }
                if (inputString[(shift1 + i) % inputString.Length] <
                    inputString[(shift2 + i) % inputString.Length])
                { 
                    return 1;
                }
            }
            return 0;
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

        public static (string, int) Transform(string inputString)
        {
            int position = 0;
            int[] shifts = Enumerable.Range(0, inputString.Length).ToArray();
            for (int i = 0; i < inputString.Length; ++i)
            {
                int min = i;
                for (int j = i + 1; j < inputString.Length; ++j)
                {
                    if (CompareRotations(inputString, shifts[min], shifts[j]) == -1)
                    {
                        min = j;
                    }
                }
                (shifts[i], shifts[min]) = (shifts[min], shifts[i]);
                if (shifts[i] == 0)
                {
                    position = i;
                }
            }
            return (GetResultString(inputString, shifts), position);
        }

        private static int[] GetShiftsArray(string inputString)
        {
            int[] result = new int[inputString.Length];
            char[] sortedInput = inputString.ToArray();
            Array.Sort(sortedInput);
            Dictionary<char, List<int>> indeces = new Dictionary<char, List<int>>();
            for (int i = 0; i < sortedInput.Length; ++i)
            {
                if (!indeces.ContainsKey(sortedInput[i]))
                {
                    indeces.Add(sortedInput[i], new List<int>());
                }
                indeces[sortedInput[i]].Add(i);
            }
            for (int i = 0; i < inputString.Length; ++i)
            {
                result[i] = indeces[inputString[i]].First();
                indeces[inputString[i]].RemoveAt(0);
            }
            return result;
        }

       public static string ReverseTransform(string inputString, int position)
        {
            char[] result = new char[inputString.Length];
            int[] shifts = GetShiftsArray(inputString);
            for (int i = 0; i < inputString.Length; ++i)
            {
                result[inputString.Length - i - 1] = inputString[position];
                position = shifts[position];
            }
            return new string(result);
        }

        private static string GetInputString()
        {
            string? inputString = Console.ReadLine();
            if (inputString is null)
            {
                throw new Exception("Failed to read a string from console");
            }
            return inputString;
        }

        private static int GetCommand()
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

        private static int GetReverseBWTPosition(int length)
        {
            int position = int.Parse(GetInputString());
            if (0 <= position && position < length)
            {
                return position;
            }
            throw new IndexOutOfRangeException();
        }

        static void Main()
        {
            if (!Test.TestIsPassed())
            {
                throw new Exception("Test failed");
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
                        (string, int) result = Transform(inputString);
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
                            Console.WriteLine($"\nResult: {ReverseTransform(inputString, position)}");
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
        }
    }

    static class Test
    {
        private static bool CaseForTransformation(string inputString, (string, int) expectedOutput, 
            int numberOfTest)
        {
            bool passed = Program.Transform(inputString) == expectedOutput;
            if (!passed)
            {
                Console.WriteLine($"Test {numberOfTest} has failed");
            }
            return passed;
        }

        private static bool CaseForReverseTransformation(string inputString, int position, 
            string expectedOutput, int numberOfTest)
        {
            bool passed = Program.ReverseTransform(inputString, position) == expectedOutput;
            if (!passed)
            {
                Console.WriteLine($"Test {numberOfTest} has failed", numberOfTest);
            }
            return passed;
        }

        public static bool TestIsPassed()
        {
            return CaseForTransformation("BANANA", ("NNBAAA", 3), 1) &&
                CaseForTransformation("abcd ww ee", ("wdeabce w ", 2), 2) &&
                CaseForTransformation("111111", ("111111", 0), 3) &&
                CaseForTransformation("", ("", 0), 4) &&
                CaseForReverseTransformation("NNBAAA", 3, "BANANA", 5) &&
                CaseForReverseTransformation("wdeabce w ", 2, "abcd ww ee", 6) &&
                CaseForReverseTransformation("111111", 4, "111111", 7) &&
                CaseForReverseTransformation("", 454, "", 8);
        }
    }
}
