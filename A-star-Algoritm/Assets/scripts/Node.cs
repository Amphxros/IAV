using UnityEngine;

[System.Serializable]
public class Node: MonoBehaviour
{
    public UnityEngine.Vector3 position;
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
    public Node(Vector3 pos)
    {
        this.position = pos;
        isObstacle = false;
    }

}
