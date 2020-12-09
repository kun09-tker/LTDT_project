using System;
using System.Collections.Generic;
using System.IO;

// This class represents a directed graph
// using adjacency list representation
class Graph
{
    public int V; // No. of vertices

    // Array of lists for
    // Adjacency List Representation
    public List<int>[] adj;

    // Constructor
    Graph(int v)
    {
        V = v;
        adj = new List<int>[v];
        for (int i = 0; i < v; ++i)
            adj[i] = new List<int>();
    }

    // Function to Add an edge into the graph
    void AddEdge(int v, int w)
    {
        adj[v].Add(w); // Add w to v's list.
    }

    // A function used by DFS
    void DFSUtil(int v, bool[] visited)
    {
        // Mark the current node as visited
        // and print it
        visited[v] = true;
        Console.Write(v + " ");

        // Recur for all the vertices
        // adjacent to this vertex
        List<int> vList = adj[v];
        foreach (var n in vList)
        {
            if (!visited[n])
                DFSUtil(n, visited);
        }
    }

    // The function to do DFS traversal.
    // It uses recursive DFSUtil()
    void DFS(int v)
    {
        // Mark all the vertices as not visited
        // (set as false by default in c#)
        bool[] visited = new bool[V];

        // Call the recursive helper function
        // to print DFS traversal
        DFSUtil(v, visited);
    }

    // Driver Code
    public static void Main(String[] args)
    {
        string[] temp = File.ReadAllLines("D:/LTDT_project/DFS.txt");
        Graph g = new Graph(int.Parse(temp[0]));

        int dong = 0;
        int[,] MaTran = new int[100, 100];
        for (int i = 1; i <= g.V; i++)
        {
            int cot = 0;
            string[] tempTwo = temp[i].Split(' ');

            foreach (string value in tempTwo)
            {
                MaTran[dong, cot] = int.Parse(value);
                cot++;
            }
            dong++;
        }
        for (int i = 0; i < g.V; i++)
        {
            for (int j = 0; j < g.V; j++)
            {
                if (MaTran[i,j]!=0)
                {
                    g.AddEdge(i, j);
                }
            }
        }
        for(int i = 0; i < g.V; i++)
        {
            Console.Write("Di tu dinh " + i + " : ");
            g.DFS(i);
            Console.WriteLine();
        }
        //g.DFS(5);

        /* 
        Console.WriteLine(
            "Following is Depth First Traversal "
            + "(starting from vertex 2)");

        g.DFS(2);*/
        Console.ReadKey();
    }
}