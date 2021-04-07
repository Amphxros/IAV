using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectNodeInGrid : MonoBehaviour
{
    Node mNode;

    public void setNode(Node n)
    {
        Debug.Log("set");
        mNode = n;
    }

    public Node getNode()
    {
        return mNode;
    }
}
