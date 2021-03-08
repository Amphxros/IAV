using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCM.IAV.Movimiento
{
    public class Merodear : Encarar
    {
        public float wanderOffset;
        public float wanderRadius;
        public float wanderRate;

        public override void Awake()
        {
            objetivo = new GameObject();
            objetivo.transform.position = transform.position;
            base.Awake();
        }

        public override Direccion GetDireccion()
        {
            Direccion steering = new Direccion();
            float wanderOrientation = Random.Range(-1.0f, 1.0f) * wanderRate;
            float objetivoOrientation = wanderOrientation + agente.orientacion;
            Vector3 orientationVec = OriToVec(agente.orientacion);
            Vector3 objetivoPosition = (wanderOffset * orientationVec) + transform.position;
            objetivoPosition = objetivoPosition + (OriToVec(objetivoOrientation) * wanderRadius);
            objetivoAux.transform.position = objetivoPosition;
            steering = base.GetDireccion();
            steering.lineal = objetivoAux.transform.position - transform.position;
            steering.lineal.Normalize();
            steering.lineal *= agente.aceleracionMax;
            
            return steering;
        }
    }

}