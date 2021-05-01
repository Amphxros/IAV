using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace UCM.IAV.Movimiento
{
    public class ComportamientoExpectador : MonoBehaviour
    {
        public GameObject patio;
        Vector3 posIni;
        bool escapar;

        NavMeshAgent agent;

        private void Start()
        {
            escapar = false;
            posIni = transform.position;
            agent = GetComponent<NavMeshAgent>();
            agent.destination = posIni;
            agent.enabled = true;
        }

        void Update()
        {
            if (escapar)
            {
                agent.destination = patio.transform.position;
            }
            else
            {
                agent.destination = posIni;
            }
        }

        public void Actualizar()
        {
            escapar = !escapar;
        }
    }
}