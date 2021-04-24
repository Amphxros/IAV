using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCM.IAV.Movimiento
{
    public class Lampara : MonoBehaviour
    {
        // Start is called before the first frame update
        private Rigidbody rb;
        public bool lamparaCaida; //esta en su sitio
        public Vector3 PosIni;
        AudioSource sonido;

        void Start()
        {
            lamparaCaida = false;
            rb = GetComponent<Rigidbody>();
            sonido = gameObject.GetComponent<AudioSource>();
        }
        public void Caida()
        {
            //Si la lampara cae, se le añade gravedad y hacemos que el publico se vaya
            lamparaCaida = true;
            rb.useGravity = true;
        }
        public void Subida()
        {
            //Ponemos la variable a true y ponemos la lampara en la posicion inicial
            lamparaCaida = false;
            rb.useGravity = false;
            transform.position = PosIni;
        }

        private void Update()
        {
            //Para que las lamparas no se puedan mover cuando estan en el suelo
            if (transform.position.y < 0.55f)
                rb.isKinematic = true;
            else
                rb.isKinematic = false;
        }
    }
}