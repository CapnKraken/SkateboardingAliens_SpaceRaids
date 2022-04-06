using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;
//attached to player

public class BuildMode : ManagedObject
{
    //Donovan Lott

    //bool for if the build mode is active
    private bool buildModeActive;

    //input system script
    private InputSystem inputSystem;

    //object with build hud canvas
    public GameObject buildHUD;

    public static BuildMode current;

    //public GridLayout gridLayout;
    //private Grid grid;

    [SerializeField] private Tilemap MainTilemap;
    [SerializeField] private TileBase whiteTile;

    public Vector3 place;

    private RaycastHit hit;

    public GameObject objectToPlace, tempObject;
    public GameObject wall, tempWall;

    public bool placeNow;

    public bool placeWall;

    public bool tempObjectExists;



    protected override void Initialize()
    {
        //build mode disabled at start
        buildModeActive = false;

        //gets input system script
        inputSystem = FindObjectOfType<InputSystem>();

        //attaches the Build_HUD canvas as a gameobject
        buildHUD = GameObject.Find("Build_HUD");

        //disables the Build HUD at the start
        buildHUD.GetComponent<Canvas>().enabled = false;

    }


    public override void OnNotify(Category category, string message, string senderData)
    {
        if (category == Category.GENERAL)
        {
            switch (message)
            {
                //activates and deactivates build mode and the build HUD based on message received
                case "buildModeOn":
                    buildModeActive = true;
                    buildHUD.GetComponent<Canvas>().enabled = true; //need to use getcomponent for the canvas since buildHUD is a GameObject
                    break;
                case "buildModeOff":
                    buildModeActive = false;
                    buildHUD.GetComponent<Canvas>().enabled = false;
                    break;
                default: break;
            }
        }
    }

    public bool isBuildModeActive()
    {
        return buildModeActive;
    }

    public override string GetLoggingData()
    {
        return name;
    }

    private void Awake()
    {
        current = this;
    }

    void Update()
    {
        if (!GameManager.Instance.pauseManager.isPaused())
        {
            if (isBuildModeActive())
            {
                if (placeNow == true)
                {
                    SendRay();
                }

                if (placeWall == true)
                {
                    objectToPlace = wall;
                }

                if (inputSystem.harvest == 2)
                {
                    PlaceWall();
                }


                // it should only activate once since the input int is 2, but instead it's rotating them every frame the key is held down
                if (inputSystem.itemRotate == 2)
                {
                    tempObject.transform.Rotate(0f, -90f, 0f, Space.World);
                }

                if (inputSystem.itemSwap == 2)
                {
                    tempObject.transform.Rotate(0f, 90f, 0f, Space.World);
                }
            }

        }
    }


    public void SendRay()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            place = new Vector3(hit.point.x, hit.point.y, hit.point.z);

            if (hit.transform.tag == "terrain")
            {
                if (tempObjectExists == false)
                {
                    Instantiate(tempWall, place, Quaternion.identity);
                    tempObject = GameObject.Find("WhiteWall(Clone)");
                    tempObjectExists = true;
                }

                if (inputSystem.harvest == 2)
                {
                    Instantiate(objectToPlace, place, tempObject.transform.rotation);
                    placeNow = false;
                    placeWall = false;
                    Destroy(tempObject);
                    tempObjectExists = false;
                }

                if (tempObject != null)
                {
                    tempObject.transform.position = place;
                }
            }

            if (inputSystem.buildMode == 2)
            {
                placeNow = false;
                placeWall = false;
                Destroy(tempObject);
                tempObjectExists = false;
            }
        }
    }

    public void PlaceWall()
    {
        placeNow = true;
        placeWall = true;
    }



}
