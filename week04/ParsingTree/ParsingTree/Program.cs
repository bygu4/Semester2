// Copyright (c) Alexander Bugaev 2024
//
// Use of this source code is governed by an MIT license
// that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

using Parsing;

if (args.Length != 1)
{
    Console.WriteLine("Incorrect arguments. Expected: { path of the file }.");
}

try
{
    ParsingTree tree = new ParsingTree(args[0]);
    tree.PrintToConsole();
    Console.WriteLine($"\nValue of the expression: {tree.Evaluate()}");
}
catch (Exception e) when (e is InvalidDataException || e is DivideByZeroException)
{
    Console.WriteLine($"\nError: {e.Message}");
}
