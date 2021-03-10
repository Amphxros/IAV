using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCM.IAV.Movimiento
{
    public class MirarHaciaAdelante : Alinear
    {
        public override Direccion GetDireccion()
        {
            Vector3 velocity = agente.velocidad;
            if (velocity.magnitude == 0)
            {
                return null;
            }

            agente.orientacion = Mathf.Atan2(-velocity.x, velocity.z);

            return base.GetDireccion();
        }
    }
}
