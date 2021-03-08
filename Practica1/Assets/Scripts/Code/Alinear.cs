using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCM.IAV.Movimiento
{
    public class Alinear : ComportamientoAgente
    {
        public float targetRadius;
        public float slowRadius;
        float timeToTarget = 0.1f;

        public override Direccion GetDireccion()
        {
            Direccion direccion = new Direccion();
            float rotation = objetivo.GetComponent<Agente>().orientacion - agente.orientacion;
            float rotationSize = Mathf.Abs(rotation);

            if (rotationSize < targetRadius)
            {
                return direccion;
            }

            float targetRotation;
            if (rotationSize > slowRadius)
            {
                targetRotation = agente.rotacionMax;
            }
            else
            {
                targetRotation = agente.rotacionMax * rotationSize / slowRadius;
            }
            targetRotation *= rotation / rotationSize;

            direccion.angular = targetRotation - agente.rotacion;
            direccion.angular /= timeToTarget;

            float angularAcceleration = Mathf.Abs(direccion.angular);
            if (angularAcceleration > agente.aceleracionAngularMax)
            {
                direccion.angular /= angularAcceleration;
                direccion.angular *= agente.aceleracionAngularMax;
            }

            return direccion;
        }
    }

}

