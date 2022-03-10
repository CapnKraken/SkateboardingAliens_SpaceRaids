//Matthew Watson

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct GameInput
{
    [Tooltip("As it is named in the unity input manager.")]
    public string inputID;    //Name of the input
    public InputType inputType; //Axis, Button, or Key (or Null)
    public bool isAxisInverted; //Only applies to axes, and defaults to false

    public GameInput(string name, InputType type)
    {
        isAxisInverted = false;
        inputID = name;
        inputType = type;
    }

    //Copy constructor
    public GameInput(GameInput g)
    {
        isAxisInverted = g.isAxisInverted;
        inputID = g.inputID;
        inputType = g.inputType;
    }
}
