namespace UCM.IAV.Movimiento
{


    // A efectos practicos el componente Leave es completamente opuesto al Arrive
    public class Leave : ComportamientoAgente
    {
        /// <summary>
        /// Obtiene la dirección
        /// </summary>
        /// <returns></returns>

        public float escapeRadius;  
        public float targetRadius;  // Radio en el que comienza a disminuir su velocidad
        public float timeToTarget;  // Tiempo usado para calcular la aceleracion hacia el objetivo

        public override Direccion GetDireccion()
        {
            float targetSpeed;
            UnityEngine.Vector3 targetVelocity;

            Direccion result = new Direccion(); 

            UnityEngine.Vector3 direction = -1 * (objetivo.transform.position - transform.position);

            //si esta fuera del radio del target no necesita moverse mas
            if (direction.magnitude > targetRadius)
            {
                result.lineal = UnityEngine.Vector3.zero;
                result.angular = 0;
                return result;
            }
            //sin embargo si aun sigue en el radio de escape movera el objeto
            if (direction.magnitude > escapeRadius)
            {
                targetSpeed = agente.velocidadMax * (direction.magnitude / escapeRadius);
            }

            else targetSpeed = agente.velocidadMax;

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

            result.angular = 0;
            return result;
        }
    }
}
