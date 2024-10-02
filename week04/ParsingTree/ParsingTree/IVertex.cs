// Copyright (c) 2024
//
// Use of this source code is governed by an MIT license
// that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

namespace Vertices;

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
