using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    //Keep track of the GameController
	public static GameController Maincontroller;
    //Keep track of the current level controller
    public static LevelController thisLevelController;

    public static SaveAndLoadController saveController;

    public static UISoundController SoundController;

    public AudioSource soundMain;
    private float volumeLevel;

    public int Points;

	void Awake () {
        //Already have controller?
        if (Maincontroller == null)
            Maincontroller = this;
        else if (Maincontroller != null & Maincontroller != this)
            Destroy(gameObject);
        DontDestroyOnLoad (this.gameObject);
        if (GetComponent<SaveAndLoadController>())
        {
            saveController = GetComponent<SaveAndLoadController>();
            saveController.InitialiseSave();
            saveController.LoadSavedStats();
            saveController.SetPoints();
        }
	}
	
	
    //Change the level to new Level(int)
    //integer represents the build number of the scene
    //this is defined in the editor
	public void GoToLevel(int Level){
		SceneManager.LoadScene(Level, LoadSceneMode.Single);
    }
    public void GoToLevel(string level)
    {
        SceneManager.LoadScene(level, LoadSceneMode.Single);
    }

    public void GoToMainMenu()
    {
        Destroy(thisLevelController.gameObject);
        SceneManager.LoadScene(0, LoadSceneMode.Single);

        //  FindObjectOfType<JumbleNumbersText>().JumbleTextNow();
       StartCoroutine(jumblePointsOnLoad());
    }

    IEnumerator jumblePointsOnLoad()
    {
        yield return new WaitForSeconds(0.5f);
        saveController = GetComponent<SaveAndLoadController>();
       // saveController.InitialiseSave();
        saveController.LoadSavedStats();
        saveController.SetPoints();
        // FindObjectOfType<JumbleNumbersText>().JumbleTextNow();
    }

    //Exits the application
	public void ExitApplication(){
		Application.Quit ();
	}


    //Disables all the sound in current Scene
    public void DisableSound()
    {
        var soundObjects = FindObjectsOfType<AudioSource>();
        if (soundMain != null)
            volumeLevel = soundMain.volume;
        else
            volumeLevel = 1;
        foreach (AudioSource a in soundObjects)
        {
            a.volume = 0;
        }
        soundMain.volume = 0;
    }

    //enables all the sound in current Scene
    public void EnableSound()
    {
        var soundObjects = FindObjectsOfType<AudioSource>();
        foreach (AudioSource a in soundObjects)
        {
            a.volume = volumeLevel;
        }
        soundMain.volume = volumeLevel;
    }


    //Adds a specific amount of points to point controller, negative values minus points
    public void AddPoints(int points)
    {
        saveController.AddPoints(points);
    }
}
