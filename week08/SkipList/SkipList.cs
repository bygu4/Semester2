// <copyright file="SkipList.cs" company="SPBU">
// Copyright (c) Alexander Bugaev 2024. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SkipList;

using System.Collections;

/// <summary>
/// Implementation of Skip List data structure.
/// Elements in this collection are contained in the ascending order.
/// </summary>
/// <typeparam name="T">Type of contained elements.</typeparam>
public class SkipList<T> : IList<T>
    where T : IComparable<T>
{
    private Element topLevelHead;
    private Element bottomLevelHead;

    private Random random;
    private bool invalidateEnumerator;

    /// <summary>
    /// Initializes a new instance of the <see cref="SkipList{T}"/> class.
    /// </summary>
    public SkipList()
    {
        this.topLevelHead = new Element(default, null, null);
        this.bottomLevelHead = this.topLevelHead;
        this.random = new Random(DateTime.Now.Millisecond);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SkipList{T}"/> class
    /// with elements of given array.
    /// </summary>
    /// <param name="items">Elements to add.</param>
    public SkipList(T[] items)
        : this()
    {
        foreach (var item in items)
        {
            this.Add(item);
        }
    }

    /// <summary>
    /// Gets number of elements contained in the collection.
    /// </summary>
    public int Count { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the collection is read-only.
    /// </summary>
    public bool IsReadOnly { get => false; }

    /// <summary>
    /// Gets or sets element on specific position in the collection.
    /// </summary>
    /// <param name="index">Zero-based index of the element in the collection.</param>
    /// <returns>Element on given position in the collection.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Index is out of collection bounds.</exception>
    /// <exception cref="NotSupportedException">Collection doesn't support setting element by index.</exception>
    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= this.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            int i = 0;
            foreach (var item in this)
            {
                if (i == index)
                {
                    return item;
                }

                ++i;
            }

            throw new ArgumentOutOfRangeException(nameof(index));
        }

        set => throw new NotSupportedException();
    }

    /// <summary>
    /// Add element to the collection.
    /// </summary>
    /// <param name="item">Element to add.</param>
    public void Add(T item)
    {
        var newLevelElement = this.AddRecursion(this.topLevelHead, item);
        if (newLevelElement != null)
        {
            this.CreateNewLevel(newLevelElement);
        }

        ++this.Count;
        this.invalidateEnumerator = true;
    }

    /// <summary>
    /// Add element to specific position of the collection.
    /// </summary>
    /// <param name="index">Zero-based index to insert element to.</param>
    /// <param name="item">Element to insert.</param>
    /// <exception cref="NotSupportedException">Operation is not supported.</exception>
    public void Insert(int index, T item) => throw new NotSupportedException();

    /// <summary>
    /// Remove given element from the collection.
    /// </summary>
    /// <param name="item">Element to remove.</param>
    /// <returns>A value indicating whether the element was removed.</returns>
    public bool Remove(T item)
    {
        var elementRemoved = this.RemoveRecursion(this.topLevelHead, item);
        this.RemoveEmptyLevels();

        if (elementRemoved)
        {
            --this.Count;
            this.invalidateEnumerator = true;
        }

        return elementRemoved;
    }

    /// <summary>
    /// Remove element from specific position of the collection.
    /// </summary>
    /// <param name="index">Zero-based index to remove element from.</param>
    public void RemoveAt(int index) => this.Remove(this[index]);

    /// <summary>
    /// Remove all elements from the collection.
    /// </summary>
    public void Clear()
    {
        while (this.Count > 0)
        {
            this.RemoveAt(0);
        }
    }

    /// <summary>
    /// Check if given element is present in the collection.
    /// </summary>
    /// <param name="item">Element to find.</param>
    /// <returns>A value indicating whether the element was found in the collection.</returns>
    public bool Contains(T item) => this.FindElement(this.topLevelHead, item) != null;

    /// <summary>
    /// Get position of first occurrence of given element in the collection.
    /// </summary>
    /// <param name="item">Element to find.</param>
    /// <returns>Zero-based index of first occurrence of the element in the collection.
    /// If element was not found, -1 is returned.</returns>
    public int IndexOf(T item)
    {
        int i = 0;
        foreach (var currentItem in this)
        {
            var comparison = currentItem.CompareTo(item);
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

    /// <summary>
    /// Copy elements of the collection to given array starting from specific position.
    /// </summary>
    /// <param name="destination">Array to copy elements to.</param>
    /// <param name="startIndex">Zero-based index of array to start copying from.</param>
    /// <exception cref="ArgumentOutOfRangeException">Given index was negative.</exception>
    /// <exception cref="ArgumentException">Destination array doesn't have enough space
    /// to copy to starting from given index to the end of the array.</exception>
    public void CopyTo(T[] destination, int startIndex)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(startIndex);

        if (destination.Length < startIndex + this.Count)
        {
            throw new ArgumentException(
                $"Destination array doesn't have enough space starting from {nameof(startIndex)}");
        }

        foreach (var item in this)
        {
            destination[startIndex++] = item;
        }
    }

    /// <summary>
    /// Get an instance of the collection Enumerator.
    /// </summary>
    /// <returns>An instance of class implementing IEnumerable.</returns>
    public IEnumerator<T> GetEnumerator() => new Enumerator(this);

    /// <summary>
    /// Get an instance of the collection Enumerator.
    /// </summary>
    /// <returns>An instance of class implementing IEnumerable.</returns>
    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

    private bool CoinFlip() => this.random.Next() % 2 == 0;

    private void CreateNewLevel(Element elementToAdd)
        => this.topLevelHead = new (default, elementToAdd, this.topLevelHead);

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

    private Element? FindElement(Element? current, T value)
    {
        if (current == null)
        {
            return null;
        }

        if (current.Value != null && current.Value.CompareTo(value) == 0)
        {
            return current;
        }

        if (current.Next != null && current.Next.Value != null &&
            current.Next.Value.CompareTo(value) <= 0)
        {
            return this.FindElement(current.Next, value);
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

            if (this.current == null)
            {
                throw new NullReferenceException();
            }

            this.current = this.current.Next;
            return this.current != null;
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
