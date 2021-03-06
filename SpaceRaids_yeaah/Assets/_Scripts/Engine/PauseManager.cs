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

        if(SceneManager.GetActiveScene().name == "TitleScene")
        {
            this.enabled = false;
            return;
        }

        //Set up canvas objects
        GUIcanvas = GameObject.FindGameObjectWithTag("GUICanvas").GetComponent<Canvas>();
        GameObject[] controlProfiles = GameObject.FindGameObjectsWithTag("ControlProfile");

        Debug.Log(controlProfiles.Length);

        profile1Gui = controlProfiles[0];
        profile2Gui = controlProfiles[1];
        profile3Gui = controlProfiles[2];

        pauseMenus = new List<GameObject>();
        pauseMenus.AddRange(GameObject.FindGameObjectsWithTag("PauseMenu"));

        paused = false;
        GUIcanvas.enabled = false;
        Time.timeScale = 1;
        inputSystem = GetComponent<InputSystem>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        SwapInputProfile(inputSystem.currentProfile);
    }

    /// <summary>
    /// For the gamemanager to call
    /// </summary>
    public void ReInitialize()
    {
        Initialize();
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
                profile1Gui.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -45, 0);
                profile2Gui.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 100000, 0);
                profile3Gui.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 100000, 0);
                break;
            case 1:
                profile1Gui.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 100000, 0);
                profile2Gui.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -45, 0);
                profile3Gui.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 100000, 0);
                break;
            case 2:
                profile1Gui.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 100000, 0);
                profile2Gui.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 100000, 0);
                profile3Gui.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -45, 0);
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
        //Application.Quit();

        //load the title scene
        SceneManager.LoadScene("TitleScene");
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

                    //freeze the game
                    Time.timeScale = 0;

                    //Show the pause menu
                    GUIcanvas.enabled = true;
                    SwitchPauseScreen(0);

                    Cursor.visible = true;
                    //Free cursor to move about the pause menu
                    Cursor.lockState = CursorLockMode.None;
                    break;
                case "resume":
                case "unpause":
                    paused = false;

                    //unfreeze the game
                    Time.timeScale = 1;

                    //hide the pause menu
                    GUIcanvas.enabled = false;

                    Cursor.visible = false;
                    //Lock cursor to center of screen
                    Cursor.lockState = CursorLockMode.Locked;
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
