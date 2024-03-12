using LZWEncoder;
using System;

if (args.Length != 2)
{
    Console.WriteLine("Incorrect format. Try: LZW {path of the file} {-c | -u}\n" +
        "c - compress file\n" +
        "u - decompress file");
    return;
}
if (args[1] == "-c")
{
    float ratio = Encoder.Compress(args[0]);
    Console.WriteLine("File was compressed");
    Console.WriteLine("Compression ratio: " + ratio);
}
if (args[1] == "-u")
{
    Encoder.Decompress(args[0]);
    Console.WriteLine("File was decompressed");
}
