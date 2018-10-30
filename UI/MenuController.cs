using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {

    public GameController mainController;


    public void Play()
    {
        GameController.Maincontroller.GoToLevel("LevelsScene");
    }

    public void About()
    {

    }


    public void Exit()
    {
        Application.Quit();
    }

	
}
