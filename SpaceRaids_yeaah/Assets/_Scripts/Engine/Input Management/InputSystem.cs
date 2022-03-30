using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This script will convert the various actual inputs (depending on the profile and settings) into variables that are passed into the actual control scripts.
//It contains an array of 3 objects of type ControlProfile, //which each contain a dictionary of input translations (things like "shoot", "walkForward", etc) which correspond to actual inputs (axes, buttons, keys)
public class InputSystem : MonoBehaviour
{
    //These floats will be between -1 and 1. 
    //These are the variables the player controller scripts will see. They do not access input directly.
    public float cameraHorizontal, cameraVertical;
    public float walkFront, walkSide;
    public float itemSelection;

    //Action button data is stored as an int
    //0: not pressed
    //1: held    (like GetKey())
    //2: pressed (like GetKeyDown())
    public int shoot, pause, harvest, buildMode;

    //array of control profiles
    public ControlProfile[] profiles = new ControlProfile[3];
    //default profile is set by changing this in the inspector
    public int currentProfile;

    //Reference to the camera sensitivity slider on the GUI
    public Slider cameraSensitivitySlider;

    //if this is true, the game will check for buttons and such in the update loop
    private bool isRebindingInputs = false;

    //The input translation of what's being changed
    private string inputToRebind;

    /// <summary>
    /// this is a dummy input which I use to store values in before applying them to the actual profile
    /// </summary>
    private GameInput gi;
    void Start()
    {
        //Keyboard/Mouse profile
        profiles[0] = new ControlProfile(false, 2000f, new Dictionary<string, GameInput>()
        {
            {"cameraRight", new GameInput("Mouse X", InputType.Axis)},
            {"cameraLeft", new GameInput("", InputType.Null)},
            {"cameraUp", new GameInput("Mouse Y", InputType.Axis)},
            {"cameraDown", new GameInput("", InputType.Null)},

            {"walkForward", new GameInput("w", InputType.Key)},
            {"walkBackward", new GameInput("s", InputType.Key)},
            {"strafeRight", new GameInput("d", InputType.Key)},
            {"strafeLeft", new GameInput("a", InputType.Key)},

            {"itemSelectRight", new GameInput("e", InputType.Key)},
            {"itemSelectLeft", new GameInput("q", InputType.Key)},

            {"shoot", new GameInput("mouse 0", InputType.Key)},
            {"harvest", new GameInput("mouse 1", InputType.Key)},
            {"buildMode", new GameInput("mouse 2", InputType.Key)},
            {"pause", new GameInput("escape", InputType.Key)},
        });

        //Keyboard only profile
        profiles[1] = new ControlProfile(false, 100f, new Dictionary<string, GameInput>()
        {
            {"cameraRight", new GameInput("right", InputType.Key)},
            {"cameraLeft", new GameInput("left", InputType.Key)},
            {"cameraUp", new GameInput("up", InputType.Key)},
            {"cameraDown", new GameInput("down", InputType.Key)},

            {"walkForward", new GameInput("w", InputType.Key)},
            {"walkBackward", new GameInput("s", InputType.Key)},
            {"strafeRight", new GameInput("d", InputType.Key)},
            {"strafeLeft", new GameInput("a", InputType.Key)},

            {"itemSelectRight", new GameInput("e", InputType.Key)},
            {"itemSelectLeft", new GameInput("q", InputType.Key)},

            {"shoot", new GameInput("space", InputType.Key)},
            {"harvest", new GameInput("left shift", InputType.Key)},
            {"buildMode", new GameInput("right shift", InputType.Key)},
            {"pause", new GameInput("escape", InputType.Key)},
        });

        //Gamepad profile
        profiles[2] = new ControlProfile(true, 100f, new Dictionary<string, GameInput>()
        {
            //These are defaults for my controller. The axes can be mixed and matched though so the system should work with any.
            {"cameraRight", new GameInput("Axis 4", InputType.Axis)},
            {"cameraLeft", new GameInput("", InputType.Null)},
            {"cameraUp", new GameInput("Axis 5", InputType.Axis)},
            {"cameraDown", new GameInput("", InputType.Null)},

            {"walkForward", new GameInput("Axis 2", InputType.Axis)},
            {"walkBackward", new GameInput("", InputType.Null)},
            {"strafeRight", new GameInput("Axis 1", InputType.Axis)},
            {"strafeLeft", new GameInput("", InputType.Null)},

            {"itemSelectRight", new GameInput("Button 5", InputType.Button)},
            {"itemSelectLeft", new GameInput("Button 4", InputType.Button)},

            {"shoot", new GameInput("Axis 10", InputType.Axis)},
            {"harvest", new GameInput("Axis 9", InputType.Axis)},
            {"buildMode", new GameInput("Axis 6", InputType.Key)},
            {"pause", new GameInput("Button 7", InputType.Button)},
        });

        //these inputs default to zero, i.e. they aren't being pressed
        shoot = 0;
        pause = 0;
        buildMode = 0;
        harvest = 0;
    }

    void Update()
    {
        if (!isRebindingInputs)
        {
            #region Update inputs during game
            //CAMERA INPUT
            cameraHorizontal = HandleMovementInput("cameraRight", "cameraLeft", false);

            //If the invert axis toggle in the gui is checked, invert vertical movement
            if (profiles[currentProfile].invertYAxis)
            {
                cameraVertical = HandleMovementInput("cameraUp", "cameraDown", true);
            }
            else
            {
                cameraVertical = 0 - HandleMovementInput("cameraUp", "cameraDown", true);
            }

            //WALKING INPUT
            walkFront = 0 - HandleMovementInput("walkForward", "walkBackward", true);
            walkSide = HandleMovementInput("strafeRight", "strafeLeft", false);

            //ITEM SELECTION INPUT
            itemSelection = HandleMovementInput("itemSelectRight", "itemSelectLeft", false);

            //ACTION INPUT
            shoot = HandleActionInput(shoot, "shoot");
            pause = HandleActionInput(pause, "pause");
            harvest = HandleActionInput(harvest, "harvest");
            buildMode = HandleActionInput(buildMode, "buildMode");

            #endregion
        }
        else //input rebinding
        {
            #region Rebind inputs
            if (profiles[currentProfile].isGamepad)
            //If we're rebinding a gamepad, we need to deal with both axes and buttons
            {
                #region Gamepad rebinding
                if (inputToRebind == "pause" || inputToRebind == "shoot" || inputToRebind == "harvest" || inputToRebind == "buildMode")
                {
                //these are action inputs, and they can be rebound to either a button or an axis
                    
                    //get data on what inputs the user is pressing at the moment
                    string a = GetAxisPressed();
                    string b = GetButtonPressed();

                    //
                    string aAvail = CheckAvailability(a);
                    string bAvail = CheckAvailability(b);

                    //gamepad profiles can't use the mouse wheel
                    if(aAvail == "Axis 11")
                    {
                        isRebindingInputs = false;
                    }
                    else if (aAvail == inputToRebind)
                    {
                        gi.inputName = a;
                        gi.inputType = InputType.Axis;
                        profiles[currentProfile].ChangeInput(inputToRebind, gi);
                        isRebindingInputs = false;
                    }
                    else if (aAvail != "none")
                    {
                        //if the name is already taken, swap the inputs
                        gi.inputName = profiles[currentProfile].GetInputs()[inputToRebind].inputName;
                        gi.inputType = profiles[currentProfile].GetInputs()[inputToRebind].inputType;
                        profiles[currentProfile].ChangeInput(aAvail, gi);

                        gi.inputName = a;
                        gi.inputType = InputType.Axis;
                        profiles[currentProfile].ChangeInput(inputToRebind, gi);
                        isRebindingInputs = false;
                    }

                    if (bAvail == inputToRebind)
                    {
                        gi.inputName = b;
                        gi.inputType = InputType.Button;
                        profiles[currentProfile].ChangeInput(inputToRebind, gi);
                        isRebindingInputs = false;
                    }
                    else if (bAvail != "none")
                    {
                        //if the name is already taken, swap the inputs
                        gi.inputName = profiles[currentProfile].GetInputs()[inputToRebind].inputName;
                        gi.inputType = profiles[currentProfile].GetInputs()[inputToRebind].inputType;
                        profiles[currentProfile].ChangeInput(bAvail, gi);

                        gi.inputName = b;
                        gi.inputType = InputType.Button;
                        profiles[currentProfile].ChangeInput(inputToRebind, gi);
                        isRebindingInputs = false;
                    }
                }
                else // only axes
                {
                    string a = GetAxisPressed();

                    string aAvail = CheckAvailability(a);

                    //gamepad profiles can't use the mouse wheel
                    if (aAvail == "Axis 11")
                    {
                        isRebindingInputs = false;
                    }
                    else if (aAvail == inputToRebind)
                    {
                        gi.inputName = a;
                        gi.inputType = InputType.Axis;
                        profiles[currentProfile].ChangeInput(inputToRebind, gi);
                        isRebindingInputs = false;
                    }
                    else if (aAvail != "none")
                    {
                        //if the name is already taken, swap the inputs
                        gi.inputName = profiles[currentProfile].GetInputs()[inputToRebind].inputName;
                        gi.inputType = InputType.Axis;
                        profiles[currentProfile].ChangeInput(aAvail, gi);

                        gi.inputName = a;
                        gi.inputType = InputType.Axis;
                        profiles[currentProfile].ChangeInput(inputToRebind, gi);
                        isRebindingInputs = false;
                    }
                }
                #endregion
            }
            else
            //The keyboard profiles deal with keys and axes, but only axis 11 (the scroll wheel)
            {
                #region Keyboard rebinding
                string s = GetKeyPressed();
                string sAvail = CheckAvailability(s);

                string axis = GetAxisPressed();
                string axisAvail = CheckAvailability(axis);

                //only let axis 11 be selected
                if(axisAvail != "none" && axisAvail != "Axis 11")
                {
                    isRebindingInputs = false;
                }
                //next block makes it so you can't set pause to mouse click.
                //that would break the menu system, which works through button clicks
                else if (s == "mouse 0" && inputToRebind == "pause")
                {
                    isRebindingInputs = false;
                }
                else if (sAvail == inputToRebind)
                {
                    //Debug.Log(inputToRebind);
                    gi.inputName = s;
                    gi.inputType = InputType.Key;
                    profiles[currentProfile].ChangeInput(inputToRebind, gi);
                    isRebindingInputs = false;
                }
                else if (s != "none")
                {
                    //if the name is already taken, swap the inputs
                    gi.inputName = profiles[currentProfile].GetInputs()[inputToRebind].inputName;
                    gi.inputType = InputType.Key;
                    profiles[currentProfile].ChangeInput(sAvail, gi);

                    gi.inputName = s;
                    gi.inputType = InputType.Key;
                    profiles[currentProfile].ChangeInput(inputToRebind, gi);
                    isRebindingInputs = false;
                }

                if (axisAvail == inputToRebind)
                {
                    gi.inputName = axis;
                    gi.inputType = InputType.Axis;
                    profiles[currentProfile].ChangeInput(inputToRebind, gi);
                    isRebindingInputs = false;
                }

                #endregion
            }

            #endregion
        }
    }

    //s is the actual input name, such as "Axis 2" or "z"
    private string CheckAvailability(string s)
    {
        if (s != "none")
        {
            //Iterate through the profile to make sure the requested input isn't being used already
            foreach (KeyValuePair<string, GameInput> keyValPair in profiles[currentProfile].GetInputs())
            {
                if (keyValPair.Value.inputName == s)
                {
                    //If the input is already in use, return the input it's going to replace
                    return keyValPair.Key;
                }
            }

            //Input isn't in use, so we're good.
            return inputToRebind;
        }

        //If the user either isn't pressing anything, or not pressing a valid key/button/axis
        return "none";
    }
    public void EditInput(string inputName)
    {
        //set the dummy gameInput object to the the input you're going to change
        gi = GetProfileInput(currentProfile, inputName);

        //check to see if the input with name inputName exists or not. It should.
        if (gi.inputType == InputType.Null)
        {
            Debug.Log("null input");
        }
        else
        {
            //go into rebind mode, and set the inputToRebind to the requested input action
            isRebindingInputs = true;
            inputToRebind = inputName;
        }
    }

    /// <summary>
    /// Return a value between -1 and 1 for movement-related inputs
    /// </summary>
    private float HandleMovementInput(string positiveMotion, string negativeMotion, bool isUpDownInput)
    {
        //Method to gather input data from the current control profile if the required input is movement with a positive and negative component

        GameInput gameInput = GetProfileInput(currentProfile, positiveMotion);
        //Debug.Log(gameInput.ToString());
        
        if (gameInput.inputType == InputType.Axis)
        //inputs that are axes ignore the negative input, because they can be both positive and negative
        {
            if (gameInput.isAxisInverted)
            {
                //All of the axes on profile 3 have the option to invert, just in case there's any funkiness with a controller.
                return 0 - Input.GetAxis(profiles[currentProfile].GetInputs()[positiveMotion].inputName);
            }
            else
            {
                //If the input is an axis, there is no need for a negative input: positiveMotion is set to Input.GetAxis(-whatever axis-), so it's -1 to 1
                return Input.GetAxis(profiles[currentProfile].GetInputs()[positiveMotion].inputName);
            }
        }
        else
        {
            //If the motion is handled by buttons/keys, you need one for positive and one for negative

            //total will be from -1 to 1; it's the movement data to return.
            float total = 0;

            if (profiles[currentProfile].isGamepad)
            //If the profile is for a controller, use buttons. If not, use keys.
            {
                if (Input.GetButton(profiles[currentProfile].GetInputs()[positiveMotion].inputName))
                    total++;

                if (Input.GetButton(profiles[currentProfile].GetInputs()[negativeMotion].inputName))
                    total--;
            }
            else
            {
                if (Input.GetKey(profiles[currentProfile].GetInputs()[positiveMotion].inputName))
                    total++;

                if (Input.GetKey(profiles[currentProfile].GetInputs()[negativeMotion].inputName))
                    total--;
            }

            //Adjust the vertical inputs so they're in the intuitive direction
            //When I tried to swap everything around to avoid redundancy it stopped working, so this if statement stays :P
            if (isUpDownInput)
            {
                total = 0 - total;
            }
            return total;
        }
    }

    /// <summary>
    /// Return an integer value containing information about 'action' input
    /// </summary>
    /// <param name="currentState">the current state of the input</param>
    /// <param name="action">the action input to be modified</param>
    /// <returns>0 = not pressed, 1 = held, 2 = first frame held</returns>
    private int HandleActionInput(int currentState, string action)
    {
        /* if nothing, return zero
         * if something
         *     if currentState is zero, return 2
         *     else return 1
         */

        GameInput i = profiles[currentProfile].GetInputs()[action];


        //These statements basically do the same thing for all inputs
        //check to see if a thing is being pressed
        //if not, return 0
        //if so, check to see if it's the first frame of being pressed
        //  if so, return 2
        //  if not, return 1
        if (i.inputType == InputType.Axis)
        {
            float f = Input.GetAxis(i.inputName);

            if (f == 0)
            {
                if(currentState == 1)
                {
                    return 3;
                }
                else
                {
                    return 0;
                }
            }
            else //since it's an action input, we don't care how much the axis is pressed, just that it is.
            {

                if (currentState == 0)
                {
                    //If this is the initial frame it's being pressed
                    return 2;
                }
                else
                {
                    return 1;
                }
            }
        }
        else //non axis inputs (buttons and keys)
        {
            if (profiles[currentProfile].isGamepad)
            {
                if (Input.GetButton(i.inputName))
                {
                    if (currentState == 0)
                    {
                        return 2;
                    }
                    else
                    {
                        return 1;
                    }
                }
                else
                {
                    if (currentState == 1)
                    {
                        return 3;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            else
            {
                if (Input.GetKey(i.inputName))
                {
                    if (currentState == 0)
                    {
                        return 2;
                    }
                    else
                    {
                        return 1;
                    }
                }
                else
                {
                    if (currentState == 1)
                    {
                        return 3;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
        }
    }

    //switch the profile to a different one
    public void SwapControlProfile(int newProfile)
    {
        currentProfile = newProfile;

        //update camera sensitivity slider
        cameraSensitivitySlider.value = profiles[currentProfile].cameraMovementSensitivity;
    }

    public void SetCameraSensitivity(float newSensitivity)
    {
        //change the profile's camera sensitivity
        profiles[currentProfile].cameraMovementSensitivity = newSensitivity;
    }

    public void ToggleInvertYAxis()
    {
        //Method called when the invert y axis gui toggle is swapped
        profiles[currentProfile].invertYAxis = !profiles[currentProfile].invertYAxis;
    }

    public void InvertAxis(string axis)
    {
        //swap isAxisInverted bool in the requested input

        GameInput gi = GetProfileInput(currentProfile, axis);
        if (gi.inputType == InputType.Axis)
        {
            gi.isAxisInverted = !gi.isAxisInverted;

            profiles[currentProfile].GetInputs()[axis] = gi;
        }
    }

    private GameInput GetProfileInput(int profile, string key)
    {
        //Get the GameInput object corresponding to the requested key if it exists

        Dictionary<string, GameInput> temp = profiles[profile].GetInputs();
        if (temp.ContainsKey(key))
        {
            return temp[key];
        }
        else
        {
            return new GameInput("", InputType.Null);
        }
    }

    public string GetButtonPressed()
    {
        for (int i = 0; i < 20; i++)
        {
            //There aren't 20 buttons, but maybe a different controller has more than mine.
            if (Input.GetKeyDown("joystick 1 button " + i))
            {
                return "Button " + i;
            }

        }
        return "none";
    }

    public string GetAxisPressed()
    {
        /*
         ONLY GOES TO 10. THERE ARE 11 AXES.
         Axis 11 (scrollwheel) was breaking stuff.
         
         May add it back in later but it's not *really* neccessary
         */


        //Only using 11 axes, so don't need to define any more
        for (int i = 0; i < 10; i++)
        {
            if (Mathf.Abs(Input.GetAxis($"Axis {i + 1}")) >= 0.7f)
            {
                return $"Axis {i + 1}";
            }

        }
        return "none";
    }

    public string GetKeyPressed()
    {
        //loop through the below array to find which keys are being pressed
        foreach (string key in keys)
        {
            if (Input.GetKey(key))
            {
                return key;

            }
        }
        return "none";
    }

    //List I got online. The KeyCodes and their strings don't match, so I couldn't convert them in a way the inputsystem would recognize
    //This should be all of the basic keys of a keyboard
    private string[] keys = new string[]
    {
    "backspace",
 "delete",
 "tab",
 "clear",
 "return",
 "pause",
 "escape",
 "space",
 "up",
 "down",
 "right",
 "left",
 "insert",
 "home",
 "end",
 "page up",
 "page down",
 "f1",
 "f2",
 "f3",
 "f4",
 "f5",
 "f6",
 "f7",
 "f8",
 "f9",
 "f10",
 "f11",
 "f12",
 "f13",
 "f14",
 "f15",
 "0",
 "1",
 "2",
 "3",
 "4",
 "5",
 "6",
 "7",
 "8",
 "9",
 "!",
 "\"",
 "#",
 "$",
 "&",
 "'",
 "(",
 ")",
 "*",
 "+",
 ",",
 "-",
 ".",
 "/",
 ":",
 ";",
 "<",
 "=",
 ">",
 "?",
 "@",
 "[",
 "\\",
 "]",
 "^",
 "_",
 "`",
 "a",
 "b",
 "c",
 "d",
 "e",
 "f",
 "g",
 "h",
 "i",
 "j",
 "k",
 "l",
 "m",
 "n",
 "o",
 "p",
 "q",
 "r",
 "s",
 "t",
 "u",
 "v",
 "w",
 "x",
 "y",
 "z",
 "numlock",
 "caps lock",
 "scroll lock",
 "right shift",
 "left shift",
 "right ctrl",
 "left ctrl",
 "right alt",
 "left alt",
 "mouse 0",
 "mouse 1",
 "mouse 2",
 "mouse 3",
 "mouse 4",
 "mouse 5",
 "mouse 6"
    };
}
