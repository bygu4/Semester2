﻿// <copyright file="Calculator.cs" company="SPBU">
// Copyright (c) Alexander Bugaev 2024. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace StackCalculator;

using Stack;
using System;

/// <summary>
/// A postfix calculator.
/// </summary>
public static class Calculator
{
    /// <summary>
    /// Parses an ariphmetic expression in postfix form and calculates its value.
    /// </summary>
    /// <param name="inputString">String to parse.</param>
    /// <param name="stack">Stack to use.</param>
    /// <returns>The calculated value.</returns>
    /// <exception cref="InvalidDataException">Failed to parse an expression.</exception>
    public static float Compute(string inputString, IStack<float> stack)
    {
        foreach (string element in inputString.Split(' '))
        {
            var elementIsNumber = int.TryParse(element, out int number);
            if (elementIsNumber || element.Length == 0)
            {
                stack.Push(number);
            }
            else
            {
                try
                {
                    var value2 = stack.Pop();
                    var value1 = stack.Pop();
                    stack.Push(Calculate(value1, value2, element[0]));
                }
                catch (InvalidOperationException e)
                {
                    throw new InvalidDataException("Incorrect expression", e);
                }
            }
        }

        var result = stack.Pop();
        if (!stack.IsEmpty())
        {
            throw new InvalidDataException("Incorrect expression");
        }

        return result;
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
                    throw new DivideByZeroException("Division by zero");
                }

                return value1 / value2;
            default:
                throw new InvalidDataException("Incorrect expression");
        }
    }
}
