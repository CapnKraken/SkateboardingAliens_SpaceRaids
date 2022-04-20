using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuButton : MonoBehaviour
{
    public void OnClick(string action)
    {
        PauseManager p = GameManager.Instance.pauseManager;

        switch (action)
        {
            case "Quit":
                p.QuitGame();
                break;
            case "Main":
                p.SwitchPauseScreen(0);
                break;
            case "Settings":
                p.SwitchPauseScreen(1);
                break;
            case "Config":
                p.SwitchPauseScreen(2);
                break;
            default:break;
        }
    }

    public void OnSliderChanged(float f)
    {
        GameManager.Instance.inputSystem.SetCameraSensitivity(f);
    }
}
