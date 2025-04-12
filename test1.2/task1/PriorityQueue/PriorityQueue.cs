// Copyright (c) Alexander Bugaev 2024
//
// Use of this source code is governed by an MIT license
// that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

namespace PriorityQueue;

/// <summary>
/// Data structure that supports adding element with specific values and int priorities and
/// getting the element with the highest priority.
/// </summary>
/// <typeparam name="T">Type of values contained in the queue.</typeparam>
public class PriorityQueue<T>
{
    private Element? head;

    /// <summary>
    /// Initializes a new instance of the <see cref="PriorityQueue{T}"/> class.
    /// </summary>
    public PriorityQueue()
    {
    }

    /// <summary>
    /// Gets a value indicating whether the queue is empty.
    /// </summary>
    public bool IsEmpty { get => this.head is null; }

    /// <summary>
    /// Add an element with set value and priority to the queue.
    /// </summary>
    /// <param name="value">Value of new element.</param>
    /// <param name="priority">Priority of new element.</param>
    public void Enqueue(T? value, int priority)
    {
        Element? previous = null;
        Element? current = this.head;
        for (; current != null && current.Priority >= priority; current = current.Next)
        {
            previous = current;
        }

        if (previous is null)
        {
            this.head = new Element(value, priority, current);
        }
        else
        {
            previous.Next = new Element(value, priority, current);
        }
    }

    /// <summary>
    /// Get value of element with the highest priority and remove element from the queue.
    /// In the case of equal priorities the first added element is returned.
    /// </summary>
    /// <returns>The value of removed element.</returns>
    /// <exception cref="InvalidOperationException">Queue was empty.</exception>
    public T? Dequeue()
    {
        if (this.head is null)
        {
            throw new InvalidOperationException("Queue was empty");
        }

        T? value = this.head.Value;
        this.head = this.head.Next;
        return value;
    }

    private class Element
    {
        public Element(T? value, int priority, Element? next)
        {
            this.Value = value;
            this.Priority = priority;
            this.Next = next;
        }

        public T? Value { get; set; }

        public int Priority { get; set; }

        public Element? Next { get; set; }
    }
}
