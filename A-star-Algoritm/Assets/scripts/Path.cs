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

    private Path() { }

    public Path(Grid2D g, Vector3 src, Vector3 dst)
    {
        this.grid = g;
        this.start = src;
        this.goal = dst;

        openList = new List<Node>();
        closedList = new List<Node>();
    }

    public Node[] Astar()
    {
        openList.Clear();
        closedList.Clear();

        Node priNode = grid.FindNodeByPosition(start);
        Node finalNode = grid.FindNodeByPosition(goal);
        if (priNode != null)
        {
            priNode.heuristic = (goal - start).magnitude;   // Distancia en magnitud del agente a la meta
            openList.Add(priNode);

            while (openList.Count > 0)
            {
                var edge = GetBestNode();
                openList.Remove(edge);

                var vecinos = grid.GetNextNeighbours(edge);
                for (int i = 0; i < 4; i++)
                {
                    var node = vecinos[i];
                    if (vecinos != null)
                    {
                        if (finalNode == node)
                        {
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
            }
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
