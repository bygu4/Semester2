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
    private const int NumpadOffset = 48;

    /// <summary>
    /// Process KeyDown event for given Calculator instance.
    /// </summary>
    /// <param name="calculator">Calculator instance for which to process operation.</param>
    /// <param name="e">Event args of KeyDown event.</param>
    public static void ProcessKeyDown(Calculator calculator, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Add || (e.KeyCode == Keys.Oemplus && e.Shift))
        {
            calculator.SetBinaryOperation(Operations.Binary.Addition);
        }
        else if (e.KeyCode == Keys.Subtract || e.KeyCode == Keys.OemMinus)
        {
            calculator.SetBinaryOperation(Operations.Binary.Substraction);
        }
        else if (e.KeyCode == Keys.Multiply || (e.KeyCode == Keys.D8 && e.Shift))
        {
            calculator.SetBinaryOperation(Operations.Binary.Multiplication);
        }
        else if (e.KeyCode == Keys.Divide || e.KeyCode == Keys.Oem2)
        {
            calculator.SetBinaryOperation(Operations.Binary.Division);
        }
        else if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Oemplus)
        {
            calculator.Calculate();
        }
        else if (e.KeyCode == Keys.Clear || e.KeyCode == Keys.Escape)
        {
            calculator.Clear();
        }
        else if (e.KeyCode == Keys.Delete)
        {
            calculator.Operand_Clear();
        }
        else if (e.KeyCode == Keys.Back)
        {
            calculator.Operand_DeleteLastDigit();
        }
        else if (e.KeyCode == Keys.Decimal || e.KeyCode == Keys.Oemcomma)
        {
            calculator.Operand_Decimal();
        }
        else if (e.KeyCode == Keys.F9)
        {
            calculator.Operand_ToNegative();
        }
        else if (e.KeyCode == Keys.D5 && e.Shift)
        {
            calculator.Operand_InPercents();
        }
        else if (e.KeyCode == Keys.Q)
        {
            calculator.Operand_Square();
        }
        else if (e.KeyCode == Keys.D2 && e.Shift)
        {
            calculator.Operand_Square();
        }
        else if (e.KeyCode == Keys.R)
        {
            calculator.Operand_Inverse();
        }
        else if (char.IsDigit((char)e.KeyCode))
        {
            calculator.Operand_AddDigit((char)e.KeyCode);
        }
        else if (char.IsDigit((char)(e.KeyCode - CalculatorKeys.NumpadOffset)))
        {
            calculator.Operand_AddDigit((char)(e.KeyCode - CalculatorKeys.NumpadOffset));
        }
    }
}
