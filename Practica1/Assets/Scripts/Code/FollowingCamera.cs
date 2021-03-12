using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    public Transform target; //target al que seguir
    private Vector3 offset; //distancia de margen entre el target y la camara

    public void Start()
    {
        offset = transform.position - target.transform.position; //calculamos el offset
    }

    public void LateUpdate()
    {
        transform.position = target.transform.position + offset; //movemos el objeto
    }
}
