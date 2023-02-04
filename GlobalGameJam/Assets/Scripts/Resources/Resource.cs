using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public string type;
    public int id;
    public float yield;

    public Resource(string _type, int _id, float _yield)
    {
        type = _type;
        id = _id;
        yield = _yield;
    }
    public string getType()
    {
        return type;
    }
    public int getId()
    {
        return id;
    }
    public float getYield()
    {
        return yield;
    }

    public void setType(string _type)
    {
        type = _type;
    }
    public void setId(int _id)
    {
        id = _id;
    }
    public void setYield(float _yield)
    {
        yield = _yield;
    }
}
