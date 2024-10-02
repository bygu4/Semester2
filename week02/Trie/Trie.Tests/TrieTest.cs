// Copyright (c) 2024
//
// Use of this source code is governed by an MIT license
// that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

namespace LZW.Tests;

using Trie;

public class TrieTest
{
    public Trie testTrie;

    private void AddElements(Trie trie, string[] keys)
    {
        for (int i = 0; i < keys.Length; ++i)
        {
            trie.Add(keys[i]);
        }
    }

    private void TestContainment(Trie trie, string[] keys, bool[] isInTrie)
    {
        for (int i = 0; i < keys.Length; ++i)
        {
            Assert.That(trie.Contains(keys[i]), Is.EqualTo(isInTrie[i]));
        }
    }

    private void RemoveKeysAndCheckOutput(Trie trie, string[] keys, bool[] output)
    {
        for (int i = 0; i < keys.Length; ++i)
        {
            Assert.That(trie.Remove(keys[i]), Is.EqualTo(output[i]));
        }
    }

    private void GetHowManyStartsWithPrefixAndCheckOutputs(Trie trie, string[] keys, int[] output)
    {
        for (int i = 0; i < keys.Length; ++i)
        {
            Assert.That(trie.HowManyStartsWithPrefix(keys[i]), Is.EqualTo(output[i]));
        }
    }

    [SetUp]
    public void Setup()
    {
        testTrie = new Trie();
        AddElements(testTrie,
            ["матмех", "матрас", "мастер", "массив", "молоко", "мамбет", "матме"]);
    }

    [Test]
    public void TestForAdd_TryToAddElementThatIsInTrie_ReturnFalseAndSizeHasNotChanged()
    {
        Assert.That(testTrie.Add("массив"), Is.False);
        Assert.That(testTrie.Size, Is.EqualTo(7));
    }

    [Test]
    public void TestForAdd_AddElementThatIsNotInTrie_ReturnTrueAndSizeHasChanged()
    {
        Assert.That(testTrie.Add("маска"), Is.True);
        Assert.That(testTrie.Size, Is.EqualTo(8));
    }

    [Test]
    public void TestForContains_SearchForSomeElements_FindElementsThatWereAdded()
    {
        TestContainment(testTrie,
            ["мат", "массивы", "матмех", "", "wqewqsd", "матме", "a"],
            [false, false, true, false, false, true, false]);
    }

    [Test]
    public void TestForRemove_TryToRemoveSomeElements_ElementsThatWereInTrieAreRemoved()
    {
        RemoveKeysAndCheckOutput(testTrie,
            ["мамбет", "мамбет", "мат", "матме", "матмех", "матрас"],
            [true, false, false, true, true, true]);
        TestContainment(testTrie,
            ["мамбет", "матмех", "мастер", "мат"],
            [false, false, true, false]);
        Assert.That(testTrie.Size, Is.EqualTo(3));
    }

    [Test]
    public void TestForHowManyStartsWithPrefix_UseMethod_GetValues()
    {
        GetHowManyStartsWithPrefixAndCheckOutputs(testTrie,
            ["матме", "матмех", "мат", "", "мо", "ма", "wqwqwq"],
            [2, 1, 3, 7, 1, 6, 0]);
    }
}
