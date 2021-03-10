using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCM.IAV.Movimiento
{    public class Llegar : ComportamientoAgente
    {
        public float targetRadius;
        public float slowRadius;
        public float timeToTarget = 0.1f;

        public override Direccion GetDireccion()
        {
            Direccion direccion = new Direccion();

            Vector3 dir = objetivo.transform.position - transform.position;
            float distance = dir.magnitude;

            if (distance < targetRadius)
            {
                return direccion;
            }

            float targetSpeed;
            if (distance > slowRadius)
            {
                targetSpeed = agente.velocidadMax;
            }
            else
            {
                targetSpeed = agente.velocidadMax * distance / slowRadius;
            }

            Vector3 targetVelocity = dir;
            targetVelocity.Normalize();
            targetVelocity *= targetSpeed;

            direccion.lineal = targetVelocity - agente.velocidad;
            direccion.lineal /= timeToTarget;

            if (direccion.lineal.magnitude > agente.aceleracionMax)
            {
                direccion.lineal.Normalize();
                direccion.lineal *= agente.aceleracionMax;
            }

            return direccion;
        }
    }
}
