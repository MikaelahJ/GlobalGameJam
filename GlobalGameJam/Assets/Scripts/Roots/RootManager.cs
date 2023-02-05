using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootManager : MonoBehaviour
{
    public static RootManager Instance;
    [SerializeField] private GameObject rootNode;
    [SerializeField] private GameObject root;
    [SerializeField] private GameObject subroot;

    float lengthOfSubroot = 1;

    public List<GameObject> roots;
    public List<Node> rootNodes;

    public Node selectedNode;
    public Sprite leekSprite;


    private LineRenderer lineRenderer;

    public List<Node> isResourceNode = new List<Node>();

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        lengthOfSubroot = subroot.GetComponent<SpriteRenderer>().sprite.rect.height / subroot.GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
        Debug.Log("Length of root in units: " + lengthOfSubroot);
        lineRenderer = GetComponent<LineRenderer>();

        if (selectedNode == null)
        {
            GameObject leek = Instantiate(rootNode);
            leek.name = "leek0";
            selectedNode = leek.GetComponent<Node>();
            selectedNode.level = 3;
            selectedNode.abilities.Add(Abilities.Resources);
            leek.GetComponent<SpriteRenderer>().sprite = leekSprite;

        }
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //        //SpawnRoot(selectedNode, cursorPos);
    //    }
    //    //if (Input.GetMouseButtonDown(1))
    //    //{
    //    //    rootNodes.Remove(selectedNode);
    //    //    selectedNode.DestroyNode();
    //    //    selectedNode = rootNodes[rootNodes.Count - 1];
    //    //}
    //}

    public void CheckAllResourceConnections()
    {
        List<Node> connectedNodes = new List<Node>();
        foreach (Node node in isResourceNode)
        {
            if (node.CheckIfConnectedToLeek())
            {
                connectedNodes.Add(node);
            }
            else
            {
                foreach (ResourcePoint resourcePoint in node.closeResources)
                {
                    ResourceManager.Instance.removeResource(resourcePoint.pumpOut());
                }
            }
        }

        foreach (Node node in connectedNodes)
        {
            foreach (ResourcePoint resourcePoint in node.closeResources)
            {
                ResourceManager.Instance.addResources(resourcePoint.pumpOut());
            }
        }

    }

    public void SpawnRoot()
    {
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        SpawnRoot(selectedNode, cursorPos);
    }

    public void SpawnRoot(Node startNode, Vector2 endPosition)
    {
        ClearPreview();
        Vector2 fromStartToEnd = endPosition - (Vector2)startNode.transform.position;

        Vector2 oneSubrootDistance = Vector2.ClampMagnitude(fromStartToEnd, lengthOfSubroot);
        int length = Mathf.FloorToInt(fromStartToEnd.magnitude / oneSubrootDistance.magnitude);
        Debug.Log(length);
        if (fromStartToEnd.magnitude == oneSubrootDistance.magnitude)
        {
            Debug.Log("Couldn't spawn root! Too close to selected node!");
            return;
        }
        int cost = ResourceManager.NEW_NODE;
        cost += ResourceManager.NEW_SUBROOT * length;

        if (!ResourceManager.Instance.CanBuyUpgrade(cost))
        {
            Debug.Log("Can't afford!");
            return;
        }

        Node newNode = startNode.SpawnRootNode(rootNode, oneSubrootDistance * length, transform);
        if (newNode == null)
        {
            //selectedNode = newNode;
            Debug.Log("No more roots allowed!");
            return;
        }
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

    public void DrawPreview()
    {
        //Debug.Log("drawing Preview");
        lineRenderer.enabled = true;

        Vector2 endPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lineRenderer.SetPosition(0, selectedNode.transform.position);
        Vector2 fromStartToEnd = endPosition - (Vector2)selectedNode.transform.position;

        Vector2 oneSubrootDistance = Vector2.ClampMagnitude(fromStartToEnd, lengthOfSubroot);
        int length = Mathf.FloorToInt(fromStartToEnd.magnitude / oneSubrootDistance.magnitude);

        lineRenderer.SetPosition(1, selectedNode.transform.position + (Vector3)(oneSubrootDistance * length));
    }

    public void ClearPreview()
    {
        lineRenderer.enabled = false;
    }
}
