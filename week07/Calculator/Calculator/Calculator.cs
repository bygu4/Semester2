namespace Calculator;

using Operations;
using System.ComponentModel;

public class Calculator : INotifyPropertyChanged
{
    private float firstOperand;
    private Operations? operation;
    private float secondOperand;

    private float buffer;
    private int numberOfDigitsInFractionalPart;
    private bool clearNextInput;

    private string expression;
    private float result;

    public event PropertyChangedEventHandler? PropertyChanged;

    public Calculator()
    {
        this.expression = string.Empty;
    }

    public string Expression
    {
        get
        {
            return this.expression;
        }

        private set
        {
            if (this.expression != value)
            {
                this.expression = value;
                this.NotifyPropertyChanged(nameof(this.Expression));
            }
        }
    }

    public float Result
    {
        get
        {
            return this.result;
        }

        private set
        {
            if (this.result != value)
            {
                this.result = value;
                this.NotifyPropertyChanged(nameof(this.Result));
            }
        }
    }

    public void AddDigitToOperand(int number)
    {
        if (this.clearNextInput)
        {
            this.Clear();
        }

        this.AddToCurrentOperand(number);
    }

    public void SetOperation(char operation)
    {
        if (this.operation is not null)
        {
            this.Calculate();
        }

        switch (operation)
        {
            case '+':
                this.operation = Operations.Addition;
                break;
            case '-':
                this.operation = Operations.Substraction;
                break;
            case '*':
                this.operation = Operations.Multiplication;
                break;
            case '/':
                this.operation = Operations.Division;
                break;
            default:
                throw new ArgumentException("Unknown operation");
        }

        this.Expression = $"{this.firstOperand} {(char)this.operation}";
        this.EmptyBuffer();
    }

    public void Calculate()
    {
        if (this.operation is null)
        {
            this.Expression = $"{this.firstOperand} =";
        }
        else
        {
            this.Expression = $"{this.firstOperand} {(char)this.operation} {this.secondOperand} =";
        }

        this.Result = this.GetResultOfOperation();
        this.firstOperand = this.Result;
        this.clearNextInput = true;
    }

    public void Clear()
    {
        this.firstOperand = 0;
        this.operation = null;
        this.secondOperand = 0;

        this.Expression = string.Empty;
        this.Result = 0;

        this.EmptyBuffer();
    }

    public void ClearOperand()
    {
        this.EmptyBuffer();
        this.Result = 0;
    }

    public void DeleteLastDigit()
    {
        if (this.numberOfDigitsInFractionalPart > 0)
        {
            --this.numberOfDigitsInFractionalPart;
            float numberAfterDeletion = (float)Math.Floor(
                this.CurrentOperand * (float)Math.Pow(10, this.numberOfDigitsInFractionalPart));
            this.CurrentOperand = numberAfterDeletion / (float)Math.Pow(10, this.numberOfDigitsInFractionalPart);
        }
        else
        {
            this.CurrentOperand /= 10;
        }
    }

    public void OperandToNegative()
    {
        this.CurrentOperand = -this.CurrentOperand;
    }

    public void OperandToInverse()
    {
        this.CurrentOperand = 1f / this.CurrentOperand;
    }

    public void OperandToSquare()
    {
        this.CurrentOperand = (float)Math.Pow(this.CurrentOperand, 2);
    }

    public void OperandToSquareRoot()
    {
        this.CurrentOperand = (float)Math.Sqrt(this.CurrentOperand);
    }

    public void OperandToFloat()
    {
        if (this.numberOfDigitsInFractionalPart == 0)
        {
            ++this.numberOfDigitsInFractionalPart;
        }
    }

    public void OperandToPercents()
    {
        this.CurrentOperand /= 100;
        this.numberOfDigitsInFractionalPart += 2;
    }

    private float CurrentOperand
    {
        get
        {
            return this.buffer;
        }

        set
        {
            this.buffer = value;
            this.SetBufferToOperand();
        }
    }

    private void EmptyBuffer()
    {
        this.buffer = 0;
        this.numberOfDigitsInFractionalPart = 0;
        this.clearNextInput = false;
    }

    private void SetBufferToOperand()
    {
        if (this.operation is null)
        {
            this.firstOperand = this.CurrentOperand;
        }
        else
        {
            this.secondOperand = this.CurrentOperand;
        }

        this.Result = this.CurrentOperand;
    }

    private void AddToCurrentOperand(int number)
    {
        if (number < 0 || number > 9)
        {
            throw new ArgumentException("Argument must be between 0 and 9");
        }

        number = (this.CurrentOperand < 0) ? -number : number;
        if (this.numberOfDigitsInFractionalPart == 0)
        {
            this.CurrentOperand = this.CurrentOperand * 10 + number;
        }
        else
        {
            this.CurrentOperand += number / (float)Math.Pow(10, this.numberOfDigitsInFractionalPart);
            ++this.numberOfDigitsInFractionalPart;
        }
    }

    private float GetResultOfOperation()
    {
        switch (this.operation)
        {
            case Operations.Addition:
                return this.firstOperand + this.secondOperand;
            case Operations.Substraction:
                return this.firstOperand - this.secondOperand;
            case Operations.Multiplication:
                return this.firstOperand * this.secondOperand;
            case Operations.Division:
                return this.firstOperand / this.secondOperand;
            default:
                return this.firstOperand;
        }
    }

    private void NotifyPropertyChanged(string propertyName)
    {
        this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
