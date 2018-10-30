using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuFunctionality : MonoBehaviour {

    public void Resume()
    {
        GameController.thisLevelController.UnpauseGameNow();
    }

    public void ToggleSound()
    {
        GameController.thisLevelController.EnableAllSound();
    }

    public void ExitToMenu()
    {
        GameController.thisLevelController.GoToMainMenu();
    }
}
