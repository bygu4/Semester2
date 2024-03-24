// Copyright 2024 Alexander Bugaev
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

namespace stackCalculator.Tests;

using StackCalculator;

public class CalculatorTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestForCompute_EmptyString_GetZero()
    {
        Assert.That(Calculator.Compute(string.Empty), Is.EqualTo(0));
    }

    [Test]
    public void TestForCompute_OneNumber_GetTheSameNumber()
    {
        Assert.That(Calculator.Compute("3421"), Is.EqualTo(3421));
    }

    [Test]
    public void TestForCompute_OneOperationWithIntegerResult_GetResult()
    {
        Assert.That(Calculator.Compute("30 40 *"), Is.EqualTo(1200));
    }

    [Test]
    public void TestForCompute_TwoOperationsWithIntegerResult_GetResult()
    {
        Assert.That(Calculator.Compute("24 5 3 + /"), Is.EqualTo(3));
    }

    [Test]
    public void TestForCompute_DivisionByZero_ThrowException()
    {
        Assert.Throws<DivideByZeroException>(() => { Calculator.Compute("532 0 /"); });
    }

    [Test]
    public void TestForCompute_UnexpectedSymbol_ThrowException()
    {
        Assert.Throws<InvalidDataException>(() => { Calculator.Compute("54 a -"); });
    }

    [Test]
    public void TestForCompute_OperationWithoutNumbers_ThrowException()
    {
        Assert.Throws<InvalidDataException>(() => { Calculator.Compute("*"); });
    }

    [Test]
    public void TestForCompute_OperationWithOneNumber_ThrowException()
    {
        Assert.Throws<InvalidDataException>(() => { Calculator.Compute("77 +"); });
    }

    [Test]
    public void TestForCompute_FloatNumberInput_ThrowException()
    {
        Assert.Throws<InvalidDataException>(() => { Calculator.Compute("11.5 12 -"); });
    }

    [Test]
    public void TestForCompute_OneNegativeNumber_GetResult()
    {
        Assert.That(Calculator.Compute("-220 20 /"), Is.EqualTo(-11));
    }

    [Test]
    public void TestForCompute_TwoNegativeNumbers_GetResult()
    {
        Assert.That(Calculator.Compute("-4 -6 *"), Is.EqualTo(24));
    }

    [Test]
    public void TestForCompute_OneOperationWithFloatResult_GetResult()
    {
        Assert.That(Calculator.Compute("1 8 /"), Is.EqualTo(0.125f));
    }

    [Test]
    public void TestForCompute_OneOperationWithNegativeFloatResult_GetResult()
    {
        Assert.That(Calculator.Compute("2 -5 /"), Is.EqualTo(-0.4f));
    }

    [Test]
    public void TestForCompute_TwoOperationsWithFloatResult_GetResult()
    {
        Assert.That(Calculator.Compute("3 8 / 15 *"), Is.EqualTo(5.625f));
    }

    [Test]
    public void TestForCompute_ZeroOnTheLeft_GetResult()
    {
        Assert.That(Calculator.Compute("0 992 -"), Is.EqualTo(-992));
    }

    [Test]
    public void TestForCompute_ZeroOnTheRight_GetResult()
    {
        Assert.That(Calculator.Compute("654 0 +"), Is.EqualTo(654));
    }

    [Test]
    public void TestForCompute_BothZeroes_GetZero()
    {
        Assert.That(Calculator.Compute("0 0 +"), Is.EqualTo(0));
    }

    [Test]
    public void TestForCompare_MultiplyByZero_GetZero()
    {
        Assert.That(Calculator.Compute("-333 45 + 0 *"), Is.EqualTo(0));
    }

    [Test]
    public void TestForCompare_MaxInteger_GetResult()
    {
        Assert.That(Calculator.Compute($"2 {int.MaxValue} *"), Is.EqualTo(2 * (float)int.MaxValue));
    }

    [Test]
    public void TestForCompare_MinInteger_GetResult()
    {
        Assert.That(Calculator.Compute($"{int.MinValue} 999 -"), Is.EqualTo((float)int.MinValue - 999));
    }
}
