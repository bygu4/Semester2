// Copyright (c) Alexander Bugaev 2024
//
// Use of this source code is governed by an MIT license
// that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

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
