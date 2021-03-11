using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCM.IAV.Movimiento
{
    public class Separacion : ComportamientoAgente
    {
        public Agente[] targets;
        public float threshold;
        public float decayCoefficient;
        private Agente character;

        void Start()
        {
            character = GetComponent<Agente>();
        }

        public override Direccion GetDireccion()
        {
            Direccion direccion = new Direccion();
            Vector3 direc;
            float distance;
            float strength;
            foreach (Agente a in targets)
            {
                direc = a.transform.position - character.transform.position;
                distance = direc.magnitude;
                if (distance < threshold)
                {
                    strength = Mathf.Min(decayCoefficient / (distance * distance), character.aceleracionMax);
                    direc.Normalize();
                    direccion.lineal += strength * direc;
                }
            }
            return direccion;
        }
    }

}