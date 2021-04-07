using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setNodeToObject : MonoBehaviour
{

    void OnTriggerEnter(Collider col)
    {
        ObjectNodeInGrid obj= col.gameObject.GetComponent<ObjectNodeInGrid>();
        if (obj!=null)
        {
            Debug.Log("me cago en todo");
            obj.setNode(getNodeToObj());
        }
    }

    Node getNodeToObj()
    {
        return FindObjectOfType<A_star_Manager>().grid.FindNodeByPosition(this.transform.position);
    }

}
