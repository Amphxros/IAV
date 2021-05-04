using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactuar : MonoBehaviour
{
    private bool isInteracting = false;
    
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
        Barca b = col.gameObject.GetComponent<Barca>();

        if (c != null)
        {
            if (isInteracting)
            {
                c.Restaurar();
            }
        }
        if (b != null)
        {
            if (isInteracting)
            {
                b.UsarBarca();
            }
        }
    }
}
