using Trie;

namespace Test
{
    class TestForTrie
    {
        static private void CaseForAdd(string[] elementsToAdd, bool[] expectedOutput,
            int expectedSize, int numberOfTest)
        {
            Trie.Trie testTrie = new Trie.Trie();
            for (int i = 0; i < elementsToAdd.Length; ++i)
            {
                if (testTrie.Add(elementsToAdd[i]) != expectedOutput[i])
                {
                    throw new Exception($"Test {numberOfTest} has failed");
                }
            }
            if (testTrie.Size != expectedSize)
            {
                throw new Exception($"Test {numberOfTest} has failed");
            }
        }

        static private void CaseForContains(string[] elementsToAdd, string[] elementsToFind,
            bool[] expectedOutput, int numberOfTest)
        {
            Trie.Trie testTrie = new Trie.Trie();
            foreach (string element in elementsToAdd)
            {
                testTrie.Add(element);
            }
            for (int i = 0; i < elementsToFind.Length; ++i)
            {
                if (testTrie.Contains(elementsToFind[i]) != expectedOutput[i])
                {
                    throw new Exception($"Test {numberOfTest} has failed");
                }
            }
        }

        static private void CaseForRemove(string[] elementsToAdd, string[] elementsToRemove,
            bool[] expectedOutput, int expectedSize, int numberOfTest)
        {
            Trie.Trie testTrie = new Trie.Trie();
            foreach (string element in elementsToAdd)
            {
                testTrie.Add(element);
            }
            for (int i = 0; i < elementsToRemove.Length; ++i)
            {
                if (testTrie.Remove(elementsToRemove[i]) != expectedOutput[i] ||
                    testTrie.Contains(elementsToRemove[i]))
                {
                    throw new Exception($"Test {numberOfTest} has failed");
                }
            }
            if (testTrie.Size != expectedSize)
            {
                throw new Exception($"Test {numberOfTest} has failed");
            }
        }

        static private void CaseForHowManyStartsWithPrefix(string[] elementsToAdd,
            string[] prefixesToFind, int[] expectedOutput, int numberOfTest)
        {
            Trie.Trie testTrie = new Trie.Trie();
            foreach (string element in elementsToAdd)
            {
                testTrie.Add(element);
            }
            for (int i = 0; i < prefixesToFind.Length; ++i)
            {
                if (testTrie.HowManyStartsWithPrefix(prefixesToFind[i]) != expectedOutput[i])
                {
                    throw new Exception($"Test {numberOfTest} has failed");
                }
            }
        }

        static public void Test()
        {
            TestForTrie.CaseForAdd(["aba", "abc", "a", "abac", "", "aba", ""], 
                [true, true, true, true, true, false, false], 5, 1);
            TestForTrie.CaseForAdd([], [], 0, 2);

            TestForTrie.CaseForContains(
                ["матмех", "матрас", "мастер", "массив", "молоко", "макака", "матме"],
                ["мат", "массивы", "матмех", "", "eweqwewq", "матме"], 
                [false, false, true, false, false, true], 3);

            TestForTrie.CaseForRemove(
                ["матмех", "матрас", "мастер", "массив", "молоко", "макака", "матме"],
                ["макака", "макака", "мат", "матме", "матмех", "матрас"],
                [true, false, false, true, true, true], 3, 4);
            TestForTrie.CaseForRemove([], ["fsafa", "321321", "weqeqe", "wwwwwwww"],
                new bool[4], 0, 5);

            TestForTrie.CaseForHowManyStartsWithPrefix(
                ["матмех", "матрас", "мастер", "массив", "молоко", "макака", "матме"],
                ["матме", "матмех", "мат", "", "мо", "ма", "wqwqwq"],
                [2, 1, 3, 7, 1, 6, 0], 6);
        }
    }
}
