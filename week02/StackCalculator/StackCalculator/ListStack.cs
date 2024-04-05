// <copyright file="ListStack.cs" company="SPBU">
// Copyright (c) Alexander Bugaev 2024. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Stack;

/// <summary>
/// Stack implemented like a List.
/// </summary>
/// <typeparam name="Type">Type of values in the Stack.</typeparam>
public class ListStack<Type> : IStack<Type>
{
    private Element? head;

    /// <summary>
    /// Initializes a new instance of the <see cref="ListStack{Type}"/> class.
    /// </summary>
    public ListStack()
    {
    }

    /// <summary>
    /// Add a new element to the Stack.
    /// </summary>
    /// <param name="value">Value to add.</param>
    public void Push(Type? value)
    {
        Element newHead = new Element(value, this.head);
        this.head = newHead;
    }

    /// <summary>
    /// Remove the last added element from the Stack and get its value.
    /// </summary>
    /// <returns>The value of removed element.</returns>
    public Type? Pop()
    {
        if (this.head is null)
        {
            throw new InvalidOperationException(
                "Stack was empty");
        }

        Type? output = this.head.Value;
        this.head = this.head.Next;
        return output;
    }

    /// <summary>
    /// Check if the Stack is empty.
    /// </summary>
    /// <returns>True if the Stack is empty, otherwise false.</returns>
    public bool IsEmpty()
    {
        return this.head is null;
    }

    private class Element
    {
        public Element(Type? value, Element? next)
        {
            this.Value = value;
            this.Next = next;
        }

        public Type? Value { get; set; }

        public Element? Next { get; set; }
    }
}
