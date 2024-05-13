// <copyright file="IStack.cs" company="SPBU">
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
