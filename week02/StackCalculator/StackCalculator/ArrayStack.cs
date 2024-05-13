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
    private int numberOfElements;
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
        if (this.numberOfElements == this.arraySize)
        {
            this.Resize(this.arraySize * 2);
        }

        this.values[this.numberOfElements] = value;
        ++this.numberOfElements;
    }

    /// <summary>
    /// Remove the last added element from the Stack and get its value.
    /// </summary>
    /// <returns>The value of removed element.</returns>
    public Type? Pop()
    {
        if (this.numberOfElements == 0)
        {
            throw new InvalidOperationException(
                "Stack was empty");
        }

        --this.numberOfElements;
        Type? value = this.values[this.numberOfElements];
        this.values[this.numberOfElements] = default(Type);
        if (this.numberOfElements < this.arraySize / 2)
        {
            this.Resize(this.arraySize / 2);
        }

        return value;
    }

    /// <summary>
    /// Check if the Stack is empty.
    /// </summary>
    /// <returns>True if the Stack is empty, otherwise false.</returns>
    public bool IsEmpty() => this.numberOfElements == 0;

    private void Resize(int size)
    {
        Type?[] newArray = new Type?[size];
        for (int i = 0; i < this.numberOfElements; ++i)
        {
            newArray[i] = this.values[i];
        }

        this.values = newArray;
        this.arraySize = size;
    }
}
