// <copyright file="ArrayStack.cs" company="SPBU">
// Copyright (c) Alexander Bugaev 2024. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Stack;

/// <summary>
/// Stack implemented by an array.
/// </summary>
/// <typeparam name="Type">Type of values in the Stack.</typeparam>
public class ArrayStack<Type> : IStack<Type>
{
    private Type?[] values;
    private int count;
    private int arraySize = 1;

    /// <summary>
    /// Initializes a new instance of the <see cref="ArrayStack{Type}"/> class.
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
                "Stack was empty");
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
