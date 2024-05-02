// <copyright file="CalculatorTest.cs" company="SPBU">
// Copyright (c) Alexander Bugaev 2024. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Calculator.Tests;

using Operations;
using System.Globalization;

public class CalculatorTest
{
    private static List<object> testCultures =
    [
        new CultureInfo("ru-RU"),
        new CultureInfo("en-US"),
    ];

    private static List<object> testCases_AddDigit =
    [
        ("", "", "0"),
        ("43299", "", "43299"),
        ("000001", "", "1"),
        ("0057800", "", "57800"),
        ("900100", "", "900100"),
    ];

    private static List<object> testCases_Decimal =
    [
        (",", "", "0,"),
        ("52,", "", "52,"),
        ("123,4", "", "123,4"),
        ("0,0001", "", "0,0001"),
        ("12,,,,", "", "12,"),
        ("85,904,12", "", "85,90412"),
    ];

    private static List<object> testCases_ToNegative =
    [
        ("±", "", "-0"),
        ("±±", "", "0"),
        ("65±", "", "-65"),
        ("±90,44", "", "-90,44"),
        ("0,000±±", "", "0,000"),
        ("1±23,±8", "", "123,8"),
    ];

    private static List<object> testCases_Back =
    [
        ("\b\b\b", "", "0"),
        ("12\b\b", "", "0"),
        ("80808\b\b", "", "808"),
        ("5,\b", "", "5"),
        ("412,\b\b\b\b", "", "0"),
        ("98,0001\b\b", "", "98,00"),
        ("±\b", "", "0"),
        ("±90,55\b\b", "", "-90,"),
        ("66±\b\b", "", "-0"),
    ];

    private static List<object> testCases_SetOperation =
    [
        ("+", "0 +", "0"),
        ("992 ×", "992 ×", "992"),
        ("86 + - ÷", "86 ÷", "86"),
        ("2 - 4", "2 -", "4"),
        ("0 - 66 +", "-66 +", "-66"),
        ("66 × - 20 +", "46 +", "46"),
        ("32 ÷ 8 + 15", "4 +", "15"),
        ("0,5 - ±6,6 × 12 +", "85,2 +", "85,2"),
        ("±99,5 ÷ 100 + 32 × 6", "31,005 ×", "6"),
    ];

    private static List<object> testCases_Calculate =
    [
        ("654 =", "654 =", "654"),
        ("90\b =", "9 =", "9"),
        ("90 - =", "90 - 0 =", "90"),
        ("32,2 × + =", "32,2 + 0 =", "32,2"),
        ("±99 × =", "-99 × 0 =", "0"),
        ("90 - 32 =", "90 - 32 =", "58"),
        ("1 + 1 = = = =", "4 + 1 =", "5"),
        ("32 ÷ 10 = ×", "3,2 ×", "3,2"),
        ("12 + 4 = - 20", "16 -", "20"),
        ("9,8 - + 1,5 ÷ ±4 =", "11,3 ÷ -4 =", "-2,825"),
        ("4 + 5 = 45", "", "45"),
        ("903 + 76 × 8 = ,", "", "0,"),
    ];

    private static List<object> testCases_ClearOperand =
    [
        ("\r", "", "0"),
        ("909,6 \r", "", "0"),
        ("32 - 90 \r", "32 -", "0"),
        ("0,001 × 72 \r 31", "0,001 ×", "31"),
        ("81 ÷ 100 \r 3 =", "81 ÷ 3 =", "27"),
        ("5,1 ÷ 7 = \r", "", "0"),
    ];

    private static List<object> testCases_Clear =
    [
        ("\n", "", "0"),
        ("90,44 \n", "", "0"),
        ("5 + \n", "", "0"),
        ("90,4 + 0,21 - \n", "", "0"),
        ("55,1 ÷ 6 × 5 \n", "", "0"),
        ("1001 - 45 = \n", "", "0"),
    ];

    private static List<object> testCases_UnaryOperations =
    [
        ("Q", "", "sqr(0)"),
        ("25Q =", "sqr(25) =", "625"),
        ("36@ =", "sqrt(36) =", "6"),
        ("5R =", "1/5 =", "0,2"),
        ("55% =", "55% =", "0,55"),
        ("10 + 7Q =", "10 + sqr(7) =", "59"),
        ("25@ + 36@ =", "sqrt(25) + sqrt(36) =", "11"),
        ("90 - 80 = R", "", "1/10"),
        ("55% + 21% =", "55% + 21% =", "0,76"),
        ("98%\b", "", "98"),
        ("1023,1Q\b\b", "", "sqr(1023,1)"),
        ("64@± =", "-sqrt(64) =", "-8"),
        ("±20R =", "1/-20 =", "-0,05"),
        ("±1R± =", "-1/-1 =", "1"),
        ("3Q± + 4Q =", "-sqr(3) + sqr(4) =", "7"),
    ];

    private static List<object> testCases_UndefinedResults =
    [
        ("0 ÷ 0 =", "0 ÷ 0 =", "Undefined"),
        ("32 ÷ 0 +", "Undefined +", "Undefined"),
        ("R =", "1/0 =", "Undefined"),
        ("±991@ =", "sqrt(-991) =", "Undefined"),
        ("45 - 50 = @ =", "sqrt(-5) =", "Undefined"),
    ];

    private static List<object> testCases_NotADigits = ['+', 'a', '\b'];

    private static List<object> testCases_UnknownOperations = ['*', '5', 'q'];

    private static List<TestCaseData> TestCaseSource_AddDigit =
        CalculatorTest.GetTestCaseData(testCases_AddDigit);

    private static List<TestCaseData> TestCaseSource_Decimal =
        CalculatorTest.GetTestCaseData(testCases_Decimal);

    private static List<TestCaseData> TestCaseSource_ToNegative =
        CalculatorTest.GetTestCaseData(testCases_ToNegative);

    private static List<TestCaseData> TestCaseSource_Back =
        CalculatorTest.GetTestCaseData(testCases_Back);

    private static List<TestCaseData> TestCaseSource_SetOperation =
        CalculatorTest.GetTestCaseData(testCases_SetOperation);

    private static List<TestCaseData> TestCaseSource_Calculate =
        CalculatorTest.GetTestCaseData(testCases_Calculate);

    private static List<TestCaseData> TestCaseSource_ClearOperand =
        CalculatorTest.GetTestCaseData(testCases_ClearOperand);

    private static List<TestCaseData> TestCaseSource_Clear =
        CalculatorTest.GetTestCaseData(testCases_Clear);

    private static List<TestCaseData> TestCaseSource_UnaryOperations =
        CalculatorTest.GetTestCaseData(testCases_UnaryOperations);

    private static List<TestCaseData> TestCaseSource_UndefinedResults =
        CalculatorTest.GetTestCaseData(testCases_UndefinedResults);

    private static List<TestCaseData> TestCaseSource_NotADigits =
        CalculatorTest.GetTestCaseData(testCases_NotADigits);

    private static List<TestCaseData> TestCaseSource_UnknownOperations =
        CalculatorTest.GetTestCaseData(testCases_UnknownOperations);

    [TestCaseSource(nameof(testCultures))]
    public void TestForDecimalSeparator(CultureInfo testCulture)
    {
        var testCalculator = CalculatorTest.TestSetUp(testCulture);
        Assert.That(
            testCalculator.DecimalSeparator,
            Is.EqualTo(testCulture.NumberFormat.NumberDecimalSeparator));
    }

    [TestCaseSource(nameof(TestCaseSource_AddDigit))]
    public void TestForAddDigit(CultureInfo testCulture, (string, string, string) testCase)
        => CalculatorTest.TestBase(testCulture, testCase);

    [TestCaseSource(nameof(TestCaseSource_Decimal))]
    public void TestForDecimal(CultureInfo testCulture, (string, string, string) testCase)
        => CalculatorTest.TestBase(testCulture, testCase);

    [TestCaseSource(nameof(TestCaseSource_ToNegative))]
    public void TestForToNegative(CultureInfo testCulture, (string, string, string) testCase)
        => CalculatorTest.TestBase(testCulture, testCase);

    [TestCaseSource(nameof(TestCaseSource_Back))]
    public void TestForBack(CultureInfo testCulture, (string, string, string) testCase)
        => CalculatorTest.TestBase(testCulture, testCase);

    [TestCaseSource(nameof(TestCaseSource_SetOperation))]
    public void TestForSetOperation(CultureInfo testCulture, (string, string, string) testCase)
        => CalculatorTest.TestBase(testCulture, testCase);

    [TestCaseSource(nameof(TestCaseSource_Calculate))]
    public void TestForCalculate(CultureInfo testCulture, (string, string, string) testCase)
        => CalculatorTest.TestBase(testCulture, testCase);

    [TestCaseSource(nameof(TestCaseSource_ClearOperand))]
    public void TestForClearOperand(CultureInfo testCulture, (string, string, string) testCase)
        => CalculatorTest.TestBase(testCulture, testCase);

    [TestCaseSource(nameof(TestCaseSource_Clear))]
    public void TestForClear(CultureInfo testCulture, (string, string, string) testCase)
        => CalculatorTest.TestBase(testCulture, testCase);

    [TestCaseSource(nameof(TestCaseSource_UnaryOperations))]
    public void TestForUnaryOperations(CultureInfo testCulture, (string, string, string) testCase)
        => CalculatorTest.TestBase(testCulture, testCase);

    [TestCaseSource(nameof(TestCaseSource_UndefinedResults))]
    public void TestForUndefinedResults(CultureInfo testCulture, (string, string, string) testCase)
        => CalculatorTest.TestBase(testCulture, testCase);

    [TestCaseSource(nameof(TestCaseSource_NotADigits))]
    public void TestForAddDigit_NotADigit_ThrowException(CultureInfo testCulture, char value)
    {
        var testCalculator = CalculatorTest.TestSetUp(testCulture);
        Assert.Throws<ArgumentException>(() => testCalculator.Operand_AddDigit(value));
    }

    [TestCaseSource(nameof(TestCaseSource_UnknownOperations))]
    public void TestForSetOperation_UnknownOperation_ThrowException(CultureInfo testCulture, char value)
    {
        var testCalculator = CalculatorTest.TestSetUp(testCulture);
        Assert.Throws<ArgumentException>(
            () => testCalculator.SetBinaryOperation((Operations.Binary)value));
    }

    private static List<TestCaseData> GetTestCaseData(List<object> testCases)
    {
        var result = new List<TestCaseData>();
        foreach (var culture in CalculatorTest.testCultures)
        {
            foreach (var testCase in testCases)
            {
                result.Add(new TestCaseData(culture, testCase));
            }
        }

        return result;
    }

    private static Calculator TestSetUp(CultureInfo testCulture)
    {
        CultureInfo.CurrentCulture = testCulture;
        return new Calculator();
    }

    private static void Execute(Calculator testCalculator, string expression)
    {
        foreach (char command in expression)
        {
            CalculatorCommands.Execute(testCalculator, command);
        }
    }

    private static void AssertThatPropertiesAreCorrectAfterExecution(
        Calculator testCalculator, string inputExpression, string expectedExpression, string result)
    {
        CalculatorTest.Execute(testCalculator, inputExpression);
        Assert.That(testCalculator.Expression, Is.EqualTo(expectedExpression));
        Assert.That(testCalculator.Result, Is.EqualTo(result));
    }

    private static void TestBase(CultureInfo testCulture, (string, string, string) testCase)
    {
        var testCalculator = CalculatorTest.TestSetUp(testCulture);
        var (inputExpression, expectedExpression, result) = testCase;

        expectedExpression = expectedExpression.Replace(",", testCulture.NumberFormat.NumberDecimalSeparator);
        result = result.Replace(",", testCulture.NumberFormat.NumberDecimalSeparator);

        CalculatorTest.AssertThatPropertiesAreCorrectAfterExecution(
            testCalculator, inputExpression, expectedExpression, result);
    }
}
