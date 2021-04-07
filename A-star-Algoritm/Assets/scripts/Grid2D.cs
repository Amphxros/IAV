using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Contiene el mapa de nodos y utilidades para busqueda
[System.Serializable]
public class Grid2D: MonoBehaviour
{
    public int width, height;       // Dimensiones del mapa
    public float yHeight;
    private GameObject cas_, obs_;
    public Node[] nodes;

    private Grid2D()
    {
    }

    public Grid2D(int w, int h, float y, GameObject cas, GameObject obs)
    {
        width = w;
        height = h;
        yHeight = y;
        cas_ = cas;
        obs_ = obs;
    }

    public void CreateGrid()
    {
        nodes = new Node[width * height];

        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                int ind = i * height + j;
                Vector3 p = new Vector3(i, yHeight, j);
                var node = new Node(p,cas_,obs_);
                nodes[ind] = node;
            }
        }

    }

    // Devuelve los nodos en las 4 direcciones
    public Node[] GetNextNeighbours(Node n) {
        var result = new Node[4];

        int x= (int)n.position.x;
        int y= (int)n.position.z;

        int[] indices = new int[]
       {
            x * height + (y + 1),
            (x + 1) * height + y,
            x * height + (y - 1),
            (x - 1) * height + y
       };

        // North
        if (y < height - 1)
            result[0] = nodes[indices[0]];

        // East
        if (x < width - 1)
            result[1] = nodes[indices[1]];

        // South
        if (y > 0)
            result[2] = nodes[indices[2]];

        // West
        if (x > 0)
            result[3] = nodes[indices[3]];


        return result;
    }

    // Entrada la posición de un nodo devuelve el objeto nodo
    public Node FindNodeByPosition(Vector3 pos)
    {
        int x = (int)pos.x;
        int y = (int)pos.z;

        if (x >= 0 && x < width && y >= 0 && y < height)
        {
            int index = x * height + y;
            var cell = nodes[index];
            return cell;
        }
        else
            throw new System.Exception(string.Format("No hay nodo en  {0}", pos));
    }
   
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        foreach (Node nd in nodes)
        {
            UnityEngine.Vector3 targetPos = nd.position;
            Gizmos.DrawSphere(targetPos, 0.5f);
        }
    }
}
