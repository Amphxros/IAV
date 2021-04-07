using UnityEngine;

[System.Serializable]
public class Node: MonoBehaviour
{
    GameObject cas, obstacle;

    public UnityEngine.Vector3 position; //equivalente a id 
    public float cost, heuristic;   // Coste del nodo (nodos cerca del minotauro), La heuristica se usa para medir la distancia al objetivo
    public bool isObstacle;         // Nodo ocupado por un muro
    public float f
    {
        get
        {
            return cost + heuristic;
        }
    }

    public Node parent;

    private Node() { }
    public Node(Vector3 pos, GameObject cas, GameObject obs, bool obstacle=false)
    {
        this.position = pos;
        isObstacle = obstacle;

        if (!isObstacle)
            Instantiate(cas, pos, Quaternion.identity);
        else
            Instantiate(obs, pos,Quaternion.identity);
    }


   

}
