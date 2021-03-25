using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Vertex : MonoBehaviour
{
    public int id; //identificador

    public List<Edge> neighbours; //vecinos

    private Vertex prev; //nodo anterior

    public Vertex getVertex() { return prev; }
    public void setVertex(Vertex v) { prev = v; }

}
