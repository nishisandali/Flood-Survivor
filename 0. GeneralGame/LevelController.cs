using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class LevelController : MonoBehaviour {

    [SerializeField]
    private GameObject PauseMenuHolder;
    private bool isPaused;
    private bool SoundEnabled = true;

    //fields for sound in the level
    [SerializeField]
    private AudioSource soundMain;
    [SerializeField]
    private AudioClip tapIncrementSound;

    //End Screen Text and objects (defined in unity during edit mode)
    [SerializeField]
	private GameObject EndLevelScreen ;
	[SerializeField]
	private Text LevelCompleteText;
	[SerializeField]
	private Text messageText;
	[SerializeField]
	private Text RewardText;
	[SerializeField]
	private GameObject MessageObj;
	[SerializeField]
	private int Reward;
    [SerializeField]
    public int LevelBasedRewardCalculation;
	[SerializeField]
	private Text buttonText;
	[SerializeField]
	private GameObject InfoObj;
	[SerializeField]
	private string[] InfoList;
	[SerializeField]
	private Text infoText;
    [SerializeField]
    private JumbleNumbersText pointsJumbler;
    //Information display index and object to disable 
    //when instructions are finished
	[SerializeField]
	private int currInfoNum;
	[SerializeField]
	private GameObject screenTap;

	[SerializeField]
	private GameObject car;
	[SerializeField]
	
    //the GameObjects which the MinigameController subclasses
    //are attached to
	private GameObject[] Controller;
    [SerializeField]
    private GameObject correctEnd;
    [SerializeField]
    private GameObject incorrectEnd;
    [SerializeField]
    private bool GameEnded;

    //Win and lose conditions and their corresponding 
    //UI elements
    [SerializeField]
    public Image[] lives;
    [SerializeField]
    private Text winCondition;
    [SerializeField]
    public int livesLeft = 3;
    [SerializeField]
    public int winLeft = 15;

    [SerializeField]
    private GameObject[] ObjectsToEnableOnStart;
    [SerializeField]
    private GameObject[] ObjectsToDisable;
    [SerializeField]
    public GameObject AdditionalObjectToDisable;
    public GameObject AdditionalObjectToDisable2;
    [SerializeField]
    private bool IsScreenOrientationLandscape;


    //Runs when the object loads
	void Start () {
        //sets the current levelcontroller referenced by GameController to this class when the scene loads
        transform.parent = GameController.Maincontroller.transform;
        GameController.thisLevelController = this;
        if (IsScreenOrientationLandscape)
            Screen.orientation = ScreenOrientation.Landscape;
        else
            Screen.orientation = ScreenOrientation.Portrait;

        if (GetComponent<AudioSource>())
        {
            soundMain = GetComponent<AudioSource>();
        }
	}


    //Ends the current level
    //LevelMessage sets a message to be displayed at the end
    //The boolean win indicates whether we have won or not
    public void EndLevel(bool Win, string levelMessage){
        if (!GameEnded)
        {
            GameEnded = true;
            //Win screen
            if (Win)
            {
                LevelCompleteText.text = "Level Complete!";
                RewardText.text = "You Earned...";
                if(pointsJumbler)
                pointsJumbler.realNumber = LevelBasedRewardCalculation;
                pointsJumbler.JumbleTextNow();
               // messageText.text = levelMessage;
                //MessageObj.SetActive (true);
                correctEnd.SetActive(true);
            }
            //Lose Screen
            else
            {
                LevelCompleteText.text = "Level failed!";
                RewardText.text = "Try Again";
                //messageText.text = levelMessage;
                //MessageObj.SetActive (false);
                //buttonText.text = "Retry Level >";
                incorrectEnd.SetActive(true);
            }
          //Tell the minigame controller to stop running the game
            foreach (GameObject g in Controller)
            {
                g.GetComponent<MinigameController>().EndGame();
            }
            foreach (GameObject obj in ObjectsToDisable)
            {
                obj.SetActive(false);
            }
            if (AdditionalObjectToDisable != null) 
                AdditionalObjectToDisable.SetActive(false);
            if(AdditionalObjectToDisable2 != null) 
                AdditionalObjectToDisable2.SetActive(false);
            GameController.Maincontroller.AddPoints(Reward);
            DisableObjects();
            //screenTap.SetActive (true);  
        }
	}
    public void EndLevel(bool Win, string levelMessage, bool hasPoints)
    {
        if (!GameEnded)
        {
            GameEnded = true;
            //Win screen
            if (Win)
            {
                LevelCompleteText.text = "Level Complete!";
                RewardText.text = "You Earned...";
                if (pointsJumbler)
                    pointsJumbler.realNumber = LevelBasedRewardCalculation;
                pointsJumbler.JumbleTextNow();
                // messageText.text = levelMessage;
                //MessageObj.SetActive (true);
                buttonText.text = "Next Level >";
                correctEnd.SetActive(true);
            }
            //Lose Screen
            else
            {
                LevelCompleteText.text = "Level failed!";
                RewardText.text = "Try Again";
                //messageText.text = levelMessage;
                //MessageObj.SetActive (false);
                buttonText.text = "Retry Level >";
                incorrectEnd.SetActive(true);
            }
            //Tell the minigame controller to stop running the game
            foreach (GameObject g in Controller)
            {
                g.GetComponent<MinigameController>().EndGame();
            }
            GameController.Maincontroller.AddPoints(Reward);
            DisableObjects();
            //screenTap.SetActive (true);  
        }
    }

    //Handles the information text before each minigame
    public void ScreenTapIncrement(){
		if(InfoList.Length <= currInfoNum){
	
            //disable the information visual objects
			InfoObj.SetActive (false);
            //disable the screen tap incrementer UI element
			screenTap.SetActive (false);
            foreach (GameObject g in Controller)
            {
                g.GetComponent<MinigameController>().StartGame();
            }
            EnableObjects();
        }
		else{
			infoText.text = InfoList [currInfoNum];
			currInfoNum++;
		}
        //Play the sound for tapping the screen pre-game
        soundMain.PlayOneShot(tapIncrementSound);
	}


    //Deduct a life and determine if the game should end
   public void loseLifeNow()
    {
        int index = 0;
        foreach(Image i in lives)
        {
            if (index <= livesLeft -1)
            {
                i.gameObject.SetActive(true);
            }
            else
            {
                i.gameObject.SetActive(false);
            }
            index++;
        } 
        if(livesLeft <= 0)
        {
            EndLevel(false, "You lost!");
        }
    }

    //Set the win condition for the game on UI
    public void setWinCondition(int winInt)
    {
        winLeft = winInt;
        winCondition.text = winLeft.ToString();
    }
    
    //Loop through object list to enable on Start
    public void EnableObjects()
    {
        foreach(GameObject g in ObjectsToEnableOnStart)
        {
            if(g != null)
                g.SetActive(true);
        }
    }

    //Disable all objects that were enabled at start
    public void DisableObjects()
    {
        foreach(GameObject g in ObjectsToEnableOnStart)
        {
            if (g != null)
                g.SetActive(false);
        }
    }


    //Pause the game
    public void PauseGameNow()
    {
        if (!isPaused)
        {
            foreach (GameObject g in Controller)
            {
                if (g.GetComponent<MinigameController>())
                {
                    g.GetComponent<MinigameController>().PauseGame();
                }
            }
            foreach(GameObject obj in ObjectsToDisable)
            {
                obj.SetActive(false);
            }
            if (AdditionalObjectToDisable != null)
                AdditionalObjectToDisable.SetActive(false);
            if (AdditionalObjectToDisable2 != null)
                AdditionalObjectToDisable2.SetActive(false);
            PauseMenuHolder.SetActive(true);
            isPaused = true;
        }
        else
        {
            UnpauseGameNow();
        }
    }


    //Resume the game
    public void UnpauseGameNow()
    {
        foreach (GameObject g in Controller)
        {
            if (g.GetComponent<MinigameController>())
            {
                g.GetComponent<MinigameController>().ResumeGame();
            }
        }
        foreach (GameObject obj in ObjectsToDisable)
        {
            obj.SetActive(true);
        }
        if (AdditionalObjectToDisable != null)
            AdditionalObjectToDisable.SetActive(true);
        if (AdditionalObjectToDisable2 != null)
            AdditionalObjectToDisable2.SetActive(true);
        PauseMenuHolder.SetActive(false);
        isPaused = false;
    }


    //Disables the sound
    public void DisableAllSound()
    {
        GameController.Maincontroller.DisableSound();
    }

    //Enables the sound or disable it if it is already enabled
    public void EnableAllSound()
    {
        if (!SoundEnabled)
        {
            GameController.Maincontroller.EnableSound();
            SoundEnabled = true;
        }
        else
        {
            DisableAllSound();
            SoundEnabled = false;
        }
    }
    

    //Changes Level to go to the main menu
    public void GoToMainMenu()
    {
        GameController.Maincontroller.GoToMainMenu();
    }

    //Change to go to specific Level with Build index
    public void GoToLevel(int level)
    {
        GameController.Maincontroller.GoToLevel(level);
    }

    //Change to go to level using Scene name
    public void GoToLevel(string level)
    {
        GameController.Maincontroller.GoToLevel(level);
    }


    //Decrease life by 1
    public void LoseLife()
    {
        livesLeft--;
    }

    //Play a specific sound clip
    public void PlaySound(AudioClip snd)
    {
        soundMain.PlayOneShot(snd);
    }

}

