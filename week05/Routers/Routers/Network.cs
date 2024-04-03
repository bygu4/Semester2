// <copyright file="Network.cs" company="SPBU">
// Copyright (c) Alexander Bugaev 2024. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Routers;

/// <summary>
/// Network of connected routers.
/// </summary>
public class Network
{
    private int numberOfRouters;

    /// <summary>
    /// Initializes a new instance of the <see cref="Network"/> class.
    /// Creates a network based on configuration from the file at the given path.
    /// </summary>
    /// <param name="filePath">File to read configuration from.</param>
    public Network(string filePath)
    {
        this.Connections = new List<Connection>();
        string[] lines = this.GetLines(filePath);
        foreach (string line in lines)
        {
            this.AddConnections(line);
        }
    }

    /// <summary>
    /// Gets the list of channels in this network.
    /// </summary>
    public List<Connection> Connections { get; private set; }

    /// <summary>
    /// Write the configuration to the file in format: {router1}: {router2} {capacity}, ...
    /// </summary>
    /// <param name="filePath">Path of the file to write to.</param>
    public void WriteConfiguration(string filePath)
    {
        var adjacentRouters = this.GetAdjacencyDict();
        using StreamWriter writer = new StreamWriter(filePath, false);
        foreach (int router in adjacentRouters.Keys)
        {
            writer.Write($"{router}:");
            for (int i = 0; i < adjacentRouters[router].Count; ++i)
            {
                var adjacentRouter = adjacentRouters[router][i];
                writer.Write($" {adjacentRouter.Item1} ({adjacentRouter.Item2})");
                if (i < adjacentRouters[router].Count - 1)
                {
                    writer.Write(',');
                }
            }

            writer.Write("\n");
        }
    }

    /// <summary>
    /// Create a network configuration of maximum capacity without cycles.
    /// </summary>
    /// <returns>True if the network is connected, otherwise false.</returns>
    public bool Configure()
    {
        int[] adjacencyComponent = Enumerable.Range(0, this.numberOfRouters).ToArray();
        this.Connections.Sort(
            (Connection x, Connection y) => { return (-x.Capacity).CompareTo(-y.Capacity); });

        for (int i = 0; i < this.Connections.Count; ++i)
        {
            var connection = this.Connections[i];
            int component1 = adjacencyComponent[connection.Routers.Item1 - 1];
            int component2 = adjacencyComponent[connection.Routers.Item2 - 1];

            if (component1 == component2)
            {
                this.Connections.Remove(connection);
                --i;
            }
            else
            {
                this.ConnectComponents(adjacencyComponent, component1, component2);
            }
        }

        return this.NetworkIsConnected(adjacencyComponent);
    }

    private string[] GetLines(string filePath)
    {
        string data;
        using (StreamReader reader = File.OpenText(filePath))
        {
            data = reader.ReadToEnd();
        }

        return data.Split('\n', StringSplitOptions.RemoveEmptyEntries);
    }

    private void AddConnections(string line)
    {
        line = line.Replace('(', ' ');
        line = line.Replace(')', ' ');
        string[] elements = line.Split(':', StringSplitOptions.TrimEntries);
        if (elements.Length != 2)
        {
            throw new InvalidDataException("Invalid file format");
        }

        int router1 = int.Parse(elements[0]);
        this.numberOfRouters = int.Max(this.numberOfRouters, router1);

        foreach (string element in elements[1].Split(',', StringSplitOptions.RemoveEmptyEntries))
        {
            string[] split = element.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (split.Length != 2)
            {
                throw new InvalidDataException("Invalid file format");
            }

            int router2 = int.Parse(split[0]);
            int capacity = int.Parse(split[1]);
            this.Connections.Add(new Connection(capacity, (router1, router2)));
            this.numberOfRouters = int.Max(this.numberOfRouters, router2);
        }
    }

    private Dictionary<int, List<(int, int)>> GetAdjacencyDict()
    {
        var adjacentRouters = new Dictionary<int, List<(int, int)>>();
        foreach (Connection connection in this.Connections)
        {
            var routers = connection.Routers;
            int capacity = connection.Capacity;
            if (adjacentRouters.TryGetValue(routers.Item1, out List<(int, int)>? elements))
            {
                elements.Add((routers.Item2, capacity));
            }
            else
            {
                adjacentRouters.Add(
                    routers.Item1, new List<(int, int)>() { (routers.Item2, capacity) });
            }
        }

        return adjacentRouters;
    }

    private void ConnectComponents(
        int[] adjacencyComponent, int component1, int component2)
    {
        for (int i = 0; i < adjacencyComponent.Length; ++i)
        {
            if (adjacencyComponent[i] == component2)
            {
                adjacencyComponent[i] = component1;
            }
        }
    }

    private bool NetworkIsConnected(int[] adjacencyComponent)
    {
        for (int i = 1; i < adjacencyComponent.Length; ++i)
        {
            if (adjacencyComponent[i - 1] != adjacencyComponent[i])
            {
                return false;
            }
        }

        return true;
    }
}
