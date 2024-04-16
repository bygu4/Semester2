namespace Calculator;

using Operations;
using System.ComponentModel;

public class Calculator : INotifyPropertyChanged
{
    private Operand firstOperand;
    private Operation? operation;
    private Operand secondOperand;

    private bool lastActionIsCalculate;

    private string expression;
    private string result;

    public event PropertyChangedEventHandler? PropertyChanged;

    public Calculator()
    {
        this.firstOperand = new Operand();
        this.secondOperand = new Operand();
        this.BindOperandsWithResultProperty();

        this.expression = string.Empty;
        this.result = Operand.Default;
    }

    public string Expression
    {
        get => this.expression;

        private set => this.ChangePropertyAndNotify(
            ref this.expression, value, nameof(this.Expression));
    }

    public string Result
    {
        get => this.result;

        private set => this.ChangePropertyAndNotify(
            ref this.result, value, nameof(this.Result));
    }

    public void Clear()
    {
        this.firstOperand.SetToDefault();
        this.operation = null;
        this.secondOperand.SetToDefault();

        this.lastActionIsCalculate = false;
        this.Expression = string.Empty;
    }

    public void Calculate()
    {
        var result = this.GetResultOfOperation();
        this.SetPropertiesAfterCalculations(result);
    }

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

    public void Operand_Clear()
    {
        this.ClearAfterCalculations();
        this.CurrentOperand.SetToDefault();
    }

    public void Operand_AddDigit(char digit)
    {
        this.ClearAfterCalculations();
        this.CurrentOperand.AddDigit(digit);
    }

    public void Operand_DeleteLastDigit()
    {
        this.ClearAfterCalculations();
        this.CurrentOperand.DeleteLastDigit();
    }

    public void Operand_ToNegative()
    {
        this.ClearAfterCalculations();
        this.CurrentOperand.ToNegative();
    }

    public void Operand_ToFloat()
    {
        this.ClearAfterCalculations();
        this.CurrentOperand.ToFloat();
    }

    public void Operand_InPercents()
    {
        this.ResetOperationAfterCalculations();
        this.CurrentOperand.InPercents();
    }

    public void Operand_Square()
    {
        this.ResetOperationAfterCalculations();
        this.CurrentOperand.Square();
    }

    public void Operand_SquareRoot()
    {
        this.ResetOperationAfterCalculations();
        this.CurrentOperand.SquareRoot();
    }

    public void Operand_Inverse()
    {
        this.ResetOperationAfterCalculations();
        this.CurrentOperand.Inverse();
    }

    private Operand CurrentOperand
    {
        get => (this.operation is null) ? this.firstOperand : this.secondOperand;
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
        this.firstOperand.PropertyChanged += OnOperandChanged;
        this.secondOperand.PropertyChanged += OnOperandChanged;
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
