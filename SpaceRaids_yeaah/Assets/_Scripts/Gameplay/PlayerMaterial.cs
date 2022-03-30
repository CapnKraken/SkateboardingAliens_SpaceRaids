using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Made by Donovan
//Attached to Player

public class PlayerMaterial : MonoBehaviour
{
    [SerializeField] private Material highlighted; //in inspector, attach highlighted Material(texture)
    private Material saveMaterial; //will keep track of the selected objects original texture before highlighting
    bool saveM = false;

    public InputSystem inputSystem; //variable for the input system script
    private int harvestInput; //controller value

    private static float material = 0; //amount of material

    public Text materialText; //text for displaying amount of material

    public RaycastHit hit; //camera target
    private Transform selectionTransform; //keeps track of selected object

    float timer = 0; //timer for holding down the button


    void Start()
    {
        inputSystem = FindObjectOfType<InputSystem>(); //assigns the input system script so it can be used
    }

    // Update is called once per frame
    void Update()
    {

        if (!GameManager.Instance.pauseManager.isPaused()) //if it's not paused
        {
            harvestInput = inputSystem.harvest; //gets the input value from InputSystem

            materialText.text = $"Material: {Mathf.Round(material)}"; //keeps the amount of material text in top left corner updated

            if (selectionTransform != null)
            {
                var selectionRenderer = selectionTransform.GetComponent<Renderer>();
                selectionRenderer.material = saveMaterial; //changes texture back to original once deselected
                selectionTransform = null;
                saveM = false;
            }
            var ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
            if (Physics.Raycast(ray, out hit, 10)) //if player camera and object are within 10 units
            {
                var selection = hit.transform; //selection is the selected object

                if (selection.CompareTag("Tree") || selection.CompareTag("Animal") || selection.CompareTag("Ore"))
                {
                    var selectionRenderer = selection.GetComponent<Renderer>(); //gets the selected objects renderer for changing the texture
                    if (selectionRenderer != null)
                    {
                        if (saveM == false)
                        {
                            saveMaterial = selectionRenderer.material; //saves the original texture before changing it to be highlighted
                            saveM = true; //bool that keeps original texture from being changed
                        }
                        selectionRenderer.material = highlighted; //changes selected object's material(texture) to new highlighted material(texture)
                    }
                    selectionTransform = selection;
                }

            }
            if (harvestInput == 1 && selectionTransform != null) //if button is pressed and an object is selected. need to add "&& build mode not activated" when build mode is introduced
            {
                timer += Time.deltaTime; //starts timer
                if (selectionTransform.CompareTag("Tree"))
                {
                    if (timer >= 1)//if slected object is a tree and the button is held for one second
                    {
                        changeMaterial(10); //add 10 material amount
                        wipeTexture(); //gets rid of the stored Material(texture)
                        Destroy(selectionTransform.gameObject); //remove selected object from game
                        timer = 0; //reset timer
                    }
                }
                else if (selectionTransform.CompareTag("Animal"))
                {
                    if (timer >= 2)
                    {
                        changeMaterial(25);
                        wipeTexture();
                        Destroy(selectionTransform.gameObject);
                        timer = 0;
                    }
                }
                else if (selectionTransform.CompareTag("Ore"))
                {
                    if (timer >= 4)
                    {
                        changeMaterial(100);
                        wipeTexture();
                        Destroy(selectionTransform.gameObject);
                        timer = 0;
                    }
                }

            }
            else
            {
                timer = 0; //timer resets if button is not being pressed
            }
        }


    }


    public static void changeMaterial(float amount)
    {
        material += amount; //changes material amount based on parameter

        if (material < 0)
        {
            material = 0; //material amount cannot go lower than zero
        }
    }

    public void wipeTexture() //removes stored texture value;
    {
        saveMaterial = null;
        saveM = false;
    }

}
