using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickItController : MinigameController {

    //Main sound source for playing sound effects
    [SerializeField]
    private AudioSource soundMain1;
  
    //Place in the game word to spawn the cars.
    [SerializeField]
	private Transform[] spawnPlace = new Transform[2];

    //The car prefabs to spawn
	[SerializeField]
	private GameObject[] spawnCars;

	//Rates of spawning and spawn randomness
	[SerializeField]
	private float spawnIntervals = 0.4f;
	[SerializeField]
	private float spawnRandomness = 1.0f;
	[SerializeField]
	private float frequencyMultiplier = 1.05f;

    //End game conditions
    [SerializeField]
    private int spawnAmountStart = 20;
	[SerializeField]
	public static int spawnAmount = 20;
    [SerializeField]
    private static int divertedAmount;
	[SerializeField]
	private float timeCountDown = 20.0f;


	//Car Speed and rotation 
    //note: speed and rotatetime are inversely related
	[SerializeField]
	private float initialCarSpeed = 2;
	[SerializeField]
	private float carSpeedMultiplier = 1.1f;
    [SerializeField]
    private float initialCarRotation = 1;
    [SerializeField]
    private float initialCarRotationTime = 0.5f;

    //keep track of the current level controller.
	[SerializeField]
	private LevelController levelController;
	[SerializeField]
	private string endGameMessage = "Good Job you diverted all the cars!";
    [SerializeField]
    private bool gameEnded;
    //When a car is facing the opposite direction, it must go the functionally opposite way when swiped (as left is defined by left swipe but it is actually right relative to the turning direction)
    [SerializeField]
    private bool isOppositeDirection;


    [SerializeField]
    private int RewardPerCar;
    [SerializeField]
    private int RewardCompletion;
    [SerializeField]
    private int AdditiveCarSpeedReward; //difficult based
    
    //Start the spawning of cars
    private  void BeginSpawn(){
		Invoke ("SpawnCar", 0);
	}

    //Spawn a random car from the array
    //and set the car's fields
	public void SpawnCar(){
        //make sure game isn't over
        if (!gameEnded)
        {
            Transform spawnSpot = determineSpawnSpot();
            GameObject carToSpawn = determineSpawnCar();
            //spawn car
            GameObject g = Instantiate(carToSpawn, spawnSpot.position, spawnSpot.rotation) as GameObject;
            //set variables
            CarRoadMove carController = g.GetComponent<CarRoadMove>();
            carController.setSpeed(initialCarSpeed * carSpeedMultiplier);
            carController.setSound(soundMain1);
            carController.setRotationSpeed(initialCarRotation);
            carController.setDiversionTime(initialCarRotationTime);
            carController.Spawner = this;
            if (isOppositeDirection)
            {
                carController.InvertDivert = true;
            }
            //carController.changeRotationSpeed(carSpeedMultiplier);
            
            levelController.setWinCondition(spawnAmount);
            //check if the spawn amount is 0 and end the game
            if (spawnAmount <= 0)
            {
                levelController.EndLevel(true, endGameMessage);
            }
            //continue spawning after set intervals
            else
            {
                Invoke("SpawnCar", spawnIntervals + Random.Range(0, spawnRandomness));
            }
        }
	}

    //get the random spawn spot
	Transform determineSpawnSpot(){
		return spawnPlace [Random.Range (0, spawnPlace.Length)];
	}

    //get the random car colour to spawn
	GameObject determineSpawnCar(){
		return spawnCars [Random.Range (0, spawnCars.Length)];
	}

    //Begin game
    override
	public void StartGame(){
        spawnAmount = spawnAmountStart;
		BeginSpawn ();
	}
    //End game
    override
    public void EndGame()
    {
        CancelInvoke("SpawnCar");
        spawnAmount = spawnAmountStart;
        gameEnded = true;
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



    //Minus the amount to spawn after diverting a car
    public void successfullyDivertedCar(float Carspeed)
    {
        spawnAmount--;
        divertedAmount++;
        AdditiveCarSpeedReward = AdditiveCarSpeedReward + (int)((Carspeed / initialCarSpeed) * 10) ;
        if (spawnAmount <= 0)
        {
            levelController.LevelBasedRewardCalculation = (divertedAmount * RewardPerCar) + RewardCompletion + AdditiveCarSpeedReward ;
            levelController.EndLevel(true, endGameMessage);
        }
    }
  

   
}
