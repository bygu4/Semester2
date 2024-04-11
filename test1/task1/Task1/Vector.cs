namespace Vector;

using Operations;

public class Vector
{
    private List<(int, int)> elements;
    private int length;

    public Vector(IList<int> input)
    {
        this.elements = new List<(int, int)>();
        int i = 0;
        for (; i < input.Count; ++i)
        {
            if (input[i] != 0)
            {
                this.elements.Add((input[i], i));
            }
        }

        this.length = i;
    }

    public bool IsNull {
        get
        {
            return this.elements.Count == 0;
        }
    }

    public Vector Add(Vector vector)
    {
        return GetVectorArraysAndApplyOperation(this, vector, Operations.Addition);
    }

    public Vector Substract(Vector vector)
    {
        return GetVectorArraysAndApplyOperation(this, vector, Operations.Substraction);
    }

    public int GetScalarProduct(Vector vector)
    {
        if (this.length != vector.length)
        {
            throw new InvalidOperationException("Lengths of vectors do not match");
        }

        int[] array1 = this.ToArray();
        int[] array2 = vector.ToArray();
        return GetScalarProduct(array1, array2);
    }

    public int[] ToArray()
    {
        int[] array = new int[this.length];
        int index = 0;
        for (int i = 0 ; i < this.elements.Count; ++i)
        {
            var element = this.elements[i];
            while (index < element.Item2)
            {
                array[index] = 0;
                ++index;
            }

            array[index] = element.Item1;
            ++index;
        }

        return array;
    }

    private int GetResultOfOperation(int value1, int value2, Operations operation)
    {
        switch (operation)
        {
            case Operations.Addition:
                return value1 + value2;
            case Operations.Substraction:
                return value1 - value2;
            case Operations.Multiplication:
                return value1 * value2;
        }

        return 0;
    }

    private int[] ApplyOperation(int[] array1, int[] array2, Operations operation)
    {
        int[] result = new int[array2.Length];
        for (int i = 0; i < array1.Length; ++i)
        {
            result[i] = GetResultOfOperation(array1[i], array2[i], operation);
        }

        return result;
    }

    private Vector GetVectorArraysAndApplyOperation(
        Vector vector1, Vector vector2, Operations operation)
    {
        if (vector1.length != vector2.length)
        {
            throw new InvalidOperationException("Lengths of vectors do not match");
        }

        int[] array1 = vector1.ToArray();
        int[] array2 = vector2.ToArray();
        return new Vector(ApplyOperation(array1, array2, operation));
    }

    private int GetScalarProduct(int[] array1, int[] array2)
    {
        int product = 0;
        for (int i = 0; i < array1.Length; ++i)
        {
            product += array1[i] * array2[i];
        }

        return product;
    }
}
