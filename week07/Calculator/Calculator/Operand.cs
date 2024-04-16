namespace Calculator;

using Operations;
using System.ComponentModel;

public class Operand : INotifyPropertyChanged
{
    public const string Default = "0";

    public string representation;
    public float value;

    public event PropertyChangedEventHandler? PropertyChanged;

    public Operand()
    {
        this.representation = Operand.Default;
    }

    public string Representation
    {
        get
        {
            return this.representation;
        }

        private set
        {
            this.representation = value;
            this.NotifyPropertyChanged(nameof(this.Representation));
        }
    }

    public float Value
    {
        get
        {
            return this.value;
        }

        private set
        {
            this.value = value;
            this.NotifyPropertyChanged(nameof(this.Value));
        }
    }

    public void SetToDefault()
    {
        this.SetByRepresentation(Operand.Default);
    }

    public void SetByValue(float value)
    {
        this.Value = value;
        this.Representation = $"{value}";
    }

    public void AddDigit(char digit)
    {
        if (!char.IsDigit(digit))
        {
            throw new ArgumentException("Argument was not a digit");
        }

        if (this.Value == 0 && this.Representation[^1] != ',')
        {
            bool isNegative = this.Representation[0] == '-';
            this.SetByRepresentation($"{(isNegative ? "-" : "")}{digit}");
        }
        else
        {
            this.SetByRepresentation($"{this.Representation}{digit}");
        }
    }

    public void DeleteLastDigit()
    {
        if (this.Value == 0 || this.Representation.Length == 1)
        {
            this.SetToDefault();
        }
        else if (this.Value < 0 && this.Representation.Length == 2)
        {
            this.SetByRepresentation($"-{Operand.Default}");
        }
        else
        {
            this.SetByRepresentation(this.Representation[0..^1]);
        }
    }

    public void ToNegative()
    {
        this.SetByValue(-this.Value);
    }

    public void ToFloat()
    {
        if (this.Representation[^1] != ',')
        {
            this.SetByRepresentation($"{this.Representation},");
        }
    }

    public void InPercents()
    {
        this.ApplyUnaryOperation(Operations.InPercents);
    }

    public void Square()
    {
        this.ApplyUnaryOperation(Operations.Square);
    }

    public void SquareRoot()
    {
        this.ApplyUnaryOperation(Operations.SquareRoot);
    }

    public void Inverse()
    {
        this.ApplyUnaryOperation(Operations.Inverse);
    }

    private void SetByRepresentation(string representation)
    {
        if (float.TryParse(representation, out float value))
        {
            this.Representation = representation;
            this.Value = value;
        }
    }

    private void ApplyUnaryOperation(Operation operation)
    {
        this.Representation = operation.GetExpression(this.Representation, string.Empty);
        this.Value = operation.GetResult(this.Value, 0);
    }

    private void NotifyPropertyChanged(string propertyName)
    {
        this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
