// Copyright (c) 2024
//
// Use of this source code is governed by an MIT license
// that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

namespace stackCalculator.Tests;

using Stack;

public class StackTest
{
    [SetUp]
    public void Setup()
    {
    }

    public static IEnumerable<TestCaseData> StackTypeTestCases
    {
        get
        {
            yield return new TestCaseData(new ListStack<float>());
            yield return new TestCaseData(new ArrayStack<float>());
        }
    }

    [TestCaseSource(nameof(StackTypeTestCases))]
    public void TestForStack_PushOneElement_GetTheSameElement(IStack<float> stack)
    {
        stack.Push(5.56f);
        Assert.That(stack.Pop(), Is.EqualTo(5.56f));
    }

    [TestCaseSource(nameof(StackTypeTestCases))]
    public void TestForStack_PushTwoElements_GetElementsInReverseOrder(IStack<float> stack)
    {
        stack.Push(0);
        stack.Push(22);
        Assert.That(stack.Pop(), Is.EqualTo(22));
        Assert.That(stack.Pop(), Is.EqualTo(0));
    }

    [TestCaseSource(nameof(StackTypeTestCases))]
    public void TestForStack_PushPopAndPushAgain_GetElements(IStack<float> stack)
    {
        stack.Push(432);
        stack.Pop();
        stack.Push(-100);
        Assert.That(stack.Pop(), Is.EqualTo(-100));
    }

    [TestCaseSource(nameof(StackTypeTestCases))]
    public void TestForStack_PopOutOfAnEmptyStack_ThrowException(IStack<float> stack)
    {
        Assert.Throws<InvalidOperationException>(() => { stack.Pop(); });
    }

    [TestCaseSource(nameof(StackTypeTestCases))]
    public void TestForStack_MorePushes_GetElements(IStack<float> stack)
    {
        int numberOfPushes = 100;
        for (int i = 0; i < numberOfPushes; ++i)
        {
            stack.Push(i);
        }
        for (int i = 0; i < numberOfPushes; ++i)
        {
            Assert.That(stack.Pop(), Is.EqualTo((float)(numberOfPushes - i - 1)));
        }
    }
}
