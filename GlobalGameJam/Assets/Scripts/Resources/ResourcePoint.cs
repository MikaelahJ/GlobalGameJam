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
        id = _id;
    }
    public int getId()
    {
        return id;
    }
    public float getYield()
    {
        return yield;
    }
    public Resource pumpOut()
    {
        if (remainingResource < 0) alive = false;
            return new Resource(type, id, yield);
    }
    public bool isAlive()
    {
        return alive;
    }
    public void drain(float drainAmount)
    {
        remainingResource -= drainAmount;
        if (remainingResource < 0) alive = false;
    }

}
