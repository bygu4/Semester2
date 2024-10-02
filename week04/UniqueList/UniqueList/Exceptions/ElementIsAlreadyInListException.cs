// Copyright (c) 2024
//
// Use of this source code is governed by an MIT license
// that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

namespace Exceptions;

/// <summary>
/// Exception indicating that an element is already present at the UniqueList.
/// </summary>
public class ElementIsAlreadyInListException : InvalidOperationException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ElementIsAlreadyInListException"/> class.
    /// </summary>
    public ElementIsAlreadyInListException()
    {
    }
}
