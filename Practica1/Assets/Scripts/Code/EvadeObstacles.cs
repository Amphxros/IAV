using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UCM.IAV.Movimiento{
    public class EvadeObstacles : ComportamientoAgente
    {
        public float look;
        public float distance;
        private Seguir seguir_; //seek al que cambiaremos el objetivo
        private MerodeoCinematico merodeo_;
        void Start()
        {
            seguir_ = GetComponent<Seguir>();
            merodeo_ = GetComponent<MerodeoCinematico>();
        }

      //  public override Direccion GetDireccion()
      //  {
      //      Direction result = new Direction();
      //         Vector3 ray = agente.velocidad;
      //          ray.Normalize();
      //          ray *= look;
      //
      //          RaycastHit hit; //raycast
      //
      //          //si hay colision
      //          if (Physics.Raycast(transform.position, ray, out hit))
      //          {
      //              result.lineal = hit.collider.gameObject.transform.position + hit.normal * distance;
      //          }
      //
      //
      //          return result;
      //  }
    }
}
