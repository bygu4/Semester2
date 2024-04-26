// <copyright file="PriorityQueueTest.cs" company="SPBU">
// Copyright (c) Alexander Bugaev 2024. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace PriorityQueue.Tests;

using PriorityQueue;

public class PriorityQueueTest
{
    private static IEnumerable<TestCaseData> PriorityQueueTest_CorrectCases
    {
        get
        {
            yield return new TestCaseData(
                new double[0],
                new int[0],
                new double[0]).Returns(true);

            yield return new TestCaseData(
                new string[] { "aaa", "bbb", "ccc" },
                new int[] { 3, 2, 1 },
                new string[0]).Returns(false);

            yield return new TestCaseData(
                new float[] { 0, 22, 31, -1.5f, -22.99f, 1002, 45.551f },
                new int[] { 80, -1, 90, 10, 1, 2, 3 },
                new float[] { 31, 0, -1.5f, 45.551f, 1002, -22.99f, 22 }).Returns(true);

            yield return new TestCaseData(
                new (int, int)[] { (16, 12), (-199, 0), (88, 88), (0, 1) },
                new int[] { -3, -3, 22, 22},
                new (int, int)[] { (88, 88), (0, 1), (16, 12), (-199, 0) }).Returns(true);
            

            yield return new TestCaseData(
                new string[] { "1221", "ewqewq", "321321", "qqqqqqqqqq" },
                new int[] { 4, 2, 1, 6 },
                new string[] { "qqqqqqqqqq", "1221" }).Returns(false);
        }
    }

    private static IEnumerable<TestCaseData> PriorityQueueTest_IncorrectCases
    {
        get
        {
            yield return new TestCaseData(
                new float[0], new int[0]);

            yield return new TestCaseData(
                new string[] { "aas", "fdssa", "221" },
                new int[] { 909, -100, 21 });
        }
    }

    [TestCaseSource(nameof(PriorityQueueTest_CorrectCases))]
    public bool PriorityQueueTest_CorrectCases_GetValuesAndCheckIfEmpty<T>(
        T?[] valuesToAdd, int[] priorities, T?[] expectedValues)
    {
        var testQueue = new PriorityQueue<T>();
        this.AddElementsToQueue(testQueue, valuesToAdd, priorities);
        this.AssertThatDequeuesMatchExpectedValues(testQueue, expectedValues);
        return testQueue.IsEmpty;
    }

    [TestCaseSource(nameof(PriorityQueueTest_IncorrectCases))]
    public void PriorityQueueTest_DequeueEmpty_ThrowException<T>(
        T?[] valuesToAdd, int[] priorities)
    {
        var testQueue = new PriorityQueue<T>();
        this.AddElementsToQueue(testQueue, valuesToAdd, priorities);
        this.ClearQueue(testQueue);
        Assert.Throws<InvalidOperationException>(() => testQueue.Dequeue());
    }

    private void AddElementsToQueue<T>(PriorityQueue<T> queue, T?[] values, int[] priorities)
    {
        for (int i = 0; i < values.Length; ++i)
        {
            queue.Enqueue(values[i], priorities[i]);
        }
    }

    private void AssertThatDequeuesMatchExpectedValues<T>(PriorityQueue<T> queue, T?[] values)
    {
        for (int i = 0; i < values.Length; ++i)
        {
            Assert.That(queue.Dequeue(), Is.EqualTo(values[i]));
        }
    }

    private void ClearQueue<T>(PriorityQueue<T> queue)
    {
        while (!queue.IsEmpty)
        {
            queue.Dequeue();
        }
    }
}
