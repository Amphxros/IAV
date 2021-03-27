using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Graph : MonoBehaviour
{

    public GameObject vertexPrefab;

    protected List<Vertex> vertices;
    protected List<List<Vertex>> neighbors;
    protected List<List<float>> costs;
    protected Dictionary<int, int> instIdToId;


    public delegate float Heuristic(Vertex a, Vertex b);

    public List<Vertex> path;
    public bool isFinished;

    // Start is called before the first frame update
    void Start()
    {
        Load();
    }

    public virtual void Load() { }

    public virtual int GetSize()
    {
        if (ReferenceEquals(vertices, null))
            return 0;
        return vertices.Count;
    }

    // Update is called once per frame
    void Update()
    {

    }


    public virtual Vertex[] getNeighbours(Vertex v)
    {
        if (neighbors.Count == 0 || ReferenceEquals(neighbors, null))
        {
            return new Vertex[0];
        }
        else if (v.id < 0 || v.id >= neighbors.Count)
        {
            return new Vertex[0];
        }

        return neighbors[v.id].ToArray();
    }


    //get a star path
    public List<Vertex> GetPath(GameObject src, GameObject dst, Heuristic h = null)
    {
        if (src == null || dst == null)
        {
            return new List<Vertex>(); // si no hay destino o origen 
        }
        else
        {
            Vertex srcV = src.GetComponent<Vertex>(); //vertice origen
            Vertex dstV = dst.GetComponent<Vertex>(); //vertice destino

            List<Edge> frontier = new List<Edge>(); //cambiar esto por una estructura real


            Edge[] edges;
            Edge node, child;
            int size = vertices.Count;
            float[] distValue = new float[size];
            int[] previous = new int[size];
            node = new Edge(srcV, 0);
            frontier.Add(node);
            distValue[srcV.id] = 0;
            previous[srcV.id] = srcV.id;
            for (int i = 0; i < size; i++)
            {
                if (i == srcV.id)
                    continue;
                distValue[i] = Mathf.Infinity;
                previous[i] = -1;
            }
            while (frontier.Count != 0)
            {
               // node = frontier.Remove();
               //int nodeId = node.vertex.id;
               // if (ReferenceEquals(node.vertex, dst))
               // {
               //     return BuildPath(src.id, node.vertex.id, ref previous);
               // }
               // edges = GetEdges(node.vertex);
               //foreach (Edge e in edges)
                {
               //     int eId = e.vertex.id;
               //     if (previous[eId] != -1)
               //         continue;
               //     float cost = distValue[nodeId] + e.cost;
               //     // key point
               //     cost += h(node.vertex, e.vertex);
               //     if (cost < distValue[e.vertex.id])
               //     {
               //         distValue[eId] = cost;
               //         previous[eId] = nodeId;
               //         frontier.Remove(e);
               //         child = new Edge(e.vertex, cost);
               //         frontier.Add(child);
               //     }
                }
            }
            return new List<Vertex>();
        }

    }

    private float RecursiveIDAstar(Vertex v, Vertex dst, float bound, Heuristic h, ref Vertex goal, ref bool[] visited)
    {
        return 0.0f;
    }

    //una heuristica
    public float EuclidDist(Vertex a, Vertex b)
    {
        Vector3 posA = a.transform.position;
        Vector3 posB = b.transform.position;
        return Vector3.Distance(posA, posB);
    }

    private List<Vertex> BuildPath(int srcId, int dstId, ref int[] prevList)
    {
        List<Vertex> path = new List<Vertex>();
        int prev = dstId;
        do
        {
            path.Add(vertices[prev]);
            prev = prevList[prev];
        } while (prev != srcId);
        return path;
    }

}