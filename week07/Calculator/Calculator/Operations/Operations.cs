// <copyright file="Operations.cs" company="SPBU">
// Copyright (c) Alexander Bugaev 2024. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Operations;

/// <summary>
/// Set of basic operations.
/// </summary>
public static class Operations
{
    /// <summary>
    /// Set of binary ariphmetic operations.
    /// </summary>
    public enum Binary : int
    {
        /// <summary>
        /// Binary operation of addition.
        /// </summary>
        Addition = '+',

        /// <summary>
        /// Binary operation of substraction.
        /// </summary>
        Substraction = '-',

        /// <summary>
        /// Binary operation of multiplication.
        /// </summary>
        Multiplication = '×',

        /// <summary>
        /// Binary operation of division.
        /// </summary>
        Division = '÷',
    }

    /// <summary>
    /// Gets conversion to percents as an instance of Operation class.
    /// </summary>
    public static Operation InPercents
    {
        get => new (
            (x, y) => $"{x}%",
            (x, y) => x / 100);
    }

    /// <summary>
    /// Gets squaring as an instance of Operation class.
    /// </summary>
    public static Operation Square
    {
        get => new (
            (x, y) => $"sqr({x})",
            (x, y) => (float)Math.Pow(x, 2));
    }

    /// <summary>
    /// Gets taking the square root as an instance of Operation class.
    /// </summary>
    public static Operation SquareRoot
    {
        get => new (
            (x, y) => $"sqrt({x})",
            (x, y) => (float)Math.Sqrt(x));
    }

    /// <summary>
    /// Gets inversion as an instance of Operation class.
    /// </summary>
    public static Operation Inverse
    {
        get => new (
            (x, y) => $"1/{x}",
            (x, y) => (x != 0) ? 1f / x : float.NaN);
    }

    private static Operation Addition
    {
        get => new (
            (x, y) => $"{x} {(char)Binary.Addition} {y} =",
            (x, y) => x + y);
    }

    private static Operation Substraction
    {
        get => new (
            (x, y) => $"{x} {(char)Binary.Substraction} {y} =",
            (x, y) => x - y);
    }

    private static Operation Multiplication
    {
        get => new (
            (x, y) => $"{x} {(char)Binary.Multiplication} {y} =",
            (x, y) => x * y);
    }

    private static Operation Division
    {
        get => new (
            (x, y) => $"{x} {(char)Binary.Division} {y} =",
            (x, y) => (y != 0) ? x / y : float.NaN);
    }

    /// <summary>
    /// Get an instance of binary operation based on given enumerable type.
    /// </summary>
    /// <param name="operation">Binary operation to get.</param>
    /// <returns>Binary operation as an instance of Operation class.</returns>
    /// <exception cref="ArgumentException">Given value is not in Operations.Binary.</exception>
    public static Operation GetOperation(Operations.Binary operation)
    {
        return operation switch
        {
            Operations.Binary.Addition => Operations.Addition,
            Operations.Binary.Substraction => Operations.Substraction,
            Operations.Binary.Multiplication => Operations.Multiplication,
            Operations.Binary.Division => Operations.Division,
            _ => throw new ArgumentException("Unknown operation"),
        };
    }
}
