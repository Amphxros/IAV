using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactuar : MonoBehaviour
{
    private bool isInteracting = false;
    //añadir el valor este de la variable de vizconde interactuando con el piano
    void FixedUpdate()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            isInteracting = true;
        }
        else
        {
            isInteracting = false;
        }
    }

    void OnTriggerStay(Collider col)
    {
        CaidaLampara c = col.gameObject.GetComponent<CaidaLampara>();
        Piano p= col.gameObject.GetComponent<Piano>();
        if (c != null) //Si es una lampara caida
        {
            if (isInteracting)
            {
                c.Restaurar();
            }
        }
        else if (p != null) //si es un piano
        {
            if (isInteracting)
            {
                p.setEnabled(false);
            }
        }

    }

    void OnTriggerExit(Collider col)
    {

    }
}
