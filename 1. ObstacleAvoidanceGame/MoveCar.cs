using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveCar : MonoBehaviour {

    //The built-in unity pathfinding class
    NavMeshAgent nav;
	[SerializeField]
	private GameObject smokeParticles;
	[SerializeField]
	private GameObject smokeEmissionPoint;
    //Current place the car is heading towards
	[SerializeField]
	private Vector3 currTargetPosition;

	void Start () {
        //get the navigation mesh agent component reference
		nav = GetComponent <NavMeshAgent> ();
	}
	

    //Move the car to a spot 
    //where the parameter is a position in the world
	public void moveCarToPosition(Vector3 whereToMove){
		currTargetPosition = whereToMove;
		nav.SetDestination (currTargetPosition);

	
	}
}
