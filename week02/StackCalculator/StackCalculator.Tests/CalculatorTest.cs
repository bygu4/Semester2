// <copyright file="CalculatorTest.cs" company="SPBU">
// Copyright (c) Alexander Bugaev 2024. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace stackCalculator.Tests;

using Stack;
using StackCalculator;

public class CalculatorTest
{
    public static IEnumerable<TestCaseData> StackTypeTestCases
    {
        get
        {
            yield return new TestCaseData(new ListStack<float>());
            yield return new TestCaseData(new ArrayStack<float>());
        }
    }

    [SetUp]
    public void Setup()
    {
    }

    [TestCaseSource(nameof(StackTypeTestCases))]
    public void TestForCompute_EmptyString_GetZero(IStack<float> stack)
    {
        Assert.That(Calculator.Compute(string.Empty, stack), Is.EqualTo(0));
    }

    [TestCaseSource(nameof(StackTypeTestCases))]
    public void TestForCompute_OneNumber_GetTheSameNumber(IStack<float> stack)
    {
        Assert.That(Calculator.Compute("3421", stack), Is.EqualTo(3421));
    }

    [TestCaseSource(nameof(StackTypeTestCases))]
    public void TestForCompute_OneOperationWithIntegerResult_GetResult(IStack<float> stack)
    {
        Assert.That(Calculator.Compute("30 40 *", stack), Is.EqualTo(1200));
    }

    [TestCaseSource(nameof(StackTypeTestCases))]
    public void TestForCompute_TwoOperationsWithIntegerResult_GetResult(IStack<float> stack)
    {
        Assert.That(Calculator.Compute("24 5 3 + /", stack), Is.EqualTo(3));
    }

    [TestCaseSource(nameof(StackTypeTestCases))]
    public void TestForCompute_DivisionByZero_ThrowException(IStack<float> stack)
    {
        Assert.Throws<DivideByZeroException>(() => { Calculator.Compute("532 0 /", stack); });
    }

    [TestCaseSource(nameof(StackTypeTestCases))]
    public void TestForCompute_UnexpectedSymbol_ThrowException(IStack<float> stack)
    {
        Assert.Throws<InvalidDataException>(() => { Calculator.Compute("54 a -", stack); });
    }

    [TestCaseSource(nameof(StackTypeTestCases))]
    public void TestForCompute_OperationWithoutNumbers_ThrowException(IStack<float> stack)
    {
        Assert.Throws<InvalidDataException>(() => { Calculator.Compute("*", stack); });
    }

    [TestCaseSource(nameof(StackTypeTestCases))]
    public void TestForCompute_OperationWithOneNumber_ThrowException(IStack<float> stack)
    {
        Assert.Throws<InvalidDataException>(() => { Calculator.Compute("77 +", stack); });
    }

    [TestCaseSource(nameof(StackTypeTestCases))]
    public void TestForCompute_OperationWithThreeNumbers_ThrowException(IStack<float> stack)
    {
        Assert.Throws<InvalidDataException>(() => { Calculator.Compute("87 90 -100 *", stack); });
    }

    [TestCaseSource(nameof(StackTypeTestCases))]
    public void TestForCompute_FloatNumberInput_ThrowException(IStack<float> stack)
    {
        Assert.Throws<InvalidDataException>(() => { Calculator.Compute("11.5 12 -", stack); });
    }

    [TestCaseSource(nameof(StackTypeTestCases))]
    public void TestForCompute_OneNegativeNumber_GetResult(IStack<float> stack)
    {
        Assert.That(Calculator.Compute("-220 20 /", stack), Is.EqualTo(-11));
    }

    [TestCaseSource(nameof(StackTypeTestCases))]
    public void TestForCompute_TwoNegativeNumbers_GetResult(IStack<float> stack)
    {
        Assert.That(Calculator.Compute("-4 -6 *", stack), Is.EqualTo(24));
    }

    [TestCaseSource(nameof(StackTypeTestCases))]
    public void TestForCompute_OneOperationWithFloatResult_GetResult(IStack<float> stack)
    {
        Assert.That(Calculator.Compute("1 8 /", stack), Is.EqualTo(0.125f));
    }

    [TestCaseSource(nameof(StackTypeTestCases))]
    public void TestForCompute_OneOperationWithNegativeFloatResult_GetResult(IStack<float> stack)
    {
        Assert.That(Calculator.Compute("2 -5 /", stack), Is.EqualTo(-0.4f));
    }

    [TestCaseSource(nameof(StackTypeTestCases))]
    public void TestForCompute_TwoOperationsWithFloatResult_GetResult(IStack<float> stack)
    {
        Assert.That(Calculator.Compute("3 8 / 15 *", stack), Is.EqualTo(5.625f));
    }

    [TestCaseSource(nameof(StackTypeTestCases))]
    public void TestForCompute_ZeroOnTheLeft_GetResult(IStack<float> stack)
    {
        Assert.That(Calculator.Compute("0 992 -", stack), Is.EqualTo(-992));
    }

    [TestCaseSource(nameof(StackTypeTestCases))]
    public void TestForCompute_ZeroOnTheRight_GetResult(IStack<float> stack)
    {
        Assert.That(Calculator.Compute("654 0 +", stack), Is.EqualTo(654));
    }

    [TestCaseSource(nameof(StackTypeTestCases))]
    public void TestForCompute_BothZeroes_GetZero(IStack<float> stack)
    {
        Assert.That(Calculator.Compute("0 0 +", stack), Is.EqualTo(0));
    }

    [TestCaseSource(nameof(StackTypeTestCases))]
    public void TestForCompare_MultiplyByZero_GetZero(IStack<float> stack)
    {
        Assert.That(Calculator.Compute("-333 45 + 0 *", stack), Is.EqualTo(0));
    }

    [TestCaseSource(nameof(StackTypeTestCases))]
    public void TestForCompare_MaxInteger_GetResult(IStack<float> stack)
    {
        Assert.That(Calculator.Compute($"2 {int.MaxValue} *", stack), Is.EqualTo(2 * (float)int.MaxValue));
    }

    [TestCaseSource(nameof(StackTypeTestCases))]
    public void TestForCompare_MinInteger_GetResult(IStack<float> stack)
    {
        Assert.That(Calculator.Compute($"{int.MinValue} 999 -", stack), Is.EqualTo((float)int.MinValue - 999));
    }
}
