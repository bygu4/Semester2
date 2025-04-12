// Copyright (c) Alexander Bugaev 2024
//
// Use of this source code is governed by an MIT license
// that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

namespace Exceptions;

/// <summary>
/// Exception indicating that an element is not present in the List.
/// </summary>
public class ElementNotFoundException : ArgumentException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ElementNotFoundException"/> class.
    /// </summary>
    public ElementNotFoundException()
    {
    }
}
