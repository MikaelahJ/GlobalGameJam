using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootManager : MonoBehaviour
{
    public static RootManager Instance;
    public GameObject rootNode;
    public GameObject root;
    public GameObject subroot;

    float lengthOfSubroot = 1;

    public List<GameObject> roots;
    public List<Node> rootNodes;

    public Node selectedNode;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //float lengthofroot = subroot.GetComponent<SpriteRenderer>().size.y;
            SpawnRoot(selectedNode, cursorPos);
        }
        if (Input.GetMouseButtonDown(1))
        {
            rootNodes.Remove(selectedNode);
            selectedNode.DestroyNode();
            selectedNode = rootNodes[rootNodes.Count - 1];
        }
    }

    void SpawnRoot(Node startNode, Vector2 endPosition)
    {
        Vector2 fromStartToEnd = endPosition - (Vector2)startNode.transform.position;

        Vector2 oneSubrootDistance = Vector2.ClampMagnitude(fromStartToEnd, lengthOfSubroot);
        int length = Mathf.FloorToInt(fromStartToEnd.magnitude / oneSubrootDistance.magnitude);

        Node newNode = startNode.SpawnRootNode(oneSubrootDistance * length, transform);
        selectedNode = newNode;

        rootNodes.Add(newNode);

        GameObject newRoot = Instantiate(root, selectedNode.transform.position, Quaternion.identity, transform);
        newNode.RootToParent = newRoot;

        Root rootScript = newRoot.GetComponent<Root>();
        rootScript.SpawnSubRoots(subroot, startNode, oneSubrootDistance, length);
        rootScript.parent = startNode;
        rootScript.child = newNode;

        roots.Add(newRoot);
        newRoot.name = "Root" + roots.Count;

    }
}
