using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCM.IAV.Movimiento
{
    public class Separation : ComportamientoAgente
    {
        private Separation[] targets;
        public float threshold;
        public float decayCoefficient;
        public float maxAcceleration;


        // Start is called before the first frame update
        void Start()
        {
            targets = Object.FindObjectsOfType<Separation>();
        }

        // Update is called once per frame
        public override Direccion GetDireccion()
        {
            Direccion result = new Direccion();
            result.angular = 0;
            result.lineal = Vector3.zero;
            foreach(Separation target in targets)
            {
                if(target!= this)
                {
                    Vector3 dir = target.GetComponent<Agente>().transform.position - agente.transform.position;
                    float distance = dir.magnitude;
                    if (distance < threshold)
                    {
                        var strength= Mathf.Min(decayCoefficient/Mathf.Pow(distance,2),maxAcceleration); //aqui pondre min no se que no se cuantos
                        dir.Normalize();
                        result.lineal += strength * dir;
                        result.angular = 0;
                    }

                }
            }
            return result;

        }
    }
}