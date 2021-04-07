using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Implementación práctica del algoritmo
public class Path: MonoBehaviour
{
    private Grid2D grid;            // mapa donde eimplementar el algoritmo
    private List<Node> openList;
    private List<Node> closedList;
    private Vector3 start, goal;    // Posiciones de inicio y meta del agente para calcular la heurística
    GameObject src_, dst_;
    Node start_, final_;
    private Path() { }

    public Path(Grid2D g, GameObject src, GameObject dst)
    {
        this.grid = g;
        this.src_ = src;
        this.dst_ = dst;

        this.start = src.transform.position;
        this.goal = dst.transform.position;

        openList = new List<Node>();
        closedList = new List<Node>();
        Debug.Log("path creado" + start +" "+ goal +" ");
    }

    public void setPlayerPos(Vector3 v)
    {
        start = v;
    }

    public Node[] Astar()
    {
        openList.Clear();
        closedList.Clear();

        Node priNode = src_.GetComponent<ObjectNodeInGrid>().getNode();
        Debug.Log("pri node");
        Debug.Log(priNode != null);
        Node finalNode = dst_.GetComponent<ObjectNodeInGrid>().getNode();
        Debug.Log("final node");
        Debug.Log(finalNode != null);
        if (priNode != null)
        {
            Debug.Log("primer nodo");
            priNode.heuristic = (goal - start).magnitude;   // Distancia en magnitud del agente a la meta
            openList.Add(priNode);

            var node = priNode;
            while (openList.Count > 0)
            {
                var edge = GetBestNode();
                openList.Remove(edge);

                var vecinos = grid.GetNextNeighbours(edge);
                for (int i = 0; i < 4; i++)
                {
                    node = vecinos[i];
                    if (vecinos != null)
                    {
                        if (finalNode == node)
                        {
                            Debug.Log("found da wae");
                            node.parent = edge;
                            return CreatePath(node);
                        }

                    }
                    float g = edge.cost + (node.position - edge.position).magnitude;
                    float h = (goal - node.position).magnitude;

                    if (openList.Contains(node) && node.f < (g + h))
                    {
                        continue;
                    }
                    if (closedList.Contains(node) && node.f < (g + h))
                    {
                        continue;
                    }
                    node.cost = g;
                    node.heuristic = h;
                    node.parent = edge;

                    if (!openList.Contains(node))
                    {
                        openList.Add(node);
                    }

                }
                if (!closedList.Contains(edge))
                {
                    closedList.Add(edge);
                }
                node = edge;
                Debug.Log(node.position.x + " " + node.position.z);
            }

            Debug.Log("path");
            return CreatePath(node); 
        }
        return null;

    }

    private Node GetBestNode()
    {
        Node result = null;
        float curr_F = float.PositiveInfinity;
        for(int i = 0; i < openList.Count; i++)
        {
            var n = openList[i];

            if (n.f < curr_F)
            {
                curr_F = n.f;
                result = n;
            }
        }
        return result;
    } 

    public Node[] CreatePath(Node dst)
    {
        var path = new List<Node>() { dst };

        var curr = dst;
        while (curr.parent != null)
        {
            curr = curr.parent;
            path.Add(curr);
        }
        path.Reverse();

        return path.ToArray();
    }

   
}
