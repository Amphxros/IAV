using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCM.IAV.Movimiento
{
    public class KinematicWander : ComportamientoAgente
    {
        // Variable de tiempo para cambiar direccion
        public float time;

        // Var para guardar la direccion preexistente del agente
        private Direccion prevDireccion;

        public override void Awake()
        {
            prevDireccion = new Direccion();
            base.Awake();
        }

        // Resta a time hasta ser menor o igual a 0.0f para crear una nueva direccion para la rata
        // luego guarda la nueva direccion y reinicia el time
        public override Direccion GetDireccion()
        {
            Direccion result = new Direccion();
            time -= Time.deltaTime;
            // Si time es menor o igual a 0 calcula una nueva direccion a la cual trasladarse
            if (time <= 0.0f)
            {
                result.lineal = new Vector3(Random.onUnitSphere.x * 3f, 0, Random.onUnitSphere.z * 3f);
                prevDireccion = result;
                time = Random.Range(1f, 3f);
            }
            // Caso contrario mantiene la direccion que ya tiene
            else
            {
                result = prevDireccion;
            }

            result.lineal.Normalize();
            result.lineal *= agente.aceleracionMax;
            result.angular = 0;

            return result;
        }
    }
}
