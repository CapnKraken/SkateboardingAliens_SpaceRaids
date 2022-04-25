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

    public GameObject objectToPlace, tempObject, selectedObject, tempSelectedObject;
    public GameObject wall, tempWall, barrier, tempBarrier, turret, tempTurret;

    public PlayerMaterial playerMaterial;
    private float materialAmount;
    private float materialCost;

    public Wall wallScript;
    public Barrier barrierScript;
    public Turret turretScript;

    private int z = 1; //used for changing items

    public bool placeNow;

    public bool placeWall;

    public bool tempObjectExists;

    /// <summary>
    /// Controls the spacing of placed objects.
    /// </summary>
    public float gridUnit;

    /// <summary>
    /// How far away the player is allowed to build.
    /// </summary>
    public float maxBuildDistance;

    /// <summary>
    /// Store the rotation of the objects you're placing.
    /// </summary>
    private Vector3 tempObjectRotation;

    protected override void Initialize()
    {
        //build mode disabled at start
        buildModeActive = false;

        //gets input system script
        inputSystem = FindObjectOfType<InputSystem>();

        playerMaterial = FindObjectOfType<PlayerMaterial>();

        //attaches the Build_HUD canvas as a gameobject
        buildHUD = GameObject.Find("Build_HUD");

        //disables the Build HUD at the start
        buildHUD.GetComponent<Canvas>().enabled = false;

        tempObjectRotation = Vector3.zero;

        selectedObject = wall; //sets wall as default building item
        tempSelectedObject = tempWall;

        wallScript = wall.GetComponent<Wall>();
        barrierScript = barrier.GetComponent<Barrier>();
        turretScript = turret.GetComponent<Turret>();

        materialCost = Wall.materialCost;

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
                PlaceWall(); //calls method once at very beginning of activating build mode to have temp wall appear
                if (placeNow == true)
                {
                    SendRay();
                }

                if (placeWall == true)
                {
                    objectToPlace = selectedObject;
                }

                if (inputSystem.harvest == 2)
                {
                    //PlaceWall();
                    
                }

                //Prevent nullrefs
                if (!tempObjectExists) return;

                // it should only activate once since the input int is 2, but instead it's rotating them every frame the key is held down
                if (inputSystem.itemRotate == 2)
                {
                    tempObject.transform.Rotate(0f, -90f, 0f, Space.World);
                    tempObjectRotation.y -= 90f;
                }

                if (inputSystem.itemSwap == 2) //switches between the three different objects
                {
                    z = (z % 3) + 1;
                    
                    if(z == 1)
                    {
                        selectedObject = wall;
                        tempSelectedObject = tempWall;
                        materialCost = Wall.materialCost;
                    }
                    else if(z == 2)
                    {
                        selectedObject = barrier;
                        tempSelectedObject = tempBarrier;
                        materialCost = Barrier.materialCost;
                    }
                    else if (z == 3)
                    {
                        selectedObject = turret;
                        tempSelectedObject = tempTurret;
                        materialCost = Turret.materialCost;
                    }
                    Destroy(tempObject); //refreshes the temporary object after switching
                    tempObjectExists = false;
                }
            }

        }
    }


    public void SendRay()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            place = new Vector3(hit.point.x, hit.point.y, hit.point.z);

            if (Vector3.Distance(transform.position, place) > maxBuildDistance)
            {
                //Don't show the ghost wall if you're pointing somewhere you can't actually place
                if(tempObjectExists)
                {
                    Destroy(tempObject);
                    tempObjectExists = false;
                }
                //exit the sendRay method
                return;
            }

            if (hit.transform.tag == "terrain")
            {
                if (tempObjectExists == false)
                {
                    Instantiate(tempSelectedObject, place, Quaternion.Euler(tempObjectRotation));
                    tempObject = GameObject.Find(tempSelectedObject.name + "(Clone)");
                    tempObjectExists = true;
                }

                if (inputSystem.harvest == 2)
                {
                    if(playerMaterial.GetMaterialAmount() >= materialCost) //if player has enough materials
                    {
                        PlayerMaterial.changeMaterial(-1 * materialCost); //subtracts material amount from player

                        Instantiate(objectToPlace, RoundXandZ(gridUnit, place), tempObject.transform.rotation);
                        placeNow = false;
                        placeWall = false;
                        Destroy(tempObject);
                        tempObjectExists = false;
                    }
                }

                if (tempObject != null)
                {
                    tempObject.transform.position = RoundXandZ(gridUnit, place);
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

    private Vector3 RoundXandZ(float unit, Vector3 current)
    {
        Vector3 final = current;

        final.x = Mathf.Round(current.x / unit) * unit;
        final.z = Mathf.Round(current.z / unit) * unit;

        return final;
    }
    public void PlaceWall()
    {
        placeNow = true;
        placeWall = true;
    }



}
