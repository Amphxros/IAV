using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barca : MonoBehaviour
{
    public int barca;
    public LogicaBarca logica;

    public GameObject posIni;
    public GameObject posFin;
    private GameObject pos;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Vizconde" || other.tag == "Fantasma")
        {
            if (barca == 1)
            {
                logica.UsarBarca1();
                if (logica.GetBarca1()) pos = posIni;
                else pos = posFin;
            }
            else if (barca == 2)
            {
                logica.UsarBarca2();
                if (logica.GetBarca2()) pos = posIni;
                else pos = posFin;
            }
            else if (barca == 3)
            {
                logica.UsarBarca3();
                if (logica.GetBarca3()) pos = posIni;
                else pos = posFin;
            }
            other.transform.position = pos.transform.position;
        }
    }
}
