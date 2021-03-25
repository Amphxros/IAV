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
        if(neighbors.Count==0 || ReferenceEquals(neighbors, null))
        {
            return new Vertex[0];
        }
       else if (v.id<0 || v.id>= neighbors.Count)
        {
            return new Vertex[0];
        }

        return neighbors[v.id].ToArray();
    }


    //get a star path
    public List<Vertex> GetPath(GameObject src, GameObject dst, Heuristic h=null)
    {
        if(src==null || dst == null)
        {
            return new List<Vertex>(); // si no hay destino o origen 
        }
        else
        {
            Vertex srcV = src.GetComponent<Vertex>(); //vertice origen
            Vertex dstV = dst.GetComponent<Vertex>(); //vertice destino




            return new List<Vertex>(); //de momento es algo
        }

    }

    private float RecursiveIDAstar( Vertex v,Vertex dst, float bound, Heuristic h, ref Vertex goal, ref bool[] visited)
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
