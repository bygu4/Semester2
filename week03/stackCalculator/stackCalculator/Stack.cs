namespace Stack
{
    public interface IStack<Type>
    {
        void Push(Type? value);

        Type? Pop();
    }

    public class StackOnPointers<Type> : IStack<Type>
    {
        private Element? head;

        private class Element
        {
            public Type? value;
            public Element? next;

            public Element(Type? value, Element? next)
            {
                this.value = value;
                this.next = next;
            }
        }

        public StackOnPointers()
        {
        }

        public void Push(Type? value)
        {
            Element newHead = new Element(value, this.head);
            this.head = newHead;
        }

        public Type? Pop()
        {
            if (this.head is null)
            {
                throw new InvalidOperationException(
                    "Attempt to pop out of an empty stack");
            }
            Type? output = this.head.value;
            this.head = this.head.next;
            return output;
        }
    }

    public class StackOnArray<Type> : IStack<Type>
    {
        private Type?[] values;
        private int count;
        private int arraySize = 1;

        public StackOnArray()
        {
            this.values = new Type?[arraySize];
        }

        private void Resize(int size)
        {
            Type?[] newArray = new Type?[size];
            for (int i = 0; i < this.count; ++i)
            {
                newArray[i] = this.values[i];
            }
            this.values = newArray;
            this.arraySize = size;
        }

        public void Push(Type? value)
        {
            if (count == arraySize)
            {
                this.Resize(arraySize * 2);
            }
            this.values[count] = value;
            ++count;
        }

        public Type? Pop()
        {
            if (count == 0)
            {
                throw new InvalidOperationException(
                    "Attempt to pop out of an empty stack");
            }
            --count;
            Type? value = this.values[count];
            this.values[count] = default(Type);
            if (count < arraySize / 2)
            {
                Resize(arraySize / 2);
            }
            return value;
        }
    }
}
