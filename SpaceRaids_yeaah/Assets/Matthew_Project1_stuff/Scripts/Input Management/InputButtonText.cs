using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputButtonText : MonoBehaviour
{
    //the name of the input translation ("shoot", "walkForward", etc)
    public string inputName;

    //reference to the game's input system
    public InputSystem inputSystem;

    //current control profile
    public int profile;

    public Text text;
    // Update is called once per frame
    void Update()
    {
        if (GuiSwap.paused)
        {
            if(inputSystem.currentProfile == profile)
            {
                //update the button's text to the current input's actual input name
                text.text = inputSystem.profiles[inputSystem.currentProfile].GetInputs()[inputName].inputID;
            }
        }
    }
}
