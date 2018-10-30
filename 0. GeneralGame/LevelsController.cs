using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsController : MonoBehaviour {

    public int[] LevelsCost;
    public LevelUIElementTracker[] LevelsTracker;
    public bool[] LevelUnlocked;
    [SerializeField]
    private AudioClip UnlockSound;

    private void Start()
    {
        LoadUnlockedLevels();
    }


    public void UnlockGame(int GameIndex)
    {
        if (GameController.saveController.GetPoints() > LevelsCost[GameIndex])
        {
            //Maybe play unlock animation?
            LevelsTracker[GameIndex].UnlockGame();
            GameController.saveController.AddPoints(-1 * LevelsCost[GameIndex]);
            GameController.saveController.UnlockLevel(GameIndex);
            print("unlocked game for " + LevelsCost[GameIndex]);
            GetComponent<AudioSource>().PlayOneShot(UnlockSound);
        }
        else
        {
            print("cant afford Game");
        }
    }  

    public void LoadUnlock(int index)
    {
        LevelsTracker[index].UnlockGame();
    }

 

    public void GoToLevel(int Level)
    {
        GameController.Maincontroller.GoToLevel(Level);
    }

    public void GoToLevel(string level)
    {
        GameController.Maincontroller.GoToLevel(level);
    }





    public void LoadUnlockedLevels()
    {
        int ind = 2;
        string[] levelsPrefsNames = GameController.saveController.LevelsUnlockedPrefName;
        foreach(string s in levelsPrefsNames)
        {
            if(PlayerPrefs.GetInt(s) == 1)
            {
                LoadUnlock(ind);
            }
            ind++;
        }
    }
}
