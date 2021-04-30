using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace UCM.IAV.Movimiento
{
    public class ComportamientoExpectador : MonoBehaviour
    {
        bool escapar;

        private void Start()
        {
            escapar = false;
        }

        void Update()
        {
            if (escapar)
            {
                if (transform.position.z != -73f)
                {
                    transform.Translate(new Vector3(0, 0, -0.2f));
                }
            }
            else
            {
                if (transform.position.z != -56f)
                {
                    transform.Translate(new Vector3(0, 0, 0.2f));
                }
            }
        }

        public void Actualizar()
        {
            escapar = !escapar;
        }
    }
}