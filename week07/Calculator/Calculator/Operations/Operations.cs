// <copyright file="Operations.cs" company="SPBU">
// Copyright (c) Alexander Bugaev 2024. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Operations;

/// <summary>
/// Set of operations.
/// </summary>
public static class Operations
{
    /// <summary>
    /// Signs of ariphmetic operations.
    /// </summary>
    public enum Signs
    {
        /// <summary>
        /// Sign of addition.
        /// </summary>
        Addition = '+',

        /// <summary>
        /// Sign of substraction.
        /// </summary>
        Substraction = '-',

        /// <summary>
        /// Sign of multiplication.
        /// </summary>
        Multiplication = '×',

        /// <summary>
        /// Sign on division.
        /// </summary>
        Division = '÷',
    }

    /// <summary>
    /// Gets addition as an instance of Operation class.
    /// </summary>
    public static Operation Addition
    {
        get => new Operation(
            (x, y) => $"{x} {(char)Signs.Addition} {y} =",
            (x, y) => x + y);
    }

    /// <summary>
    /// Gets substraction as an instance of Operation class.
    /// </summary>
    public static Operation Substraction
    {
        get => new Operation(
            (x, y) => $"{x} {(char)Signs.Substraction} {y} =",
            (x, y) => x - y);
    }

    /// <summary>
    /// Gets multiplication as an instance of Operation class.
    /// </summary>
    public static Operation Multiplication
    {
        get => new Operation(
            (x, y) => $"{x} {(char)Signs.Multiplication} {y} =",
            (x, y) => x * y);
    }

    /// <summary>
    /// Gets division as an instance of Operation class.
    /// </summary>
    public static Operation Division
    {
        get => new Operation(
            (x, y) => $"{x} {(char)Signs.Division} {y} =",
            (x, y) => x / y);
    }

    /// <summary>
    /// Gets conversion to percents as an instance of Operation class.
    /// </summary>
    public static Operation InPercents
    {
        get => new Operation(
            (x, y) => $"{x}%",
            (x, y) => x / 100);
    }

    /// <summary>
    /// Gets squaring as an instance of Operation class.
    /// </summary>
    public static Operation Square
    {
        get => new Operation(
            (x, y) => $"sqr({x})",
            (x, y) => (float)Math.Pow(x, 2));
    }

    /// <summary>
    /// Gets taking the square root as an instance of Operation class.
    /// </summary>
    public static Operation SquareRoot
    {
        get => new Operation(
            (x, y) => $"sqrt({x})",
            (x, y) => (float)Math.Sqrt(x));
    }

    /// <summary>
    /// Gets inversion as an instance of Operation class.
    /// </summary>
    public static Operation Inverse
    {
        get => new Operation(
            (x, y) => $"1/{x}",
            (x, y) => 1f / x);
    }

    /// <summary>
    /// Get an instance of ariphmetic operation based on given operation sign.
    /// </summary>
    /// <param name="sign">The sign of operation.</param>
    /// <returns>Ariphmetic operation as an instance of Operation class.</returns>
    /// <exception cref="ArgumentException">Given sign is not in Operations.Signs.</exception>
    public static Operation GetOperationBySign(char sign)
    {
        return sign switch
        {
            (char)Operations.Signs.Addition => Operations.Addition,
            (char)Operations.Signs.Substraction => Operations.Substraction,
            (char)Operations.Signs.Multiplication => Operations.Multiplication,
            (char)Operations.Signs.Division => Operations.Division,
            _ => throw new ArgumentException("Unknown operation"),
        };
    }
}
