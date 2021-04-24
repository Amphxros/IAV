using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCM.IAV.Movimiento
{
    public class ComportamientoExpectador : MonoBehaviour
    {
        bool huir = false;

        // Si una lampara se cae los expectadores huiran
        public void LamparaCaida()
        {
            huir = true;
        }

        public void LamparaReparada()
        {
            huir = false;
        }

        // Update is called once per frame
        void Update()
        {
            //Si estan huyendo, los expectadores saldran del palco al patio. Caso contrario regresaran
            if (huir)
            {
                if (transform.position.z >= -25)
                    transform.Translate(new Vector3(0, 0, -0.2f));
            }
            else
            {
                if (transform.position.z <= -12.7f)
                {
                    transform.Translate(new Vector3(0, 0, 0.2f));
                }
            }
        }
    }
}