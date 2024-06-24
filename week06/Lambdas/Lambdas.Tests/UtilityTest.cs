// <copyright file="UtilityTest.cs" company="SPBU">
// Copyright (c) Alexander Bugaev 2024. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Lambdas.Tests;

using Utility;

public class UtilityTest
{
    private static IEnumerable<TestCaseData> MapTestCases
    {
        get
        {
            yield return new TestCaseData(
                new int[] { -5, 0, 2, 3, 1, -10, 7 },
                (int x) => (int)Math.Pow(x, 2),
                new int[] { 25, 0, 4, 9, 1, 100, 49 });
            yield return new TestCaseData(
                new List<float> { 65, -0.5f, 2.3333f, -1002.1f, -543, 23.001f },
                (float x) => -2 * x,
                new List<float> { -130, 1, -4.6666f, 2004.2f, 1086, -46.002f });
            yield return new TestCaseData(
                new List<string> { "banana", "", "abcde", "123456", "ololol"},
                (string x) => Reverse(x),
                new List<string> { "ananab", "", "edcba", "654321", "lololo" });
            yield return new TestCaseData(
                new int[] { 4332, 131, -99, 0, 1, 2, 44, 23, -554, -12 },
                (int x) => 0,
                new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0});
        }
    }

    private static IEnumerable<TestCaseData> FilterTestCases
    {
        get
        {
            yield return new TestCaseData(
                new float[] { 55.01f, 0, 85, 42.432f, -100, -99.99f, 33.5f, 33, 1, 2, 3 },
                (float x) => x > 25,
                new List<float> { 55.01f, 85, 42.432f, 33.5f, 33 });
            yield return new TestCaseData(
                new List<string> { "abrakadabra", "", "1011010", "iewfjoewj", "lalal", "e" },
                (string x) => x.Length > 5,
                new List<string> { "abrakadabra", "1011010", "iewfjoewj" });
            yield return new TestCaseData(
                new string[] { "", "10202", "11001", "10", "21001", "10111", "10000" },
                (string x) => x.StartsWith("10"),
                new List<string> { "10202", "10", "10111", "10000" });
            yield return new TestCaseData(
                new List<int> { 0, -22, 4331, 1, 99, -23, 5, 9, 100, -3232 },
                (int x) => x % 2 == 0,
                new List<int> { 0, -22, 100, -3232 });
        }
    }

    private static IEnumerable<TestCaseData> FoldTestCases
    {
        get
        {
            yield return new TestCaseData(
                new int[] { -99, 100, 45, 5, -10, 0, 33 },
                (int acc, int elem) => 2 * acc + elem,
                -2383);
            yield return new TestCaseData(
                new List<string> { "ab", "", "raca", "da", "b", "ra" },
                (string acc, string elem) => acc + elem,
                "abracadabra");
            yield return new TestCaseData(
                new (int, int)[] { (0, 0), (54, 32), (0, 12), (5, 6), (7, 8) },
                ((int, int) acc, (int, int) elem) => (acc.Item1 + elem.Item1, acc.Item2 - elem.Item2),
                (66, -58));
            yield return new TestCaseData(
                new List<float> { 0, 5.5f, -45.1f, 3, 4, -2.5f },
                (float acc, float elem) => acc + (int)Math.Pow(elem, 2),
                2095);
        }
    }

    [TestCaseSource(nameof(MapTestCases))]
    public void MapTest<T>(
        IList<T?> collection, Func<T?, T?> method, IList<T?> expectedCollection)
    {
        Utility.Map(collection, method);
        AssertThatCollectionsAreEqual(collection, expectedCollection);
    }

    [TestCaseSource(nameof(FilterTestCases))]
    public void FilterTest<T>(
        IList<T?> collection, Func<T?, bool> method, List<T> expectedOutput)
    {
        var output = Utility.Filter(collection, method);
        Assert.That(output, Is.EqualTo(expectedOutput));
    }

    [TestCaseSource(nameof(FoldTestCases))]
    public void FoldTest<T>(
        IList<T?> collection, Func<T?, T?, T?> method, T? expectedOutput)
    {
        var output = Utility.Fold(collection, method);
        Assert.That(output, Is.EqualTo(expectedOutput));
    }

    private static void AssertThatCollectionsAreEqual<T>(IList<T?> collection1, IList<T?> collection2)
    {
        Assert.That(collection1.Count, Is.EqualTo(collection2.Count));
        for (int i = 0; i < collection1.Count; ++i)
        {
            Assert.That(collection1[i], Is.EqualTo(collection2[i]));
        }
    }

    private static string Reverse(string element)
    {
        char[] characters = element.ToCharArray();
        Array.Reverse(characters);
        return new string(characters);
    }
}
