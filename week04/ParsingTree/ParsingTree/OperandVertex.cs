// <copyright file="OperandVertex.cs" company="SPBU">
// Copyright (c) Alexander Bugaev 2024. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Vertices;

/// <summary>
/// Implementation of the operand vertex of Parsing Tree.
/// </summary>
public class OperandVertex : IVertex
{
    private int value;

    /// <summary>
    /// Initializes a new instance of the <see cref="OperandVertex"/> class.
    /// </summary>
    /// <param name="value">Value of the operand.</param>
    public OperandVertex(int value)
    {
        this.value = value;
    }

    /// <summary>
    /// Get the value of the operand.
    /// </summary>
    /// <returns>The value of the operand.</returns>
    public float Evaluate()
    {
        return this.value;
    }

    /// <summary>
    /// Print the operand to the console.
    /// </summary>
    public void PrintToConsole()
    {
        Console.Write(this.value);
    }

    /// <summary>
    /// Get the operand in a string form.
    /// </summary>
    /// <returns>The operand in a string form.</returns>
    public string GetExpression()
    {
        return $"{this.value}";
    }
}
