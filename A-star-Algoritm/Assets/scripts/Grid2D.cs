using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Grid2D
{
    public int width, height;
    public float yHeight;

    public Node[] nodes;

    private Grid2D()
    {
    }

    public Grid2D(int w, int h, float y)
    {
        width = w;
        height = h;
        yHeight = y;
    }

    public void CreateGrid()
    {
        nodes = new Node[width * height];

        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                int ind = i * height + 1;
                Vector3 p = new Vector3(i, yHeight, j);
                var node = new Node(p);
                nodes[ind] = node;
            }
        }

    }

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

        throw new System.Exception(string.Format("No hay nodo en  {0}", pos));
    }

}
