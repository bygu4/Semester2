// Copyright (c) 2024
//
// Use of this source code is governed by an MIT license
// that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

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
