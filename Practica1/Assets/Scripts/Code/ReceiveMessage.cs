using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UCM.IAV.Movimiento;
namespace IAV
{
    public class ReceiveMessage : MonoBehaviour
    {
        //metodo que recibe los mensajes que el SendMessage envia y los computa en funcion del tag del objeto actual
        public void Receive(MESSAGE_ID id)
        {
            switch (id)
            {
                //si estamos en el caso de que sea un mensaje de tipo TOCAR_FLAUTA
                case MESSAGE_ID.TOCAR_FLAUTA:
                    // En caso de ser perro cambia sus comportamientos de seguir y huir
                    if (gameObject.CompareTag("Dog"))
                    {
                        GetComponent<Arrive>().enabled = false;
                        GetComponent<Leave>().enabled = true;
                    }
                    // La rata cambia sus comportamientos de merodear y seguir
                   else if (gameObject.CompareTag("Rat"))
                    {
                        GetComponent<KinematicWander>().enabled = false;
                        GetComponent<Arrive>().enabled = true;
                        GetComponent<Leave>().enabled = false;
                    }

                    else if (gameObject.CompareTag("Text"))
                    {
                        GetComponent<Text>().enabled = false;
                    }
                    else if (gameObject.CompareTag("Audio")) {
                        GetComponent<AudioSource>().volume = 1.0f;
                    }
                    break;
                //si estamos en caso de ser un mensaje de tipo NO_TOCARFLAUTA
                case MESSAGE_ID.NO_TOCARFLAUTA:
                    //si es un perro volvera con el objetivo activando su componente Arrive
                    if (gameObject.CompareTag("Dog"))
                    {
                        GetComponent<Arrive>().enabled = true;
                        GetComponent<Leave>().enabled = false;
                    }
                    //si no, en caso de ser una rata
                    else if (gameObject.CompareTag("Rat"))
                    {
                        GetComponent<KinematicWander>().enabled = true;
                        GetComponent<Arrive>().enabled = false;
                        GetComponent<Leave>().enabled = true;
                    }
                   //si es un texto
                   else if (gameObject.CompareTag("Text"))
                   {
                        GetComponent<Text>().enabled = true;
                   }
                   // si es un Audio
                   else if (gameObject.CompareTag("Audio"))
                   {
                        GetComponent<AudioSource>().volume = 0.0f;
                   }
                   break;

            }

        }
    }
}