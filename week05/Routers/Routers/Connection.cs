// Copyright (c) 2024
//
// Use of this source code is governed by an MIT license
// that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

namespace Routers;

/// <summary>
/// Channel between two routers in the network.
/// </summary>
public class Connection
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Connection"/> class.
    /// </summary>
    /// <param name="capacity">Capacity of the channel.</param>
    /// <param name="routers">Numbers of connected routers.</param>
    public Connection(int capacity, (int, int) routers)
    {
        if (capacity < 0)
        {
            throw new ArgumentException("Capacity must be non-negative");
        }

        if (routers.Item1 <= 0 || routers.Item2 <= 0)
        {
            throw new ArgumentException("Invalid router number");
        }

        this.Capacity = capacity;
        this.Routers = routers;
    }

    /// <summary>
    /// Gets the capacity of the channel.
    /// </summary>
    public int Capacity { get; }

    /// <summary>
    /// Gets numbers of routers connected by this channel.
    /// </summary>
    public (int, int) Routers { get; }

    /// <summary>
    /// Check if this Connection is equal to given set of numbers.
    /// </summary>
    /// <param name="element">Set of integers to compare to connection properties.</param>
    /// <returns>Value indicating that this Connection is equal to given element.</returns>
    public bool IsEqualTo((int, (int, int)) element)
    {
        return this.Capacity == element.Item1 && this.Routers == element.Item2;
    }
}
