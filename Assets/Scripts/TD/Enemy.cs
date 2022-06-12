using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float maxHealth = 1;
    public float health = 1;
    public float Speed = 1;
    public int ID;

    public NavMeshAgent agent;
    public Vector3 target;

    public void Initialize(Vector3 position)
    {
        health = maxHealth;
        transform.position = position;
    }

    public void handleDmg(int dmg)
    {
        health -= dmg;

        if (health <= 0)
        {
            health = 0;
        }
    }
}
