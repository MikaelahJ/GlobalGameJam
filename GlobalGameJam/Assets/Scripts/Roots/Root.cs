using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour
{
    public AudioClip growing;

    public Node parent;
    public Node child;

    public bool isBroken = false;

    public List<GameObject> subroots;
    public Dictionary<int, List<Sprite>> allRootSprites = new Dictionary<int, List<Sprite>>();
    public List<Sprite> lvl1RootSprites;
    public List<Sprite> lvl2RootSprites;
    public List<Sprite> lvl3RootSprites;

     void Awake()
    {
        allRootSprites.Add(1, lvl1RootSprites);
        allRootSprites.Add(2, lvl2RootSprites);
        allRootSprites.Add(3, lvl3RootSprites);
    }

    public void SpawnSubRoots(GameObject subroot, Node start, Vector2 oneSubrootDistance, int length, int level = 1)
    {
        AudioManager.Instance.Play(growing);

        for (int i = 0; i < length; i++)
        {
            GameObject newSubRoot = Instantiate(subroot, (Vector2)start.transform.position + oneSubrootDistance/2 + (oneSubrootDistance * i), Quaternion.identity, transform);

            SetRootSprite(subroot, level);

            newSubRoot.name = "Subroot" + i;
            newSubRoot.transform.up = oneSubrootDistance;
            subroots.Add(newSubRoot);
        }
    }

    void SetRootSprite(GameObject subroot, int level)
    {
        int randomRoot = 0;
        int previousRoot = 0;

        if (!allRootSprites.ContainsKey(level))
        {
            Debug.Log("Sprites for this level could not be found! level " + level);
            return;

        }
        List<Sprite> rootSprites = allRootSprites[level];

        while (randomRoot == previousRoot && rootSprites.Count > 1)
            randomRoot = Random.Range(0, rootSprites.Count);

        subroot.GetComponent<SpriteRenderer>().sprite = rootSprites[randomRoot];
        previousRoot = randomRoot;
    }

    public void LevelUpRoots(int level)
    {
        foreach (GameObject root in subroots)
        {
            SetRootSprite(root, level);
        }
    }
}
