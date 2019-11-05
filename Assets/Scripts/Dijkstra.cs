using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dijkstra : MonoBehaviour {

    private Object nearest_node;
    static int V;
    
    // Use this for initialization
	void Start ()
    {
        V = 9;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
		
	}

    private int MinDistance(int[] distance, bool[]sptSet)
    {
        int min = int.MaxValue, min_index = -1;

        for (int v = 0; v < V; v++)
        {
            if (sptSet[v] == false && distance[v] <= min)
            {
                min = distance[v];
                min_index = v;
            }
        }

        return min_index;
    }

    void dijkstra (int[,] graph, int source)
    {
        int[] distance = new int[V];
        bool[] sptSet = new bool[V];

        for (int i = 0; i < V; i++)
        {
            distance[i] = int.MaxValue;
            sptSet[i] = false;
        }

        distance[source] = 0;

        for (int count = 0; count < V-1; count++)
        {
            int u = MinDistance(distance, sptSet);
            sptSet[u] = true;

            for (int v = 0; v < V; v++)
            {
                if (!sptSet[v] && graph[u,v] != 0 && 
                    distance[u] != int.MaxValue && 
                    distance[u] + graph[u, v] < distance[v])
                {
                    distance[v] = distance[u] + graph[u, v];
                }
            }
        }
    }
}
