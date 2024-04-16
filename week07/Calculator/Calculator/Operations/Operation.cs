// <copyright file="Operation.cs" company="SPBU">
// Copyright (c) Alexander Bugaev 2024. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Operations;

/// <summary>
/// Class representing an operation.
/// </summary>
public class Operation
{
    private Func<string, string, string> expressionFunction;
    private Func<float, float, float> resultFunction;

    /// <summary>
    /// Initializes a new instance of the <see cref="Operation"/> class.
    /// </summary>
    /// <param name="expressionFunction">Function to get the representation of the operation.</param>
    /// <param name="resultFunction">Function to get the result of the operation.</param>
    public Operation(
        Func<string, string, string> expressionFunction,
        Func<float, float, float> resultFunction)
    {
        this.expressionFunction = expressionFunction;
        this.resultFunction = resultFunction;
    }

    /// <summary>
    /// Get the representation of the operation with given arguments.
    /// </summary>
    /// <param name="x">The first argument of the operation.</param>
    /// <param name="y">The second argument of the operation.</param>
    /// <returns>The representation obtained by the function set for of the operation.</returns>
    public string GetExpression(string x, string y)
    {
        return this.expressionFunction(x, y);
    }

    /// <summary>
    /// Get the result of the operation with given arguments.
    /// </summary>
    /// <param name="x">The first argument of the operation.</param>
    /// <param name="y">The second argument of the operation.</param>
    /// <returns>The result obtained by the function set for of the operation.</returns>
    public float GetResult(float x, float y)
    {
        return this.resultFunction(x, y);
    }
}
