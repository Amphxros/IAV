using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCM.IAV.Movimiento
{
    public class LookWhereYouAreGoing : Align
    {
        public override Direccion GetDireccion()
        {
            Vector3 velocity = agente.velocidad;
            if (velocity.magnitude > 0)
            {
                return base.GetDireccion();
            }

            else return null;
        }
    }
}

