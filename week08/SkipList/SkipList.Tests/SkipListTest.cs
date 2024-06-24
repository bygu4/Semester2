// <copyright file="SkipListTest.cs" company="SPBU">
// Copyright (c) Alexander Bugaev 2024. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SkipList.Tests;

public static class SkipListTest
{
    private static List<object> elementsToAdd =
    [
        new string[] { "eewq", "", "32123", "aaa", "aaa", "qqqwwwqqq" },
        new float[] { 22.2f, 0, 0.5f, -100, 43.99f, 0, 8 },
        new (int, int)[] { (1, 2), (55, 2), (-12, 0), (66, 3), (78, 33) },
    ];

    private static List<object> elementsToRemove =
    [
        new string[] { "TTTTTT", "", "1", "aaa", "eewq", "" },
        new float[] { 8, 0, -100, 9090909, -1.11f, 8 },
        new (int, int)[] { (55, 2), (0, 0), (55, -55), (1, 2) },
    ];

    private static List<object> removeOutput =
    [
        new bool[] { false, true, false, true, true, false },
        new bool[] { true, true, true, false, false, false },
        new bool[] { true, false, false, true },
    ];

    private static List<object> indicesToRemoveFrom =
    [
        new int[] { 0, 1, 2 },
        new int[] { 4, 0, 0 },
        new int[] { 1, 1 },
    ];

    private static List<object> collectionsAfterRemoving =
    [
        new string[] { "32123", "aaa", "qqqwwwqqq" },
        new float[] { 0, 0.5f, 22.2f, 43.99f },
        new (int, int)[] { (-12, 0), (66, 3), (78, 33) },
    ];

    private static List<object> elementsToFind =
    [
        new string[] { "", "", "4325225", "wqw", "aaa" },
        new float[] { 0, -100, -100, 999, 7, 5 },
        new (int, int)[] { (1, 2), (1001, 1001), (1, 2), (-12, 0) },
    ];

    private static List<object> elementsWereFound =
    [
        new bool[] { true, true, false, false, true },
        new bool[] { true, true, true, false, false, false },
        new bool[] { true, false, true, true },
    ];

    private static List<object> foundElementsIndices =
    [
        new int[] { 0, 0, -1, -1, 2 },
        new int[] { 1, 0, 0, -1, -1, -1 },
        new int[] { 1, -1, 1, 0 },
    ];

    private static List<object> destinationLengths = [78, 44, 5];

    private static List<object> startIndices_Correct = [45, 10, 0];

    private static List<object> startIndices_Negative = [-1, -2, -3];

    private static List<object> startIndices_Overflow = [78, 42, 1];

    private static List<object> elements = ["aaa", 8, (1, 2)];

    private static List<object> indicesInRange = [2, 3, 1];

    private static List<object> indicesOutOfRange = [-1, 100, 5];

    private static List<TestCaseData> testCases_IndexOutOfRange =
        SkipListTest.GetTestCaseData(elementsToAdd, indicesOutOfRange);
    
    private static List<TestCaseData> testCases_NotSupported =
        SkipListTest.GetTestCaseData(elementsToAdd, indicesInRange, elements);
    
    private static List<TestCaseData> testCases_Remove =
        SkipListTest.GetTestCaseData(elementsToAdd, elementsToRemove, removeOutput, collectionsAfterRemoving);

    private static List<TestCaseData> testCases_RemoveAt =
        SkipListTest.GetTestCaseData(elementsToAdd, indicesToRemoveFrom, collectionsAfterRemoving);

    private static List<TestCaseData> testCases_Contains =
        SkipListTest.GetTestCaseData(elementsToAdd, elementsToFind, elementsWereFound);
    
    private static List<TestCaseData> testCases_IndexOf =
        SkipListTest.GetTestCaseData(elementsToAdd, elementsToFind, foundElementsIndices);

    private static List<TestCaseData> testCases_CopyTo_Correct =
        SkipListTest.GetTestCaseData(elementsToAdd, destinationLengths, startIndices_Correct);

    private static List<TestCaseData> testCases_CopyTo_NegativeIndex =
        SkipListTest.GetTestCaseData(elementsToAdd, destinationLengths, startIndices_Negative);

    private static List<TestCaseData> testCases_CopyTo_Overflow =
        SkipListTest.GetTestCaseData(elementsToAdd, destinationLengths, startIndices_Overflow);

    private static List<TestCaseData> testCases_ChangeCollectionDuringIteration =
        SkipListTest.GetTestCaseData(elementsToAdd, elements);

    [TestCaseSource(nameof(elements))]
    public static void ConstructorTest_WithoutInitialization_CollectionIsEmpty<T>(
        T _) where T : IComparable<T>
    {
        var testCollection = new SkipList<T>();
        SkipListTest.AssertThatCollectionsAreEqual(testCollection, Array.Empty<T>());
    }

    [TestCaseSource(nameof(elementsToAdd))]
    public static void ConstructorTest_WithInitialization_CheckElementsAndCount<T>(
        T[] elementsToAdd) where T : IComparable<T>
    {
        var testCollection = new SkipList<T>(elementsToAdd);
        Array.Sort(elementsToAdd);
        SkipListTest.AssertThatCollectionsAreEqual(testCollection, elementsToAdd);
    }

    [TestCaseSource(nameof(elementsToAdd))]
    public static void IsReadOnlyTest_GetProperty_ReturnsFalse<T>(
        T[] elementsToAdd) where T : IComparable<T>
    {
        var testCollection = new SkipList<T>(elementsToAdd);
        Assert.That(testCollection.IsReadOnly, Is.False);
    }

    [TestCaseSource(nameof(testCases_IndexOutOfRange))]
    public static void GetItemTest_IndexOutOfRange_ThrowException<T>(
        T[] elementsToAdd, int index) where T : IComparable<T>
    {
        var testCollection = new SkipList<T>(elementsToAdd);
        Assert.Throws<ArgumentOutOfRangeException>(() => { T _ = testCollection[index]; });
    }

    [TestCaseSource(nameof(testCases_NotSupported))]
    public static void SetItemTest_NotSupported_ThrowException<T>(
        T[] elementsToAdd, int index, T element) where T : IComparable<T>
    {
        var testCollection = new SkipList<T>(elementsToAdd);
        Assert.Throws<NotSupportedException>(() => { testCollection[index] = element; });
    }

    [TestCaseSource(nameof(elementsToAdd))]
    public static void AddTest_AddSomeElements_CheckElementsAndCount<T>(
        T[] elementsToAdd) where T : IComparable<T>
    {
        var testCollection = new SkipList<T>();
        SkipListTest.AddElements(testCollection, elementsToAdd);
        Array.Sort(elementsToAdd);
        SkipListTest.AssertThatCollectionsAreEqual(testCollection, elementsToAdd);
    }

    [TestCaseSource(nameof(testCases_NotSupported))]
    public static void InsertTest_NotSupported_ThrowException<T>(
        T[] elementsToAdd, int index, T element) where T : IComparable<T>
    {
        var testCollection = new SkipList<T>();
        Assert.Throws<NotSupportedException>(() => testCollection.Insert(index, element));
    }

    [TestCaseSource(nameof(testCases_Remove))]
    public static void RemoveTest_AddThenRemoveSomeElements_MatchOutputAndCollections<T>(
        T[] elementsToAdd,
        T[] elementsToRemove,
        bool[] expectedOutput,
        T[] expectedCollection) where T : IComparable<T>
    {
        var testCollection = new SkipList<T>(elementsToAdd);
        var output = SkipListTest.RemoveElements(testCollection, elementsToRemove);
        Assert.That(output, Is.EqualTo(expectedOutput));
        SkipListTest.AssertThatCollectionsAreEqual(testCollection, expectedCollection);
    }

    [TestCaseSource(nameof(testCases_RemoveAt))]
    public static void RemoveAtTest_AddSomeElementsThenRemoveAtCorrectIndices_CheckElementsAndCount<T>(
        T[] elementsToAdd,
        int[] indicesToRemoveFrom,
        T[] expectedCollection) where T : IComparable<T>
    {
        var testCollection = new SkipList<T>(elementsToAdd);
        SkipListTest.RemoveAtIndices(testCollection, indicesToRemoveFrom);
        SkipListTest.AssertThatCollectionsAreEqual(testCollection, expectedCollection);
    }

    [TestCaseSource(nameof(testCases_IndexOutOfRange))]
    public static void RemoveAtTest_IndexOutOfRange_ThrowException<T>(
        T[] elementsToAdd, int index) where T : IComparable<T>
    {
        var testCollection = new SkipList<T>(elementsToAdd);
        Assert.Throws<ArgumentOutOfRangeException>(() => testCollection.RemoveAt(index));
    }

    [TestCaseSource(nameof(elementsToAdd))]
    public static void ClearTest_AddSomeElementsThenClear_CollectionIsEmpty<T>(
        T[] elementsToAdd) where T : IComparable<T>
    {
        var testCollection = new SkipList<T>(elementsToAdd);
        testCollection.Clear();
        SkipListTest.AssertThatCollectionsAreEqual(testCollection, Array.Empty<T>());
    }

    [TestCaseSource(nameof(testCases_Contains))]
    public static void ContainsTest_AddSomeElementsThenSearch_MatchOutput<T>(
        T[] elementsToAdd, T[] elementsToFind, bool[] expectedOutput) where T : IComparable<T>
    {
        var testCollection = new SkipList<T>(elementsToAdd);
        var output = SkipListTest.GetSearchResults(testCollection, elementsToFind);
        Assert.That(output, Is.EqualTo(expectedOutput));
    }

    [TestCaseSource(nameof(testCases_IndexOf))]
    public static void IndexOfTest_AddSomeElementsThenGetIndices_MatchOutput<T>(
        T[] elementsToAdd, T[] elementsToFind, int[] expectedOutput) where T : IComparable<T>
    {
        var testCollection = new SkipList<T>(elementsToAdd);
        var output = SkipListTest.GetIndices(testCollection, elementsToFind);
        Assert.That(output, Is.EqualTo(expectedOutput));
    }

    [TestCaseSource(nameof(testCases_CopyTo_Correct))]
    public static void CopyToTest_CorrectArguments_MatchDestinationWithCollection<T>(
        T[] elementsToAdd, int destinationLength, int startIndex) where T : IComparable<T>
    {
        var testCollection = new SkipList<T>(elementsToAdd);
        var destination = new T[destinationLength];
        testCollection.CopyTo(destination, startIndex);
        destination = destination[startIndex..(startIndex + testCollection.Count)];
        Assert.That(destination, Is.EqualTo(testCollection));
    }

    [TestCaseSource(nameof(testCases_CopyTo_NegativeIndex))]
    public static void CopyToTest_NegativeIndex_ThrowException<T>(
        T[] elementsToAdd, int destinationLength, int startIndex) where T : IComparable<T>
    {
        var testCollection = new SkipList<T>(elementsToAdd);
        var destination = new T[destinationLength];
        Assert.Throws<ArgumentOutOfRangeException>(() => testCollection.CopyTo(destination, startIndex));
    }

    [TestCaseSource(nameof(testCases_CopyTo_Overflow))]
    public static void CopyToTest_DestinationOverflow_ThrowException<T>(
        T[] elementsToAdd, int destinationLength, int startIndex) where T : IComparable<T>
    {
        var testCollection = new SkipList<T>(elementsToAdd);
        var destination = new T[destinationLength];
        Assert.Throws<ArgumentException>(() => testCollection.CopyTo(destination, startIndex));
    }

    [TestCaseSource(nameof(elementsToAdd))]
    public static void EnumeratorTest_Iterate_GetElements<T>(
        T[] elementsToAdd) where T : IComparable<T>
    {
        var testCollection = new SkipList<T>(elementsToAdd);
        var list = SkipListTest.IterateAndAddElementsToList(testCollection);
        Assert.That(list, Is.EqualTo(testCollection));
    }

    [TestCaseSource(nameof(testCases_ChangeCollectionDuringIteration))]
    public static void EnumeratorTest_ChangeCollectionDuringIteration_ThrowException<T>(
        T[] elementsToAdd, T element) where T : IComparable<T>
    {
        var testCollection = new SkipList<T>(elementsToAdd);
        Assert.Throws<InvalidOperationException>(
            () => SkipListTest.AddElementDuringIteration(testCollection, element));
        Assert.Throws<InvalidOperationException>(
            () => SkipListTest.RemoveElementDuringIteration(testCollection, element));
    }

    private static List<TestCaseData> GetTestCaseData(params List<object>[] arguments)
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

    private static void AssertThatCollectionsAreEqual<T>(IList<T> collection1, IList<T> collection2)
    {
        Assert.That(collection1.Count, Is.EqualTo(collection2.Count));
        Assert.That(collection1, Is.EqualTo(collection2));
    }

    private static void ApplyAction<TElement, TArgument>(
        IList<TElement> collection,
        Action<IList<TElement>, TArgument> action,
        TArgument[] arguments)
    {
        foreach (var argument in arguments)
        {
            action(collection, argument);
        }
    }

    private static TResult[] ApplyFunction<TElement, TResult>(
        IList<TElement> collection,
        Func<IList<TElement>, TElement, TResult> function,
        TElement[] elements)
    {
        var result = new TResult[elements.Length];
        for (int i = 0; i < elements.Length; ++i)
        {
            result[i] = function(collection, elements[i]);
        }

        return result;
    }

    private static void AddElements<T>(IList<T> collection, T[] elements)
        => SkipListTest.ApplyAction(
            collection, (c, item) => c.Add(item), elements);

    private static bool[] RemoveElements<T>(IList<T> collection, T[] elements)
        => SkipListTest.ApplyFunction(
            collection, (c, item) => c.Remove(item), elements);

    private static void RemoveAtIndices<T>(IList<T> collection, int[] indices)
        => SkipListTest.ApplyAction(
            collection, (c, index) => c.RemoveAt(index), indices);
    private static bool[] GetSearchResults<T>(IList<T> collection, T[] elements)
        => SkipListTest.ApplyFunction(
            collection, (c, item) => c.Contains(item), elements);

    private static int[] GetIndices<T>(IList<T> collection, T[] elements)
        => SkipListTest.ApplyFunction(
            collection, (c, item) => c.IndexOf(item), elements);
    
    private static List<T> IterateAndAddElementsToList<T>(IList<T> collection)
    {
        var list = new List<T>();
        foreach (var element in collection)
        {
            list.Add(element);
        }

        return list;
    }

    private static void AddElementDuringIteration<T>(IList<T> collection, T elementToAdd)
    {
        foreach (var _ in collection)
        {
            collection.Add(elementToAdd);
        }
    }

    private static void RemoveElementDuringIteration<T>(IList<T> collection, T elementToRemove)
    {
        foreach (var _ in collection)
        {
            collection.Remove(elementToRemove);
        }
    }
}
