using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Este script es el que usa Unity para crear TODO lo referente al algoritmo
// Gestiona la implementacion del mapa, algoritmo, costes y obstaculos.....
public class A_star_Manager : MonoBehaviour
{
    public int width, height;       // Tamaño del mapa a evaluar
    public GameObject goal, cas, obs;     // Punto en el que se encuentra el agente y meta


    public Grid2D grid;            // Red de nodos con costes y obstaculos
    public Path astar;              // Algoritmo de busqueda

    public Node[] n;
    public GameObject player;
    private Actor a;
    private Vector3 start;


    void Awake()
    {
        start = new Vector3();
        grid = new Grid2D(width, height,0,cas,obs);
        a = player.GetComponent<Actor>();
        grid.CreateGrid();  // Creación inicial del mapa
    }
    void Start()
    {
        Debug.Log("Start");
        start.x = (int)player.transform.position.x;
        start.y = (int)player.transform.position.y;
        start.z = (int)player.transform.position.z;

        astar = new Path(grid, player, goal);

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
          astar.setPlayerPos(start);

            Debug.Log("Astar activado");
            CreateAstarPath();

            Debug.Log("Astar done");

        }
    }

    public void CreateAstarPath()
    {
        var crono = new System.Diagnostics.Stopwatch();
        crono.Start();  // Timer para Debug
        n= astar.Astar();

        Debug.Log("Calculando camino");
        crono.Stop();

        if(astar!=null && n.Length > 0)
        {
            var pos = new List <UnityEngine.Vector3>();
            pos = ArrayToList(n);
            a.Move(pos.ToArray());

            Debug.Log("Personaje Movido");
        }
    }

    private List<Vector3> ArrayToList(Node[] node)
    {
        List<Vector3> result= new List<UnityEngine.Vector3>();

        for(int i = 0; i < node.Length; i++)
        {
            var npoint = node[i];

            if (node[i].position != null)
                result.Add(node[i].position);
        }
        return result;
    }
  
}
