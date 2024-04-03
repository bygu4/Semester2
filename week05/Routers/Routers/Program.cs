// <copyright file="Program.cs" company="SPBU">
// Copyright (c) Alexander Bugaev 2024. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using Routers;

if (args.Length != 2)
{
    Console.WriteLine("Incorrect arguments. Expected: {input file path} {output file path}");
    return (int)ErrorCodes.IncorrectArguments;
}

try
{
    Network network = new (args[0]);
    bool isConnected = network.Configure();
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
