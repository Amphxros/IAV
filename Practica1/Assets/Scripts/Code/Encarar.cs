using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCM.IAV.Movimiento
{
    public class Encarar : Alinear
    {
        protected GameObject objetivoAux;

        public override void Awake()
        {
            base.Awake();
            objetivoAux = objetivo;
            objetivo = new GameObject();
            objetivo.AddComponent<Agente>();
        }

        public override Direccion GetDireccion()
        {
            Vector3 dir = objetivoAux.transform.position - transform.position;

            if (dir.magnitude > 0.0f)
            {
                float objOrientation = Mathf.Atan2(dir.x, dir.y);
                objOrientation *= Mathf.Rad2Deg;
                objetivo.GetComponent<Agente>().orientacion = objOrientation;
            }

            return base.GetDireccion();
        }
    }

}
