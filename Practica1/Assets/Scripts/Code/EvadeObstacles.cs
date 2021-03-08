using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCM.IAV.Movimiento 
{
    public class EvadeObstacles : ComportamientoAgente
    {

        public float maxAcceleration;
        public float rd_;

        private Seguir seguir_;

        void Start()
        {
            seguir_ = GetComponent<Seguir>();
        }

    
    }
}
