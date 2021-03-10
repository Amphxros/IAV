﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                        GetComponent<MerodeoCinematico>().enabled = false;
                        //GetComponent<Seguir>().enabled = true;
                        GetComponent<Alinear>().enabled = true;
                        GetComponent<Llegar>().enabled = true;
                        GetComponent<EvadeObstacles>().enabled = false;
                    }
                    break;

                case MESSAGE_ID.NO_TOCARFLAUTA:
                    if (gameObject.CompareTag("Dog"))
                    {
                        GetComponent<Seguir>().enabled = false;
                    }
                    if (gameObject.CompareTag("Rat"))
                    {
                        GetComponent<MerodeoCinematico>().enabled = true;
                        //GetComponent<Seguir>().enabled = false;
                        GetComponent<Alinear>().enabled = false;
                        GetComponent<Llegar>().enabled = false;
                        GetComponent<EvadeObstacles>().enabled = true;
                    }
                    break;

            }

        }
    }
}