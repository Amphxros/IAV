using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UCM.IAV.Movimiento{
    public class EvadeObstacles : Seguir
    {
        public float look;
        public float distance;
        RaycastHit hit; //raycast

        public override Direccion GetDireccion()
        {
          
            Direccion result = new Direccion();
            Vector3 ray = agente.velocidad;
            ray.Normalize();
            ray *= look;

            Debug.DrawRay(transform.position, ray * 10, Color.white, 2.0f, true);

            //si hay colision
            Vector3 dir;
            if (Physics.Raycast(transform.position, ray, out hit))
            {
                dir= hit.collider.gameObject.transform.position + hit.normal * distance;
            }
            else 
                return null;

            result.lineal = dir - transform.position;
            result.lineal.Normalize();
            result.lineal = result.lineal * agente.aceleracionMax;
            result.angular = 0;
            return result;
        }
    }
}
