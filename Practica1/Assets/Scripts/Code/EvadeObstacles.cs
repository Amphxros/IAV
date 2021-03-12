using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UCM.IAV.Movimiento{
    public class EvadeObstacles : ComportamientoAgente
    {
        public float look; // distancia de vista
        public float distance; //
        RaycastHit hit; //raycast

        public override Direccion GetDireccion()
        {
            Direccion result = new Direccion();
            Vector3 ray = agente.velocidad; // vector hacia donde irá el raycast
            ray.Normalize();
            ray *= look;

            // Para poder saber a donde apuntan
            // Debug.DrawRay(transform.position, ray * 10, Color.white, 2.0f, true); 

            //comprobamos mediante el raycast si hay una colision, en caso de que haya se cambia de direccion
            Vector3 dir;
            if (Physics.Raycast(transform.position, ray, out hit))
            {
                dir = (hit.point - transform.position);
                dir += hit.normal * distance;
                if (Mathf.Abs( Vector3.Angle(hit.point,agente.velocidad))< 10 )
                {
                    dir += transform.forward * agente.velocidadMax;
                }
                
            }
            else
            {
                result.lineal = Vector3.zero;
                result.angular = 0;
                return result;
            }

            result.lineal = dir;
            result.lineal.Normalize();
            result.lineal = result.lineal * agente.aceleracionMax;
            result.angular = 0;
            return result;
        }
    }
}
