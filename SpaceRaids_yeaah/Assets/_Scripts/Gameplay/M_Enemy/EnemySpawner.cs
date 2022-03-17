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

    protected override void Initialize()
    {
        timer = 0;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer > waitTime)
        {
            //Spawn the enemy
            Vector3 spawnPos = new Vector3(Random.Range(0 - bound, bound), 3, Random.Range(0 - bound, bound));

            Instantiate(enemy, spawnPos, Quaternion.identity);

            //reset the timer
            timer = 0;
        }
    }

    public override void OnNotify(Category category, string message, string senderData)
    {
        
    }

    public override string GetLoggingData()
    {
        return name;
    }

}
