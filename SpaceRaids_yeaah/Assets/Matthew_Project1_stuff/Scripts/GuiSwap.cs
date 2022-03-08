using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiSwap : MonoBehaviour
{
    public static bool paused;


    //a reference to the gui canvas
    public Canvas GUIcanvas;

    private InputSystem inputSystem;

    //references to the individual profile editing screens
    public GameObject profile1Gui, profile2Gui, profile3Gui;
    void Start()
    {
        paused = true;
        GUIcanvas.enabled = true;
        inputSystem = GetComponent<InputSystem>();

        SwapGui(inputSystem.currentProfile);
    }

    // Update is called once per frame
    void Update()
    {
        if(inputSystem.toggleGUI == 2)
        {
            if (GUIcanvas.enabled)
            {
                paused = false;

            }
            else
            {
                paused = true;

            }
            GUIcanvas.enabled = !GUIcanvas.enabled;   
        }

        if (paused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
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
}
