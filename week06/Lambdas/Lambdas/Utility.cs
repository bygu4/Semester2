// <copyright file="Utility.cs" company="SPBU">
// Copyright (c) Alexander Bugaev 2024. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

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
    /// Get an array of bool values which are the outputs
    /// of given method applied to each element of the collection.
    /// </summary>
    /// <typeparam name="T">Type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to apply method to.</param>
    /// <param name="method">The method to apply to each element.</param>
    /// <returns>The array of method outputs.</returns>
    public static bool[] Filter<T>(IList<T?> collection, Func<T?, bool> method)
    {
        bool[] result = new bool[collection.Count];
        for (int i = 0; i < collection.Count; ++i)
        {
            result[i] = method(collection[i]);
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
