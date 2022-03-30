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
    public float detectionRange;

    //reference to the player object
    private GameObject player, ship;
    private float playerDistance;

    protected override void Initialize()
    {
        agent = GetComponent<NavMeshAgent>();
        speed = Random.Range(speedRange.x, speedRange.y);

        player = GameObject.FindGameObjectWithTag("Player");
        ship = GameObject.FindGameObjectWithTag("Ship");

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
        playerDistance = Vector3.Distance(player.transform.position, gameObject.transform.position);
        Debug.Log(playerDistance);
        //follow player
        if (playerDistance <= detectionRange)
        {
            agent.destination = player.transform.position;
        }
        //attack ship
        else
        {
            agent.destination = ship.transform.position;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //Damage the player with a variable damage rate
            string s = $"DamagePlayer {Random.Range(damageRange.x, damageRange.y)}";
            Notify(Category.Player, s);
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
