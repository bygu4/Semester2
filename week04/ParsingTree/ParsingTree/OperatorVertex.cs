// Copyright (c) 2024
//
// Use of this source code is governed by an MIT license
// that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

namespace Vertices;

using Operations;
using System;

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
