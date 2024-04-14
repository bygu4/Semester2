namespace Calculator;

using Operations;

public class Calculator
{
    private float firstOperand;
    private Operations? operation;
    private float secondOperand;

    private float buffer;
    private int numberOfDigitsInFractionalPart;
    private bool clearNextInput;

    public Calculator()
    {
        this.Equation = string.Empty;
    }

    public string Equation { get; private set; }

    public float Result { get; private set; }

    public void AddDigit(int number)
    {
        if (this.clearNextInput)
        {
            this.Clear();
        }

        this.AddDigitToBuffer(number);
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

        this.Equation = $"{this.firstOperand} {(char)this.operation}";
        this.EmptyBuffer();
    }

    public void Calculate()
    {
        if (this.operation is null)
        {
            this.Equation = $"{this.firstOperand} =";
        }
        else
        {
            this.Equation = $"{this.firstOperand} {(char)this.operation} {this.secondOperand} =";
        }

        this.Result = this.GetResultOfOperation();
        this.firstOperand = this.Result;
        this.clearNextInput = true;
    }

    public void ToNegative()
    {
        this.Buffer = -this.Buffer;
    }

    public void ToInverse()
    {
        this.Buffer = 1f / this.Buffer;
    }

    public void ToSquared()
    {
        this.Buffer = (float)Math.Pow(this.Buffer, 2);
    }

    public void ToSquareRoot()
    {
        this.Buffer = (float)Math.Sqrt(this.Buffer);
    }

    public void ToFloat()
    {
        if (this.numberOfDigitsInFractionalPart == 0)
        {
            ++this.numberOfDigitsInFractionalPart;
        }
    }


    public void Clear()
    {
        this.firstOperand = 0;
        this.operation = null;
        this.secondOperand = 0;

        this.Equation = string.Empty;
        this.Result = 0;
        this.clearNextInput = false;

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
                this.Buffer * (float)Math.Pow(10, this.numberOfDigitsInFractionalPart));
            this.Buffer = numberAfterDeletion / (float)Math.Pow(10, this.numberOfDigitsInFractionalPart);
        }
        else
        {
            this.Buffer /= 10;
        }
    }

    private float Buffer
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

    private void SetBufferToOperand()
    {
        if (this.operation is null)
        {
            this.firstOperand = this.Buffer;
        }
        else
        {
            this.secondOperand = this.Buffer;
        }

        this.Result = this.Buffer;
    }

    private void AddDigitToBuffer(int number)
    {
        if (number < 0 || number > 9)
        {
            throw new ArgumentException("Argument must be between 0 and 9");
        }

        number = (this.Buffer < 0) ? -number : number;
        if (this.numberOfDigitsInFractionalPart == 0)
        {
            this.Buffer = this.Buffer * 10 + number;
        }
        else
        {
            this.Buffer += number / (float)Math.Pow(10, this.numberOfDigitsInFractionalPart);
            ++this.numberOfDigitsInFractionalPart;
        }
    }

    private void EmptyBuffer()
    {
        this.buffer = 0;
        this.numberOfDigitsInFractionalPart = 0;
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
}
