using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCM.IAV.Movimiento
{
    public class SeparacionVertical : ComportamientoAgente
    {
        public Agente[] targets;
        public float radius;
        private Agente character;

        void Start()
        {
            character = GetComponent<Agente>();
        }

        public override Direccion GetDireccion()
        {
            float shortestTime = Mathf.Infinity;
            Agente firstTarget = null;
            float firstMinSeparation = 0;
            float firstDistance = 0;
            Vector3 firstRelativePos = Vector3.zero;
            Vector3 firstRelativeVel = Vector3.zero;

            Vector3 relativePos;
            Vector3 relativeVel;
            float relativeSpeed;
            float timeToCollision;
            float distance;
            float minSeparation;
            foreach (Agente a in targets)
            {
                relativePos = a.transform.position - character.transform.position;
                relativeVel = a.velocidad - character.velocidad;
                relativeSpeed = relativeVel.magnitude;

                timeToCollision = Vector3.Dot(relativePos, relativeVel) / (relativeSpeed * relativeSpeed);
                distance = relativePos.magnitude;
                minSeparation = distance - relativeSpeed * timeToCollision;
                if (minSeparation > 2 * radius)
                    continue;
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
            if (firstMinSeparation <= 0.0f || firstDistance < 2 * radius)
            {
                relativePos = firstTarget.transform.position - character.transform.position;
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