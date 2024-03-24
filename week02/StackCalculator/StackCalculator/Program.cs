// <copyright file="Program.cs" company="SPBU">
// Copyright (c) Alexander Bugaev 2024. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using StackCalculator;

Console.WriteLine("-----Stack calculator-----");
Console.WriteLine("\nEnter an expression in postfix form: ");
try
{
    string? inputString = Console.ReadLine();
    if (inputString is null)
    {
        throw new ArgumentNullException();
    }

    Console.WriteLine($"\nResult: {Calculator.Compute(inputString)}");
}
catch (Exception)
{
    Console.WriteLine("\nAn error occured");
}
