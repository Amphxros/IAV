using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Barca : MonoBehaviour
{
    public MeshRenderer barcaPos1;
    public MeshRenderer barcaPos2;

    public OffMeshLink Pos1;
    public OffMeshLink Pos2;

    bool fantasma = false;

    public void UsarBarca()
    {
        if (Pos1.activated)
        {
            Pos1.activated = false;
            Pos2.activated = true;
            barcaPos1.enabled = false;
            barcaPos2.enabled = true;
        }
        else
        {

            Pos1.activated = true;
            Pos2.activated = false;
            barcaPos1.enabled = true;
            barcaPos2.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Fantasma" && !fantasma)
        {
            fantasma = true;
            UsarBarca();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Fantasma")
        {
            fantasma = false;
        }
    }
}
