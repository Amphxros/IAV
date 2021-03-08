using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCM.IAV.Movimiento
{    public class Llegar : ComportamientoAgente
    {
        public float maxSpeed;
        public float targetRadius;
        public float slowRadius;
        float timeToTarget = 0.25f;

        // Update is called once per frame
        public override Direccion GetDireccion()
        {
            Direccion direccion = new Direccion();
            Vector3 dir = objetivo.transform.position - transform.position;
            float distance = dir.magnitude;

            // Check if we are there, return no steering
            if (distance < targetRadius)
            {
                return direccion;
            }

            // If we are outside de SlowRadius, then move at max speed
            float targetSpeed;
            if (distance > slowRadius)
            {
                targetSpeed = maxSpeed;
            }
            // Otherwise calculate a scaled speed
            else
            {
                targetSpeed = maxSpeed * distance / slowRadius;
            }

            // Target velocity combines speed and direction
            Vector3 targetVelocity = dir;
            targetVelocity.Normalize();
            targetVelocity *= targetSpeed;

            // Acceleration tries to get to the target velocity
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
