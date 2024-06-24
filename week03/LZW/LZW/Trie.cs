// <copyright file="Trie.cs" company="SPBU">
// Copyright (c) Alexander Bugaev 2024. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Trie;

/// <summary>
/// Data structure for containing strings with the set value of specific type.
/// </summary>
/// <typeparam name="Type">The type of string values.</typeparam>
public class Trie<Type>
{
    private Vertex root;

    /// <summary>
    /// Initializes a new instance of the <see cref="Trie{Type}>"/> class.
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
    /// <param name="value">The value to set for the string.</param>
    /// <returns>Returns true is the string was added, otherwise false.</returns>
    public bool Add(string element, Type? value)
    {
        var current = this.root;
        foreach (char character in element)
        {
            if (!current.Edges.ContainsKey(character))
            {
                current.Edges.Add(character, new Vertex());
            }

            current = current.Edges[character];
        }

        if (!current.IsTerminal)
        {
            current.IsTerminal = true;
            current.Value = value;
            ++this.Size;
            return true;
        }

        return false;
    }

    /// <summary>
    /// Try to add specified character to the Trie.
    /// </summary>
    /// <param name="character">Character to add.</param>
    /// <param name="value">The value to set for the character.</param>
    /// <returns>Returns true is the character was added, otherwise false.</returns>
    public bool Add(char character, Type? value)
        => this.Add(character.ToString(), value);

    /// <summary>
    /// Check if the string is the Trie.
    /// </summary>
    /// <param name="element">String to check in the Trie.</param>
    /// <returns>Returns true if the string was in Trie, otherwise false.</returns>
    public bool Contains(string element)
    {
        Vertex? foundVertex = this.GetVertex(element);
        return foundVertex is not null && foundVertex.IsTerminal;
    }

    /// <summary>
    /// Get the set value of the given string.
    /// </summary>
    /// <param name="element">String to find in Trie.</param>
    /// <returns>The value of the string if it was found.</returns>
    /// <exception cref="KeyNotFoundException">Given string was not found in the Trie.</exception>
    public Type? Value(string element)
    {
        var foundVertex = this.GetVertex(element);
        if (foundVertex is null || !foundVertex.IsTerminal)
        {
            throw new KeyNotFoundException();
        }

        return foundVertex.Value;
    }

    private Vertex? GetVertex(string element)
    {
        var current = this.root;
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

        public Type? Value { get; set; }

        public bool IsTerminal { get; set; }

        public Dictionary<char, Vertex> Edges { get; set; }
    }
}
