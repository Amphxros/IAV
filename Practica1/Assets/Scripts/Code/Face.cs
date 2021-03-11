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

            // Si la posicion es distinta de 0 se deben hacer cambios
            if (direction.magnitude > 0)
            {
                float objetivoOrientation = Mathf.Atan2(-direction.x, direction.z);
                objetivoOrientation *= Mathf.Rad2Deg;
                objetivo.GetComponent<Agente>().orientacion = objetivoOrientation;
            }

            return base.GetDireccion();
        }
    }
}

