namespace Operations;

public class Operation
{
    private Func<string, string, string> expressionFunction;
    private Func<float, float, float> resultFunction;

    public Operation(
        Func<string, string, string> expressionFunction,
        Func<float, float, float> resultFunction)
    {
        this.expressionFunction = expressionFunction;
        this.resultFunction = resultFunction;
    }

    public string GetExpression(string x, string y)
    {
        return this.expressionFunction(x, y);
    }

    public float GetResult(float x, float y)
    {
        return this.resultFunction(x, y);
    }
}
