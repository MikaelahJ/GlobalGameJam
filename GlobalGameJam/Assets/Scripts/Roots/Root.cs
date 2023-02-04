using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour
{
    public Node parent;
    public Node child;

    public List<GameObject> subroots;
    public List<Sprite> rootSprites;

    Vector2 start;

    public void SpawnSubRoots(GameObject subroot, Node start, Vector2 oneSubrootDistance, int length)
    {
        int randomRoot = 0;
        int previousRoot = 0;

        for (int i = 0; i < length; i++)
        {
            GameObject newSubRoot = Instantiate(subroot, (Vector2)start.transform.position + oneSubrootDistance/2 + (oneSubrootDistance * i), Quaternion.identity, transform);
            while (randomRoot == previousRoot && rootSprites.Count > 1)
                randomRoot = Random.Range(0, rootSprites.Count);

            subroot.GetComponent<SpriteRenderer>().sprite = rootSprites[randomRoot];
            previousRoot = randomRoot;

            newSubRoot.name = "Subroot" + i;
            newSubRoot.transform.up = oneSubrootDistance;
            subroots.Add(newSubRoot);
        }
    }
}
