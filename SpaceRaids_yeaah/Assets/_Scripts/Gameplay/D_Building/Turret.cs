using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : BuildingObjectBase
{
    [Tooltip("Fire rate in shots per second.")]
    public float fireRate;

    public float damage;

    public float speed;

    public GameObject bulletPrefab;

    private float fireDelay;
    private float fireTimer;

    private bool isFiring;

    public override float MaxHealth()
    {
        return 100;
    }

    public override float BuildingHealth()
    {
        return 100;
    }

    public override float MaterialCost()
    {
        return 10;
    }

    protected override void Initialize()
    {
        isFiring = false;

        fireTimer = 0;

        fireDelay = 1 / fireRate;
    }

    public override void Update()
    {
        if (BuildingHealth() <= 0)
        {
            Destroy(gameObject);
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

    static void Shoot()
    {

    }
}
