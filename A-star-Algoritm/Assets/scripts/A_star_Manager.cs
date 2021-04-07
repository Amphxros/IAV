using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Este script es el que usa Unity para crear TODO lo referente al algoritmo
// Gestiona la implementacion del mapa, algoritmo, costes y obstaculos.....
public class A_star_Manager : MonoBehaviour
{
    public int width, height;       // Tamaño del mapa a evaluar
    public GameObject goal;     // Punto en el que se encuentra el agente y meta

    private Grid2D grid;            // Red de nodos con costes y obstaculos
    public Path astar;              // Algoritmo de busqueda

    public Node[] n;
    public GameObject player;
    private Actor a;


    void Awake()
    {
        grid = new Grid2D(width, height,0);
        a = player.GetComponent<Actor>();
    }
    void Start()
    {
        grid.CreateGrid();  // Creación inicial del mapa
        astar = new Path(grid, player.transform.position, goal.transform.position);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            CreateAstarPath();
        }
    }

    public void CreateAstarPath()
    {
        var crono = new System.Diagnostics.Stopwatch();
        crono.Start();  // Timer para Debug
        n= astar.Astar();
        crono.Stop();
    
        if(astar!=null && n.Length > 0)
        {
            var pos = new List <UnityEngine.Vector3>();
            pos = ArrayToList(n);
            a.Move(pos.ToArray());
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
    
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        foreach (Node nd in n){
            UnityEngine.Vector3 targetPos = nd.position;
            Gizmos.DrawSphere(targetPos,0.5f);
        }
    }
}
