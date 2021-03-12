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
            Vector3 vec1 = transform.position + (wanderOffset * OriToVec(agente.orientacion));
            //transform.position = new Vector3(vec1.x, 0, vec1.z);

            // Calcular la ubicacion del objetivo
            Vector3 vec2 = wanderRadius * OriToVec(targetOrientation);
            //transform.position += new Vector3(vec2.x, 0, vec2.z)*2;

            Direccion result = base.GetDireccion();
            //agente.orientacion = targetOrientation + Random.Range(-2.0f, 2.0f);

            // Marcar la aceleracion lineal hacia la direccion de la orientacion
            result.lineal = agente.aceleracionMax * OriToVec(agente.orientacion);
            result.angular = 0;

            return result;
        }
    }
}
