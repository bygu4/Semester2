// <copyright file="List.cs" company="SPBU">
// Copyright (c) Alexander Bugaev 2024. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace List;

using Exceptions;

/// <summary>
/// Data structure that supports adding elements to the end,
/// removing elements by value, getting and setting values by index.
/// </summary>
public class List
{
    /// <summary>
    /// Initializes a new instance of the <see cref="List"/> class.
    /// </summary>
    public List()
    {
    }

    /// <summary>
    /// Gets the number of elements in the List.
    /// </summary>
    public int Size { get; private set; }

    /// <summary>
    /// Gets or sets the first element in the List.
    /// </summary>
    protected Vertex? Tail { get; set; }

    /// <summary>
    /// Gets or sets the last element in the List.
    /// </summary>
    protected Vertex? Head { get; set; }

    /// <summary>
    /// Add an element to the end of the List.
    /// </summary>
    /// <param name="value">Value of the element to add.</param>
    public virtual void Add(int value)
    {
        Vertex newVertex = new Vertex(value);
        if (this.Head is not null)
        {
            this.Head.Next = newVertex;
        }

        if (this.Tail is null)
        {
            this.Tail = newVertex;
        }

        this.Head = newVertex;
        ++this.Size;
    }

    /// <summary>
    /// Remove the element with given value from the List.
    /// </summary>
    /// <param name="value">Value of element to remove.</param>
    public void Remove(int value)
    {
        var (previous, current) = this.GetVertexByValue(value);
        if (current == this.Head)
        {
            this.Head = previous;
        }

        if (previous is null)
        {
            if (this.Tail is null)
            {
                throw new NullReferenceException();
            }

            this.Tail = this.Tail.Next;
        }
        else
        {
            previous.Next = current.Next;
        }

        --this.Size;
    }

    /// <summary>
    /// Get value of the element on given position in the List.
    /// </summary>
    /// <param name="index">Index of the element in the List.</param>
    /// <returns>Value of the element on given position.</returns>
    public int GetValue(int index)
    {
        var vertex = this.GetVertexByIndex(index);
        return vertex.Value;
    }

    /// <summary>
    /// Set value of the element on given position in the List to the specified value.
    /// </summary>
    /// <param name="value">Value to set for the element.</param>
    /// <param name="index">Index of the element in the List.</param>
    public virtual void SetValue(int value, int index)
    {
        var vertex = this.GetVertexByIndex(index);
        vertex.Value = value;
    }

    private (Vertex?, Vertex) GetVertexByValue(int value)
    {
        Vertex? previous = null;
        Vertex? current = this.Tail;
        for (; current != null && current.Value != value; current = current.Next)
        {
            previous = current;
        }

        if (current is null)
        {
            throw new ElementNotFoundException();
        }

        return (previous, current);
    }

    private Vertex GetVertexByIndex(int index)
    {
        if (index < 0 || index >= this.Size)
        {
            throw new IndexOutOfRangeException();
        }

        Vertex? current = this.Tail;
        for (int i = 0; current is not null && i < index; ++i)
        {
            current = current.Next;
        }

        if (current is null)
        {
            throw new NullReferenceException();
        }

        return current;
    }

    /// <summary>
    /// Class implementing a vertex of the List.
    /// </summary>
    protected class Vertex
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Vertex"/> class.
        /// </summary>
        /// <param name="value">Value to set for the Vertex.</param>
        public Vertex(int value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Gets or sets the current value of the Vertex.
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// Gets or sets the next Vertex for the current one.
        /// </summary>
        public Vertex? Next { get; set; }
    }
}
