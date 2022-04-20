//Matthew Watson

//GameManager object
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// A singleton class to operate as the heart of the game's code. <br/>
/// Unless an exception is absolutely necessary, other scripts should only reference this one.
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// The notification management object
    /// </summary>
    public Messenger messenger;

    /// <summary>
    /// The object to control pausing and unpausing.
    /// </summary>
    public PauseManager pauseManager;

    /// <summary>
    /// The game's input manager.
    /// </summary>
    public InputSystem inputSystem;


    #region Singleton

    //The allowed instance of the GameManager
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            //Get rid of it if there's already one in the scene.
            Destroy(this.gameObject);
        }
        else if(_instance != null && _instance == this)
        {
            Initialize();
        }
        else
        {
            _instance = this;

            //I want my Game Manager to stick around
            DontDestroyOnLoad(gameObject);

            //Separate this into another part of code so
            //it's not hidden in the Singleton implementation.
            Initialize();
        }
    }
    #endregion


    /// <summary>
    /// Called after creating the singleton. Place setup information here.
    /// </summary>
    private void Initialize()
    {
        //construct the messenger
        messenger = new Messenger();

        //only need the pause manager, input manager etc. if the scene is the game scene
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            pauseManager = GetComponent<PauseManager>();

            inputSystem = GetComponent<InputSystem>();

            inputSystem.cameraSensitivitySlider = GameObject.FindGameObjectWithTag("camsenseslider").GetComponent<Slider>();

            //Reboot the pause manager
            pauseManager.ReInitialize();
        }
    }

    #region Screen Setup
    /// <summary>
    /// Sets the aspect ratio to the specified numbers. <br/>
    /// Form- height:width
    /// </summary>
    /// <param name="width">Second number</param>
    /// <param name="height">First number</param>
    private void SetAspectRatio(int height, int width)
    {
        int screenHeight = Screen.height;
        int screenWidth = Screen.width;



        if (screenWidth >= screenHeight * 1.5f)
        {
            screenWidth = (int)((width * screenHeight) / (float)height);
        }
        else
        {
            screenHeight = (int)((height * screenWidth) / (float)width);
        }

        Screen.SetResolution(screenWidth, screenHeight, true);
    }
    #endregion

    /// <summary>
    /// In this method, update the messenger.
    /// </summary>
    private void Update()
    {
        //make objects respond to notifications
        messenger.Update();
    }
}
