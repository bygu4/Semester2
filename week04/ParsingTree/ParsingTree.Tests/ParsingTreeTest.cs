// Copyright (c) Alexander Bugaev 2024
//
// Use of this source code is governed by an MIT license
// that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

namespace Parsing.Tests;

using Parsing;

public class ParsingTreeTest
{
    private string testFilesDirectory = Path.Join("../../..", "TestFiles");

    private string GetFilePath(string fileName)
    {
        return Path.Join(testFilesDirectory, fileName);
    }

    [Test]
    public void TestForParsingTree_EmptyFile_ThrowException()
    {
        string filePath = GetFilePath("EmptyFile.txt");
        Assert.Throws<InvalidDataException>(() => { new ParsingTree(filePath); });
    }

    [TestCase("SingleNumber.txt", 999334)]
    [TestCase("SingleNegativeNumber.txt", -432)]
    public void TestForParsingTree_SingleNumbers_GetTheSameNumbers(string fileName, float number)
    {
        string filePath = GetFilePath(fileName);
        ParsingTree tree = new ParsingTree(filePath);
        Assert.That(tree.GetExpression().Equals(number.ToString()), Is.True);
        Assert.That(AreEqual(tree.Evaluate(), number), Is.True);
    }


    [TestCase("SimpleExpression.txt", "(15 / -2)", -7.5f)]
    [TestCase("NestedLeftOperand.txt", "((15 - 9) / 12)", 0.5f)]
    [TestCase("NestedRightOperand.txt", "(-12 * (2 + 3))", -60)]
    public void TestForParsingTree_SimpleExpressions_GetCorrectValueAndExpression(
        string fileName, string expression, float value)
    {
        string filePath = GetFilePath(fileName);
        ParsingTree tree = new ParsingTree(filePath);
        Assert.That(tree.GetExpression().Equals(expression), Is.True);
        Assert.That(AreEqual(tree.Evaluate(), value), Is.True);
    }

    [TestCase("NestedBothOperands.txt", "((-33 / -11) * (77 + 23))", 300)]
    [TestCase("ComplicatedExpression.txt", "(((32 / 4) - (-3 * 3)) * (-1200 + 22))", -20026)]
    public void TestForParsingTree_ComplicatedExpressions_GetCorrectValueAndExpression(
        string fileName, string expression, int value)
    {
        string filePath = GetFilePath(fileName);
        ParsingTree tree = new ParsingTree(filePath);
        Assert.That(tree.GetExpression().Equals(expression), Is.True);
        Assert.That(AreEqual(tree.Evaluate(), value), Is.True);
    }

    [Test]
    public void TestForParsingTree_DivisionByZero_ThrowException()
    {
        string filePath = GetFilePath("DivisionByZero.txt");
        ParsingTree tree = new ParsingTree(filePath);
        Assert.Throws<DivideByZeroException>(() => { tree.Evaluate(); });
    }

    [TestCase("UnexpectedCharacter.txt")]
    [TestCase("SingleOperator.txt")]
    [TestCase("TwoOperators.txt")]
    [TestCase("MissingOperand.txt")]
    [TestCase("TwoNumbersWithoutOperator.txt")]
    public void TestForParsingTree_InvalidData_ThrowException(string fileName)
    {
        string filePath = GetFilePath(fileName);
        Assert.Throws<InvalidDataException>(() => { new ParsingTree(filePath); });
    }

    private bool AreEqual(float number1, float number2)
    {
        return Math.Abs(number1 - number2) < float.Epsilon;
    }
}
