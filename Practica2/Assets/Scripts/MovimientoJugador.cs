using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    private Transform playerTr;
    private float timeInput = 0.5f;
    private float gridCellSize = 1.0f;

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
                playerTr.position = playerTr.position + new Vector3(-gridCellSize, 0, 0);
                Debug.Log("Izq");
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                playerTr.position = playerTr.position + new Vector3(gridCellSize, 0, 0);
                Debug.Log("der");
            }
            else if (Input.GetKey(KeyCode.UpArrow))
            {
                playerTr.position = playerTr.position + new Vector3(0, 0, gridCellSize);
                Debug.Log("avan");
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                playerTr.position = playerTr.position + new Vector3(0, 0, -gridCellSize);
                Debug.Log("volver");
            }
            timeInput = 0.5f;
        }
        else
        {
            timeInput -= Time.deltaTime;
        }
    }

    public void SetGridCellSize(float size)
    {
        gridCellSize = size;
    }
}
