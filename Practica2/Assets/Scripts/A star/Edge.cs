using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Edge: MonoBehaviour
{
    public float cost;
    public Vertex vertex;

    public Edge()
    {
    }
    public Edge(Vertex src=null, float coste=1f)
    {
        vertex = src;
        cost = coste;
    }

    public int CompareTo(Edge other)
    {
        float result = cost - other.cost;
        int idA = vertex.id;
        int idB = other.vertex.id;
        if (idA == idB)
            return 0;
        return (int)result;
    }


    public bool Equals(Edge other)
    {
        return (other.vertex.id == this.vertex.id);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
