using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarRoadMove : MonoBehaviour {

    //reference to the main audio to play swipe sounds
    //(this should be removed in later update as
    //FlickItSwipeInput can tell audio to play sounds when swiping)
    [SerializeField]
    public AudioSource soundMain;
    [SerializeField]
    private AudioClip swipeSound;

    //Speed of the car
    [SerializeField]
    private float Speed = 4;
    //Checking the trigger colliders
    private int checkCount = 2;
    //Reference to the objects rigidbody
    Rigidbody myRigidbody;
    private Vector3 rotationChange;
    [SerializeField]
    private float rotationSpeed = 2;
    //do we rotate the car now
    [SerializeField]
    private bool isDivert;
    //have we swiped
    [SerializeField]
    private bool hasDiverted;
    //did we swipe left, other we swiped right
    private bool SwipedLeft;
    //was this car diverted completely before destroyed
    private bool divertedCar;
    //are we showing the arrow already
    private bool shownArrow;

    //are we diverting left?
    public bool divertLeft;
    [SerializeField]
    //How long does the car turn for
    private float diversionTime;
    [SerializeField]
    private float destroyTime = 9;
    //Animation reference to the arrow object's animation class
    [SerializeField]
    private Animation diversionArrow;
    //arrow animation names
    [SerializeField]
    private string showArrowName = "showArrow";
    [SerializeField]
    private string flashArrow = "flashArrow";
    [SerializeField]
    private string showArrowLeft = "ArrowShow";
    [SerializeField]
    private string showArrowRight = "ArrowShowright";
    [SerializeField]
    private string unShowArrow = "ArrowunShow";
    //the controller that spawned me (cannot be static since there are two in this particular game)
    [SerializeField]
    public FlickItController Spawner;
    public bool InvertDivert;


    //Define the reference to the objects Rigidbody 
    //for movement
    //and begin self-destruct
    void Start() {
        myRigidbody = GetComponent<Rigidbody>();
        Destroy(gameObject, 9);
    }

    //When Destroy() is called and before
    //this object is terminated

    void FixedUpdate() {
        //Move the car forward 
        myRigidbody.MovePosition(transform.position + transform.forward * Speed * Time.deltaTime);
        //Turn the car if it is diverted
        if (isDivert) {
            Quaternion deltaRotation = Quaternion.Euler(rotationChange * rotationSpeed * Time.deltaTime);
            myRigidbody.MoveRotation(myRigidbody.rotation * deltaRotation);
        }
    }

    //
    private void DivertCar(bool Left) {
        isDivert = true;
        divertedCar = true;
        divertLeft = Left;
        if (divertLeft) {
            DivertLeft();
        }
        else {
            DivertRight();
        }
        //End the diversion after a set amount of time
        Invoke("EndDiversion", diversionTime);
    }

   /* IEnumerator EndDiversion(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        isDivert = false;
    }*/

      private void OnMouseDown()
      {
        swipeDivert(true);
      }

    //set the car to divert Left
    void DivertLeft(){
        rotationChange = new Vector3(0, 180, 0);
        unshowArrow();

	}

    //set the car to divert Right
	void DivertRight(){
        rotationChange = new Vector3(0, -180, 0);
        unshowArrow();
    }

    //stop diverting the car
    void EndDiversion()
    {
        isDivert = false;
    }

    //enable the car to be diverted
    public void swipeDivert(bool left)
    {
        if (InvertDivert)
        {
            divertLeft = !left;

            if (checkCount > 0)
                if (!shownArrow)
                    showArrow(left);
        }
        else
        {
            divertLeft = left;

            if (checkCount > 0)
                if (!shownArrow)
                    showArrow(!left);
        }
        hasDiverted = true;
        soundMain.PlayOneShot(swipeSound);
    }
    
    //When the car's collider is triggered by 
    //the divert object it will proceed to turn the appropriate way
    //There are two divert objects because early iterations had two sides of the road (two way street).
    void OnTriggerEnter(Collider other){
        //Have we already been diverted?
       
        if (hasDiverted && !isDivert)
            {
            print("hitdiversion");
                if (other.gameObject.tag == "left" && divertLeft)
                {
                    DivertCar(true);
                }
                else if (other.gameObject.tag == "right" && !divertLeft)
                {
                    DivertCar(false);
                }
            }
       if (other.gameObject.tag == "WinTag")
        {
            print("Success");
            Spawner.successfullyDivertedCar(Speed);
        }
        checkCount--;
	}

  

    //change the rotationalSpeed of this car
    public void changeRotationSpeed(float multiplier)
    {
        diversionTime = diversionTime * multiplier;
        rotationSpeed = rotationSpeed * multiplier;
    }

    //show arrow by running animation depending 
    //which way it is ment to be shown
    public void showArrow(bool left)
    {
        shownArrow = true;
        if (left) 
        diversionArrow.Play(showArrowRight, PlayMode.StopAll);
        else
          diversionArrow.Play(showArrowLeft, PlayMode.StopAll);
    }

    //makes allow dissapear gracefully through animation
    public void unshowArrow()
    {
        diversionArrow.Play(unShowArrow, PlayMode.StopAll);
    }

    public void setSpeed(float speed)
    {
        Speed = speed;
    }
    public void setSound(AudioSource source)
    {
        soundMain = source;
    }
    public void setRotationSpeed(float speed)
    {
        rotationSpeed = speed;
    }
    public void setDiversionTime(float divertTime)
    {
        diversionTime = divertTime;
    }

}
