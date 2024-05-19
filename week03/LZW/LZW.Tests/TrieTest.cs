// <copyright file="TrieTest.cs" company="SPBU">
// Copyright (c) Alexander Bugaev 2024. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace LZW.Tests;

using Trie;

public static class TrieTest
{
    private static Trie<int> testTrie;

    [SetUp]
    public static void Setup()
    {
        testTrie = new Trie<int>();
        testTrie.Add('a', 432);
        testTrie.Add('1', -999);
        AddElements(testTrie, 
            ["матмех", "матрас", "мастер", "массив", "молоко", "мамбет", "матме"], 
            [999, -1, 0, 3, 2, 89, -100]);
    }

    [Test]
    public static void TestForAdd_TryToAddElementThatIsInTrie_ReturnFalseAndSizeHasNotChanged()
    {
        Assert.That(testTrie.Add("массив", 0), Is.False);
        Assert.That(testTrie.Size, Is.EqualTo(9));
    }

    [Test]
    public static void TestForAdd_AddElementThatIsNotInTrie_ReturnTrueAndSizeHasChanged()
    {
        Assert.That(testTrie.Add("маска", 0), Is.True);
        Assert.That(testTrie.Size, Is.EqualTo(10));
    }

    [Test]
    public static void TestForContains_SearchForSomeElements_FindElementsThatWereAdded()
    {
        TestContainment(testTrie, 
            ["мат", "массивы", "матмех", "", "wqewqsd", "матме", "a"],
            [false, false, true, false, false, true, true]);
    }

    [Test]
    public static void TestForValue_KeysNotInTrie_ThrowException()
    {
        TestValues_KeysNotInTrie(testTrie, 
            ["", "масса", "мат", "мастера", "матрац"]);
    }

    [Test]
    public static void TestForValue_KeysInTrie_GetValues()
    {
        TestValues_KeysInTrie(testTrie, 
            ["мастер", "матме", "молоко", "матмех", "1"], 
            [0, -100, 2, 999, -999]);
    }

    private static void AddElements(Trie<int> trie, string[] keys, int[] values)
    {
        for (int i = 0; i < keys.Length; ++i)
        {
            trie.Add(keys[i], values[i]);
        }
    }

    private static void TestContainment(Trie<int> trie, string[] keys, bool[] isInTrie)
    {
        for (int i = 0; i < keys.Length; ++i)
        {
            Assert.That(trie.Contains(keys[i]), Is.EqualTo(isInTrie[i]));
        }
    }

    private static void TestValues_KeysNotInTrie(Trie<int> trie, string[] keys)
    {
        for (int i = 0; i < keys.Length; ++i)
        {
            Assert.Throws<KeyNotFoundException>(() => { trie.Value(keys[i]); });
        }
    }

    private static void TestValues_KeysInTrie(Trie<int> trie, string[] keys, int[] values)
    {
        for (int i = 0; i < keys.Length; ++i)
        {
            Assert.That(trie.Value(keys[i]), Is.EqualTo(values[i]));
        }
    }
}
