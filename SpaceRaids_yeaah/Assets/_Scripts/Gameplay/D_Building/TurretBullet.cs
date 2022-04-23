//Matthew Watson

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBullet : MonoBehaviour
{
    private float damage;

    public Rigidbody rb;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "M_BasicEnemy")
        {
            //Hit enemy here.
            M_BasicEnemy enemy = collision.gameObject.GetComponent<M_BasicEnemy>();
            enemy.Hurt(damage);
        }

        DeleteBullet();
    }


    private void DeleteBullet()
    {
        //If I implement object pooling, this method will change
        Destroy(gameObject);
    }

    public void SetVars(float speed, float damage)
    {
        this.damage = damage;

        rb.velocity = transform.forward * speed;
    }

}
