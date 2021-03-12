using UnityEngine;

namespace UCM.IAV.Movimiento
{

    /// <summary>
    /// Clase para modelar el comportamiento de SEGUIR a otro agente
    /// </summary>
    public class Arrive : ComportamientoAgente
    {
        /// <summary>
        /// Obtiene la dirección
        /// </summary>
        /// <returns></returns>
        /// 

        public float slowRadius;        //Radio cercano al objetivo en el que la entidad empieza a decelerar
        public float targetRadius;      //Radio que se considera que la entidad ha llegado al objetivo
        public float timeToTarget;      //Tiempo al objetivo constante
        bool onTarget;

        public void setOnTarget(bool b) { onTarget = b; }
        public bool getOnTarget() { return onTarget; }

        public void Start()
        {
            onTarget = false;
        }

        //Sobreescribe el método GetDireccion que utiliza ComportamientoAgente
        public override Direccion GetDireccion()
        {
            float targetSpeed;
            UnityEngine.Vector3 targetVelocity;

            Direccion result = new Direccion();
            UnityEngine.Vector3 direction = (objetivo.transform.position - transform.position);

            // Si esta dentro del "targetRadius" ha llegado a su destino
            if (direction.magnitude < targetRadius)
            {
                result.lineal = Vector3.left;
                result.angular = Vector3.Angle(transform.position , transform.position +agente.velocidad);
                if (!onTarget)
                    onTarget = true;
                return result;
            }
            // Si esta dentro del "slowRadius" reduce su velocidad
            if (direction.magnitude < slowRadius)
            {
                targetSpeed = agente.velocidadMax * (direction.magnitude / slowRadius);
            }
            else targetSpeed = agente.velocidadMax;

            if (onTarget)
                onTarget = false;

            targetVelocity = direction;
            targetVelocity.Normalize();
            targetVelocity *= targetSpeed;

            // Aceleración a la velocidad objetivo
            result.lineal = targetVelocity - agente.velocidad;
            result.lineal /= timeToTarget;

            // Comprueba si la acceleración está por encima del máximo
            if (result.lineal.magnitude > agente.aceleracionMax)
            {
                result.lineal.Normalize();
                result.lineal *= agente.aceleracionMax;
            }

            result.angular = Vector3.Angle(agente.transform.position,agente.transform.position+ result.lineal);
            return result;
        }
    }
}
