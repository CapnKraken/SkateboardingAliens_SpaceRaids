using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Upgrades : MonoBehaviour
{
    public PlayerMaterial playerMaterial;

    private PlayerHealthHandler healthHandler;
    private PlayerController playerController;

    private StandardGun standardGun;

    private Wall wall;
    private Barrier barrier;
    private Turret turret;

    float inHealthCost, inSpeedCost; //player upgrade material costs

    float inFireSpeedCost, inDamageCost, decSpreadCost; //weapon upgrade material costs

    float inWallHealthCost, inBarrierHealthCost, inTurretHealthCost, inTurretSpeedCost; //building upgrade material costs

    private Text currentText;
    private float currentCost;

    private void Start()
    {
        playerMaterial = FindObjectOfType<PlayerMaterial>();

        healthHandler = FindObjectOfType<PlayerHealthHandler>();
        playerController = FindObjectOfType<PlayerController>();

        standardGun = FindObjectOfType<StandardGun>();

        wall = FindObjectOfType<Wall>();
        barrier = FindObjectOfType<Barrier>();
        turret = FindObjectOfType<Turret>();

        inHealthCost = 10;
        inSpeedCost = 10;

        inFireSpeedCost = 10;
        inDamageCost = 10;
        decSpreadCost = 10;

        inWallHealthCost = 10;
        inBarrierHealthCost = 10;
        inTurretHealthCost = 10;
        inTurretSpeedCost = 10;

        currentText = gameObject.GetComponentInChildren(typeof(Text)) as Text;
    }

    void Update()
    {

        if(currentCost > playerMaterial.GetMaterialAmount())
        {
            GetComponent<Button>().interactable = false;
        }

        buttonText(gameObject.name);
    }

    public void IncreaseHealth()
    {
        if (playerMaterial.GetMaterialAmount() >= inHealthCost)
        {
            healthHandler.ChangeMaxHealth(10);
            healthHandler.changeHealth(10);
            PlayerMaterial.changeMaterial(inHealthCost * -1);
            inHealthCost = inHealthCost * 2; //cost doubles each time
        }
        
    }

    public void IncreaseSpeed()
    {
        if (playerMaterial.GetMaterialAmount() >= inSpeedCost)
        {
            playerController.ChangeMaxSpeed(2);
            PlayerMaterial.changeMaterial(inSpeedCost * -1);
            inSpeedCost = inSpeedCost * 2; //cost doubles each time
        }

    }

    public void IncreaseFireSpeed()
    {
        Debug.Log("Increase Fire Speed");
        if (playerMaterial.GetMaterialAmount() >= inFireSpeedCost)
        {
            standardGun.ChangeFireSpeed(10);
            PlayerMaterial.changeMaterial(inFireSpeedCost * -1);
            inFireSpeedCost = inFireSpeedCost * 2; //cost doubles each time
        }

    }

    public void IncreaseDamage()
    {
        if (playerMaterial.GetMaterialAmount() >= inDamageCost)
        {
            standardGun.ChangeDamage(2);
            PlayerMaterial.changeMaterial(inDamageCost * -1);
            inDamageCost = inDamageCost * 2; //cost doubles each time
        }

    }

    public void DecreaseSpread()
    {
        if(decSpreadCost < 80)
        {
            if (playerMaterial.GetMaterialAmount() >= decSpreadCost)
            {
                standardGun.ChangeSpread(-1);
                PlayerMaterial.changeMaterial(decSpreadCost * -1);
                decSpreadCost = decSpreadCost * 2; //cost doubles each time
            }
        }

    }

    public void IncreaseWallHealth()
    {
        if(wall != null)
        {
            if (playerMaterial.GetMaterialAmount() >= inWallHealthCost)
            {
                Wall.ChangeWallMaxHealth(5);
                wall.changeHealth(5);
                PlayerMaterial.changeMaterial(inWallHealthCost * -1);
                inWallHealthCost = inWallHealthCost * 2; //cost doubles each time
            }
        }


    }

    public void IncreaseBarrierHealth()
    {
        if (playerMaterial.GetMaterialAmount() >= inBarrierHealthCost)
        {
            Barrier.ChangeBarrierMaxHealth(5);
            barrier.changeHealth(5);
            PlayerMaterial.changeMaterial(inBarrierHealthCost * -1);
            inBarrierHealthCost = inBarrierHealthCost * 2; //cost doubles each time
        }

    }

    public void IncreaseTurretHealth()
    {
        if (playerMaterial.GetMaterialAmount() >= inTurretHealthCost)
        {
            turret.ChangeTurretMaxHealth(5);
            turret.changeHealth(5);
            PlayerMaterial.changeMaterial(inTurretHealthCost * -1);
            inTurretHealthCost = inTurretHealthCost * 2; //cost doubles each time
        }

    }

    public void IncreaseTurretFireSpeed()
    {
        if (playerMaterial.GetMaterialAmount() >= inTurretSpeedCost)
        {
            turret.ChangeTurretSpeed(5);
            PlayerMaterial.changeMaterial(inTurretSpeedCost * -1);
            inTurretSpeedCost = inTurretSpeedCost * 2; //cost doubles each time
        }

    }

    void buttonText(string buttonName)
    {
        switch (buttonName) //updates the material cost in the button descriptions
        {
            case "Increase Health Button":
                currentText.text = "Increase Health\nCost: " + inHealthCost;
                currentCost = inHealthCost;
                break;
            case "Increase Speed Button":
                currentText.text = "Increase Speed\nCost: " + inSpeedCost;
                currentCost = inSpeedCost;
                break;
            case "Increase Fire Speed Button":
                currentText.text = "Increase Fire Speed\nCost: " + inFireSpeedCost;
                currentCost = inFireSpeedCost;
                break;
            case "Increase Damage Button":
                currentText.text = "Increase Damage\nCost: " + inDamageCost;
                currentCost = inDamageCost;
                break;
            case "Decrease Spread Button":
                if (decSpreadCost >= 80)
                {
                    currentText.text = "Maximum Spread Reduction";
                    GetComponent<Button>().interactable = false;
                }
                else
                {
                    currentText.text = "Decrease Spread\nCost: " + decSpreadCost;
                }
                currentCost = decSpreadCost;
                break;
            case "Increase Wall Health Button":
                currentText.text = "Increase Wall Health\nCost: " + inWallHealthCost;
                currentCost = inWallHealthCost;
                break;
            case "Increase Barrier Health Button":
                currentText.text = "Increase Barrier Health\nCost: " + inBarrierHealthCost;
                currentCost = inBarrierHealthCost;
                break;
            case "Increase Turret Health Button":
                currentText.text = "Increase Turret Health\nCost: " + inTurretHealthCost;
                currentCost = inTurretHealthCost;
                break;
            case "Increase Turret Fire Speed Button":
                currentText.text = "Increase Turret Fire Speed\nCost: " + inTurretSpeedCost;
                currentCost = inTurretSpeedCost;
                break;
            default: break;
        }
    }


}
