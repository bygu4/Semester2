// Copyright (c) Alexander Bugaev 2024
//
// Use of this source code is governed by an MIT license
// that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

namespace Parsing;

using Vertices;
using Operations;
using System;

/// <summary>
/// Class representing an ariphmetic expression in a form of a tree.
/// </summary>
public class ParsingTree
{
    private IVertex root;

    /// <summary>
    /// Initializes a new instance of the <see cref="ParsingTree"/> class.
    /// Builds a new Parsing Tree based on data from the file at the given path.
    /// </summary>
    /// <param name="filePath">Path of the file containing expression in prefix form.</param>
    public ParsingTree(string filePath)
    {
        string inputString = this.GetString(filePath);
        inputString = inputString.Replace('(', ' ');
        inputString = inputString.Replace(')', ' ');
        string[] elements = inputString.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        int index = 0;
        this.root = this.Build(elements, ref index);

        if (index != elements.Length)
        {
            throw new InvalidDataException("Invalid file format");
        }
    }

    /// <summary>
    /// Get value of the expression stored in this tree.
    /// </summary>
    /// <returns>The calculated value.</returns>
    public float Evaluate()
    {
        return this.root.Evaluate();
    }

    /// <summary>
    /// Print the expression stored in this tree to the console.
    /// </summary>
    public void PrintToConsole()
    {
        this.root.PrintToConsole();
    }

    /// <summary>
    /// Get the expression stored in this tree.
    /// </summary>
    /// <returns>The expression in the string form.</returns>
    public string GetExpression()
    {
        return this.root.GetExpression();
    }

    private string GetString(string filePath)
    {
        using StreamReader reader = new StreamReader(filePath);
        return reader.ReadToEnd();
    }

    private Operations GetOperation(string element)
    {
        if (element.Length != 1)
        {
            throw new InvalidDataException("Invalid file format");
        }

        switch (element[0])
        {
            case '+':
                return Operations.Addition;
            case '-':
                return Operations.Substraction;
            case '*':
                return Operations.Multiplication;
            case '/':
                return Operations.Division;
            default:
                throw new InvalidDataException("Invalid file format");
        }
    }

    private IVertex Build(string[] elements, ref int index)
    {
        if (index >= elements.Length)
        {
            throw new InvalidDataException("Invalid file format");
        }

        int number = 0;
        if (int.TryParse(elements[index], out number))
        {
            ++index;
            return new OperandVertex(number);
        }

        Operations operation = this.GetOperation(elements[index]);
        ++index;
        IVertex leftChild = this.Build(elements, ref index);
        IVertex rightChild = this.Build(elements, ref index);
        return new OperatorVertex(operation, leftChild, rightChild);
    }
}
