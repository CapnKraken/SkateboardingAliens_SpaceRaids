using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ControlProfile 
{
    /* INPUTS:
     * note: For gamepads, camera and movement will automatically be axes
     * 
     * Camera right //if this is an axis, camera left will be blank
     * Camera left
     * Camera up   //same as right, but in the vertical direction
     * Camera down
     * 
     * Walk forward //if axis, next will be blank
     * Walk back
     * Strafe right //same as forward, but to the side
     * Strafe left
     * 
     * Shoot //action inputs
     * Show GUI
     */

    public bool isGamepad; //is the profile for a controller?

    public float cameraMovementSensitivity; //multiplier to change how much the camera moves in response to a directional input

    /// <summary>
    /// The list that shows up in the inspector. Dictionaries aren't serializable so this is part of a workaround.
    /// </summary>
    public List<KeyValPair> listInputs;
    
    private Dictionary<string, GameInput> inputs; //Create the dictionary to store the inputs

    public bool invertYAxis; //toggle for inverting the y axis for camera movement

    public ControlProfile()
    {
        //Default Control Profile
        isGamepad = false;

        invertYAxis = false;

        cameraMovementSensitivity = 2000;

        /*
        inputs = new Dictionary<string, GameInput>()
        {
            {"cameraRight", new GameInput("Mouse X", InputType.Axis)},
            {"cameraLeft", new GameInput("", InputType.Null)},
            {"cameraUp", new GameInput("Mouse Y", InputType.Axis)},
            {"cameraDown", new GameInput("", InputType.Null)},

            {"walkForward", new GameInput("w", InputType.Key)},
            {"walkBackward", new GameInput("s", InputType.Key)},
            {"strafeRight", new GameInput("d", InputType.Key)},
            {"strafeLeft", new GameInput("a", InputType.Key)},

            {"shoot", new GameInput("mouse 0", InputType.Key)},
            {"toggleGUI", new GameInput("space", InputType.Key)},
        };
        */

        inputs = new Dictionary<string, GameInput>();
        foreach (KeyValPair pair in listInputs)
        {
            inputs.Add(pair.actionName, pair.inputData);
        }
    }

    public ControlProfile(bool isGamepad, float cameraSensitivity, Dictionary<string, GameInput> inputs)
    {
        //custom constructor

        invertYAxis = false;
        this.cameraMovementSensitivity = cameraSensitivity;
        this.isGamepad = isGamepad;
        this.inputs = new Dictionary<string, GameInput>(inputs);
    }

    public void ChangeInput(string key, GameInput gi)
    {
        //switch the input to something else
        if (inputs.ContainsKey(key))
        {
            inputs[key] = new GameInput(gi);
        }
    }

    public Dictionary<string, GameInput> GetInputs()
    {
        return inputs;
    }

    [System.Serializable]
    public struct KeyValPair
    {
        [Tooltip("Your name for the action.")]
        public string actionName;
        public GameInput inputData;
    }
}

//The GameInput objects use this type
public enum InputType {Axis, Button, Key, Null}


