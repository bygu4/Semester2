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
    private bool lastActionIsSetOperation;

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
        this.lastActionIsSetOperation = false;
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
        if (!this.lastActionIsCalculate && !this.lastActionIsSetOperation)
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
        this.CurrentOperand_ExecuteInputMethod((Operand x) => x.SetToDefault());
    }

    /// <summary>
    /// Add given digit to the last operand of current expression.
    /// </summary>
    /// <param name="digit">Digit to add.</param>
    public void Operand_AddDigit(char digit)
    {
        this.CurrentOperand_ExecuteInputMethod((Operand x) => x.AddDigit(digit));
    }

    /// <summary>
    /// Delete the last digit from the last operand of current expression.
    /// </summary>
    public void Operand_DeleteLastDigit()
    {
        this.CurrentOperand_ExecuteInputMethod((Operand x) => x.DeleteLastDigit());
    }

    /// <summary>
    /// Add decimal point to the last operand of current expression.
    /// </summary>
    public void Operand_Decimal()
    {
        this.CurrentOperand_ExecuteInputMethod((Operand x) => x.Decimal());
    }

    /// <summary>
    /// Convert the last operand of current expression to the negative one.
    /// </summary>
    public void Operand_ToNegative()
    {
        this.CurrentOperand_ExecuteUnaryOperationMethod((Operand x) => x.ToNegative());
    }

    /// <summary>
    /// Convert the last operand of current expression to percents.
    /// </summary>
    public void Operand_InPercents()
    {
        this.CurrentOperand_ExecuteUnaryOperationMethod((Operand x) => x.InPercents());
    }

    /// <summary>
    /// Raise the last operand of the expression to the power of two.
    /// </summary>
    public void Operand_Square()
    {
        this.CurrentOperand_ExecuteUnaryOperationMethod((Operand x) => x.Square());
    }

    /// <summary>
    /// Set the last operand of the expression as its square root.
    /// </summary>
    public void Operand_SquareRoot()
    {
        this.CurrentOperand_ExecuteUnaryOperationMethod((Operand x) => x.SquareRoot());
    }

    /// <summary>
    /// Set the last operand of the expression to the inverse one.
    /// </summary>
    public void Operand_Inverse()
    {
        this.CurrentOperand_ExecuteUnaryOperationMethod((Operand x) => x.Inverse());
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
        this.lastActionIsSetOperation = false;
    }

    private void SetPropertiesAfterOperationSetting(string expressionToSet)
    {
        this.Expression = expressionToSet;
        this.secondOperand.SetToDefault();
        this.Result = this.firstOperand.Representation;

        this.lastActionIsCalculate = false;
        this.lastActionIsSetOperation = true;
    }

    private void ResetBeforeInput()
    {
        if (this.lastActionIsCalculate)
        {
            this.Clear();
        }
    }

    private void ResetBeforeUnaryOperation()
    {
        if (this.lastActionIsCalculate)
        {
            this.operation = null;
            this.Expression = string.Empty;
        }
    }

    private void CurrentOperand_ExecuteInputMethod(Action<Operand> inputMethod)
    {
        this.ResetBeforeInput();
        inputMethod(this.CurrentOperand);
    }

    private void CurrentOperand_ExecuteUnaryOperationMethod(Action<Operand> unaryOperationMethod)
    {
        this.ResetBeforeUnaryOperation();
        unaryOperationMethod(this.CurrentOperand);
        this.lastActionIsSetOperation = this.operation == null;
    }

    private void OnOperandChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (sender is not null)
        {
            this.Result = ((Operand)sender).Representation;
            this.lastActionIsCalculate = false;
            this.lastActionIsSetOperation = false;
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
