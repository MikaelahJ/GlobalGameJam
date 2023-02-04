using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public int damage;
    public float speed;
    public float timeBetweenAttacks;
    public Sprite sprite;

    public GameObject targetRoot;

    public bool foundRoot;
    public bool attacking;

    public Enemy(int _health, int _damage, float _speed, float _timeBetweenAttacks)
    {
        health = _health;
        damage = _damage;
        speed = _speed;
        timeBetweenAttacks = _timeBetweenAttacks;
    }

    public void Attack()
    {
        if (targetRoot.GetComponent<RootHealth>().rootDestroyed)
            RootDestroyed();
        else
            targetRoot.GetComponent<RootHealth>().LoseHealth(damage);
    }

    private void RootDestroyed()
    {
        attacking = false;
        foundRoot = false;
    }

    public void LoseHealth(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Debug.Log("dead");
            EnemyManager.Instance.EnemyKilled(this.gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Root") && !foundRoot)
        {
            foundRoot = true;
            targetRoot = collision.gameObject;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Root"))
        {

            attacking = true;
            targetRoot = collision.gameObject;
        }
        else if (collision.gameObject.CompareTag("BoundsBox"))
        {
            EnemyManager.Instance.RemoveEnemy(this.gameObject);
        }
    }
}
