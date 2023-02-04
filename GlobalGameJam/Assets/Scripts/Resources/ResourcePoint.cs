using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcePoint : MonoBehaviour
{
    public float remainingResource;
    public string type;
    public float yield;
    public int id;
    public bool alive = true;

    public void setId(int _id)
    {
        id = id;
    }
    public Resource pumpOut()
    {
       
            remainingResource -= yield;
        if (remainingResource < 0) alive = false;
            return new Resource(type, id, yield);
    }
    public bool isAlive()
    {
        return alive;
    }

}
