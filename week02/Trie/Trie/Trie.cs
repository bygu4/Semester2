// Copyright 2024 Alexander Bugaev
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

namespace Trie;

/// <summary>
/// Data structure for containing strings.
/// </summary>
public class Trie
{
    private Vertex root;

    /// <summary>
    /// Initializes a new instance of the <see cref="Trie"/> class.
    /// </summary>
    public Trie()
    {
        this.root = new Vertex();
    }

    /// <summary>
    /// Gets number of containing strings.
    /// </summary>
    public int Size { get; private set; }

    /// <summary>
    /// Try to add specified string to the Trie.
    /// </summary>
    /// <param name="element">String to add.</param>
    /// <returns>Returns true is the string was added, otherwise false.</returns>
    public bool Add(string element)
    {
        return this.AddRecursion(this.root, element, 0);
    }

    /// <summary>
    /// Try to remove specified string from Trie.
    /// </summary>
    /// <param name="element">String to remove.</param>
    /// <returns>Returns true if the string was removed, otherwise false.</returns>
    public bool Remove(string element)
    {
        return this.RemoveRecursion(this.root, element, 0).Item1;
    }

    /// <summary>
    /// Check if the string is in the Trie.
    /// </summary>
    /// <param name="element">String to check in the Trie.</param>
    /// <returns>Returns true if the string was in Trie, otherwise false.</returns>
    public bool Contains(string element)
    {
        Vertex? foundVertex = this.GetVertex(element);
        return foundVertex is not null && foundVertex.IsTerminal;
    }

    /// <summary>
    /// Get how many strings in the Trie start with specified prefix.
    /// </summary>
    /// <param name="prefix">Prefix to search in strings.</param>
    /// <returns>Number of strings in the Trie that start with specified prefix.</returns>
    public int HowManyStartsWithPrefix(string prefix)
    {
        Vertex? foundVertex = this.GetVertex(prefix);
        if (foundVertex is not null)
        {
            return (foundVertex.IsTerminal ? 1 : 0) + foundVertex.NumberOfDescendants;
        }

        return 0;
    }

    private bool AddRecursion(Vertex current, string element, int index)
    {
        if (index == element.Length)
        {
            if (!current.IsTerminal)
            {
                current.IsTerminal = true;
                ++this.Size;
                return true;
            }

            return false;
        }

        if (!current.Edges.ContainsKey(element[index]))
        {
            current.Edges.Add(element[index], new Vertex());
        }

        bool elementAdded = this.AddRecursion(current.Edges[element[index]], element, index + 1);
        if (elementAdded)
        {
            ++current.NumberOfDescendants;
        }

        return elementAdded;
    }

    private (bool, bool) RemoveRecursion(Vertex current, string element, int index)
    {
        bool elementFound = false;
        bool toRemove = false;
        if (index == element.Length)
        {
            if (current.IsTerminal)
            {
                elementFound = true;
                current.IsTerminal = false;
                --this.Size;
            }

            toRemove = !current.IsTerminal && current.Edges.Count == 0;
            return (elementFound, toRemove);
        }

        try
        {
            (elementFound, toRemove) = this.RemoveRecursion(current.Edges[element[index]], element, index + 1);
            if (elementFound)
            {
                --current.NumberOfDescendants;
            }

            if (toRemove)
            {
                current.Edges.Remove(element[index]);
                toRemove = !current.IsTerminal && current.Edges.Count == 0;
            }

            return (elementFound, toRemove);
        }
        catch (KeyNotFoundException)
        {
            return (false, false);
        }
    }

    private Vertex? GetVertex(string element)
    {
        Vertex? current = this.root;
        foreach (char character in element)
        {
            current.Edges.TryGetValue(character, out current);
            if (current is null)
            {
                break;
            }
        }

        return current;
    }

    private class Vertex
    {
        public Vertex()
        {
            this.Edges = new Dictionary<char, Vertex>();
        }

        public bool IsTerminal { get; set; }

        public int NumberOfDescendants { get; set; }

        public Dictionary<char, Vertex> Edges { get; set; }
    }
}
