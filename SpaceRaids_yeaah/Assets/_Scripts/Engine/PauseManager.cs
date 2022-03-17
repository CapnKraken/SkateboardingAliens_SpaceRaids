using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : ManagedObject
{
    private bool paused;


    //a reference to the gui canvas
    public Canvas GUIcanvas;

    private InputSystem inputSystem;

    //references to the individual input profile editing screens
    public GameObject profile1Gui, profile2Gui, profile3Gui;

    /// <summary>
    /// A list of parent game objects for the different screens in the pause menu.
    /// </summary>
    public List<GameObject> pauseMenus;

    protected override void Initialize()
    {
        paused = false;
        GUIcanvas.enabled = false;
        inputSystem = GetComponent<InputSystem>();

        SwapInputProfile(inputSystem.currentProfile);
    }

    public void SwitchPauseScreen(int index)
    {
        //Make sure the index is within range
        if(index >= 0 && index < pauseMenus.Count)
        {
            for(int i = 0; i < pauseMenus.Count; i++)
            {
                if(i == index)
                {
                    pauseMenus[i].SetActive(true);
                }
                else
                {
                    pauseMenus[i].SetActive(false);
                }
            }
        }
    }

    public void SwapInputProfile(int newProfile)
    {
        switch (newProfile)
        {
            //activate the current input profile and hide the rest
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

    /// <summary>
    /// Quit the game. <br/>
    /// METHOD IS TEMPORARY. JUST FOR DEMO
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
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
                    SwitchPauseScreen(0);
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
