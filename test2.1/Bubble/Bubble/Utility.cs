// Copyright (c) 2024
//
// Use of this source code is governed by an MIT license
// that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

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
