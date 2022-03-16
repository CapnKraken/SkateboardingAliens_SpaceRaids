//Matthew Watson

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class M_BasicEnemy : ManagedObject
{
    private NavMeshAgent agent;

    public float health;

    [Tooltip("MinDamage, MaxDamage")]
    public Vector2 damageRange;

    public Vector2 speedRange;
    private float speed;

    //reference to the player object
    private GameObject player;

    protected override void Initialize()
    {
        agent = GetComponent<NavMeshAgent>();
        speed = Random.Range(speedRange.x, speedRange.y);

        player = GameObject.FindGameObjectWithTag("Player");

        agent.speed = speed;
    }

    public void Die()
    {
        Notify(Category.GENERAL, $"SpawnExplosion {transform.position.x} {transform.position.y} {transform.position.z}");
        Destroy(gameObject);
    }

    public void Hurt(float amount)
    {
        health -= amount;
        if(health <= 0)
        {
            Die();
        }
    }

    private void Update()
    {
        agent.destination = player.transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Player")
        {
            //Damage the player with a variable damage rate
            Notify(Category.Player, $"DamagePlayer {Random.Range(damageRange.x, damageRange.y)}");
        }
    }

    public override void OnNotify(Category category, string message, string senderData)
    {
        //respond to notifications here
    }

    public override string GetLoggingData()
    {
        //Don't worry about this one. 
        //Theoretically, it has a debugging use
        return name;
    }

}
