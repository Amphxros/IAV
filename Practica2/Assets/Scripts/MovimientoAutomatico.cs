using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoAutomatico : MonoBehaviour
{
    private float gridCellSize = 1.0f;
    public float timeToUpdate = 1.0f;
    private Transform playerTr;
    private int direction = 0;
    private List<Vertex> vert;

    void Start()
    {
        playerTr = GetComponent<Transform>();
    }

    public void GetPathToExit(List<Vertex> vertices)
    {
        vert = vertices;
    }

    void Update()
    {
        if (timeToUpdate < 0.0f)
        {
            // Cambiar la posicion del player a la del vertice que sigue
            //playerTr.position = vert[direction].transform.position;
            //direction++;
            timeToUpdate = 1.0f;
        }
        else
        {
            timeToUpdate -= Time.deltaTime;
        }
    }

    public void SetGridCellSize(float size)
    {
        gridCellSize = size;
    }
}
