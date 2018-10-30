using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelTester : MonoBehaviour {

    public void GoToLevel(int Level)
    {
        SceneManager.LoadScene(Level, LoadSceneMode.Single);
    }
    // Update is called once per frame
    void Update () {
		if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            GoToLevel(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GoToLevel(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GoToLevel(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            GoToLevel(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            GoToLevel(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            GoToLevel(5);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            GoToLevel(6);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            GoToLevel(7);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            GoToLevel(0);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            GameController.saveController.AddPoints(100); 
        }
    }
}
