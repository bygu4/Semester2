﻿// <copyright file="Stack.cs" company="SPBU">
// Copyright (c) Alexander Bugaev 2024. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Stack;

/// <summary>
/// LastIn-FirstOut data structure containing values of specified type.
/// </summary>
/// <typeparam name="Type">Type of values in the Stack.</typeparam>
public interface IStack<Type>
{
    /// <summary>
    /// Add a new element to the Stack.
    /// </summary>
    /// <param name="value">Value to add.</param>
    public void Push(Type? value);

    /// <summary>
    /// Remove the last added element from the Stack and get its value.
    /// </summary>
    /// <returns>The value of removed element.</returns>
    public Type? Pop();

    /// <summary>
    /// Check if the Stack is empty.
    /// </summary>
    /// <returns>True if the Stack is empty, otherwise false.</returns>
    public bool IsEmpty();
}

/// <summary>
/// Stack implementation.
/// </summary>
/// <typeparam name="Type">Type of values in the Stack.</typeparam>
public class ListStack<Type> : IStack<Type>
{
    private Element? head;

    /// <summary>
    /// Initializes a new instance of the <see cref="ListStack{T}"/> class.
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
                "Attempt to pop out of an empty stack");
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

/// <summary>
/// Stack implemantation.
/// </summary>
/// <typeparam name="Type">Type of values in the Stack.</typeparam>
public class ArrayStack<Type> : IStack<Type>
{
    private Type?[] values;
    private int count;
    private int arraySize = 1;

    /// <summary>
    /// Initializes a new instance of the <see cref="ArrayStack{T}"/> class.
    /// </summary>
    public ArrayStack()
    {
        this.values = new Type?[this.arraySize];
    }

    /// <summary>
    /// Add a new element to the Stack.
    /// </summary>
    /// <param name="value">Value to add.</param>
    public void Push(Type? value)
    {
        if (this.count == this.arraySize)
        {
            this.Resize(this.arraySize * 2);
        }

        this.values[this.count] = value;
        ++this.count;
    }

    /// <summary>
    /// Remove the last added element from the Stack and get its value.
    /// </summary>
    /// <returns>The value of removed element.</returns>
    public Type? Pop()
    {
        if (this.count == 0)
        {
            throw new InvalidOperationException(
                "Attempt to pop out of an empty stack");
        }

        --this.count;
        Type? value = this.values[this.count];
        this.values[this.count] = default(Type);
        if (this.count < this.arraySize / 2)
        {
            this.Resize(this.arraySize / 2);
        }

        return value;
    }

    /// <summary>
    /// Check if the Stack is empty.
    /// </summary>
    /// <returns>True if the Stack is empty, otherwise false.</returns>
    public bool IsEmpty()
    {
        return this.count == 0;
    }

    private void Resize(int size)
    {
        Type?[] newArray = new Type?[size];
        for (int i = 0; i < this.count; ++i)
        {
            newArray[i] = this.values[i];
        }

        this.values = newArray;
        this.arraySize = size;
    }
}
