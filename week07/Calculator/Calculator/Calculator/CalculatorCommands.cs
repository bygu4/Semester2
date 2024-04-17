// <copyright file="CalculatorCommands.cs" company="SPBU">
// Copyright (c) Alexander Bugaev 2024. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

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
    public const char Enter = '\n';

    /// <summary>
    /// Value of Clear command.
    /// </summary>
    public const char Clear = '\r';

    /// <summary>
    /// Value of Operand_Clear command.
    /// </summary>
    public const char Delete = '\\';

    /// <summary>
    /// Value of Operand_DeleteLastDigit command.
    /// </summary>
    public const char Back = '\b';

    /// <summary>
    /// Value of Operand_ToNegative command.
    /// </summary>
    public const char ToNegative = '±';

    /// <summary>
    /// Value of Operand_Decimal command.
    /// </summary>
    public const char Decimal = ',';

    /// <summary>
    /// Value of Operand_InPercents command.
    /// </summary>
    public const char Percent = '%';

    /// <summary>
    /// Value of Operand_Square command.
    /// </summary>
    public const char Square = '²';

    /// <summary>
    /// Value of Operand_SquareRoot command.
    /// </summary>
    public const char SquareRoot = '½';

    /// <summary>
    /// Value of Operand_Inverse command.
    /// </summary>
    public const char Inverse = '⁻';

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
        else if (Enum.IsDefined(typeof(Operations.Signs), (int)command))
        {
            calculator.SetOperationBySign(command);
        }

        switch (command)
        {
            case CalculatorCommands.Enter:
                calculator.Calculate();
                return;
            case CalculatorCommands.Clear:
                calculator.Clear();
                return;
            case CalculatorCommands.Delete:
                calculator.Operand_Clear();
                return;
            case CalculatorCommands.Back:
                calculator.Operand_DeleteLastDigit();
                return;
            case CalculatorCommands.ToNegative:
                calculator.Operand_ToNegative();
                return;
            case CalculatorCommands.Decimal:
                calculator.Operand_Decimal();
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
