// <copyright file="NetworkTest.cs" company="SPBU">
// Copyright (c) Alexander Bugaev 2024. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Routers.Tests;

using Routers;

public class NetworkTest
{
    private string testFilesDirectory = Path.Join("../../..", "TestFiles");

    [SetUp]
    public void Setup()
    {
    }

    [TestCaseSource(nameof(CorrectCases))]
    public bool TestForNetwork_CorrectFile_CheckConnectionsAndGetResult(
        string fileName, string connections)
    {
        Network testNetwork = new Network(GetTestFilePath(fileName));
        bool isConnected = testNetwork.Configure();
        AssertThatConnectionsAreEqual(testNetwork, ParseTestCaseArgument(connections));
        return isConnected;
    }

    [TestCase("IncorrectFormat.txt")]
    [TestCase("MissingCapacity.txt")]
    public void TestForNetwork_InvalidFileFormat_ThrowException(string fileName)
    {
        Assert.Throws<InvalidDataException>(() => { new Network(GetTestFilePath(fileName)); });
    }

    [TestCase("NegativeCapacity.txt")]
    [TestCase("IncorrectNumber.txt")]
    public void TestForNetwork_IncorrectData_ThrowException(string fileName)
    {
        Assert.Throws<ArgumentException>(() => { new Network(GetTestFilePath(fileName)); });
    }

    private string GetTestFilePath(string fileName)
    {
        return Path.Join(testFilesDirectory, fileName);
    }

    private static IEnumerable<TestCaseData> CorrectCases
    {
        get
        {
            yield return new TestCaseData("EmptyFile.txt", "").Returns(true);
            yield return new TestCaseData("SingleRouter.txt", "").Returns(true);
            yield return new TestCaseData("TwoConnectedRouters.txt", "0 1 2").Returns(true);
            yield return new TestCaseData("TwoDisconnectedRouters.txt", "").Returns(false);
            yield return new TestCaseData(
                "ThreeRoutersConnectedWithoutCycle.txt", "12 3 1; 5 1 2").Returns(true);
            yield return new TestCaseData(
                "ThreeRoutersConnectedWithCycle.txt", "10 1 2; 6 3 2").Returns(true);
            yield return new TestCaseData("TwoRoutersConnectedTwice.txt", "55 1 2").Returns(true);
            yield return new TestCaseData("NetworkWithLoop.txt", "13 1 2").Returns(true);
            yield return new TestCaseData(
                "DisconnectedNetworkOfFiveRouters.txt", "100 1 5; 34 2 4; 12 1 3").Returns(false);
            yield return new TestCaseData(
                "FiveRoutersConnectedWithCycles.txt",
                "100 2 5; 70 3 5; 42 1 3; 15 4 5").Returns(true);
        }
    }

    private (int, (int, int))[] ParseTestCaseArgument(string argument)
    {
        string[] elements = argument.Split("; ", StringSplitOptions.RemoveEmptyEntries);
        (int, (int, int))[] connections = new (int, (int, int))[elements.Length];
        for (int i = 0; i < elements.Length; ++i)
        {
            string[] split = elements[i].Split(' ');
            int capacity = int.Parse(split[0]);
            (int, int) routers = (int.Parse(split[1]), int.Parse(split[2]));
            connections[i] = (capacity, routers);
        }
        return connections;
    }

    private void AssertThatConnectionsAreEqual(Network network, (int, (int, int))[] connections)
    {
        Assert.That(network.Connections.Count, Is.EqualTo(connections.Length));
        for (int i = 0; i < connections.Length; ++i)
        {
            Assert.That(network.Connections[i].IsEqualTo(connections[i]), Is.True);
        }
    }
}
