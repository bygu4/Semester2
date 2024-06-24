// <copyright file="Utility.cs" company="SPBU">
// Copyright (c) Alexander Bugaev 2024. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Utility;

/// <summary>
/// Class containing some useful methods.
/// </summary>
public static class Utility
{
    /// <summary>
    /// Sort given collection according to given comparer instance.
    /// </summary>
    /// <typeparam name="T">Type of elements contained in the collection.</typeparam>
    /// <param name="collection">Collection to sort.</param>
    /// <param name="comparer">IComparer implementation to compare elements of the collection.</param>
    public static void Sort<T>(IList<T> collection, IComparer<T> comparer)
    {
        for (int i = 0; i < collection.Count; ++i)
        {
            for (int j = 1; j < collection.Count; ++j)
            {
                if (comparer.Compare(collection[j - 1], collection[j]) > 0)
                {
                    (collection[j - 1], collection[j]) = (collection[j], collection[j - 1]);
                }
            }
        }
    }
}
