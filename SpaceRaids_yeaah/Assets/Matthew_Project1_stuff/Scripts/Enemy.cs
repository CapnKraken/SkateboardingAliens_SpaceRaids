using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody rb; // the object's own rigidbody
    public Transform player; //the transform of the player object

    public GameObject healthbar; //the 3d gui
    public float maxHealth = 100; //default max health of the enemy
    private float health;

    public float force;
    void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        //Keep the healthbar facing the player
        healthbar.transform.LookAt(player.transform);

        //get a reference to the healtbar's current scale so I don't have to type it out three times
        Vector3 currentHealthbarScale = healthbar.transform.localScale;

        //script to scale the healthbar based on how much health the enemy has in relation to its max health
        healthbar.transform.localScale = new Vector3(1.0f * health / maxHealth, currentHealthbarScale.y, currentHealthbarScale.z);
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        //the projectiles have a second sphere collider that is bigger than their own radius. That's the trigger this responds to
        //Without it, sometimes the balls were bouncing off of the enemy and not getting destroyed

        //Both the small and big projectiles are tagged with "Bullet"
        if(collision.gameObject.tag == "Bullet")
        {
            //Damage the enemy based on the projectile's damage value
            health -= collision.gameObject.GetComponent<Projectile>().damage;

            //Destroy the projectile
            Destroy(collision.gameObject);
        }

        if(health <= 0)
        {
            //Kill the enemy
            Destroy(this.gameObject);
        }
    }

}
