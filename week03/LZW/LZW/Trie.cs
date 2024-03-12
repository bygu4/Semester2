namespace Trie;

public class Trie<Type>
{
    private Vertex root;
    public int Size { get; private set; }

    private class Vertex
    {
        public Type? value;
        public bool isTerminal;
        public Dictionary<char, Vertex> edges;

        public Vertex()
        {
            this.edges = new Dictionary<char, Vertex>();
        }
    }

    public Trie()
    {
        this.root = new Vertex();
    }

    private bool AddRecursion(Vertex current, string element, int index, Type? value)
    {
        if (index == element.Length)
        {
            if (!current.isTerminal)
            {
                current.isTerminal = true;
                current.value = value;
                ++this.Size;
                return true;
            }
            return false;
        }
        if (!current.edges.ContainsKey(element[index]))
        {
            current.edges.Add(element[index], new Vertex());
        }
        return AddRecursion(current.edges[element[index]], element, index + 1, value);
    }

    public bool Add(string element, Type? value)
    {
        return AddRecursion(this.root, element, 0, value);
    }

    public bool Add(char character, Type? value)
    {
        return Add(character.ToString(), value);
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

    public Type? Value(string element)
    {
        Vertex? foundVertex = GetVertex(element);
        if (foundVertex is null)
        {
            throw new KeyNotFoundException();
        }
        return foundVertex.value;
    }
}
