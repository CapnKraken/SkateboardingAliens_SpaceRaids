//MatthewWatson

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script handles the player moving forward, backward, left, and right.
//It is applied to the object that parents the up and down camera motion object.
public class PlayerController : MonoBehaviour
{
    public InputSystem inputSystem;
    //public float walkingAcceleration; //the rate at which the player will reach his walking speed
    public float walkingSpeed; //The maximum speed the player will move 

    //A reference to the main player object's rigidbody
    private Rigidbody rb;

    //Variables for moving the player
    private Vector3 playerVelocity, currentYVel;

    //These variables are set to their counterparts in inputSystem.
    private float strafeMotion, walkMotion;
    private int shoot;

    //Both types of bullets assigned to this in the inspector
    public GameObject[] bulletTypes;

    //The empty object that sits in front of the player and serves as the place the bullets spawn from
    public Transform spawnPos;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    private void Update()
    {
        //check to make sure the game isn't paused
        if (!GuiSwap.paused)
        {
            //Obtain input data from the InputSystem
            strafeMotion = inputSystem.walkSide;
            walkMotion = inputSystem.walkFront;
            shoot = inputSystem.shoot;

            //if(walkMotion != 0)
            //{
            //    Debug.Log($"Walkmotion: {walkMotion}");
            //}

            //Calculate the force to apply to the player by adding the two motion vectors together
            playerVelocity = (transform.forward * walkMotion + transform.right * strafeMotion) * walkingSpeed + currentYVel;

            //shoot is an int, a value of 2 is the equivalent of GetKeyDown (so it'll only activate on first frame of input)
            if (shoot == 2)
            {
                //pick an int that's 0 or 1
                int bulletType = Random.Range(0, 2);

                //instantiate the prefab stored at array index bulletType of bulletTypes
                GameObject g = Instantiate(bulletTypes[bulletType], spawnPos.position, spawnPos.rotation);

                //get a reference to the projectile script of g
                Projectile p = g.GetComponent<Projectile>();

                //set the projectile's starting velocity based on its speed variable
                p.rb.velocity = spawnPos.transform.forward * p.speed;

                //destroy the projectile after 5 seconds
                Destroy(g, 5.0f);
            }
        }
    }

    private void FixedUpdate()
    {
        //adjust for gravity
        currentYVel = new Vector3(0, rb.velocity.y, 0);

        //Set the player's velocity
        rb.velocity = playerVelocity;
    }

}
