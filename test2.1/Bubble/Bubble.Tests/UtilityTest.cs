// Copyright (c) 2024
//
// Use of this source code is governed by an MIT license
// that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

namespace Utility.Tests;

public static class Tests
{
    private static List<object> arraysToSort =
    [
        new List<int> { 90, -100, 5, 6, 7, -1, 0, 0, 2 },
        new string[] { "abc", "ccc", "3321", "bca", "cab", "aaa" },
        new List<string> { "2321321", "", "aa", "1", "2" },
        new (int, int)[] { (1, 4), (6, 2), (7, 8), (0, 0), (-10, -1) },
        new List<float> { 5.5f, 5.5f, 5.5f },
        new List<uint> { 77, 0, 32, 54, 1, 2, 3 },
        Array.Empty<double>(),
    ];

    private static List<object> comparers =
    [
        new Comparer<int>((x, y) => x.CompareTo(y)),
        new Comparer<string>((x, y) => x.CompareTo(y)),
        new Comparer<string>((x, y) => x.Length.CompareTo(y.Length)),
        new Comparer<(int, int)>((x, y) => x.Item2.CompareTo(y.Item2)),
        new Comparer<float>((x, y) => x.CompareTo(y)),
        new Comparer<uint>((x, y) => (x % 7).CompareTo(y % 7)),
        new Comparer<double>((x, y) => x.CompareTo(y)),
    ];

    private static List<object> results =
    [
        new List<int> { -100, -1, 0, 0, 2, 5, 6, 7, 90 },
        new string[] { "3321", "aaa", "abc", "bca", "cab", "ccc" },
        new List<string> { "", "1", "2", "aa", "2321321" },
        new (int, int)[] { (-10, -1), (0, 0), (6, 2), (1, 4), (7, 8) },
        new List<float> { 5.5f, 5.5f, 5.5f },
        new List<uint> { 77, 0, 1, 2, 3, 32, 54 },
        Array.Empty<double>(),
    ];

    private static List<TestCaseData> testCases = GetTestCases(arraysToSort, comparers, results);

    [TestCaseSource(nameof(testCases))]
    public static void SortTest_SortArrayWithSetComparer_MatchResults<T>(
        IList<T> arrayToSort, IComparer<T> comparer, IList<T> result)
    {
        Utility.Sort(arrayToSort, comparer);
        Assert.That(arrayToSort, Is.EqualTo(result));
    }

    private static List<TestCaseData> GetTestCases(params List<object>[] arguments)
    {
        var result = new List<TestCaseData>();
        var testCase = new List<object>();
        for (int i = 0; i < arguments[0].Count; ++i)
        {
            foreach (var argument in arguments)
            {
                testCase.Add(argument[i]);
            }

            result.Add(new TestCaseData(testCase.ToArray()));
            testCase.Clear();
        }

        return result;
    }

    private class Comparer<T> : IComparer<T>
    {
        private Func<T, T, int> compareFunction;

        public Comparer(Func<T, T, int> compareFunction)
            => this.compareFunction = compareFunction;

        public int Compare(T? x, T? y)
        {
            if (x == null || y == null)
            {
                throw new NullReferenceException();
            }

            return this.compareFunction(x, y);
        }
    }
}