using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Barca : MonoBehaviour
{
    public int barca;
    public LogicaBarca logica;
    public GameObject pos;

    private bool siendoUsada = false;
    private void OnTriggerEnter(Collider other)
    {
        if (!siendoUsada)
        {
            siendoUsada = true;
            if (other.tag == "Vizconde" || other.tag == "Fantasma")
            {
                if (barca == 1)
                {
                    logica.UsarBarca1();
                }
                else if (barca == 2)
                {
                    logica.UsarBarca2();
                }
                else if (barca == 3)
                {
                    logica.UsarBarca3();
                }

                if (other.tag == "Fantasma")
                {
                    Vector3 destOrig = other.GetComponent<NavMeshAgent>().destination;
                    other.GetComponent<NavMeshAgent>().enabled = false;
                    other.transform.position = new Vector3(pos.transform.position.x, other.transform.position.y, pos.transform.position.z);
                    other.GetComponent<NavMeshAgent>().enabled = true;
                    other.GetComponent<NavMeshAgent>().SetDestination(destOrig);
                }                
                else if (other.tag == "Vizconde")
                {
                    other.transform.position = new Vector3(pos.transform.position.x, other.transform.position.y, pos.transform.position.z);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        siendoUsada = false;
    }
}
