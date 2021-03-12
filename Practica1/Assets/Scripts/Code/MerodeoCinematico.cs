using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCM.IAV.Movimiento
{
    public class MerodeoCinematico : ComportamientoAgente
    {
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
            Direccion result = new Direccion();
            time -= Time.deltaTime;
            if (time <= 0.0f)
            {
                result.lineal = new Vector3(Random.onUnitSphere.x * 3, 0, Random.onUnitSphere.z * 3f);
                prevDireccion = result;
                time = Random.Range(1f, 3f);
            }
            else
            {
                result = prevDireccion;
            }

            result.lineal.Normalize();
            result.lineal *= agente.aceleracionMax;

            return result;
        }
    }
}
