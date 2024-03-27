// <copyright file="Vertices.cs" company="SPBU">
// Copyright (c) Alexander Bugaev 2024. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Vertices;

using Operations;
using System;

/// <summary>
/// The vertex of Parsing Tree.
/// Can be either an operand or an operator.
/// Supports calculating its value and printing the expression it represents to the console.
/// </summary>
public interface IVertex
{
    /// <summary>
    /// Calculate the value of vertex.
    /// </summary>
    /// <returns>The calculated value.</returns>
    public float Evaluate();

    /// <summary>
    /// Print the expression of the vertex to the console.
    /// </summary>
    public void PrintToConsole();

    /// <summary>
    /// Get the expression represented by the vertex.
    /// </summary>
    /// <returns>The expression in the string form.</returns>
    public string GetExpression();
}

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

/// <summary>
/// Implementation of the operator vertex of Parsing Tree.
/// </summary>
public class OperatorVertex : IVertex
{
    private Operations operation;
    private IVertex leftChild;
    private IVertex rightChild;

    /// <summary>
    /// Initializes a new instance of the <see cref="OperatorVertex"/> class.
    /// </summary>
    /// <param name="operation">An operation to set for this vertex.</param>
    /// <param name="leftChild">The left child vertex to set.</param>
    /// <param name="rightChild">The right child vertex to set.</param>
    public OperatorVertex(Operations operation, IVertex leftChild, IVertex rightChild)
    {
        this.operation = operation;
        this.leftChild = leftChild;
        this.rightChild = rightChild;
    }

    /// <summary>
    /// Evaluate child vertices and apply to them an operation set for this vertex.
    /// </summary>
    /// <returns>The calculated result.</returns>
    public float Evaluate()
    {
        float leftOperand = this.leftChild.Evaluate();
        float rightOperand = this.rightChild.Evaluate();
        switch (this.operation)
        {
            case Operations.Addition:
                return leftOperand + rightOperand;
            case Operations.Substraction:
                return leftOperand - rightOperand;
            case Operations.Multiplication:
                return leftOperand * rightOperand;
            case Operations.Division:
                if (Math.Abs(rightOperand) < float.Epsilon)
                {
                    throw new DivideByZeroException("Division by zero");
                }

                return leftOperand / rightOperand;
            default:
                return 0;
        }
    }

    /// <summary>
    /// Print the expression this vertex represents to the console.
    /// </summary>
    public void PrintToConsole()
    {
        Console.Write("(");
        this.leftChild.PrintToConsole();
        Console.Write($" {(char)this.operation} ");
        this.rightChild.PrintToConsole();
        Console.Write(")");
    }

    /// <summary>
    /// Get the expression represented by this vertex.
    /// </summary>
    /// <returns>The expression in the string form.</returns>
    public string GetExpression()
    {
        return $"({this.leftChild.GetExpression()} {(char)this.operation} {this.rightChild.GetExpression()})";
    }
}
