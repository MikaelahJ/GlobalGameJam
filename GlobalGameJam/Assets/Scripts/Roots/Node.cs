using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public enum Abilities
{
    Empty,
    Vision,
    Resources,
    Defence
}

public class Node : MonoBehaviour
{
    public Node parent;
    public GameObject RootToParent;

    public List<Node> children;

    public int level = 1;
    public GameObject canvasUI;
    public List<Abilities> abilities = new List<Abilities>();

    public Node(Node parent)
    {
        this.parent = parent;
    }

    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i < level; i++)
        {
            abilities.Add(Abilities.Empty);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DisplayUI()
    {
        foreach (Transform child in canvasUI.transform)
        {
            child.gameObject.SetActive(!child.gameObject.activeSelf);
        }
    }

    public void AddAbility(Abilities newAbility)
    {
        if (abilities.Contains(newAbility))
        {
            Debug.Log("Ability already exists!");
            return;
        }
        for (int i = 0; i < abilities.Count; i++)
        {
            if (abilities[i] == Abilities.Empty)
            {
                Debug.Log("Added new ability: " + newAbility);
                abilities[i] = newAbility;
                return;
            }
        }
        Debug.Log("Couldn't add "+ newAbility + ", no slots empty");
    }

    public void UpgradeNode()
    {
        if(parent.level <= level)
        {
            Debug.Log("Parent node too low level!");
            return;
        }
        Debug.Log("Upgraded node");
        level++;
        abilities.Add(Abilities.Empty);
    }

    public Node SpawnRootNode(GameObject rootNode, Vector2 position, Transform parent)
    {
        GameObject newRootNode = Instantiate(rootNode, (Vector2)transform.position + position, Quaternion.identity, parent);
        newRootNode.name = "Node";

        Node newNode = newRootNode.GetComponent<Node>();
        newNode.parent = this;
        children.Add(newNode);

        return newNode;
    }

    public void DestroyNode()
    {
        RootManager.Instance.rootNodes.Remove(this);
        parent.children.Remove(this);
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
