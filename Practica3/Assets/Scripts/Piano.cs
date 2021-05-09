using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piano : MonoBehaviour
{
    MeshRenderer m;
    private bool enabled;
    // Start is called before the first frame update
    void Start()
    {
        enabled = true;
       // m = GetComponent<MeshRenderer>();
    }
    public void setEnabled(bool b)
    {
        enabled = b;
    }
}
