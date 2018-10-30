using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllCar : MinigameController {

    //reference the current Car
	[SerializeField]
	private MoveCar carControlled;

    private bool gameStarted;
    public bool AllowMouseInput = true;

	void Update () {
        
        //did we touch the screen this frame?
		if (gameStarted && Input.touchCount > 0)
		{
            //Send a raycast to the touch position
			var ray = Camera.main.ScreenPointToRay (Input.GetTouch(0).position);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) 
			{
          
		
                    //did we hit the road (a place to move the car)
				if (hit.collider.tag == "Road") {
						carControlled.moveCarToPosition (hit.point);		
				} 
			}
		}
        if (AllowMouseInput && Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {


                //did we hit the road (a place to move the car)
                if (hit.collider.tag == "Road")
                {
                    carControlled.moveCarToPosition(hit.point);
                }
            }
        }
     
	}

    //Begin game
    override
    public void StartGame()
    {
        gameStarted = true;
    }
    //End game
    override
    public void EndGame()
    {
        gameStarted = false;
    }

    //Pause game by setting time scale to 0.
    override
    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    //Resume game by setting timescale to 1.
    override
    public void ResumeGame()
    {
        Time.timeScale = 1;
    }


}
