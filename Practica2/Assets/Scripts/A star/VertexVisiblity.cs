using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertexVisiblity : Vertex
{

    void Awake()
    {
        neighbours = new List<Edge>();
    }
    public void FindNeighbours(List<Vertex> vertices)
    {
        Collider c = gameObject.GetComponent<Collider>();

        c.enabled = false;

        Vector3 dir = Vector3.zero;
        Vector3 src = transform.position;
        Vector3 dst = Vector3.zero;

        RaycastHit[] hits;

        Ray ray;
        float distance = 0.0f;

        for(int i=0;i<vertices.Count; i++)
        {
            if (vertices[i] == this)
                continue;
            dst = vertices[i].transform.position;
            dir = dst - src;
            distance = dir.magnitude;

            ray = new Ray(src, dir);
            hits = Physics.RaycastAll(ray, distance);
            if (hits.Length == 1)
            {
                GameObject g = hits[0].collider.gameObject;
                Vertex v = g.GetComponent<Vertex>();
                if(v!=null && v.enabled)
                {
                    Edge e = new Edge(v);
                    e.cost = distance;
                    if (v != vertices[i])
                        continue;
                    e.vertex = v;
                    neighbours.Add(e);

                }
            }
            c.enabled = true;
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Vector3 originPos, targetPos;
        originPos = transform.position;
        foreach (Edge e in neighbours)
        {
            targetPos = e.vertex.transform.position;
            Gizmos.DrawLine(originPos, targetPos);
        }
    }

}
