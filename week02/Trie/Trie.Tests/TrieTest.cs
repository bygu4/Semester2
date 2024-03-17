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
            ["������", "������", "������", "������", "������", "������", "�����"]);
    }

    [Test]
    public void TestForAdd_TryToAddElementThatIsInTrie_ReturnFalseAndSizeHasNotChanged()
    {
        Assert.That(testTrie.Add("������"), Is.False);
        Assert.That(testTrie.Size, Is.EqualTo(7));
    }

    [Test]
    public void TestForAdd_AddElementThatIsNotInTrie_ReturnTrueAndSizeHasChanged()
    {
        Assert.That(testTrie.Add("�����"), Is.True);
        Assert.That(testTrie.Size, Is.EqualTo(8));
    }

    [Test]
    public void TestForContains_SearchForSomeElements_FindElementsThatWereAdded()
    {
        TestContainment(testTrie,
            ["���", "�������", "������", "", "wqewqsd", "�����", "a"],
            [false, false, true, false, false, true, false]);
    }

    [Test]
    public void TestForRemove_TryToRemoveSomeElements_ElementsThatWereInTrieAreRemoved()
    {
        RemoveKeysAndCheckOutput(testTrie,
            ["������", "������", "���", "�����", "������", "������"],
            [true, false, false, true, true, true]);
        TestContainment(testTrie,
            ["������", "������", "������", "���"],
            [false, false, true, false]);
        Assert.That(testTrie.Size, Is.EqualTo(3));
    }

    [Test]
    public void TestForHowManyStartsWithPrefix_UseMethod_GetValues()
    {
        GetHowManyStartsWithPrefixAndCheckOutputs(testTrie,
            ["�����", "������", "���", "", "��", "��", "wqwqwq"],
            [2, 1, 3, 7, 1, 6, 0]);
    }
}
