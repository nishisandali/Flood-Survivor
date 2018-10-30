using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveAndLoadController : MonoBehaviour {


    [SerializeField]
    public Text PointsText;




    [SerializeField]
    private string HasInitialisedPrefsName;
    [SerializeField]
    private string pointsPrefsName;
    [SerializeField]
    private string totalAccumulatedPointsPrefsName;
    [SerializeField]
    private string OwnedItemsPrefName;

    [SerializeField]
    private bool hasInitialisedPrefs;
    [SerializeField]
    private int currentPoints;
    [SerializeField]
    private int accumulatedPoints;
    [SerializeField]
    private int ownedItems1;
    [SerializeField]
    private int ownedItems2;

    [SerializeField]
    private int[] ownedItemsArray1;
    [SerializeField]
    private int[] ownedItemsArray2;


    [SerializeField]
    public string[] LevelsUnlockedPrefName = new string[4];
    [SerializeField]
    private int[] LevelsUnlocked = new int[4];

    private void Start()
    {
        InitialiseSave();
    }

    public void InitialiseSave()
    {
        if (PlayerPrefs.GetInt(HasInitialisedPrefsName) != 1)
        {
            PlayerPrefs.SetInt(HasInitialisedPrefsName, 1);
            hasInitialisedPrefs = true;
            PlayerPrefs.SetInt(pointsPrefsName, 10);
            PlayerPrefs.SetInt(totalAccumulatedPointsPrefsName, 10);
            PlayerPrefs.SetInt(OwnedItemsPrefName + "1", 1000000000);
            PlayerPrefs.SetInt(OwnedItemsPrefName + "2", 1000000000);
            PlayerPrefs.Save();
        }
     
    }

    public void LoadSavedStats()
    {
        currentPoints = PlayerPrefs.GetInt(pointsPrefsName);
        accumulatedPoints = PlayerPrefs.GetInt(totalAccumulatedPointsPrefsName);
        ownedItems1 = PlayerPrefs.GetInt(OwnedItemsPrefName + "1");
    }

    public int GetPoints()
    {
        return currentPoints;
    }

    public int GetAccumulatedPoints()
    {
        return accumulatedPoints;
    }

    public int GetOwnedItems()
    {
        return ownedItems1;
    }


    public void AddPoints(int ToAdd)
    {
        currentPoints += ToAdd;
        PlayerPrefs.SetInt(pointsPrefsName, currentPoints);
        SetPoints();
    }

    public void SetPoints()
    {
        if (PointsText != null)
            // PointsText.GetComponent<JumbleNumbersText>().JumbleTextForTime(PointsText, currentPoints, 0.7f);

            PointsText.text = currentPoints.ToString() ;
    }


    public void UseItem(int itemStructureNum, int ItemStructureIndex)
    {

    }

    public void PurchaseItem(int itemStructureNum, int ItemStructureIndex, int ItemPurchaseAmount)
    {

    }

    public void UnlockLevel(int level)
    {
        PlayerPrefs.SetInt(LevelsUnlockedPrefName[level-2], 1);
        LevelsUnlocked[level-2] = 1;
    }

    public void UnlockAllLevels()
    {
        int ind = 0;
        foreach(string s in LevelsUnlockedPrefName)
        {
            if (s != "")
            {
                PlayerPrefs.SetInt(s, 1);
                LevelsUnlocked[ind] = 1;
            }
            ind++;
        }
    }

    public void ResetLevels()
    {
        int ind = 0;
        foreach (string s in LevelsUnlockedPrefName)
        {
            if (s != "")
            {
                PlayerPrefs.SetInt(s, 0);
                LevelsUnlocked[ind] = 0;
            }
            ind++;
        }
    }

    public void ResetGame()
    {
        currentPoints = 0;
        PlayerPrefs.SetInt(pointsPrefsName, currentPoints);
        ResetLevels();
        SetPoints();
    }

}
