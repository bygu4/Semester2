// Copyright (c) Alexander Bugaev 2024
//
// Use of this source code is governed by an MIT license
// that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

using Routers;

if (args.Length != 2)
{
    Console.WriteLine("Incorrect arguments. Expected: {input file path} {output file path}");
    return (int)ErrorCodes.IncorrectArguments;
}

try
{
    var network = new Network(args[0]);
    var isConnected = network.Configure();
    if (!isConnected)
    {
        Console.Error.Write("Error: Network is not connected\n");
    }

    network.WriteConfiguration(args[1]);
    return isConnected ? (int)ErrorCodes.Success : (int)ErrorCodes.NetworkIsNotConnected;
}
catch (IOException e) when (e is FileNotFoundException || e is DirectoryNotFoundException)
{
    Console.WriteLine("Error: File not found");
    return (int)ErrorCodes.FileNotfound;
}
catch (Exception e) when (e is InvalidDataException || e is ArgumentException)
{
    Console.WriteLine($"Error: {e.Message}");
    return (int)ErrorCodes.InvalidData;
}
