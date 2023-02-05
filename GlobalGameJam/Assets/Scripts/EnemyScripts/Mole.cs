using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : Enemy
{
    public Mole(int health, int damage, float speed, float timeBetweenAttacks) : base(health, damage, speed, timeBetweenAttacks)
    { }

    private bool hasPlayedNarrator;
    public AudioClip enemyNarrator;

    private SpriteRenderer spriteRenderer;
    public Animator animator;

    private Vector2 min;
    private Vector2 max;

    private float timer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        min = new Vector2(transform.position.x - 5, transform.position.y);
        max = new Vector2(transform.position.x + 5, transform.position.y);
    }

    private void Update()
    {
        if (!attacking)
        {
            Move();
            animator.SetBool("Gnaw", false);
        }
        else if (attacking)
        {
            if (!hasPlayedNarrator)
            {
                AudioManager.Instance.PlayNarrator(enemyNarrator);
            }

                hasPlayedNarrator = true;

            animator.SetBool("Gnaw", true);
            timer += Time.deltaTime;
            if (timer >= timeBetweenAttacks)
            {
                Attack();
                timer = 0;
            }
        }
        else if (dead)
        {
            Debug.Log("Animating_dead");
            animator.SetBool("dead", true);
        }
    }

    private void Move()
    {
        if (transform.rotation.z % 360 < 180 && transform.rotation.z % 360 > 0)
           transform.GetChild(0).GetComponent<SpriteRenderer>().flipY = true;
        else
            transform.GetChild(0).GetComponentInChildren<SpriteRenderer>().flipY = false;

        if (foundRoot)
        {
            transform.position = Vector3.Lerp(transform.position, targetRoot.transform.position, speed / 10 * Time.deltaTime);
            transform.up = targetRoot.transform.position - transform.position;
        }
        else
        {
            transform.position += transform.up * speed * Time.deltaTime;

            //float time = Mathf.PingPong(Time.time * speed / 10, 1);
            //transform.position = Vector3.Lerp(min, max, time);
        }
    }

}
