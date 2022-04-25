using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [Tooltip("Fire rate in shots per second.")]
    public float fireRate;

    public float damage;

    public float speed;

    public GameObject bulletPrefab;
    public GameObject pivot;
    public GameObject bulletSpawn;
    //public GameObject target;
    public Transform targetTransform;
    public bool targetInRange;
    public GameObject turretRadius;

    private float fireDelay;
    private float fireTimer;

    private bool isFiring;

    public static float health, maxHealth, materialCost;

    void Start()
    {
        health = 75;
        maxHealth = 75;
        materialCost = 2;

        fireRate = 25;
        speed = 20;
        damage = 100;

        targetInRange = turretRadius.GetComponent<TurretRadius>().targetInRange;

        isFiring = false;

        fireTimer = 0;

        fireDelay = 1 / fireRate;
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

        if (turretRadius.GetComponent<TurretRadius>().target != null && health != 0)
        {
            if (turretRadius.GetComponent<TurretRadius>().targetInRange == true)
            {
                //targetInRange = true;
                pivot.transform.LookAt(turretRadius.GetComponent<TurretRadius>().target.transform);

                isFiring = true;
            }
            else
            {
                //targetInRange = false;
                isFiring = false;
            }
        }
        else
        {
            //targetInRange = false;
            isFiring = false;
        }




        if (isFiring)
        {
            fireTimer -= Time.deltaTime;
            if (fireTimer <= 0)
            {
                fireTimer = fireDelay;

                Shoot();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "M_BasicEnemy")
        {
            changeHealth(0 - Random.Range(5, 10));
        }
    }

   
    public void changeTurretHealth(float amount)
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

    public void ChangeTurretMaxHealth(float amount)
    {
        maxHealth += amount;
    }

    public void ChangeTurretSpeed(float amount)
    {
        speed += amount;
    }

    private void Shoot()
    {
        Vector3 newRotation = bulletSpawn.transform.rotation.eulerAngles + new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), Random.Range(-3f, 3f));
        TurretBullet t = Instantiate(bulletPrefab, bulletSpawn.transform.position + bulletSpawn.transform.forward * 2, Quaternion.Euler(newRotation)).GetComponent<TurretBullet>();
        t.SetVars(speed, damage);

        //destroy the projectile after 7 seconds
        Destroy(t.gameObject, 7.0f);
    }
}
