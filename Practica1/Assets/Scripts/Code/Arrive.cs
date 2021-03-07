using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
algoritmo obtenido en el libr de Ian Millington: AI for games 3rd edition
class Arrive{
    character: Kinematic
    target: Kinematic

    maxAccel: float
    maxSpeed: float

    slowRd: float
    timeToTarget: float

    function getSteering()-> SteeringOutput{
    result= new SteeringOutput
    direction=target.pos-character.pos
    distance= direction.length
    if(distance<targetRd){
    return;
    }
    ...

    }



}
     
*/
public class Arrive : MonoBehaviour
{
    Transform character;
    public Transform target;

    public float maxAccel;
    public float maxSpeed;

    public float slowRd;
    public float timeToTarget;

    private Vector3 direction_;
    private Vector3 distance_;


    // Start is called before the first frame update
    void OnEnable()
    {
        character = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
