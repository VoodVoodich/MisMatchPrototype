using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph
{
    HashSet<string> nodes = new HashSet<string>();

    Dictionary<string, HashSet<string>> edges = new Dictionary<string, HashSet<string>>();

    public void AddNode(string node)
    {
        if (!nodes.Contains(node))
            nodes.Add(node);
    }

    public void AddEdge(string node1, string node2)
    {
        if (edges.ContainsKey(node1) && !edges[node1].Contains(node2))
            edges[node1].Add(node2);
        else if (!edges.ContainsKey(node1))
            edges[node1] = new HashSet<string> { node2 };
    }
}


