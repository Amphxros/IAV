using System.Collections;
using System.Collections.Generic;
using UCM.IAV.Navegacion;
using UnityEngine;

public class PlayerAutomatic : MonoBehaviour
{
    private float gridCellSize = 1.0f;
    public float timeToUpdate = 1.0f;
    private float timePass = 0.0f;
    private Transform playerTr;
    private int puesto = 10000;
    private List<Vertex> path;

    void Start()
    {
        playerTr = GetComponent<Transform>();
    }

    public void GetPathToExit(List<Vertex> vertices)
    {
        path = vertices;
        puesto = path.Count - 1;
    }

    void Update()
    {
        if (timePass < 0.0f && path != null && puesto>=0)
        {
            // Cambiar la posicion del player a la del vertice que sigue
            playerTr.position = path[puesto].transform.position;
            puesto--;
            timePass = timeToUpdate;
           // Debug.Log("automatico");
        }
        else
        {
            timePass -= Time.deltaTime;
        }
    }

}
