using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfluenceMap : MonoBehaviour
{
    public int dimensionX, dimensionZ;

    private Vector3[,] points; 
    // Start is called before the first frame update
    void Start()
    {
        points = new Vector3[dimensionX, dimensionZ];



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
