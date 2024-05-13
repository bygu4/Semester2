// <copyright file="CustomButtonClickArgs.cs" company="SPBU">
// Copyright (c) Alexander Bugaev 2024. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace CalculatorGUI;

/// <summary>
/// Class of event arguments for the click of CustomButton.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="CustomButtonClickArgs"/> class.
/// </remarks>
/// <param name="value">Value to set for this instance.</param>
public class CustomButtonClickArgs(char value)
    : EventArgs
{
    /// <summary>
    /// Gets the value of this instance.
    /// </summary>
    public char Value { get; private set; } = value;
}
