using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootHealth : MonoBehaviour
{
    public AudioClip rootBreak;

    private int maxHealth = 10;
    private int health = 10;

    public bool rootDestroyed;
    public void LoseHealth(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            RootDestroyed();
            Debug.Log("Root destroyed");
        }
    }

    private void RootDestroyed()
    {
        AudioManager.Instance.EffectsSource.PlayOneShot(rootBreak);
        rootDestroyed = true;

        GetComponentInParent<Root>().isBroken = true;
        GetComponentInParent<Root>().brokenRoots.Add(this);

        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
    }

    public void RepairRoot()
    {
        health = maxHealth;
        rootDestroyed = false;

        GetComponentInParent<Root>().isBroken = false;
        //GetComponentInParent<Root>().brokenRoots.Remove(this);

        GetComponent<Collider2D>().enabled = true;
        GetComponent<SpriteRenderer>().enabled = true;
    }
}
