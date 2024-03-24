// Copyright 2024 Alexander Bugaev
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

namespace StackCalculator;

using Stack;
using System;

/// <summary>
/// A postfix calculator.
/// </summary>
public static class Calculator
{
    /// <summary>
    /// Parses an ariphmetic expression in postfix form an calculates its value.
    /// </summary>
    /// <param name="inputString">String to parse.</param>
    /// <returns>The calculated value.</returns>
    /// <exception cref="InvalidDataException">Failed to parse an expression.</exception>
    public static float Compute(string inputString)
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
                try
                {
                    float value2 = stack.Pop();
                    float value1 = stack.Pop();
                    stack.Push(Calculate(value1, value2, element[0]));
                }
                catch (InvalidOperationException)
                {
                    throw new InvalidDataException();
                }
            }
        }

        return stack.Pop();
    }

    private static float Calculate(float value1, float value2, char operand)
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
}
