using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    private string type;
    private byte id;
    private float yield;

    public Resource(string _type, byte _id, float _yield)
    {
        type = _type;
        id = _id;
        yield = _yield;
    }
    public string getType()
    {
        return type;
    }
    public byte getId()
    {
        return id;
    }
    public float getYield()
    {
        return yield;
    }
}
