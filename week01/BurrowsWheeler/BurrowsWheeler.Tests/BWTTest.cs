// Copyright (c) Alexander Bugaev 2024
//
// Use of this source code is governed by an MIT license
// that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

namespace BurrowsWheeler.Tests;

using BurrowsWheeler;

public static class BWTTest
{
    [Test]
    public static void TestForTransformation_EmptyString_GetEmptyStringAndZero()
    {
        Assert.That(BWT.Transform(string.Empty), Is.EqualTo((string.Empty, 0)));
    }

    [Test]
    public static void TestForTransformation_OneCharacter_GetTheCharacterAndZero()
    {
        Assert.That(BWT.Transform("G"), Is.EqualTo(("G", 0)));
    }

    [Test]
    public static void TestForTransformation_StringOfEqualCharacters_GetTheSameStringAndZero()
    {
        Assert.That(BWT.Transform("jjjjjjjjjj"), Is.EqualTo(("jjjjjjjjjj", 0)));
    }

    [TestCase("abcabcabcabc", "ccccaaaabbbb", 0)]
    [TestCase("BANANA", "NNBAAA", 3)]
    [TestCase("That's an awfully hot coffee pot", "sntyett h  effowT ulachp 'oaofal", 6)]
    public static void TestForTransformation_CommonCase_GetResult(
        string inputString, string expectedString, int expectedPosition)
    {
        Assert.That(BWT.Transform(inputString), Is.EqualTo((expectedString, expectedPosition)));
    }

    [TestCase(-1)]
    [TestCase(0)]
    [TestCase(23)]
    public static void TestForReverseTransform_EmptyString_GetEmptyString(int position)
    {
        Assert.That(BWT.ReverseTransform(string.Empty, position), Is.EqualTo(string.Empty));
    }

    [Test]
    public static void TestForReverseTransform_OneCharacterAndZero_GetTheCharacter()
    {
        Assert.That(BWT.ReverseTransform("K", 0), Is.EqualTo("K"));
    }

    [TestCase(2)]
    [TestCase(5)]
    public static void TestForReverseTransform_StringOfEqualCharacters_GetTheSameString(int position)
    {
        Assert.That(BWT.ReverseTransform("11111111111", position), Is.EqualTo("11111111111"));
    }

    [TestCase(-1)]
    [TestCase(7)]
    [TestCase(90)]
    public static void TestForReverseTransform_IndexOutOfRange_ThrowException(int position)
    {
        Assert.Throws<IndexOutOfRangeException>(() => { BWT.ReverseTransform("abcdefg", position); });
    }

    [TestCase("ccccaaaabbbb", 0, "abcabcabcabc")]
    [TestCase("NNBAAA", 3, "BANANA")]
    [TestCase("sntyett h  effowT ulachp 'oaofal", 6, "That's an awfully hot coffee pot")]
    public static void TestForReverseTransform_CommonCase_GetResult(
        string inputString, int position, string expectedString)
    {
        Assert.That(BWT.ReverseTransform(inputString, position), Is.EqualTo(expectedString));
    }
}
