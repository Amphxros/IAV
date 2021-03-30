using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphGrid : Graph
{
    public GameObject obstaclePrefab;

    [Range(0, Mathf.Infinity)]
    public float defaultCost = 1f;

    [Range(0, Mathf.Infinity)]
    public float maximumCost = Mathf.Infinity;

    int numCols;
    int numRows;
    GameObject[] vertexObjs;
    bool[,] mapVertices;
    public float cellSize = 1f;


    private int GridToId(int x, int y)
    {
        return Mathf.Max(numRows, numCols) * y + x;
    }

    public override Vertex GetNearestVertex(Vector3 position)
    {
        int col = (int)(position.x / cellSize);
        int row = (int)(position.z / cellSize);
        Vector2 p = new Vector2(col, row);
        List<Vector2> explored = new List<Vector2>();
        Queue<Vector2> queue = new Queue<Vector2>();
        queue.Enqueue(p);
        do
        {
            p = queue.Dequeue();
            col = (int)p.x;
            row = (int)p.y;
            int id = GridToId(col, row);
            if (mapVertices[row, col])
                return vertices[id];

            if (!explored.Contains(p))
            {
                explored.Add(p);
                int i, j;
                for (i = row - 1; i <= row + 1; i++)
                {
                    for (j = col - 1; j <= col + 1; j++)
                    {
                        if (i < 0 || j < 0)
                            continue;
                        if (j >= numCols || i >= numRows)
                            continue;
                        if (i == row && j == col)
                            continue;
                        queue.Enqueue(new Vector2(j, i));
                    }
                }
            }
        } while (queue.Count != 0);
        return null;
    }

    protected void SetNeighbours(int x, int y, bool get8 = false)
    {
        int col = x;
        int row = y;
        int i, j;
        int vertexId = GridToId(x, y);
        neighbors[vertexId] = new List<Vertex>();
        costs[vertexId] = new List<float>();
        Vector2[] pos = new Vector2[0];
        if (get8)
        {
            pos = new Vector2[8];
            int c = 0;
            for (i = row - 1; i <= row + 1; i++)
            {
                for (j = col - 1; j <= col; j++)
                {
                    pos[c] = new Vector2(j, i);
                    c++;
                }
            }
        }
        else
        {
            pos = new Vector2[4];
            pos[0] = new Vector2(col, row - 1);
            pos[1] = new Vector2(col - 1, row);
            pos[2] = new Vector2(col + 1, row);
            pos[3] = new Vector2(col, row + 1);
        }
        foreach (Vector2 p in pos)
        {
            i = (int)p.y;
            j = (int)p.x;
            if (i < 0 || j < 0)
                continue;
            if (i >= numRows || j >= numCols)
                continue;
            if (i == row && j == col)
                continue;
            if (!mapVertices[i, j])
                continue;
            int id = GridToId(j, i);
            neighbors[vertexId].Add(vertices[id]);
            costs[vertexId].Add(defaultCost);
        }
    }

}
