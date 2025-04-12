// Copyright (c) Alexander Bugaev 2024
//
// Use of this source code is governed by an MIT license
// that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

namespace Operations;

/// <summary>
/// Class representing an operation.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="Operation"/> class.
/// </remarks>
/// <param name="representationFunction">Function to get the representation of the operation.</param>
/// <param name="resultFunction">Function to get the result of the operation.</param>
public class Operation(
    Func<string, string, string> representationFunction,
    Func<float, float, float> resultFunction)
{
    private Func<string, string, string> representationFunction = representationFunction;
    private Func<float, float, float> resultFunction = resultFunction;

    /// <summary>
    /// Get representation of the operation with given arguments.
    /// </summary>
    /// <param name="x">The first argument of the operation.</param>
    /// <param name="y">The second argument of the operation.</param>
    /// <returns>The representation obtained by the function set for the operation.</returns>
    public string GetRepresentation(string x, string y) => this.representationFunction(x, y);

    /// <summary>
    /// Get result of the operation with given arguments.
    /// </summary>
    /// <param name="x">The first argument of the operation.</param>
    /// <param name="y">The second argument of the operation.</param>
    /// <returns>The result obtained by the function set for the operation.</returns>
    public float GetResult(float x, float y) => this.resultFunction(x, y);
}
