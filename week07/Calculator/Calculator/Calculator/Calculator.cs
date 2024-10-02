// Copyright (c) 2024
//
// Use of this source code is governed by an MIT license
// that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

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

    /// <summary>
    /// Gets decimal separator used in the Calculator in current culture.
    /// </summary>
    public string DecimalSeparator
    {
        get => this.CurrentOperand.DecimalSeparator;
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
    /// Set operation of current expression based on given enumerable type.
    /// </summary>
    /// <param name="operation">Operation to set.</param>
    public void SetBinaryOperation(Operations.Binary operation)
    {
        if (!this.lastActionIsCalculate && !this.lastActionIsSetOperation)
        {
            this.Calculate();
        }

        this.operation = Operations.GetOperation(operation);
        this.SetPropertiesAfterOperationSetting(
            $"{this.firstOperand.Representation} {(char)operation}");
    }

    /// <summary>
    /// Clear the last operand of current expression.
    /// </summary>
    public void Operand_Clear()
        => this.CurrentOperand_ExecuteInputMethod((Operand x) => x.SetToDefault());

    /// <summary>
    /// Add given digit to the last operand of current expression.
    /// </summary>
    /// <param name="digit">Digit to add.</param>
    public void Operand_AddDigit(char digit)
        => this.CurrentOperand_ExecuteInputMethod((Operand x) => x.AddDigit(digit));

    /// <summary>
    /// Delete the last character of the last operand of current expression.
    /// </summary>
    public void Operand_Back()
        => this.CurrentOperand_ExecuteInputMethod((Operand x) => x.Back());

    /// <summary>
    /// Add decimal point to the last operand of current expression.
    /// </summary>
    public void Operand_Decimal()
        => this.CurrentOperand_ExecuteInputMethod((Operand x) => x.Decimal());

    /// <summary>
    /// Convert the last operand of current expression to the negative one.
    /// </summary>
    public void Operand_ToNegative()
        => this.CurrentOperand_ExecuteUnaryOperationMethod((Operand x) => x.ToNegative());

    /// <summary>
    /// Convert the last operand of current expression to percents.
    /// </summary>
    public void Operand_InPercents()
        => this.CurrentOperand_ExecuteUnaryOperationMethod((Operand x) => x.InPercents());

    /// <summary>
    /// Raise the last operand of the expression to the power of two.
    /// </summary>
    public void Operand_Square()
        => this.CurrentOperand_ExecuteUnaryOperationMethod((Operand x) => x.Square());

    /// <summary>
    /// Set the last operand of the expression as its square root.
    /// </summary>
    public void Operand_SquareRoot()
        => this.CurrentOperand_ExecuteUnaryOperationMethod((Operand x) => x.SquareRoot());

    /// <summary>
    /// Set the last operand of the expression to the inverse one.
    /// </summary>
    public void Operand_Inverse()
        => this.CurrentOperand_ExecuteUnaryOperationMethod((Operand x) => x.Inverse());

    private (string, float) GetResultOfOperation()
    {
        if (this.operation is null)
        {
            return ($"{this.firstOperand.Representation} =", this.firstOperand.Value);
        }
        else
        {
            var expression = this.operation.GetRepresentation(
                this.firstOperand.Representation, this.secondOperand.Representation);
            var result = this.operation.GetResult(
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
        => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

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
