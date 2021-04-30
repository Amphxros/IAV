using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCM.IAV.Movimiento
{
    public class Lampara : MonoBehaviour
    {
        public ComportamientoExpectador ComportamientoExpectador;
        public CaidaLampara Izquierda;
        public CaidaLampara Derecha;

        private void Update()
        {
            if ((Izquierda.LamparaHaCaido() && Derecha.LamparaHaCaido()) || (!Izquierda.LamparaHaCaido() && !Derecha.LamparaHaCaido()))
            {
                ComportamientoExpectador.Actualizar();
            }
        }
    }
}