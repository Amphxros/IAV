using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UCM.IAV.Movimiento{
    public class EvadeObstacles : Seguir
    {
        public float look;
        public float distance;
        RaycastHit hit; //raycast

        //public override Direccion GetDireccion()
        //{
        //    return result;
        //}

        void update() {

            Direccion result = new Direccion();
            Vector3 ray = agente.velocidad;
            ray.Normalize();
            ray *= look;

            Debug.DrawRay(transform.position, ray * 10, Color.white, 2.0f, true);

            //si hay colision
            if (Physics.Raycast(transform.position, ray, out hit))
            {
                result.lineal = hit.collider.gameObject.transform.position + hit.normal * distance;
            }

            agente.SetDireccion(result);
        }
    }
}
