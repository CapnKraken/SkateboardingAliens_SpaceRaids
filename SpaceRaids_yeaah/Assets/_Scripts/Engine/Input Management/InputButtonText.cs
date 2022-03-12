using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputButtonText : MonoBehaviour
{
    //the name of the input translation ("shoot", "walkForward", etc)
    public string inputName;

    //reference to the game's input system
    private InputSystem inputSystem;

    //current control profile
    public int profile;

    public Text text;

    private void Start()
    {
        inputSystem = GameManager.Instance.inputSystem;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.pauseManager.isPaused())
        {
            if(inputSystem.currentProfile == profile)
            {
                //update the button's text to the current input's actual input name
                text.text = inputSystem.profiles[inputSystem.currentProfile].GetInputs()[inputName].inputName;
            }
        }
    }

    //What to do when the button is clicked.
    public void OnButtonPressed()
    {
        inputSystem.EditInput(inputName);
    }

    //For the axis toggles in profile 3
    public void OnInvertAxis() 
    {
        inputSystem.InvertAxis(inputName);
    }

    //Toggle invert y axis
    public void InvertYAxisToggle()
    {
        inputSystem.ToggleInvertYAxis();
    }
}
