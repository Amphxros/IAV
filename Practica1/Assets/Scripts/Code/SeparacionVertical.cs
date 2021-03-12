using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCM.IAV.Movimiento
{
    public class SeparacionVertical : ComportamientoAgente
    {
        public Agente[] targets; //lista de agentes
        public float radius; //radio
        private Agente character; //Componente agente del GO

        void Start()
        {
            character = GetComponent<Agente>(); //obtenemos el componente
        }

        public override Direccion GetDireccion()
        {
            float shortestTime = Mathf.Infinity;
            Agente firstTarget = null; //primer agente 
            float firstMinSeparation = 0;
            float firstDistance = 0;

            //vectores
            Vector3 firstRelativePos = Vector3.zero;
            Vector3 firstRelativeVel = Vector3.zero;
            Vector3 relativePos;
            Vector3 relativeVel;
         
            //floats
            float relativeSpeed;
            float timeToCollision;
            float distance;
            float minSeparation;

            //por cada agente de la lista
            foreach (Agente a in targets)
            {
                //calcula las posiciones y velocidades relativas
                relativePos = a.transform.position - character.transform.position;
                relativeVel = a.velocidad - character.velocidad;
                relativeSpeed = relativeVel.magnitude;

                //calcula el tiempo de colision, distancia y la separacion minima necesaria
                timeToCollision = Vector3.Dot(relativePos, relativeVel) / (relativeSpeed * relativeSpeed);
                distance = relativePos.magnitude;
                minSeparation = distance - relativeSpeed * timeToCollision;
                if (minSeparation > 2 * radius)
                {
                    continue;
                }

                if (timeToCollision > 0 && timeToCollision < shortestTime)
                {
                    shortestTime = timeToCollision;
                    firstTarget = a;
                    firstMinSeparation = minSeparation;
                    firstDistance = distance;
                    firstRelativePos = relativePos;
                    firstRelativeVel = relativeVel;
                }
            }

            if (firstTarget == null)
            {
                return null;
            }
            else if (firstMinSeparation <= 0.0f || firstDistance < 2 * radius)
            {
                relativePos = firstTarget.transform.position - character.transform.position; //cambia su posicion relativa respecto al lider
            }
            else
            {
                relativePos = firstRelativePos + firstRelativeVel * shortestTime;
            }
            relativePos.Normalize();
            
            Direccion direccion = new Direccion();
            direccion.lineal = relativePos * character.aceleracionMax;
            direccion.angular = 0;

            return direccion;
        }
    }

}