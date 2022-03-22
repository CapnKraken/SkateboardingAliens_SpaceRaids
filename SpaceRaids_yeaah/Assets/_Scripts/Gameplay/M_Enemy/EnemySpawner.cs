using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : ManagedObject
{
    public float waitTime;
    private float timer;

    //How big is the map
    public int bound;

    public GameObject enemy;

    private bool canSpawnEnemies;

    protected override void Initialize()
    {
        timer = 0;
        canSpawnEnemies = false;
    }

    private void Update()
    {
        //Only spawn enemies if it's the correct time
        if (canSpawnEnemies)
        {
            timer += Time.deltaTime;
            if (timer > waitTime)
            {
                //Spawn the enemy
                Vector3 spawnPos = new Vector3(Random.Range(0 - bound, bound), 3, Random.Range(0 - bound, bound));

                Instantiate(enemy, spawnPos, Quaternion.identity);

                //reset the timer
                timer = 0;
            }
        }
    }

    public override void OnNotify(Category category, string message, string senderData)
    {
        if(category == Category.GENERAL)
        {
            switch (message.Split()[0])
            {
                case "Daybreak":
                    canSpawnEnemies = false;
                    break;
                case "Nightfall":
                    canSpawnEnemies = true;
                    break;
                default: break;
            }
        }
    }

    public override string GetLoggingData()
    {
        return name;
    }

}
