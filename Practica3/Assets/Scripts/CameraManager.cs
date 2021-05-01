using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject[] cameras = new GameObject[3];

  
    void Start()
    {
        cameras[0].GetComponent<Camera>().enabled = true;   
        cameras[1].GetComponent<Camera>().enabled =false;   
        cameras[2].GetComponent<Camera>().enabled =false;    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {

            cameras[0].GetComponent<Camera>().enabled = true;
            cameras[1].GetComponent<Camera>().enabled = false;
            cameras[2].GetComponent<Camera>().enabled = false;
          
        }   
        else if (Input.GetKeyUp(KeyCode.O))
        {

            cameras[0].GetComponent<Camera>().enabled = false;
            cameras[1].GetComponent<Camera>().enabled = true;
            cameras[2].GetComponent<Camera>().enabled = false;
        }   
        else if (Input.GetKeyUp(KeyCode.I))
        {

            cameras[0].GetComponent<Camera>().enabled = false;
            cameras[1].GetComponent<Camera>().enabled = false;
            cameras[2].GetComponent<Camera>().enabled = true;
        }   
    }
}
