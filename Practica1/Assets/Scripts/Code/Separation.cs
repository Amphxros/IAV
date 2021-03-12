using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCM.IAV.Movimiento
{
    public class Separation : ComportamientoAgente
    {
        // Lista de posibles targets inteligentes (objetivos)
        private Separation[] targets;
        // Umbral sobre el cual tomar accion
        public float threshold;
        // Constante de coeficiente de caida para la ISL (Inverse Square Law)
        public float decayCoefficient;
        // Aceleracion maxima del agente
        public float maxAcceleration;

        void Start()
        {
            targets = Object.FindObjectsOfType<Separation>();
        }

        public override Direccion GetDireccion()
        {
            Direccion result = new Direccion();
            result.angular = 0;
            result.lineal = Vector3.zero;
            // Se realiza en cada target del vector
            foreach (Separation target in targets)
            {
                if (target != this)
                {
                    // Inspecciona que el target esta cerca
                    Vector3 dir = target.GetComponent<Agente>().transform.position - agente.transform.position;
                    float distance = dir.magnitude;
                    if (distance < threshold)
                    {
                        // Calcula la fuerza de repulsion
                        // utilizando la ISL (Inverse Square Law)
                        var strength = Mathf.Min(decayCoefficient / Mathf.Pow(distance, 2), maxAcceleration);

                        // Adiciona la aceleracion
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