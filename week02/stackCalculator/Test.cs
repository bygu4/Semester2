using StackCalculator;
using System;

namespace Tests
{
    static class TestForCalculator
    {
        static private bool Equals(float number1, float number2)
        {
            return (Math.Abs(number1 - number2) < float.Epsilon);
        }

        static private void Case(string inputString, float expectedOutput,
            bool invalidInput, int numberOfTest)
        {
            try
            {
                float result = Calculator.Compute(inputString);
                if (!Equals(result, expectedOutput))
                {
                    throw new Exception();
                }
            }
            catch (Exception e)
            {
                if (!invalidInput || e is not DivideByZeroException && 
                    e is not InvalidDataException && e is not InvalidOperationException)
                {
                    throw new Exception($"Test {numberOfTest} has failed");
                }
            }
        }

        static public void Test()
        {
            Case("", 0, false, 1);
            Case("33 5 + 0 /", 0, true, 2);
            Case("rewrwewq", 0, true, 3);
            Case("8 +", 0, true, 4);
            Case("-99 100 /", -0.99f, false, 5);
            Case("7 90 83 - /", 1, false, 6);
            Case("-333 -2 *", 666, false, 7);
            Case("909090", 909090, false, 8);
            Case("-49 50 + 10 2 - /", 0.125f, false, 9);
        }
    }
}
