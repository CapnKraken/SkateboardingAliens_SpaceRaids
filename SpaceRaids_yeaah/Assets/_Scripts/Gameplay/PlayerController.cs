//Matthew Watson

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script handles the player moving forward, backward, left, and right.
//It is applied to the object that parents the up and down camera motion object.
public class PlayerController : ManagedObject
{
    private InputSystem inputSystem;
    //public float walkingAcceleration; //the rate at which the player will reach his walking speed

    /// <summary>
    /// The player's initial walking speed.
    /// </summary>
    public float walkingSpeed;

    /// <summary>
    /// The maximum attainable speed
    /// </summary>
    public float maxSpeed;

    /// <summary>
    /// This increases slowly over time while the player is walking forward.
    /// </summary>
    private float forwardSpeedAddition;

    /// <summary>
    /// How fast the player accelerates
    /// </summary>
    public float accelerationRate;

    //A reference to the main player object's rigidbody
    private Rigidbody rb;

    //Variables for moving the player
    private Vector3 playerVelocity, currentYVel;

    //These variables are set to their counterparts in inputSystem.
    private float strafeMotion, walkMotion;
    private int shoot;

    //The empty object that sits in front of the player and serves as the place the bullets spawn from
    public Transform spawnPos;

    protected override void Initialize()
    {
        //get the rigidbody
        rb = GetComponent<Rigidbody>();

        //get the input system from game manager
        inputSystem = GameManager.Instance.GetComponent<InputSystem>();
    }


    #region Update and FixedUpdate
    private void Update()
    {
        //check to make sure the game isn't paused
        if (!GameManager.Instance.pauseManager.isPaused())
        {
            //Obtain input data from the InputSystem
            strafeMotion = inputSystem.walkSide;
            walkMotion = inputSystem.walkFront;
            shoot = inputSystem.shoot;
            //harvest = inputSystem.harvest;

            walkMotion *= (walkingSpeed + forwardSpeedAddition * Time.deltaTime);

            //Verify the speed is in check
            if (walkMotion >= maxSpeed) walkMotion = maxSpeed;

            strafeMotion *= walkingSpeed;

            //handle acceleration
            if(walkMotion != 0)
            {
                forwardSpeedAddition += accelerationRate;
            }

            //Calculate the force to apply to the player by adding the two motion vectors together
            playerVelocity = (transform.forward *walkMotion + transform.right * strafeMotion) + currentYVel;

            #region Handle Shooting
            //shoot is an int, a value of 2 is the equivalent of GetKeyDown (so it'll only activate on first frame of input)
            if (shoot == 2)
            {
                Notify(Category.Shooting, "PlayerShoot_Start");
                //Debug.Log("Starting to shoot");
            }
            //value of 3 is getKeyUp.
            else if(shoot == 3)
            {
                Notify(Category.Shooting, "PlayerShoot_End");
                //Debug.Log("Shooting over");
            }
            #endregion
        }

        #region Pause and unpause
        if (inputSystem.pause == 2)
        {
            if (GameManager.Instance.pauseManager.isPaused())
            {
                Notify(Category.GENERAL, "Resume");
            }
            else
            {
                Notify(Category.GENERAL, "Pause");
            }
        }
        #endregion
    }

    //Perform the player's physics update
    private void FixedUpdate()
    {
        if (rb.velocity.z == 0)
        {
            forwardSpeedAddition = 0;
        }

        //adjust for gravity
        currentYVel = new Vector3(0, rb.velocity.y, 0);

        //Set the rigidbody velocity to the variable we set up in here
        rb.velocity = playerVelocity;
    }

    #endregion

    #region notifications

    public override string GetLoggingData()
    {
        return name;
    }

    public override void OnNotify(Category category, string message, string senderData)
    {
        switch (message.Split()[0])
        {
            default:break;
        }
    }
    #endregion

}
