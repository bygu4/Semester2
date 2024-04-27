﻿namespace Vector;

using Operations;

public class Vector
{
    private List<Element> elements;
    private int length;

    public Vector(IList<int> input)
    {
        this.elements = new List<Element>();
        for (int i = 0; i < input.Count; ++i)
        {
            if (input[i] != 0)
            {
                this.elements.Add(new Element(input[i], i));
            }
        }

        this.length = input.Count;
    }

    public bool IsNull {
        get
        {
            return this.elements.Count == 0;
        }
    }

    public Vector Add(Vector vector)
    {
        return this.GetVectorArraysAndApplyOperation(this, vector, Operations.Addition);
    }

    public Vector Substract(Vector vector)
    {
        return this.GetVectorArraysAndApplyOperation(this, vector, Operations.Substraction);
    }

    public int GetScalarProduct(Vector vector)
    {
        if (this.length != vector.length)
        {
            throw new InvalidOperationException("Lengths of vectors do not match");
        }

        int[] array1 = this.ToArray();
        int[] array2 = vector.ToArray();

        int product = 0;
        for (int i = 0; i < array1.Length; ++i)
        {
            product += array1[i] * array2[i];
        }

        return product;
    }

    public int[] ToArray()
    {
        int[] array = new int[this.length];
        int index = 0;
        for (int i = 0 ; i < this.elements.Count; ++i)
        {
            var element = this.elements[i];
            while (index < element.Index)
            {
                array[index] = 0;
                ++index;
            }

            array[index] = element.Value;
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
            default:
                throw new ArgumentException("Unknown operation");
        }
    }

    private int[] ApplyOperation(int[] array1, int[] array2, Operations operation)
    {
        int[] result = new int[array1.Length];
        for (int i = 0; i < array1.Length; ++i)
        {
            result[i] = this.GetResultOfOperation(array1[i], array2[i], operation);
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
        return new Vector(this.ApplyOperation(array1, array2, operation));
    }

    private class Element
    {
        public int Value { get; }

        public int Index { get; }

        public Element(int value, int index)
        {
            this.Value = value;
            this.Index = index;
        }
    }
}
