using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int walkSpeed, health, damage;
    public float detectionRange, attackRange;
    private float distance;

    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        distance = Vector3.Distance(player.transform.position, gameObject.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        //if player is out of range
        if (distance >= detectionRange)
        {

        }

        //if player is in detection range
        if (distance <= detectionRange)
        {
            //follows player

        //if player is in attack range
        }
        if (distance <= attackRange)
        {
            
        }
    }

}
