using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCM.IAV.Movimiento
{
    public class Merodear : Face
    {
        // Radio y compensacion del circulo
        public float wanderOffset;
        public float wanderRadius;

        // Velocidad maxima a la que la orientacion puede cambiar
        public float wanderRate;
        // Orientacion actual del agente
        float wanderOrientation;

        public override Direccion GetDireccion()
        {
            // Calcular el objetivo que delegar a Face
            // Actualizar wanderOrientation
            wanderOrientation += Random.Range(-1.0f, 1.0f) * wanderRate;

            // Calcular la orientacion del objetivo combinada
            float targetOrientation = wanderOrientation + agente.orientacion;
            // Calcular el centro del circulo
            Vector3 target = transform.position + (wanderOffset* OriToVec(agente.orientacion));

            // Calcular la ubicacion del objetivo
            target += wanderRadius * OriToVec(targetOrientation);

            // Delegar a Face
            Direccion result = base.GetDireccion();

            // Marcar la aceleracion lineal hacia la direccion de la orientacion
            result.lineal = agente.aceleracionMax * OriToVec(agente.orientacion);

            return result;
        }
    }
}
