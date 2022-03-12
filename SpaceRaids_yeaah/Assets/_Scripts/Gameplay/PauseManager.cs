using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : ManagedObject
{
    private bool paused;


    //a reference to the gui canvas
    public Canvas GUIcanvas;

    private InputSystem inputSystem;

    //references to the individual profile editing screens
    public GameObject profile1Gui, profile2Gui, profile3Gui;

    protected override void Initialize()
    {
        paused = false;
        GUIcanvas.enabled = false;
        inputSystem = GetComponent<InputSystem>();

        SwapGui(inputSystem.currentProfile);
    }

    public void SwapGui(int newGui)
    {
        //Next I need methods in InputSystem to handle getting input data when the button is pressed

        switch (newGui)
        {
            //activate the current gui and hide the rest
            case 0:
                profile1Gui.SetActive(true);
                profile2Gui.SetActive(false);
                profile3Gui.SetActive(false);
                break;
            case 1:
                profile1Gui.SetActive(false);
                profile2Gui.SetActive(true);
                profile3Gui.SetActive(false); 
                break;
            case 2:
                profile1Gui.SetActive(false);
                profile2Gui.SetActive(false);
                profile3Gui.SetActive(true);
                break;
            default:break;
        }
    }

    /// <summary>
    /// Getter for the paused boolean.
    /// </summary>
    public bool isPaused()
    {
        return paused;
    }

    #region Notifications
    public override void OnNotify(Category category, string message, string senderData)
    {
        if(category == Category.GENERAL)
        {
            switch (message.Split()[0].ToLower())
            {
                case "pause":
                    paused = true;
                    Time.timeScale = 0;
                    GUIcanvas.enabled = true;
                    break;
                case "resume":
                case "unpause":
                    paused = false;
                    Time.timeScale = 1;
                    GUIcanvas.enabled = false;
                    break;
                default:break;
            }
        }
    }

    public override string GetLoggingData()
    {
        return name + "_PauseManager";
    }
    #endregion
}
