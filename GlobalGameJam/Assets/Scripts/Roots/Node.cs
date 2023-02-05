using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using TMPro;

public enum Abilities
{
    Empty,
    Vision,
    Resources,
    Defence
}

public class Node : MonoBehaviour
{
    public AudioClip snapAttack;

    public Node parent;
    public GameObject RootToParent;

    public List<Node> children;
    public int maxAmountOfChildren = 3;

    public int level = 1;
    public GameObject canvasUI;
    public List<Abilities> abilities = new List<Abilities>();

    public GameObject vision;

    public List<ResourcePoint> closeResources = new List<ResourcePoint>();
    public List<Node> validToAddResource = new List<Node>();

    private bool isDefence;
    private bool isAttacking;
    private int damage;
    private float timer;
    private float timeBetweenAttacks;
    private GameObject enemyInRange;

    [SerializeField] private TextMeshProUGUI resourceText;
    [SerializeField] private TextMeshProUGUI upgradeText;
    [SerializeField] private TextMeshProUGUI visionText;
    [SerializeField] private TextMeshProUGUI defenseText;
    [SerializeField] private TextMeshProUGUI repairText;

    public Node(Node parent)
    {
        this.parent = parent;
    }

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
                AudioManager.Instance.EffectsSource.PlayOneShot(snapAttack);
                enemyInRange.GetComponent<Mole>().LoseHealth(damage);
                enemyInRange.GetComponent<Mole>().animator.SetTrigger("Hit");
                timer = 0f;
            }
        }
    }

    public void DisplayUI()
    {
        DisplayUI(!canvasUI.transform.GetChild(0).gameObject.activeSelf);
    }

    public void DisplayUI(bool enabled)
    {
        foreach (Transform child in canvasUI.transform)
        {
            child.gameObject.SetActive(enabled);
        }

        UpdateResourceCost();
        UpdateRepairCost();

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
                switch (newAbility)
                {
                    case Abilities.Vision:
                    {
                            if (CanBuyUpgrade(ResourceManager.UPGRADE_VISION))
                            {
                                abilities[i] = newAbility;
                                AddVision();
                                break;
                            }

                            return;
                    }
                    case Abilities.Resources:
                    {
                            validToAddResource.Clear();
                            if (TryAddResources(this))
                            {
                                if(CanBuyUpgrade(validToAddResource.Count * ResourceManager.UPGRADE_RESOURCE))
                                {
                                    Debug.Log("Added resource ability");

                                    foreach (Node node in validToAddResource)
                                    {
                                        AddResourceAbility(node);
                                    }
                                }

                                break;
                            }
                            Debug.Log("Could not add resource ability");
                            return;
                    }
                    case Abilities.Defence:
                    {
                            if (CanBuyUpgrade(ResourceManager.UPGRADE_VISION))
                            {

                                abilities[i] = newAbility;
                                SetAsDefenceNode();
                                break;
                            }
                            return;
                    }
                }
                Debug.Log("Added new ability: " + newAbility);
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

    void AddVision()
    {
        GameObject newVision = Instantiate(vision);
        newVision.transform.position = transform.position;
        newVision.SetActive(true);
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

    private bool TryAddResources(Node current)
    {
        if (current.abilities.Contains(Abilities.Resources))
        {
            return false;
        }

        if (current.parent.abilities.Contains(Abilities.Resources))
        {
            validToAddResource.Add(current);
            //AddResourceAbility(current);
            return true;
        }
        else if (current.parent.abilities.Contains(Abilities.Empty))
        {
            if (TryAddResources(current.parent))
            {
                validToAddResource.Add(current);
                //AddResourceAbility(current);
                return true;
            }
        }
        return false;
    }

    private void AddResourceAbility(Node current)
    {
        for (int i = 0; i < current.abilities.Count; i++)
        {
            if (current.abilities[i] == Abilities.Empty)
            {
                current.abilities[i] = Abilities.Resources;

                RootManager.Instance.isResourceNode.Add(this);

                foreach (ResourcePoint resourcePoint in current.closeResources)
                {
                    TryConnectResourcePoint(resourcePoint, current);
                }

                return;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collided with trigger");
        if(other.gameObject.TryGetComponent(out ResourcePoint resourcePoint))
        {
            Debug.Log("found resource");
            closeResources.Add(resourcePoint);
            TryConnectResourcePoint(resourcePoint, this);
            
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

    void TryConnectResourcePoint(ResourcePoint resourcePoint, Node node)
    {
        if (CheckIfConnectedToLeek(node))
            ResourceManager.Instance.addResources(resourcePoint.pumpOut());
    }

    public bool CheckIfConnectedToLeek()
    {
        return CheckIfConnectedToLeek(this);
    }

    public bool CheckIfConnectedToLeek(Node current)
    {
        if(current.parent == null)
        {
            Debug.Log("Resource is connected to leek");
            return true;
        }
        if (current.RootToParent.GetComponent<Root>().isBroken)
        {
            Debug.Log("Above root is broken at " + current.transform.position);
            return false;
        }
        if (!current.abilities.Contains(Abilities.Resources))
        {
            Debug.Log("Node can't transport resources at " + current.transform.position);
            return false;
        }
        return CheckIfConnectedToLeek(current.parent);
    }

    bool CanBuyUpgrade(int cost)
    {
        return ResourceManager.Instance.CanBuyUpgrade(cost);
    }

    public string GetResourceCost()
    {
        validToAddResource.Clear();
        if (TryAddResources(this))
        {
            return (validToAddResource.Count * ResourceManager.UPGRADE_RESOURCE).ToString();
        }

        return "X";
    }

    public void UpdateResourceCost()
    {
        resourceText.text = GetResourceCost();
    }


    public void UpdateRepairCost()
    {
        if(parent == null) { return; }
        repairText.text = "Repair: " + RootToParent.GetComponent<Root>().GetRepairCost().ToString();
    }

    public void RepairRoot()
    {
        int cost = RootToParent.GetComponent<Root>().GetRepairCost();
        if (ResourceManager.Instance.CanBuyUpgrade(cost))
        {
            Debug.Log("Repaired roots");
            RootToParent.GetComponent<Root>().RepairRoots();
            return;
        }
        Debug.Log("Can't afford to repair roots");
    }
}
