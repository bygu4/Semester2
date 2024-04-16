// <copyright file="Calculator.cs" company="SPBU">
// Copyright (c) Alexander Bugaev 2024. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Calculator;

using Operations;
using System.ComponentModel;

/// <summary>
/// Step-by-step calculator.
/// Operands are added by digits.
/// Operations can be set by given operation sign.
/// Supports unary functions for operands.
/// </summary>
public class Calculator : INotifyPropertyChanged
{
    private Operand firstOperand;
    private Operation? operation;
    private Operand secondOperand;

    private bool lastActionIsCalculate;

    private string expression;
    private string result;

    /// <summary>
    /// Initializes a new instance of the <see cref="Calculator"/> class.
    /// </summary>
    public Calculator()
    {
        this.firstOperand = new Operand();
        this.secondOperand = new Operand();
        this.BindOperandsWithResultProperty();

        this.expression = string.Empty;
        this.result = Operand.Default;
    }

    /// <summary>
    /// Handlers that are invoked when Calculator properties are changed.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Gets current expression of the Calculator.
    /// </summary>
    public string Expression
    {
        get => this.expression;

        private set => this.ChangePropertyAndNotify(
            ref this.expression, value, nameof(this.Expression));
    }

    /// <summary>
    /// Gets current result of the Calculator.
    /// </summary>
    public string Result
    {
        get => this.result;

        private set => this.ChangePropertyAndNotify(
            ref this.result, value, nameof(this.Result));
    }

    private Operand CurrentOperand
    {
        get => (this.operation is null) ? this.firstOperand : this.secondOperand;
    }

    /// <summary>
    /// Reset properties of the Calculator.
    /// </summary>
    public void Clear()
    {
        this.firstOperand.SetToDefault();
        this.operation = null;
        this.secondOperand.SetToDefault();

        this.lastActionIsCalculate = false;
        this.Expression = string.Empty;
    }

    /// <summary>
    /// Get result of current expression and save it to the Result property.
    /// </summary>
    public void Calculate()
    {
        var result = this.GetResultOfOperation();
        this.SetPropertiesAfterCalculations(result);
    }

    /// <summary>
    /// Set operation of current expreession based on given operation sign.
    /// </summary>
    /// <param name="sign">Sign of the operation.</param>
    public void SetOperationBySign(char sign)
    {
        if (this.secondOperand.Value != 0 && !this.lastActionIsCalculate)
        {
            this.Calculate();
        }

        this.operation = Operations.GetOperationBySign(sign);
        this.SetPropertiesAfterOperationSetting(
            $"{this.firstOperand.Representation} {sign}");
    }

    /// <summary>
    /// Clear the last operand of current expression.
    /// </summary>
    public void Operand_Clear()
    {
        this.ClearAfterCalculations();
        this.CurrentOperand.SetToDefault();
    }

    /// <summary>
    /// Add given digit to the last operand of current expression.
    /// </summary>
    /// <param name="digit">Digit to add.</param>
    public void Operand_AddDigit(char digit)
    {
        this.ClearAfterCalculations();
        this.CurrentOperand.AddDigit(digit);
    }

    /// <summary>
    /// Delete the last digit from the last operand of current expression.
    /// </summary>
    public void Operand_DeleteLastDigit()
    {
        this.ClearAfterCalculations();
        this.CurrentOperand.DeleteLastDigit();
    }

    /// <summary>
    /// Convert the last operand of current expression to the negative one.
    /// </summary>
    public void Operand_ToNegative()
    {
        this.ClearAfterCalculations();
        this.CurrentOperand.ToNegative();
    }

    /// <summary>
    /// Convert the last operand of current expression to float.
    /// </summary>
    public void Operand_ToFloat()
    {
        this.ClearAfterCalculations();
        this.CurrentOperand.ToFloat();
    }

    /// <summary>
    /// Convert the last operand of current expression to percents.
    /// </summary>
    public void Operand_InPercents()
    {
        this.ResetOperationAfterCalculations();
        this.CurrentOperand.InPercents();
    }

    /// <summary>
    /// Raise the last operand of the expression to the power of two.
    /// </summary>
    public void Operand_Square()
    {
        this.ResetOperationAfterCalculations();
        this.CurrentOperand.Square();
    }

    /// <summary>
    /// Set the last operand of the expression as its square root.
    /// </summary>
    public void Operand_SquareRoot()
    {
        this.ResetOperationAfterCalculations();
        this.CurrentOperand.SquareRoot();
    }

    /// <summary>
    /// Set the last operand of the expression to the inverse one.
    /// </summary>
    public void Operand_Inverse()
    {
        this.ResetOperationAfterCalculations();
        this.CurrentOperand.Inverse();
    }

    private (string, float) GetResultOfOperation()
    {
        if (this.operation is null)
        {
            return ($"{this.firstOperand.Representation} =", this.firstOperand.Value);
        }
        else
        {
            string expression = this.operation.GetExpression(
                this.firstOperand.Representation, this.secondOperand.Representation);
            float result = this.operation.GetResult(
                this.firstOperand.Value, this.secondOperand.Value);

            return (expression, result);
        }
    }

    private void SetPropertiesAfterCalculations((string, float) propertiesToSet)
    {
        this.Expression = propertiesToSet.Item1;
        this.firstOperand.SetByValue(propertiesToSet.Item2);
        this.lastActionIsCalculate = true;
    }

    private void SetPropertiesAfterOperationSetting(string expressionToSet)
    {
        this.Expression = expressionToSet;
        this.secondOperand.SetToDefault();
        this.Result = this.firstOperand.Representation;
        this.lastActionIsCalculate = false;
    }

    private void ClearAfterCalculations()
    {
        if (this.lastActionIsCalculate)
        {
            this.Clear();
        }
    }

    private void ResetOperationAfterCalculations()
    {
        if (this.lastActionIsCalculate)
        {
            this.operation = null;
            this.Expression = string.Empty;
        }
    }

    private void OnOperandChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (sender is not null)
        {
            this.Result = ((Operand)sender).Representation;
        }
    }

    private void BindOperandsWithResultProperty()
    {
        this.firstOperand.PropertyChanged += this.OnOperandChanged;
        this.secondOperand.PropertyChanged += this.OnOperandChanged;
    }

    private void NotifyPropertyChanged(string propertyName)
    {
        this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void ChangePropertyAndNotify(
        ref string property, string newValue, string propertyName)
    {
        if (property != newValue)
        {
            property = newValue;
            this.NotifyPropertyChanged(propertyName);
        }
    }
}
