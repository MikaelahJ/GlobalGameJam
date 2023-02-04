using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Node : MonoBehaviour
{
    public GameObject rootNode;
    public Node parent;

    public GameObject RootToParent;

    public List<Node> child;

    int childrenAllowed;

    public Node(Node parent)
    {
        this.parent = parent;
    }

    // Start is called before the first frame update
    void Start()
    {
        childrenAllowed = UnityEngine.Random.Range(1, 3);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    if (child.Count < childrenAllowed)
        //        SpawnRootNode((Vector2)transform.position + Vector2.right);
        //}
    }

    public Node SpawnRootNode(Vector2 position, Transform parent)
    {
        GameObject newRootNode = Instantiate(rootNode, (Vector2)transform.position + position, Quaternion.identity, parent);
        newRootNode.name = "Node" + (Int32.Parse(gameObject.name[4..]) + 1);

        Node newNode = newRootNode.GetComponent<Node>();
        newNode.parent = this;
        child.Add(newNode);

        return newNode;
    }

    public void DestroyNode()
    {
        RootManager.Instance.rootNodes.Remove(this);
        parent.child.Remove(this);
        RootManager.Instance.roots.Remove(RootToParent);
        Destroy(RootToParent);
        Destroy(gameObject);
    }
}
