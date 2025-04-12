// Copyright (c) Alexander Bugaev 2024
//
// Use of this source code is governed by an MIT license
// that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

namespace Utility;

/// <summary>
/// Some useful list methods.
/// </summary>
public static class Utility
{
    /// <summary>
    /// Apply the given method to each element of the collection.
    /// </summary>
    /// <typeparam name="T">Type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to edit.</param>
    /// <param name="method">The method to apply to each element.</param>
    public static void Map<T>(IList<T?> collection, Func<T?, T?> method)
    {
        for (int i = 0; i < collection.Count; ++i)
        {
            collection[i] = method(collection[i]);
        }
    }

    /// <summary>
    /// Get list of elements from given collection for which given method returns true.
    /// </summary>
    /// <typeparam name="T">Type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to which the method is applied.</param>
    /// <param name="method">The method that gets element of collection and returns bool value.</param>
    /// <returns>List of elements for which the method returns true.</returns>
    public static List<T?> Filter<T>(IList<T?> collection, Func<T?, bool> method)
    {
        var result = new List<T?>();
        for (int i = 0; i < collection.Count; ++i)
        {
            if (method(collection[i]))
            {
                result.Add(collection[i]);
            }
        }

        return result;
    }

    /// <summary>
    /// Get cumulative value of the collection based on the given method.
    /// </summary>
    /// <typeparam name="T">Type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to which the method is applied.</param>
    /// <param name="method">The method that takes current cumulative value,
    /// the next element in the collection and returns the next cumulative value.</param>
    /// <returns>Cumulative value of the collection.</returns>
    public static T? Fold<T>(IList<T?> collection, Func<T?, T?, T?> method)
    {
        T? accelerator = default;
        foreach (T? element in collection)
        {
            accelerator = method(accelerator, element);
        }

        return accelerator;
    }
}
