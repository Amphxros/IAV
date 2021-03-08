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
            receives = Object.FindObjectsOfType<ReceiveMessage>();
        }

        // Esto es para cuando añada un GM que con FInd es muy ineficiente
        public void AddReceiver(ReceiveMessage r)
        {

        }

        //envia los mensajes a sus receptores
        public void Send(MESSAGE_ID id)
        {
            foreach (ReceiveMessage m in receives)
            {
                m.Receive(id);
            }
        }
    }
}