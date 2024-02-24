namespace Trie
{
    class Trie
    {
        private Vertex root;
        public int Size { get; private set; }

        private class Vertex
        {
            public bool isTerminal;
            public int numberOfDescendants;
            public Dictionary<char, Vertex> edges;

            public Vertex()
            {
                this.isTerminal = false;
                this.numberOfDescendants = 0;
                this.edges = new Dictionary<char, Vertex>();
            }
        }

        public Trie()
        {
            this.root = new Vertex();
            this.Size = 0;
        }

        private bool AddRecursion(Vertex current, string element, int index)
        {
            if (index == element.Length)
            {
                if (!current.isTerminal)
                {
                    current.isTerminal = true;
                    ++this.Size;
                    return true;
                }
                return false;
            }
            if (!current.edges.ContainsKey(element[index]))
            {
                current.edges.Add(element[index], new Vertex());
            }
            bool elementAdded = AddRecursion(current.edges[element[index]], element, index +1);
            if (elementAdded)
            {
                ++current.numberOfDescendants;
            }
            return elementAdded;
        }

        public bool Add(string element)
        {
            return AddRecursion(this.root, element, 0);
        }

        private (bool, bool) RemoveRecursion(Vertex current, string element, int index)
        {
            bool elementFound = false;
            bool toRemove = false;
            if (index == element.Length)
            {
                if (current.isTerminal)
                {
                    elementFound = true;
                    current.isTerminal = false;
                    --this.Size;
                }
                toRemove = !current.isTerminal && current.edges.Count == 0;
                return (elementFound, toRemove);
            }
            try
            {
                (elementFound, toRemove) = RemoveRecursion(current.edges[element[index]], element, index + 1);
                if (elementFound)
                {
                    --current.numberOfDescendants;
                }
                if (toRemove)
                {
                    current.edges.Remove(element[index]);
                    toRemove = !current.isTerminal && current.edges.Count == 0;
                }
                return (elementFound, toRemove);
            }
            catch (KeyNotFoundException)
            {
                return (false, false);
            }
        }

        public bool Remove(string element)
        {
            return RemoveRecursion(this.root, element, 0).Item1;
        }

        private Vertex? GetVertex(string element)
        {
            Vertex current = this.root;
            foreach (char character in element)
            {
                try
                {
                    current = current.edges[character];
                }
                catch (KeyNotFoundException)
                {
                    return null;
                }
            }
            return current;
        }

        public bool Contains(string element)
        {
            Vertex? foundVertex = GetVertex(element);
            return foundVertex is not null && foundVertex.isTerminal;
        }

        public int HowManyStartsWithPrefix(String prefix)
        {
            Vertex? foundVertex = GetVertex(prefix);
            if (foundVertex is not null)
            {
                return (foundVertex.isTerminal ? 1 : 0) + foundVertex.numberOfDescendants;
            }
            return 0;
        }
    }
}
