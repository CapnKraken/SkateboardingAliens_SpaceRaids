using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthHandler : ManagedObject
{
    //UI elements which show the player's health data
    public Image healthBar;
    public Text healthText;

    //player's maximum health
    //value is set to 100 in the inspector
    public float maxHealth;
    //player's current health
    private float health;

    //the timer that controls health regeneration
    private float healingTimer;

    protected override void Initialize()
    {
        //health is set to maximum by default
        health = maxHealth;
        healingTimer = 0;
    }


    void Update()
    {
        //Adjust the size of the healthbar image based on the ratio of health to maxhealth
        healthBar.rectTransform.sizeDelta = new Vector2(200.0f * (health / maxHealth), 30.0f);

        //Only try to regen if the health isn't maxed out
        if(health < maxHealth)
        {
            healingTimer += Time.deltaTime;
            if(healingTimer >= 10)
            {
                //wait 10 seconds, then reset the timer and heal the player by 2 points
                changeHealth(2.0f);
                healingTimer = 0;
            }
        }

        //Display the numerical value of the player's health
        healthText.text = $"Health: {Mathf.Round(health)}";
    }

    private void changeHealth(float amount)
    {
        //Method to increase or decrease the player health, and make sure it never goes below 0 or above maxHealth
        health += amount;
        if(health > maxHealth)
        {
            health = maxHealth;
        }

        if(health < 0)
        {
            health = 0;
        }
    }

    public void ChangeMaxHealth(float amount)
    {
        maxHealth += amount;
    }


    #region Notifications

    public override void OnNotify(Category category, string message, string senderData)
    {
        if (category == Category.Player)
        {
            if(message.Split()[0] == "DamagePlayer")
            {
                //damage the player by the specified amount
                changeHealth(0 - float.Parse(message.Split()[1]));
            }
        }
    }

    public override string GetLoggingData()
    {
        return name;
    }

    #endregion
}
