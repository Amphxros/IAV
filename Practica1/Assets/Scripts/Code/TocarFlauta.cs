using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IAV
{
    public class TocarFlauta : MonoBehaviour
    {
        // componente que envia un mensaje
        private SendMessage message;

        void Start()
        {
            message = GetComponent<SendMessage>();
        }

        void Update()
        {
            // Si toca la flauta envia el mensaje correspondiente
            // Cada ReceiveMessage tiene su comportamiento
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