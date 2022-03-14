//Matthew Watson

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : ManagedObject
{
    private float damage;

    public Rigidbody rb;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            //Hit enemy here.

        }

        DeleteBullet();
    }

    protected override void Initialize()
    {
       
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

    #region Notifications

    public override void OnNotify(Category category, string message, string senderData)
    {
        
    }

    public override string GetLoggingData()
    {
        return name;
    }

    #endregion
}
