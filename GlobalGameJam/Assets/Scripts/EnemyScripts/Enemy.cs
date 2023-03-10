using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public AudioClip gnawingSound;

    public int health;
    public int damage;
    public float speed;
    public float timeBetweenAttacks;
    public Sprite sprite;

    public GameObject targetRoot;

    public bool foundRoot;
    public bool attacking;
    public bool dying;
    public float deathTimer;
    public bool dead;

    public Enemy(int _health, int _damage, float _speed, float _timeBetweenAttacks)
    {
        health = _health;
        damage = _damage;
        speed = _speed;
        timeBetweenAttacks = _timeBetweenAttacks;
    }

    public void Attack()
    {
        if (!AudioManager.Instance.EffectsSource.isPlaying)
            AudioManager.Instance.Play(gnawingSound);

        if (targetRoot.GetComponent<RootHealth>().rootDestroyed)
        {
            RootDestroyed();
            AudioManager.Instance.EffectsSource.Stop();
        }

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
            dying = true;
            StartCoroutine(kill());
            deathTimer = 2.0F;
            Debug.Log("Dying");
        }
    }

    public IEnumerator kill()
    {
        yield return new WaitForSeconds(2);
        Debug.Log("dead");
        dead = true;
        removeCorpse();
    }
    public void removeCorpse()
    {
        Debug.Log("Enemykilled");
        EnemyManager.Instance.EnemyKilled(gameObject);
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
