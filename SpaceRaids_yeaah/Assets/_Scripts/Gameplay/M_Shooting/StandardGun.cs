//Matthew Watson

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardGun : ManagedObject
{
    [Tooltip("Fire rate in shots per second.")]
    public float fireRate;

    /// <summary>
    /// How much damage the gun does
    /// </summary>
    public float damage;

    /// <summary>
    /// How fast the bullet travels
    /// </summary>
    public float speed;

    /// <summary>
    /// Prefab for the bullet.
    /// </summary>
    public GameObject bulletPrefab;

    /// <summary>
    /// Shot delay in seconds
    /// </summary>
    private float fireDelay;
    private float fireTimer;

    /// <summary>
    /// Whether or not the gun is firing
    /// </summary>
    private bool isFiring;

    protected override void Initialize()
    {
        isFiring = false;

        fireTimer = 0;

        //get how many seconds to wait between shots
        fireDelay = 1 / fireRate;
    }

    #region Update

    private void Update()
    {
        if (isFiring)
        {
            fireTimer -= Time.deltaTime;
            if(fireTimer <= 0)
            {
                fireTimer = fireDelay;

                FireGun();
            }
        }
    }

    #endregion

    #region Fire Gun

    private void FireGun()
    {
        Vector3 newRotation = transform.rotation.eulerAngles + new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), Random.Range(-3f, 3f));
        //Spawn the player bullet
        PlayerBullet p = Instantiate(bulletPrefab, transform.position + transform.forward * 2, Quaternion.Euler(newRotation)).GetComponent<PlayerBullet>();
        p.SetVars(speed, damage);

        //destroy the projectile after 7 seconds
        Destroy(p.gameObject, 7.0f);
    }

    #endregion

    #region Notifications

    public override void OnNotify(Category category, string message, string senderData)
    {
        if(category == Category.Shooting)
        {
            switch (message)
            {
                case "PlayerShoot_Start":
                    isFiring = true;
                    fireTimer = 0;
                    break;
                case "PlayerShoot_End":
                    isFiring = false;
                    break;
                default:break;
            }
        }
    }

    public override string GetLoggingData()
    {
       return name;
    }

    #endregion
}
