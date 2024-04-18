// <copyright file="CalculatorTest.cs" company="SPBU">
// Copyright (c) Alexander Bugaev 2024. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Calculator.Tests;

using Operations;

public class CalculatorTest
{
    private Calculator testCalculator;

    [SetUp]
    public void Setup()
    {
        this.testCalculator = new Calculator();
    }

    [TestCase("", "", "0")]
    [TestCase("43299", "", "43299")]
    [TestCase("000001", "", "1")]
    [TestCase("0057800", "", "57800")]
    [TestCase("900100", "", "900100")]
    public void TestForAddDigit(string inputExpression, string exprectedExpression, string result)
    {
        this.AssertThatPropertiesAreCorrectAfterExecution(inputExpression, exprectedExpression, result);
    }

    [TestCase(",", "", "0,")]
    [TestCase("52,", "", "52,")]
    [TestCase("123,4", "", "123,4")]
    [TestCase("0,0001", "", "0,0001")]
    [TestCase("12,,,,", "", "12,")]
    [TestCase("85,904,12", "", "85,90412")]
    public void TestForDecimal(string inputExpression, string exprectedExpression, string result)
    {
        this.AssertThatPropertiesAreCorrectAfterExecution(inputExpression, exprectedExpression, result);
    }

    [TestCase("±", "", "-0")]
    [TestCase("±±", "", "0")]
    [TestCase("65±", "", "-65")]
    [TestCase("±90,44", "", "-90,44")]
    [TestCase("0,000±±", "", "0,000")]
    [TestCase("1±23,±8", "", "123,8")]
    public void TestForToNegative(string inputExpression, string exprectedExpression, string result)
    {
        this.AssertThatPropertiesAreCorrectAfterExecution(inputExpression, exprectedExpression, result);
    }

    [TestCase("\b\b\b", "", "0")]
    [TestCase("12\b\b", "", "0")]
    [TestCase("80808\b\b", "", "808")]
    [TestCase("5,\b", "", "5")]
    [TestCase("412,\b\b\b\b", "", "0")]
    [TestCase("98,0001\b\b", "", "98,00")]
    [TestCase("±\b", "", "0")]
    [TestCase("±90,55\b\b", "", "-90,")]
    [TestCase("66±\b\b", "", "-0")]
    public void TestForDeleteLastDigit(string inputExpression, string exprectedExpression, string result)
    {
        this.AssertThatPropertiesAreCorrectAfterExecution(inputExpression, exprectedExpression, result);
    }

    [TestCase("+", "0 +", "0")]
    [TestCase("992 ×", "992 ×", "992")]
    [TestCase("86 + - ÷", "86 ÷", "86")]
    [TestCase("2 - 4", "2 -", "4")]
    [TestCase("0 - 66 +", "-66 +", "-66")]
    [TestCase("66 × - 20 +", "46 +", "46")]
    [TestCase("32 ÷ 8 + 15", "4 +", "15")]
    [TestCase("0,5 - ±6,6 × 12 +", "85,2 +", "85,2")]
    [TestCase("±99,5 ÷ 100 + 32 × 6", "31,005 ×", "6")]
    public void TestForSetOperation(string inputExpression, string exprectedExpression, string result)
    {
        this.AssertThatPropertiesAreCorrectAfterExecution(inputExpression, exprectedExpression, result);
    }

    [TestCase("654 =", "654 =", "654")]
    [TestCase("90\b =", "9 =", "9")]
    [TestCase("90 - =", "90 - 0 =", "90")]
    [TestCase("32,2 × + =", "32,2 + 0 =", "32,2")]
    [TestCase("±99 × =", "-99 × 0 =", "0")]
    [TestCase("90 - 32 =", "90 - 32 =", "58")]
    [TestCase("1 + 1 = = = =", "4 + 1 =", "5")]
    [TestCase("32 ÷ 10 = ×", "3,2 ×", "3,2")]
    [TestCase("12 + 4 = - 20", "16 -", "20")]
    [TestCase("9,8 - + 1,5 ÷ ±4 =", "11,3 ÷ -4 =", "-2,825")]
    [TestCase("4 + 5 = 45", "", "45")]
    [TestCase("903 + 76 × 8 = ,", "", "0,")]
    public void TestForCalculate(string inputExpression, string exprectedExpression, string result)
    {
        this.AssertThatPropertiesAreCorrectAfterExecution(inputExpression, exprectedExpression, result);
    }

    [TestCase("\r", "", "0")]
    [TestCase("909,6 \r", "", "0")]
    [TestCase("32 - 90 \r", "32 -", "0")]
    [TestCase("0,001 × 72 \r 31", "0,001 ×", "31")]
    [TestCase("81 ÷ 100 \r 3 =", "81 ÷ 3 =", "27")]
    [TestCase("5,1 ÷ 7 = \r", "", "0")]
    public void TestForOperandClear(string inputExpression, string exprectedExpression, string result)
    {
        this.AssertThatPropertiesAreCorrectAfterExecution(inputExpression, exprectedExpression, result);
    }

    [TestCase("\n", "", "0")]
    [TestCase("90,44 \n", "", "0")]
    [TestCase("5 + \n", "", "0")]
    [TestCase("90,4 + 0,21 - \n", "", "0")]
    [TestCase("55,1 ÷ 6 × 5 \n", "", "0")]
    [TestCase("1001 - 45 = \n", "", "0")]
    public void TestForClear(string inputExpression, string exprectedExpression, string result)
    {
        this.AssertThatPropertiesAreCorrectAfterExecution(inputExpression, exprectedExpression, result);
    }

    [TestCase("Q", "", "sqr(0)")]
    [TestCase("25Q =", "sqr(25) =", "625")]
    [TestCase("36@ =", "sqrt(36) =", "6")]
    [TestCase("5R =", "1/5 =", "0,2")]
    [TestCase("55% =", "55% =", "0,55")]
    [TestCase("10 + 7Q =", "10 + sqr(7) =", "59")]
    [TestCase("25@ + 36@ =", "sqrt(25) + sqrt(36) =", "11")]
    [TestCase("90 - 80 = R", "", "1/10")]
    [TestCase("55% + 21% =", "55% + 21% =", "0,76")]
    [TestCase("98%\b", "", "98")]
    [TestCase("1023,1Q\b\b", "", "sqr(1023,1)")]
    [TestCase("64@± =", "-sqrt(64) =", "-8")]
    [TestCase("±20R =", "1/-20 =", "-0,05")]
    [TestCase("±1R± =", "-1/-1 =", "1")]
    [TestCase("3Q± + 4Q =", "-sqr(3) + sqr(4) =", "7")]
    public void TestForUnaryOperations(string inputExpression, string exprectedExpression, string result)
    {
        this.AssertThatPropertiesAreCorrectAfterExecution(inputExpression, exprectedExpression, result);
    }

    [TestCase("0 ÷ 0 =", "0 ÷ 0 =", "Undefined")]
    [TestCase("32 ÷ 0 +", "Undefined +", "Undefined")]
    [TestCase("R =", "1/0 =", "Undefined")]
    [TestCase("±991@ =", "sqrt(-991) =", "Undefined")]
    [TestCase("45 - 50 = @ =", "sqrt(-5) =", "Undefined")]
    public void TestForUndefinedResults(string inputExpression, string exprectedExpression, string result)
    {
        this.AssertThatPropertiesAreCorrectAfterExecution(inputExpression, exprectedExpression, result);
    }

    [TestCase('+')]
    [TestCase('a')]
    [TestCase('\b')]
    public void TestForAddDigit_NotADigit_ThrowException(char value)
    {
        Assert.Throws<ArgumentException>(() => this.testCalculator.Operand_AddDigit(value));
    }

    [TestCase('*')]
    [TestCase('5')]
    [TestCase('q')]
    public void TestForSetOperation_UnknownOperation_ThrowException(char value)
    {
        Assert.Throws<ArgumentException>(() =>
            this.testCalculator.SetBinaryOperation((Operations.Binary)value));
    }

    private void Execute(string expression)
    {
        foreach (char command in expression)
        {
            CalculatorCommands.Execute(this.testCalculator, command);
        }
    }

    private void AssertThatPropertiesAreCorrectAfterExecution(
        string inputExpression, string exprectedExpression, string result)
    {
        this.Execute(inputExpression);
        Assert.That(this.testCalculator.Expression, Is.EqualTo(exprectedExpression));
        Assert.That(this.testCalculator.Result, Is.EqualTo(result));
    }
}
