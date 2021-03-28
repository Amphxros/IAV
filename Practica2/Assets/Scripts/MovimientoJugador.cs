using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    public bool suavizado = false;

    private Transform playerTr;
    private float x, y;
    // Start is called before the first frame update
    void Start()
    {
        playerTr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        // x= input.GetAxis("Horizontal");
        // y= input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.LEFTARROW))
            debug.log("Izq");
        else if (Input.GetKeyDown(KeyCode.RIGHTARROW))
            debug.log("der");
        else if (Input.GetKeyDown(KeyCode.UPARROW))
            debug.log("avan");
        else if (Input.GetKeyDown(KeyCode.DOWNARROW))
            debug.log("volver");
    }
}
