using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuildingObjectBase : ManagedObject
{
    //Made by Donovan - this class is not used by anything anymore and can be deleted. it was causing too much trouble and making separate building classes without inheritance workrd much better
    /*public static float health, maxHealth, materialCost;
    public abstract float MaxHealth();
    public abstract float BuildingHealth();
    public abstract float MaterialCost();

    protected override void Initialize()
    {
        health = BuildingHealth();
        maxHealth = MaxHealth();
        materialCost = MaterialCost();
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

    public virtual void Update()
    {
        Debug.Log(gameObject.name + health + maxHealth);
        if(health <= 0)
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

    public override void OnNotify(Category category, string message, string senderData)
    {
        //respond to notifications here
    }

    public override string GetLoggingData()
    {
        return name;
    }*/

}
