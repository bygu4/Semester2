using System;
using System.Collections.Immutable;
using System.Linq.Expressions;

namespace BurrowsWheeler
{
    class Program
    {
        static int CompareRotations(string inputString, int shift1, int shift2)
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

        static string GetResultString(string inputString, int[] shifts)
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
            for (int i = 0; i < inputString.Length - 1; ++i)
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

        static int[] GetShiftsArray(string inputString)
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

        static void Main(string[] args)
        {
            if (!Test.TestIsPassed())
            {
                throw new Exception("Test failed");
            }
            Console.WriteLine("0 - Exit\n" +
                "1 - Burrows-Wheeler Transformation\n" +
                "2 - Reverse Transformation");
            Console.WriteLine("\nEnter a command: ");
            int command = int.Parse(Console.ReadLine());

            while (command != 0)
            {
                switch (command)
                {
                    case 0:
                        continue;
                    case 1:
                        Console.WriteLine("\nEnter a string: ");
                        var inputString = Console.ReadLine();
                        (string, int) result = Transform(inputString);
                        Console.WriteLine("\nResult: {0}\nPosition: {1}", result.Item1, result.Item2);
                        break;
                    case 2:
                        Console.WriteLine("\nEnter a string: ");
                        inputString = Console.ReadLine();
                        Console.WriteLine("\nEnter a result position: ");
                        int position = int.Parse(Console.ReadLine());
                        Console.WriteLine("\nResult: {0}", ReverseTransform(inputString, position));
                        break;
                    default:
                        Console.WriteLine("\nUnknown command");
                        break;
                }
                Console.WriteLine("\nEnter a command: ");
                command = int.Parse(Console.ReadLine());
            }
        }
    }

    class Test
    {
        static bool CaseForTransformation(string inputString, (string, int) expectedOutput, 
            int numberOfTest)
        {
            bool passed = Program.Transform(inputString) == expectedOutput;
            if (!passed)
            {
                Console.WriteLine("Test {0} has failed", numberOfTest);
            }
            return passed;
        }

        static bool CaseForReverseTransformation(string inputString, int position, 
            string expectedOutput, int numberOfTest)
        {
            bool passed = Program.ReverseTransform(inputString, position) == expectedOutput;
            if (!passed)
            {
                Console.WriteLine("Test {0} has failed", numberOfTest);
            }
            return passed;
        }

        public static bool TestIsPassed()
        {
            return CaseForTransformation("BANANA", ("NNBAAA", 3), 1) &&
                CaseForTransformation("abcd ww ee", ("wdeabce w ", 2), 2) &&
                CaseForTransformation("", ("", 0), 3) &&
                CaseForReverseTransformation("NNBAAA", 3, "BANANA", 4) &&
                CaseForReverseTransformation("wdeabce w ", 2, "abcd ww ee", 5) &&
                CaseForReverseTransformation("", 454, "", 6);
        }
    }
}
