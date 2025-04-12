// Copyright (c) Alexander Bugaev 2024
//
// Use of this source code is governed by an MIT license
// that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

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
