using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour
{
    public Node parent;
    public Node child;

    public List<GameObject> subroots;

    Vector2 start;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnSubRoots(GameObject subroot, Node start, Vector2 oneSubrootDistance, int length)
    {
        for (int i = 1; i < length; i++)
        {
            GameObject newSubRoot = Instantiate(subroot,(Vector2)start.transform.position + (oneSubrootDistance * i), Quaternion.identity, transform);
            newSubRoot.name = "Subroot" + i;
            newSubRoot.transform.up = oneSubrootDistance;
            subroots.Add(newSubRoot);
        }
    }
}
