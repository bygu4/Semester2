// Copyright (c) 2024
//
// Use of this source code is governed by an MIT license
// that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

namespace Routers.Tests;

using Routers;

public static class NetworkTest
{
    private static string testFilesDirectory = Path.Join("../../..", "TestFiles");

    private static IEnumerable<TestCaseData> CorrectCases
    {
        get
        {
            yield return new TestCaseData("EmptyFile.txt", "").Returns(true);
            yield return new TestCaseData("SingleRouter.txt", "").Returns(true);
            yield return new TestCaseData("TwoConnectedRouters.txt", "0, 1, 2").Returns(true);
            yield return new TestCaseData("TwoDisconnectedRouters.txt", "").Returns(false);
            yield return new TestCaseData(
                "ThreeRoutersConnectedWithoutCycle.txt", "12, 3, 1; 5, 1, 2").Returns(true);
            yield return new TestCaseData(
                "ThreeRoutersConnectedWithCycle.txt", "10, 1, 2; 6, 3, 2").Returns(true);
            yield return new TestCaseData("TwoRoutersConnectedTwice.txt", "55, 1, 2").Returns(true);
            yield return new TestCaseData("NetworkWithLoop.txt", "13, 1, 2").Returns(true);
            yield return new TestCaseData(
                "DisconnectedNetworkOfFiveRouters.txt", "100, 1, 5; 34, 2, 4; 12, 1, 3").Returns(false);
            yield return new TestCaseData(
                "FiveRoutersConnectedWithCycles.txt",
                "100, 2, 5; 70, 3, 5; 42, 1, 3; 15, 4, 5").Returns(true);
        }
    }

    [TestCaseSource(nameof(CorrectCases))]
    public static bool TestForNetwork_CorrectFile_CheckConnectionsAndGetResult(
        string fileName, string connections)
    {
        Network testNetwork = new Network(GetTestFilePath(fileName));
        bool isConnected = testNetwork.Configure();
        AssertThatConnectionsAreEqual(testNetwork, ParseTestCaseArgument(connections));
        return isConnected;
    }

    [TestCase("IncorrectFormat.txt")]
    [TestCase("MissingCapacity.txt")]
    public static void TestForNetwork_InvalidFileFormat_ThrowException(string fileName)
    {
        Assert.Throws<InvalidDataException>(() => { new Network(GetTestFilePath(fileName)); });
    }

    [TestCase("NegativeCapacity.txt")]
    [TestCase("IncorrectNumber.txt")]
    public static void TestForNetwork_IncorrectData_ThrowException(string fileName)
    {
        Assert.Throws<ArgumentException>(() => { new Network(GetTestFilePath(fileName)); });
    }

    private static string GetTestFilePath(string fileName)
        => Path.Join(testFilesDirectory, fileName);

    private static (int, (int, int))[] ParseTestCaseArgument(string argument)
    {
        string[] elements = argument.Split("; ", StringSplitOptions.RemoveEmptyEntries);
        (int, (int, int))[] connections = new (int, (int, int))[elements.Length];
        for (int i = 0; i < elements.Length; ++i)
        {
            string[] split = elements[i].Split(", ");
            int capacity = int.Parse(split[0]);
            (int, int) routers = (int.Parse(split[1]), int.Parse(split[2]));
            connections[i] = (capacity, routers);
        }

        return connections;
    }

    private static void AssertThatConnectionsAreEqual(Network network, (int, (int, int))[] connections)
    {
        Assert.That(network.Connections.Count, Is.EqualTo(connections.Length));
        for (int i = 0; i < connections.Length; ++i)
        {
            Assert.That(network.Connections[i].IsEqualTo(connections[i]), Is.True);
        }
    }
}
