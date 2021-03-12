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
                        GetComponent<Arrive>().enabled = false;
                        GetComponent<Leave>().enabled = true;
                    }
                    if (gameObject.CompareTag("Rat"))
                    {
                       GetComponent<KinematicWander>().enabled = false;
                        GetComponent<Arrive>().enabled = true;
                        GetComponent<Leave>().enabled = false;
                       // GetComponent<Merodear>().enabled = false;
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
                        GetComponent<Arrive>().enabled = true;
                        GetComponent<Leave>().enabled = false;
                    }
                    if (gameObject.CompareTag("Rat"))
                    {
                        GetComponent<KinematicWander>().enabled = true;
                        GetComponent<Arrive>().enabled = false;
                        GetComponent<Leave>().enabled = true;
                       // GetComponent < Merodear >().enabled= true;
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