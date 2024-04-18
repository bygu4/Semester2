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
    private Func<string, string, string> representationFunction;
    private Func<float, float, float> resultFunction;

    /// <summary>
    /// Initializes a new instance of the <see cref="Operation"/> class.
    /// </summary>
    /// <param name="representationFunction">Function to get the representation of the operation.</param>
    /// <param name="resultFunction">Function to get the result of the operation.</param>
    public Operation(
        Func<string, string, string> representationFunction,
        Func<float, float, float> resultFunction)
    {
        this.representationFunction = representationFunction;
        this.resultFunction = resultFunction;
    }

    /// <summary>
    /// Get representation of the operation with given arguments.
    /// </summary>
    /// <param name="x">The first argument of the operation.</param>
    /// <param name="y">The second argument of the operation.</param>
    /// <returns>The representation obtained by the function set for the operation.</returns>
    public string GetRepresentation(string x, string y)
    {
        return this.representationFunction(x, y);
    }

    /// <summary>
    /// Get result of the operation with given arguments.
    /// </summary>
    /// <param name="x">The first argument of the operation.</param>
    /// <param name="y">The second argument of the operation.</param>
    /// <returns>The result obtained by the function set for the operation.</returns>
    public float GetResult(float x, float y)
    {
        return this.resultFunction(x, y);
    }
}
