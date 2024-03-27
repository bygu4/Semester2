// <copyright file="Program.cs" company="SPBU">
// Copyright (c) Alexander Bugaev 2024. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using Parsing;

if (args.Length != 1)
{
    Console.WriteLine("Missing argument: path of the file.");
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
