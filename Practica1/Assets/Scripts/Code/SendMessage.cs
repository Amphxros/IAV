using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IAV
{
    public enum MESSAGE_ID { TOCAR_FLAUTA, NO_TOCARFLAUTA } //tipos de mensaje
    public class SendMessage : MonoBehaviour
    {
        private ReceiveMessage[] receives; // lista de Receptores 
        void Start()
        {
            receives = Object.FindObjectsOfType<ReceiveMessage>(); //busca en la escena todos los GO con el componente ReceiveMessage
        }

        //envia los mensajes a sus receptores
        public void Send(MESSAGE_ID id)
        {
            //manda a cada un de los Receivers un mensaje id
            foreach (ReceiveMessage m in receives)
            {
                m.Receive(id); 
            }
        }
    }
}