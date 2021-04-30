using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalancaLampara : MonoBehaviour
{
    public CaidaLampara Izquierda;
    public CaidaLampara Derecha;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Fantasma")
        {
            Debug.Log("Entra");
            Izquierda.Caer();
            Derecha.Caer();
        }
        else if (col.gameObject.tag == "Vizconde")
        {
            Izquierda.Restaurar();
            Derecha.Restaurar();
        }
    }
}
