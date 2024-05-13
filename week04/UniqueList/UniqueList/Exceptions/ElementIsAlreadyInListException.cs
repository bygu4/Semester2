// <copyright file="ElementIsAlreadyInListException.cs" company="SPBU">
// Copyright (c) Alexander Bugaev 2024. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

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
