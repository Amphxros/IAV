using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCM.IAV.Movimiento
{
    public class MerodeoCinematico : ComportamientoAgente
    {
        public float maxSpeed;
        public float maxRotation;
        /// <summary>
        /// Tiempo en segundos para cambiar de direccion
        /// </summary>
        public float time;

        /// <summary>
        /// Guarda la direccion hacia la que va la rata antes de girar
        /// </summary>
        private Direccion prevDireccion;

        public override void Awake()
        {
            prevDireccion = new Direccion();
            base.Awake();
        }

        /// <summary>
        /// Resta a t hasta que sea <= a 0
        /// Luego crea una nueva direccion aleatoria en los ejes x, z y actualiza dirAnterior
        /// Finalmente crea un nuevo intervalo de tiempo hasta el siguiente cambio
        /// </summary>
        /// <returns></returns>
        public override Direccion GetDireccion()
        {
            Direccion direccion = new Direccion();
            time -= Time.deltaTime;
            if (time <= 0.0f)
            {
                direccion.lineal.x = Random.Range(-1.0f, 1.0f) * maxSpeed;
                direccion.lineal.z = Random.Range(-1.0f, 1.0f) * maxRotation;
                prevDireccion = direccion;
                time = Random.Range(1.0f, 3.0f);
            }
            else
            {
                direccion = prevDireccion;
            }

            direccion.lineal.Normalize();
            direccion.lineal *= agente.aceleracionMax;

            return direccion;
        }
    }
}
