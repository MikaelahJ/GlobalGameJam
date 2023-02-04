using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : Enemy
{
    public Mole(int health, int damage, float speed, float timeBetweenAttacks) : base(health, damage, speed, timeBetweenAttacks)
    { }

    private SpriteRenderer spriteRenderer;

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
        }
        else if (attacking)
        {
            timer += Time.deltaTime;
            if (timer >= timeBetweenAttacks)
            {
                Attack();
                timer = 0;
            }
        }
    }

    private void Move()
    {
        if (transform.rotation.z < 180 && transform.rotation.z > 0)
            GetComponentInChildren<SpriteRenderer>().flipY = true;
        else
            GetComponentInChildren<SpriteRenderer>().flipY = false;

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
