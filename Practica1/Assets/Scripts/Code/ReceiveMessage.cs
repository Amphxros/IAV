using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UCM.IAV.Movimiento;
namespace IAV
{
    public class ReceiveMessage : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        public void Receive(MESSAGE_ID id)
        {
            switch (id)
            {
                case MESSAGE_ID.TOCAR_FLAUTA:
                    if (gameObject.CompareTag("Dog"))
                    {
                        GetComponent<Seguir>().enabled = true;
                    }
                    if (gameObject.CompareTag("Rat"))
                    {
                        GetComponent<Merodear>().enabled = false;
                        GetComponent<Llegar>().enabled = true;
                        //GetComponent<EvadeObstacles>().enabled = false;
                    }
                    if (gameObject.CompareTag("Text"))
                    {
                        GetComponent<Text>().enabled = false;
                    }
                    if (gameObject.CompareTag("Audio")) {
                        GetComponent<AudioSource>().volume = 1.0f;
                    }
                    break;

                case MESSAGE_ID.NO_TOCARFLAUTA:
                    if (gameObject.CompareTag("Dog"))
                    {
                        GetComponent<Seguir>().enabled = false;
                    }
                    if (gameObject.CompareTag("Rat"))
                    {
                        GetComponent<Merodear>().enabled = true;
                        GetComponent<Llegar>().enabled = false;
                        //GetComponent<EvadeObstacles>().enabled = true;
                    }
                    if (gameObject.CompareTag("Text"))
                    {
                        GetComponent<Text>().enabled = true;
                    }
                    if (gameObject.CompareTag("Audio"))
                    {
                        GetComponent<AudioSource>().volume = 0.0f;
                    }
                    break;

            }

        }
    }
}