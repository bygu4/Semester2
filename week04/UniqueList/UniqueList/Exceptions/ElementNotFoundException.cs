// <copyright file="ElementNotFoundException.cs" company="SPBU">
// Copyright (c) Alexander Bugaev 2024. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

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
