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
                // Para convertir la direccion a formato velocidad (Vector2D to Float) descomponemos las componentes X y Z
                // Y calculamos la Arcotangente conjunta
                objetivo.GetComponent<Agente>().orientacion = Mathf.Atan2(-direction.x, direction.z);
            }

            return base.GetDireccion(); //devlvemos la direccion de la clase padre
        }
    }
}

