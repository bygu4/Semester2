﻿// Copyright (c) 2024
//
// Use of this source code is governed by an MIT license
// that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

namespace Calculator;

using Operations;

/// <summary>
/// Class for defining and processing char commands.
/// </summary>
public static class CalculatorCommands
{
    /// <summary>
    /// Value of Calculate command.
    /// </summary>
    public const char Calculate = '=';

    /// <summary>
    /// Value of Clear command.
    /// </summary>
    public const char Clear = '\n';

    /// <summary>
    /// Value of Operand_Clear command.
    /// </summary>
    public const char Delete = '\r';

    /// <summary>
    /// Value of Operand_Back command.
    /// </summary>
    public const char Back = '\b';

    /// <summary>
    /// Value of Operand_Decimal command.
    /// </summary>
    public const char Decimal = ',';

    /// <summary>
    /// Value of Operand_ToNegative command.
    /// </summary>
    public const char ToNegative = '±';

    /// <summary>
    /// Value of Operand_InPercents command.
    /// </summary>
    public const char Percent = '%';

    /// <summary>
    /// Value of Operand_Square command.
    /// </summary>
    public const char Square = 'Q';

    /// <summary>
    /// Value of Operand_SquareRoot command.
    /// </summary>
    public const char SquareRoot = '@';

    /// <summary>
    /// Value of Operand_Inverse command.
    /// </summary>
    public const char Inverse = 'R';

    /// <summary>
    /// Execute Calculator method for given instance based on given char command.
    /// </summary>
    /// <param name="calculator">
    /// The instance of Calculator class for which to execute a method.</param>
    /// <param name="command">Command to execute.</param>
    public static void Execute(Calculator calculator, char command)
    {
        if (char.IsDigit(command))
        {
            calculator.Operand_AddDigit(command);
        }
        else if (Enum.IsDefined(typeof(Operations.Binary), (int)command))
        {
            calculator.SetBinaryOperation((Operations.Binary)command);
        }

        switch (command)
        {
            case CalculatorCommands.Calculate:
                calculator.Calculate();
                return;
            case CalculatorCommands.Clear:
                calculator.Clear();
                return;
            case CalculatorCommands.Delete:
                calculator.Operand_Clear();
                return;
            case CalculatorCommands.Back:
                calculator.Operand_Back();
                return;
            case CalculatorCommands.Decimal:
                calculator.Operand_Decimal();
                return;
            case CalculatorCommands.ToNegative:
                calculator.Operand_ToNegative();
                return;
            case CalculatorCommands.Percent:
                calculator.Operand_InPercents();
                return;
            case CalculatorCommands.Square:
                calculator.Operand_Square();
                return;
            case CalculatorCommands.SquareRoot:
                calculator.Operand_SquareRoot();
                return;
            case CalculatorCommands.Inverse:
                calculator.Operand_Inverse();
                return;
        }
    }
}
