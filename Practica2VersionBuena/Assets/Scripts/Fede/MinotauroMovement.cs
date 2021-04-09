using System.Collections;
using System.Collections.Generic;
using UCM.IAV.Navegacion;
using UnityEngine;

public class MinotauroMovement : MonoBehaviour
{
    public float timeToUpdate =1.0f;
    public GraphGrid graph;
    private Transform minotauroTransform;
    private float timeActual;
    private Edge[] vecinos;
    private Vertex vertActual;
    private bool followPlayer = false;
    public Transform playerTr;
    private Vector3 dir;
    int col, row;
    void Start()
    {
        minotauroTransform = GetComponent<Transform>();
        timeActual = timeToUpdate;
    }

    
    void Update()
    {
        if (playerTr.position.x == minotauroTransform.position.x || playerTr.position.z == minotauroTransform.position.z)
        {

            followPlayer = true;
        }
        else
        {
            followPlayer = false;
        }

       

        if (timeToUpdate < 0.0f )
        {
           setDamage(graph.defaultCost* 5);

            if (!followPlayer)
            {
                movAleatorio();
            }
            else
            {
                dir = playerTr.position - minotauroTransform.position;
                if (dir.magnitude == 1)
                {
                    if (vecinos[0].vertex.transform.position == playerTr.position)
                    {
                        int ran = Random.Range(1, vecinos.Length);
                        minotauroTransform.position = vecinos[ran].vertex.transform.position;
                    }
                    else 
                        minotauroTransform.position= vecinos[0].vertex.transform.position;
                }
                else
                {
                    dir /= dir.magnitude;
                    col = (int)((transform.position + dir).x / graph.cellSize);
                    row = (int)((transform.position + dir).z / graph.cellSize);
                    if (graph.getVertexById(graph.GridToId(col, row)).tag == "Wall")
                    {
                        followPlayer = false;
                        movAleatorio();
                    }
                    else
                    {
                        minotauroTransform.position = graph.getVertexById(graph.GridToId(col, row)).transform.position;
                    }
                }
            }
            timeToUpdate = timeActual;
            setDamage(graph.defaultCost * 5);
        }
        else
        {
            timeToUpdate -= Time.deltaTime;
        }
    }

    void setDamage(float damage)
    {
        vertActual = graph.GetNearestVertex(minotauroTransform.position);
        vecinos = graph.GetEdges(vertActual);

        for (int i = 0; i < vecinos.Length; i++)
        {
            vecinos[i].cost = damage;
        }
        

    }

    void movAleatorio()
    {

        int ran = Random.Range(0, vecinos.Length);
        transform.position = vecinos[ran].vertex.transform.position;
    }

    
}
