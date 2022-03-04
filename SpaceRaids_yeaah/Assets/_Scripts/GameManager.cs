//Matthew Watson

//GameManager object
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    #region Ticker List
    /// <summary>
    /// List of objects that need determinstic updates.
    /// </summary>
    private List<ManagedObject> managedObjects;

    public void AddTicker(ManagedObject t)
    {
        managedObjects.Add(t);
    }

    public void RemoveTicker(ManagedObject t)
    {
        managedObjects.Remove(t);
    }
    #endregion

    /// <summary>
    /// Called after creating the singleton. Place setup information here.<br/>
    /// Keep it to a minimum, please, future me.
    /// </summary>
    private void Initialize()
    {
        //construct the messenger
        messenger = new Messenger();

        //initialize the ticker list
        managedObjects = new List<ManagedObject>();
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
    /// In this method, update all notifiable objects and the messenger.
    /// </summary>
    private void Update()
    {
        //make objects respond to notifications
        messenger.Update();
    }
}
