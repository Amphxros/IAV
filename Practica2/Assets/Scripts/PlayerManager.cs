using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private MovimientoJugador movJugador;
    private MovimientoAutomatico movAuto;

    // Esto lo deberia tener A*
    public float tamCasilla = 1.0f;
    public float timeToActiveHelp = 2.5f;

    private float SpaceBarPressedTime = 0.0f;
    public bool suavizado = false;

    public GameObject salida;
    private Graph grafo;
    private List<Vertex> vertices;

    private bool spaceBar = false;

    // private componente que pinte la ruta de salida

    void Start()
    {
        movJugador = GetComponent<MovimientoJugador>();
        movAuto = GetComponent<MovimientoAutomatico>();
        //grafo = GetComponent<Graph>();
        
        if (tamCasilla != 1.0f)
        {
            movJugador.SetGridCellSize(tamCasilla);
            movAuto.SetGridCellSize(tamCasilla);
        }
    }

    void Update()
    {
        // Podria ir en el scrip de A*
        if (Input.GetKeyDown(KeyCode.S))
        {
            suavizado = !suavizado;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            SpaceBarPressedTime += Time.deltaTime;
            spaceBar = true;
            Debug.Log("Clicando space bar");
        }
        if (Input.GetKeyUp(KeyCode.Space) && spaceBar)
        {
            // Llamar a A* para que deje de dibujar
            SpaceBarPressedTime = 0.0f;
            spaceBar = false;
            Debug.Log("Deja de dibujar");
        }
        if (SpaceBarPressedTime >timeToActiveHelp)
        {
            // Manda mensaje de control automatico
            movAuto.enabled = true;
            movJugador.enabled = false;
            //movAuto.GetPathToExit(vertices);
            Debug.Log("Cambiando a modo automatico");
        }
        else
        {
            // Mostrar la ruta del hilo
            movAuto.enabled = false;
            movJugador.enabled = true;
            // Llamar al A* para que dibuje la ruta
            //vertices = grafo.GetPath(gameObject, salida);
            Debug.Log("Cambiando a modo manual");
            Debug.Log("Dibuja el camino de A*");
        }
    }
}
