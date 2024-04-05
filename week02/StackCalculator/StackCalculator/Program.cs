// <copyright file="Program.cs" company="SPBU">
// Copyright (c) Alexander Bugaev 2024. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using Stack;
using StackCalculator;

Console.WriteLine("----- Stack calculator -----");
Console.Write("\nEnter an expression in postfix form: ");
try
{
    string? inputString = Console.ReadLine();
    if (inputString is null)
    {
        throw new ArgumentNullException();
    }

    ListStack<float> stack = new ListStack<float>();
    Console.WriteLine($"\nResult: {Calculator.Compute(inputString, stack)}\n");
}
catch (Exception e) when (e is InvalidDataException || e is DivideByZeroException)
{
    Console.WriteLine($"\nError: {e.Message}\n");
}
