using Stack;
using System;

namespace Calc
{
    static class Calculator
    {
        static private float Calculate(float value1, float value2, char operand)
        {
            switch (operand)
            {
                case '+':
                    return value1 + value2;
                case '-':
                    return value1 - value2;
                case '*':
                    return value1 * value2;
                case '/':
                    if (Math.Abs(value2) < float.Epsilon)
                    {
                        throw new DivideByZeroException();
                    }
                    return value1 / value2;
                default:
                    throw new InvalidDataException();
            }
        }

        static public float Compute(string inputString)
        {
            Stack<float> stack = new Stack<float>();
            foreach (string element in inputString.Split(' '))
            {
                int number = 0;
                bool elementIsNumber = int.TryParse(element, out number);
                if (elementIsNumber || element.Length == 0)
                {
                    stack.Push((float)number);
                }
                else
                {
                    float value2 = stack.Pop();
                    float value1 = stack.Pop();
                    stack.Push(Calculate(value1, value2, element[0]));
                }
            }
            return stack.Pop();
        }
    }
}
