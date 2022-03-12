using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileButton : MonoBehaviour
{
    public void OnButtonPressed(int newProfile)
    {
        GameManager.Instance.inputSystem.SwapControlProfile(newProfile);
        GameManager.Instance.pauseManager.SwapInputProfile(newProfile);
    }
}
