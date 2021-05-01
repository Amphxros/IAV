using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCM.IAV.Movimiento
{
    public class Lampara : MonoBehaviour
    {
        public ComportamientoExpectador[] expectadores;

        public CaidaLampara Izquierda;
        public CaidaLampara Derecha;

        bool lamparasHanCaido;
        bool lamparasHanVuelto;

        private void Update()
        {
            if (Izquierda.LamparaHaCaido() && Derecha.LamparaHaCaido() && !lamparasHanCaido) {
                lamparasHanCaido = true;
                for (int i = 0; i < expectadores.Length; i++)
                {
                    expectadores[i].Actualizar();
                }
            }
            else if (!Izquierda.LamparaHaCaido() && !Derecha.LamparaHaCaido() && lamparasHanCaido)
            {
                lamparasHanCaido = false;
                for (int i = 0; i < expectadores.Length; i++)
                {
                    expectadores[i].Actualizar();
                }
            }
        }
    }
}