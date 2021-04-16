using System.Collections;
using System.Collections.Generic;
using UCM.IAV.Navegacion;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Transform playerTr;
    public float timeToKey = 0.5f;
    private float timeInput = 0.5f;
    public TesterGraph aStar;
    public GraphGrid grid;
    private Vector3 nextPos;
    private GameObject verticeActual;

    private int col, row;

    void Start()
    {
        playerTr = GetComponent<Transform>();
    }

    void Update()
    {
        if (timeInput < 0.0f)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                nextPos = playerTr.position;
                nextPos.x -= grid.cellSize;
                col = (int)(nextPos.x / grid.cellSize);
                row = (int)(nextPos.z / grid.cellSize);
                if (col >= 0)
                {
                    verticeActual = grid.getVertexById(grid.GridToId(col, row));
                    if (verticeActual.CompareTag(aStar.vertexTag))
                    {
                        playerTr.position = verticeActual.transform.position;
                       // Debug.Log("Izq");

                        timeInput = timeToKey;
                    }

                }
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                nextPos = playerTr.position;
                nextPos.x += grid.cellSize;
                col = (int)(nextPos.x / grid.cellSize);
                row = (int)(nextPos.z / grid.cellSize);
                if (col < grid.getCol())
                {
                    verticeActual = grid.getVertexById(grid.GridToId(col, row));
                    if (verticeActual.CompareTag(aStar.vertexTag))
                    {
                        playerTr.position = verticeActual.transform.position;
                        // Debug.Log("Der");

                        timeInput = timeToKey;
                    }
                }
            }

            else if (Input.GetKey(KeyCode.UpArrow))
            {
                nextPos = playerTr.position;
                nextPos.z += grid.cellSize;
                col = (int)(nextPos.x / grid.cellSize);
                row = (int)(nextPos.z / grid.cellSize);
                if (row < grid.getRow())
                {
                    verticeActual = grid.getVertexById(grid.GridToId(col, row));
                    if (verticeActual.CompareTag(aStar.vertexTag))
                    {
                        playerTr.position = verticeActual.transform.position;
                        // Debug.Log("Arriba");

                        timeInput = timeToKey;
                    }
                }
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                nextPos = playerTr.position;
                nextPos.z -= grid.cellSize;
                col = (int)(nextPos.x / grid.cellSize);
                row = (int)(nextPos.z / grid.cellSize);
                if (row >= 0)
                {
                    verticeActual = grid.getVertexById(grid.GridToId(col, row));
                    if (verticeActual.CompareTag(aStar.vertexTag))
                    {
                        playerTr.position = verticeActual.transform.position;
                        //  Debug.Log("Abajo");

                        timeInput = timeToKey;
                    }
                }
            }
            
        }
        else
        {
            timeInput -= Time.deltaTime;
        }
    }
}
