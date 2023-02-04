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
    public int maxAmountOfChildren = 3;

    public int level = 1;
    public GameObject canvasUI;
    public List<Abilities> abilities = new List<Abilities>();

    private bool isDefence;
    private bool isAttacking;
    private int damage;
    private float timer;
    private float timeBetweenAttacks;
    private GameObject enemyInRange;

    public Node(Node parent)
    {
        this.parent = parent;
    }

    // Start is called before the first frame update
    void Start()
    {
        timeBetweenAttacks = 1;
        damage = 2;
        for (int i = 0; i < level; i++)
        {
            abilities.Add(Abilities.Empty);
        }
    }

    void Update()
    {
        if (isDefence)
        {
            timer += Time.deltaTime;
            if (isAttacking && timer >= timeBetweenAttacks)
            {
                enemyInRange.GetComponent<Mole>().LoseHealth(damage);
                timer = 0f;
            }
        }
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
                if (newAbility == Abilities.Defence)
                {
                    SetAsDefenceNode();
                }

                return;
            }
        }
        Debug.Log("Couldn't add " + newAbility + ", no slots empty");
    }

    public void UpgradeNode()
    {
        if (parent.level <= level)
        {
            Debug.Log("Parent node too low level!");
            return;
        }


        Debug.Log("Upgraded node");
        level++;
        RootToParent.GetComponent<Root>().LevelUpRoots(level);
        abilities.Add(Abilities.Empty);
    }

    public Node SpawnRootNode(GameObject rootNode, Vector2 position, Transform parent)
    {
        if (children.Count < maxAmountOfChildren)
        {
            GameObject newRootNode = Instantiate(rootNode, (Vector2)transform.position + position, Quaternion.identity, parent);
            newRootNode.name = "Node";

            Node newNode = newRootNode.GetComponent<Node>();
            newNode.parent = this;
            children.Add(newNode);

            return newNode;
        }
        return null;
    }

    public void DestroyNode()
    {
        RootManager.Instance.rootNodes.Remove(this);
        parent.children.Remove(this);
        RootManager.Instance.roots.Remove(RootToParent);
        Destroy(RootToParent);
        Destroy(gameObject);
    }

    private void SetAsDefenceNode()
    {
        isDefence = true;
        maxAmountOfChildren = 0;

        var hitCollider = Instantiate(new GameObject(), transform);
        hitCollider.AddComponent<CircleCollider2D>();
        hitCollider.GetComponent<CircleCollider2D>().isTrigger = true;
        hitCollider.GetComponent<CircleCollider2D>().radius = 3;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collided with trigger");
        if(other.gameObject.TryGetComponent(out ResourcePoint resourcePoint))
        {
            Debug.Log("found resource");
            //if connectedtoleek
            ResourceManager.Instance.addResources(resourcePoint.pumpOut());
        }

        if (isDefence && other.gameObject.CompareTag("Enemy") && other is CapsuleCollider2D && enemyInRange == null)
        {
            isAttacking = true;
            enemyInRange = other.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isAttacking = false;
        enemyInRange = null;
    }
}
