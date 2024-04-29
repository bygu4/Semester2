namespace SkipList;

using System.Collections;

public class SkipList<T> : IList<T>
    where T : IComparable<T>
{
    private Element topLevelHead;
    private Element bottomLevelHead;

    private Random random;
    private bool invalidateEnumerator;

    public SkipList()
    {
        this.topLevelHead = new Element(default, null, null);
        this.bottomLevelHead = this.topLevelHead;
        this.random = new Random(DateTime.Now.Millisecond);
    }

    public int Count { get; private set; }

    public bool IsReadOnly { get => false; }


    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= this.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            int i = 0;
            foreach (var value in this)
            {
                if (i == index)
                {
                    return value;
                }

                ++i;
            }

            throw new NullReferenceException();
        }

        set => throw new NotSupportedException();
    }

    public void Add(T value)
    {
        var newLevelElement = this.AddRecursion(this.topLevelHead, value);
        if (newLevelElement != null)
        {
            this.CreateNewLevel(newLevelElement);
        }

        ++this.Count;
        this.invalidateEnumerator = true;
    }

    public void Insert(int index, T value) => throw new NotSupportedException();

    public bool Remove(T value)
    {
        var elementRemoved = this.RemoveRecursion(this.topLevelHead, value);
        this.RemoveEmptyLevels();

        if (elementRemoved)
        {
            --this.Count;
            this.invalidateEnumerator = true;
        }

        return elementRemoved;
    }

    public void RemoveAt(int index) => this.Remove(this[index]);

    public void Clear()
    {
        while (this.Count > 0)
        {
            this.RemoveAt(0);
        }
    }

    public bool Contains(T value) => this.FindElement(this.topLevelHead, value) != null;

    public int IndexOf(T value)
    {
        int i = 0;
        foreach (var currentValue in this)
        {
            var comparison = currentValue.CompareTo(value);
            if (comparison == 0)
            {
                return i;
            }
            else if (comparison > 0)
            {
                break;
            }

            ++i;
        }

        return -1;
    }

    public void CopyTo(T[] array, int startIndex)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(startIndex);

        if (array.Length < startIndex + this.Count)
        {
            throw new ArgumentException(
                $"Destination array doesn't have enough space starting from {nameof(startIndex)}");
        }

        foreach (var value in this)
        {
            array[startIndex++] = value;
        }
    }

    public IEnumerator<T> GetEnumerator() => new Enumerator(this);
    
    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

    private bool CoinFlip() => this.random.Next() % 2 == 0;

    private void CreateNewLevel(Element elementToAdd)
        => this.topLevelHead = new(default, elementToAdd, this.topLevelHead);

    private void RemoveEmptyLevels()
    {
        while (this.topLevelHead.Next != null && this.topLevelHead.Down != null)
        {
            this.topLevelHead = this.topLevelHead.Down;
        }
    }
    
    private Element? AddRecursion(Element current, T value)
    {
        while (current.Next != null && current.Next.Value != null &&
            current.Next.Value.CompareTo(value) < 0)
        {
            current = current.Next;
        }

        var downElement = (current.Down != null) ?
            this.AddRecursion(current.Down, value) : null;
        
        if (downElement != null || current.Down == null)
        {
            current.Next = new Element(value, current.Next, downElement);
            return this.CoinFlip() ? current.Next : null;
        }

        return null;
    }

    private bool RemoveRecursion(Element current, T value)
    {
        while (current.Next != null && current.Next.Value != null &&
            current.Next.Value.CompareTo(value) < 0)
        {
            current = current.Next;
        }

        bool removed = false;

        if (current.Down != null)
        {
            removed = this.RemoveRecursion(current.Down, value);
        }

        if (current.Next != null && current.Next.Value != null &&
            current.Next.Value.CompareTo(value) == 0)
        {
            current.Next = current.Next.Next;
            removed = true;
        }

        return removed;
    }

    private Element? FindElement(Element current, T value)
    {
        while (current.Next != null && current.Next.Value != null &&
            current.Next.Value.CompareTo(value) < 0)
        {
            current = current.Next;
        }

        if (current.Down == null)
        {
            return current;
        }

        return this.FindElement(current.Down, value);
    }

    private class Enumerator : IEnumerator<T>
    {
        private SkipList<T> collection;
        private Element? current;

        public Enumerator(SkipList<T> collection)
        {
            this.collection = collection;
            this.collection.invalidateEnumerator = false;
            this.current = this.collection.bottomLevelHead;
        }

        public T Current
        {
            get
            {
                if (this.current == null || this.current.Value == null)
                {
                    throw new NullReferenceException();
                }

                return this.current.Value;
            }
        }

        object IEnumerator.Current => (object)this.Current;

        public bool MoveNext()
        {
            this.CheckIteratorValidity();

            if (this.current != null)
            {
                this.current = this.current.Next;
                return true;
            }

            return false;
        }

        public void Reset()
        {
            this.CheckIteratorValidity();
            this.current = this.collection.bottomLevelHead;
        }

        public void Dispose() => GC.SuppressFinalize(this);

        private void CheckIteratorValidity()
        {
            if (this.collection.invalidateEnumerator)
            {
                throw new InvalidOperationException("Collection was changed during iteration");
            }
        }
    }

    private class Element(T? value, Element? next, Element? down)
    {
        public T? Value { get; set; } = value;

        public Element? Next { get; set; } = next;

        public Element? Down { get; set; } = down;
    }
}
