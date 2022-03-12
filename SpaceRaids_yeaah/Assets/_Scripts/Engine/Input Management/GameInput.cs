using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct GameInput
{
    public string inputName;    //Name of the input
    public InputType inputType; //Axis, Button, or Key (or Null)
    public bool isAxisInverted; //Only applies to axes, and defaults to false

    public GameInput(string name, InputType type)
    {
        isAxisInverted = false;
        inputName = name;
        inputType = type;
    }

    //Copy constructor
    public GameInput(GameInput g)
    {
        isAxisInverted = g.isAxisInverted;
        inputName = g.inputName;
        inputType = g.inputType;
    }

    public override string ToString()
    {
        return $"inputName: {inputName}\ninputType: {inputType}\nisAxisInverted: {isAxisInverted}";
    }
}
