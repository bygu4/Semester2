namespace Operations;

public static class Operations
{
    public enum Signs
    {
        Addition = '+',
        Substraction = '-',
        Multiplication = '×',
        Division = '÷',
    }

    public static Operation Addition
    {
        get => new Operation(
            (x, y) => $"{x} {(char)Signs.Addition} {y} =",
            (x, y) => x + y);
    }

    public static Operation Substraction
    {
        get => new Operation(
            (x, y) => $"{x} {(char)Signs.Substraction} {y} =",
            (x, y) => x - y);
    }

    public static Operation Multiplication
    {
        get => new Operation(
            (x, y) => $"{x} {(char)Signs.Multiplication} {y} =",
            (x, y) => x * y);
    }

    public static Operation Division
    {
        get => new Operation(
            (x, y) => $"{x} {(char)Signs.Division} {y} =",
            (x, y) => x / y);
    }

    public static Operation InPercents
    {
        get => new Operation(
            (x, y) => $"{x}%",
            (x, y) => x / 100);
    }

    public static Operation Square
    {
        get => new Operation(
            (x, y) => $"sqr({x})",
            (x, y) => (float)Math.Pow(x, 2));
    }

    public static Operation SquareRoot
    {
        get => new Operation(
            (x, y) => $"sqrt({x})",
            (x, y) => (float)Math.Sqrt(x));
    }

    public static Operation Inverse
    {
        get => new Operation(
            (x, y) => $"1/{x}",
            (x, y) => 1f / x);
    }

    public static Operation GetOperationBySign(char sign)
    {
        switch (sign)
        {
            case (char)Operations.Signs.Addition:
                return Operations.Addition;
            case (char)Operations.Signs.Substraction:
                return Operations.Substraction;
            case (char)Operations.Signs.Multiplication:
                return Operations.Multiplication;
            case (char)Operations.Signs.Division:
                return Operations.Division;
            default:
                throw new ArgumentException("Unknown operation");
        }
    }
}
