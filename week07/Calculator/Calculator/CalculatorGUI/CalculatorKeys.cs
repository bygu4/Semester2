// <copyright file="CalculatorKeys.cs" company="SPBU">
// Copyright (c) Alexander Bugaev 2024. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace CalculatorGUI;

using Calculator;
using Operations;

/// <summary>
/// Class for processing keyboard input.
/// </summary>
public static class CalculatorKeys
{
    private const char Default = (char)0;
    private const int NumpadOffset = 48;

    /// <summary>
    /// Get key value of KeyDown event.
    /// </summary>
    /// <param name="e">Event args of KeyDown event.</param>
    /// <returns>The value of pressed key.</returns>
    public static char GetCommand(KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Add || (e.KeyCode == Keys.Oemplus && e.Shift))
        {
            return (char)Operations.Signs.Addition;
        }
        else if (e.KeyCode == Keys.Subtract || e.KeyCode == Keys.OemMinus)
        {
            return (char)Operations.Signs.Substraction;
        }
        else if (e.KeyCode == Keys.Multiply || (e.KeyCode == Keys.D8 && e.Shift))
        {
            return (char)Operations.Signs.Multiplication;
        }
        else if (e.KeyCode == Keys.Divide || e.KeyCode == Keys.Oem2)
        {
            return (char)Operations.Signs.Division;
        }
        else if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Oemplus)
        {
            return CalculatorCommands.Enter;
        }
        else if (e.KeyCode == Keys.Clear || e.KeyCode == Keys.Escape)
        {
            return CalculatorCommands.Clear;
        }
        else if (e.KeyCode == Keys.Delete)
        {
            return CalculatorCommands.Delete;
        }
        else if (e.KeyCode == Keys.Back)
        {
            return CalculatorCommands.Back;
        }
        else if (e.KeyCode == Keys.Decimal || e.KeyCode == Keys.Oemcomma)
        {
            return CalculatorCommands.Decimal;
        }
        else if (e.KeyCode == Keys.D5 && e.Shift)
        {
            return CalculatorCommands.Percent;
        }
        else if (char.IsDigit((char)e.KeyCode))
        {
            return (char)e.KeyCode;
        }
        else if (char.IsDigit((char)(e.KeyCode - CalculatorKeys.NumpadOffset)))
        {
            return (char)(e.KeyCode - CalculatorKeys.NumpadOffset);
        }

        return CalculatorKeys.Default;
    }
}
