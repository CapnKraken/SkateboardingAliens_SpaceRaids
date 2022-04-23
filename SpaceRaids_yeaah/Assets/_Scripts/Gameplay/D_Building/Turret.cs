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
    public GameObject pivot;
    public GameObject bulletSpawn;
    //public GameObject target;
    public Transform targetTransform;
    public bool targetInRange;
    public GameObject turretRadius;

    private float fireDelay;
    private float fireTimer;

    private bool isFiring;

    public override float MaxHealth()
    {
        return 75;
    }

    public override float BuildingHealth()
    {
        return 75;
    }

    public override float MaterialCost()
    {
        return 10;
    }

    protected override void Initialize()
    {
        fireRate = 25;
        speed = 20;
        damage = 100;

        targetInRange = turretRadius.GetComponent<TurretRadius>().targetInRange;
        //target = turretRadius.GetComponent<TurretRadius>().target;

        isFiring = false;

        fireTimer = 0;

        fireDelay = 1 / fireRate;

        //bulletPrefab = Resources.Load("Assets/Prefabs/Projectiles.Turretbullet", GameObject) as GameObject;
    }

    public override void Update()
    {
        
        
        if (BuildingHealth() <= 0)
        {
            Destroy(gameObject);
        }

        if(turretRadius.GetComponent<TurretRadius>().target != null && BuildingHealth() != 0)
        {
            if(turretRadius.GetComponent<TurretRadius>().targetInRange == true)
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

    private void Shoot()
    {
        Vector3 newRotation = bulletSpawn.transform.rotation.eulerAngles + new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), Random.Range(-3f, 3f));
        TurretBullet t = Instantiate(bulletPrefab, bulletSpawn.transform.position + bulletSpawn.transform.forward * 2, Quaternion.Euler(newRotation)).GetComponent<TurretBullet>();
        t.SetVars(speed, damage);

        //destroy the projectile after 7 seconds
        Destroy(t.gameObject, 7.0f);
    }
}
