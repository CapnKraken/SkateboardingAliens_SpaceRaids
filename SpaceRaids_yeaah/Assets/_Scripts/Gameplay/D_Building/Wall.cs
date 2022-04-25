using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public static float health, maxHealth, materialCost;

    void Start()
    {
        health = 75;
        maxHealth = 75;
        materialCost = 2;
    }

    public void changeHealth(float amount)
    {
        health += amount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }

        if (health < 0)
        {
            health = 0;
        }
    }

    public void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "M_BasicEnemy")
        {
            changeHealth(0 - Random.Range(5, 10));
        }
    }

    public static void changeWallHealth(float amount)
    {
        health += amount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }

        if (health < 0)
        {
            health = 0;
        }
    }

    public static void ChangeWallMaxHealth(float amount)
    {
        maxHealth += amount;
    }
}
