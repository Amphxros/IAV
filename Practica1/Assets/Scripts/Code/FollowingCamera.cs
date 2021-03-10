using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    public Transform target;
    private Vector3 offset;

    public void Start()
    {
        offset = transform.position - target.transform.position;
    }

    public void LateUpdate()
    {
        transform.position = target.transform.position + offset;
    }
}
