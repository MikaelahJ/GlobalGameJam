using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Node : MonoBehaviour
{
    public Node parent;

    public GameObject RootToParent;

    public List<Node> child;

    public Node(Node parent)
    {
        this.parent = parent;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public Node SpawnRootNode(GameObject rootNode, Vector2 position, Transform parent)
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


    private void OnTriggerEnter2D(Collider2D other)
    {
        //exempel kod för resource detection
        //if(other.gameObject.TryGetComponent(out Resource resource))
        {

        }
    }
}
