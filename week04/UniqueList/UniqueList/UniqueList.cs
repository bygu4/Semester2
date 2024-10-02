// Copyright (c) 2024
//
// Use of this source code is governed by an MIT license
// that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

namespace UniqueList;

using List;
using Exceptions;

/// <summary>
/// A List without repeating elements.
/// </summary>
public class UniqueList : List
{
    /// <summary>
    /// Add an element to the end of the List.
    /// </summary>
    /// <param name="value">Value of the element to add.</param>
    public override void Add(int value)
    {
        this.CheckThatElementIsNotInList(value, -1);
        base.Add(value);
    }

    /// <summary>
    /// Set value of the element on given position in the List to the specified value.
    /// </summary>
    /// <param name="value">Value to set for the element.</param>
    /// <param name="index">Index of the element in the List.</param>
    public override void SetValue(int value, int index)
    {
        this.CheckThatElementIsNotInList(value, index);
        base.SetValue(value, index);
    }

    private void CheckThatElementIsNotInList(int value, int index)
    {
        int currentIndex = 0;
        for (Vertex? current = this.Tail; current is not null; current = current.Next)
        {
            if (current.Value == value && currentIndex != index)
            {
                throw new ElementIsAlreadyInListException();
            }

            ++currentIndex;
        }
    }
}
