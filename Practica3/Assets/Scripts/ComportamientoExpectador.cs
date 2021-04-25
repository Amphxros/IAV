using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace UCM.IAV.Movimiento
{
    public class ComportamientoExpectador : MonoBehaviour
    {
        public GameObject destination;
        NavMeshAgent navmeshAgent;
        bool moving = false;
        bool huir = false;
        Vector3 origPosition;

        private void Start()
        {
            navmeshAgent = GetComponent<NavMeshAgent>();
            origPosition = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            if (moving)
            {
                if (huir)
                {
                    navmeshAgent.destination = destination.transform.position;
                }
                else navmeshAgent.destination = origPosition;
            }
            if (transform.position == navmeshAgent.destination) moving = false;
        }

        void ExpectadorHuye()
        {
            moving = true;
            huir = !huir;
        }
    }
}