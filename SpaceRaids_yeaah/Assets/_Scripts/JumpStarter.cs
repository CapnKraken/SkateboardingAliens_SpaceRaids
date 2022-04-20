//I hate this and it's super jank but this is to manually set up the gamemanager when the scene is loaded


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpStarter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.JumpStart();
    }
}
