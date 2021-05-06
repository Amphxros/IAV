using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorDesigner.Runtime;

public class Cantante : MonoBehaviour
{
    public NavMeshAgent meshAgent;
    public GameObject fantasma;
    public GameObject vizconde;
    public GameObject celda;

    void FixedUpdate()
    {
        var inmovilizada = (SharedBool)GlobalVariables.Instance.GetVariable("DivaInmovilizada");
        var encarcelada = (SharedBool)GlobalVariables.Instance.GetVariable("DivaEncarcelada");
        var secuestrada = (SharedBool)GlobalVariables.Instance.GetVariable("DivaSecuestrada");

        if (inmovilizada.Value)
        {
            if (secuestrada.Value)
            {
                if (gameObject.activeSelf)
                {
                    gameObject.SetActive(false);
                    transform.position = celda.transform.position;
                }
            }
            if (encarcelada.Value)
            {
                if (!gameObject.activeSelf)
                {
                    gameObject.SetActive(true);
                    meshAgent.enabled = false;
                }
            }
        }
    }
}
