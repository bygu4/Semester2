using LZWEncoder;

using System;

if (args.Length != 2 || args[1] != "-c" && args[1] != "-u")
{
    Console.WriteLine("Incorrect format. Try: {path of the file} {-c | -u}\n" +
        "c - compress file\n" +
        "u - decompress file");
    return;
}
try
{
    if (args[1] == "-c")
    {
        float ratio = Encoder.Compress(args[0]);
        Console.WriteLine("File was compressed");
        Console.WriteLine("Compression ratio: " + ratio);
    }
    else if (args[1] == "-u")
    {
        Encoder.Decompress(args[0]);
        Console.WriteLine("File was decompressed");
    }
}
catch (IOException e) when (e is FileNotFoundException || e is DirectoryNotFoundException)
{
    Console.WriteLine("Error: file not found");
}
catch (InvalidDataException)
{
    Console.WriteLine("Error: invalid file format");
}
