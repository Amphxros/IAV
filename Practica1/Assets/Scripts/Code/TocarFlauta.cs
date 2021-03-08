using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IAV
{
    public class TocarFlauta : MonoBehaviour
    {

        private SendMessage message; // componente que envia un mensaje

        void Start()
        {
            message = GetComponent<SendMessage>();
        }

        void Update()
        {
            //si toca la flauta envia el mensaje correspondiente
            if (Input.GetKey(KeyCode.Space))
            {
                message.Send(MESSAGE_ID.TOCAR_FLAUTA);
            }
            else
            {
                message.Send(MESSAGE_ID.NO_TOCARFLAUTA);
            }

        }
    }
}