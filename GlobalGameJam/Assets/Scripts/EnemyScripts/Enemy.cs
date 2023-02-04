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

    public Enemy(int _health, int _damage, float _speed, float _timeBetweenAttacks)
    {
        health = _health;
        damage = _damage;
        speed = _speed;
        timeBetweenAttacks = _timeBetweenAttacks;
    }

    public void Test()
    {
        Debug.Log("hej " + health + " " + damage + " " + speed);
    }
}
