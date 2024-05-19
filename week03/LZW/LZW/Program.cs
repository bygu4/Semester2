// <copyright file="Program.cs" company="SPBU">
// Copyright (c) Alexander Bugaev 2024. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using LZWEncoder;

if (args.Length != 2 || (args[1] != "-c" && args[1] != "-u"))
{
    Console.WriteLine("Incorrect arguments. Try: {path of the file} {-c | -u}\n" +
        "c - compress file\n" +
        "u - decompress file");
    return;
}

try
{
    if (args[1] == "-c")
    {
        var ratio = Encoder.Compress(args[0]);
        Console.WriteLine("File was compressed");
        Console.WriteLine($"Compression ratio: {ratio}");
    }
    else if (args[1] == "-u")
    {
        Encoder.Decompress(args[0]);
        Console.WriteLine("File was decompressed");
    }
}
catch (IOException e) when (e is FileNotFoundException || e is DirectoryNotFoundException)
{
    Console.WriteLine("Error: File not found");
}
catch (InvalidDataException e)
{
    Console.WriteLine($"Error: {e.Message}");
}
