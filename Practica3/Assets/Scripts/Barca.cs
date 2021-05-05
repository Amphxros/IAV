using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barca : MonoBehaviour
{
    public int barca;
    public LogicaBarca logica;

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
                    Vector3 pos = logica.UsarBarca1();
                    other.transform.position = pos;
                }
                else if (barca == 2)
                {
                    Vector3 pos = logica.UsarBarca2();
                    other.transform.position = pos;
                }
                else if (barca == 3)
                {
                    Vector3 pos = logica.UsarBarca3();
                    pos.y = 94;
                    other.transform.position = pos;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        siendoUsada = false;
    }
}
