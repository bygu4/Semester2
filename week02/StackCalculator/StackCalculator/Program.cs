// Copyright 2024 Alexander Bugaev
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

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
