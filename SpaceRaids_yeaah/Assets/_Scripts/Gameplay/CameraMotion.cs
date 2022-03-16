//Matthew Watson

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script controls the motion of the camera. It takes input data from the MovementSystem attached to the GameManager object.

public class CameraMotion : MonoBehaviour
{
    
    //The object I want to rotate about the y axis for looking around horizontally
    private Transform parent;

    private Rigidbody parentRB;

    //Will obtain the movement input information from here
    public InputSystem inputSystem;
    void Start()
    {
        //set parent to the transform of the parent object, in this case, the rotator object
        parent = transform.parent;
        parentRB = parent.GetComponent<Rigidbody>();
    }

    void Update()
    {
        //Rotate this object to look up and down. Without the - sign, the vertical camera movement would be inverted
        
        transform.Rotate(new Vector3(0 - inputSystem.cameraVertical * inputSystem.profiles[inputSystem.currentProfile].cameraMovementSensitivity * Time.deltaTime, 0, 0));

        //Rotate the parent object, so as to not mess with this object's y rotation axis. 
        parentRB.MoveRotation(Quaternion.Euler(parent.rotation.eulerAngles + new Vector3(0, inputSystem.cameraHorizontal * inputSystem.profiles[inputSystem.currentProfile].cameraMovementSensitivity * Time.deltaTime, 0)));

        //The following block clamps the vertical camera movement, so the player can only look all the way up and down, but no further
        {
            //Current rotation about the x axis- this variable is manipulated and then applied back to localRotation
            float currentXRotation = transform.localEulerAngles.x;

            //It took a bit of trial and error to figure out that the "looking up" 90 degree arc is actually 360 to 270 degrees rather than 0 to -90 degrees. 
            if (currentXRotation >= 0f && currentXRotation <= 90f)
            {
                currentXRotation = Mathf.Clamp(currentXRotation, 0f, 90f);
            }
            else if (currentXRotation >= 270f && currentXRotation <= 360f)
            {
                currentXRotation = Mathf.Clamp(currentXRotation, 270f, 360f);
            }

            //Adjust the local rotation accordingly
            transform.localRotation = Quaternion.Euler(currentXRotation, 0, 0);
        }
        
    }
}
