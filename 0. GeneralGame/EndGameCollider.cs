using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameCollider : MonoBehaviour {


    //is this path correct
	[SerializeField]
	private bool isCorrectPath;
	[SerializeField] 
    //Message for end game screen
	private string endGameMessage;
	[SerializeField]
	private string playerTag = "Player";
    [SerializeField]
    private int wrongCount = 1;
    private Collider hit;
    [SerializeField]
    private int WinPoints = 50;
  
    //Check the object hit
	void OnTriggerEnter(Collider other){
        //Did we hit the player
        if (other.transform.root.tag == playerTag){
            hit = other;
            other.transform.root.tag = "left";
            if(!isCorrectPath)
            wrongPathIncrement();
            else
            {
                GameController.thisLevelController.LevelBasedRewardCalculation = WinPoints;
                GameController.thisLevelController.EndLevel(true, "Good Work! You avoided the hazards!");
            }
		}
	}

    //Wrong path/ lose life
    void wrongPathIncrement()
    {

        loseLife();
        if (hit.transform.root.GetComponent<CarRoadMove>())
        {
            hit.transform.root.GetComponent<CarRoadMove>().setSpeed(0.5f);
        }
    }


    //minus a life from the level controller
    public void loseLife()
    {
        GameController.thisLevelController.livesLeft--;
        GameController.thisLevelController.loseLifeNow();
    }
}
