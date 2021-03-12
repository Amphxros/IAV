using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCM.IAV.Movimiento
{
    public class Face : Align
    {
        public override Direccion GetDireccion()
        {
            // Calcula el objetivo a delegar para alinear
            // Averigua la posicion del objetivo
            Vector3 direction = objetivo.transform.position - transform.position;

            if (direction.magnitude > 0)
            {
                objetivo.GetComponent<Agente>().orientacion = Mathf.Atan2(-direction.x, direction.z);
            }

            return base.GetDireccion();
        }
    }
}

