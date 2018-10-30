using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AdminPageController : MonoBehaviour {

    public GameObject AdminPageObject;
    public GameObject AdminControlObject;
    public Text AdminDebugText;
    private string AdminPassword = "sesadmin";
    public Text AdminPasswordText;

    private void Start()
    {

    }

    public bool CheckPassword(string password)
    {
        if(AdminPassword == password)
        {
            return true;
        }
        return false;
    }


    public void AttemptToLoginToAdmin()
    {
        if (CheckPassword(AdminPasswordText.text))
        {
            AdminControlObject.SetActive(true);
            AdminPasswordText.text = "Welcome";
            DebugAdmin("Correct Password, Admin Controls UNLOCKED");
        }
        else
        {
            AdminPasswordText.text = "";
            DebugAdmin("Incorrect Password, SES Staff Only.");
        }
    }

    public void AddAdminPoints()
    {
        GameController.saveController.AddPoints(1000);
        DebugAdmin("Added 1000 Points");
    }

    public void UnlockAllLevels()
    {
        GameController.saveController.UnlockAllLevels();
        DebugAdmin("All Levels Unlocked");
    }

    public void ResetGame()
    {
        GameController.saveController.ResetGame();
        DebugAdmin("Game Reset, Points set to 0 and all levels are Locked");
    }


    public void DebugAdmin(string dbug)
    {
        AdminDebugText.text = dbug;
        StartCoroutine(EndDebugTextShow(3));
    }

    IEnumerator EndDebugTextShow(float showtime)
    {
        yield return new WaitForSeconds(showtime);
        AdminDebugText.text = "";
    }
	
    public void ReturnToMenu()
    {
        AdminPasswordText.text = "";
        AdminPageObject.SetActive(false);
        AdminControlObject.SetActive(false);
    }

    public void ShowAdminPage()
    {

        AdminPageObject.SetActive(true);
        AdminControlObject.SetActive(false);
    }

}
