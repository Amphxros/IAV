using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCM.IAV.Movimiento
{
    public class Align : ComportamientoAgente
    {
        public float targetRadius = 5f;
        public float slowRadius = 10f;
        public float timeToTarget = 0.1f;

        public override Direccion GetDireccion()
        {
            Direccion result = new Direccion();

            // Obtener la direccion hacia el objetivo
            float rotation = objetivo.GetComponent<Agente>().orientacion - agente.orientacion;
            float rotationSize = Mathf.Abs(rotation);

            // Si ya se esta en esa posicion no hay giro
            if (rotationSize < targetRadius)
            {
                result.lineal = -agente.velocidad*agente.aceleracionMax;
                result.lineal = Quaternion.Euler(0, 5, 0) * result.lineal;
                result.angular = 0;

                return result;
            }

            float targetRotation;
            // Si esta afuera de slowRadius usar rotation maxima
            if (rotationSize > slowRadius)
            {
                targetRotation = agente.rotacionMax;
            }
            // Caso contrario calcula la rotacion escalada
            else
            {
                targetRotation = agente.rotacionMax * rotationSize / slowRadius;
            }

            // Combinar velocidad y direccion
            targetRotation *= rotation / rotationSize;

            // La aceleracion intenta llegar a la rotacion del objetivo
            result.angular = targetRotation - agente.rotacion;
            result.angular /= timeToTarget;

            // Revisa si la aceleracion es demasiado grande
            float angularAcceleration = Mathf.Abs(result.angular);
            if (angularAcceleration > agente.aceleracionAngularMax)
            {
                result.angular /= angularAcceleration;
                result.angular *= agente.aceleracionAngularMax;
            }
            result.lineal = Vector3.zero;

            return result;
        }
    }
}
