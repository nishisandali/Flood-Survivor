using UnityEngine;

public class BoatController : MonoBehaviour {



    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    private float boatSpeed;
    [SerializeField]
    private float naturalSlowSpeed;
    [SerializeField]
    private float maxTurnSpeed;
    [SerializeField]
    private float turnSpeed;
    [SerializeField]
    private float brakeSpeed;
    [SerializeField]
    private float smoothSpeed = 0.01f;

    [SerializeField]
    private bool isMoving;
    [SerializeField]
    private bool isTurning;
  
    [SerializeField]
    private bool isBraking;

    [SerializeField]
    private float updater = 2f;
    [SerializeField]
    private float multiplier = 0.2f;

    [SerializeField]
    private float turnMultiplier = 0.01f;
    [SerializeField]
    private float turnIncrement = 0.01f;

    [SerializeField]
    private Transform boat;
    private Rigidbody rigid;


    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        boat = transform;
    }

    // Update is called once per frame
    void Update () {
        //float accelx;
        //float accely;
        float forwardMove;
        float BrakeAmount;

       // boat.localEulerAngles  =  new Vector3(accelx, 0, accely);
        if (isMoving)
        {
            if (boatSpeed < maxSpeed)
            {
                boatSpeed += smoothSpeed;
            }
        }
        else
        {
            if (boatSpeed > 0)
            {
                boatSpeed -= smoothSpeed / 3   ;
            }
        }      


        if (!isTurning) {
            if (turnSpeed > 0)
                turnSpeed -= turnIncrement;
            else if (turnSpeed < 0)
                turnSpeed += turnIncrement;
            else if (turnSpeed == 0)
                turnSpeed = 0;
        }

        if (isMoving)
        {

            rigid.AddForce(boatSpeed * transform.forward);
            //move and rotate the boat
           // forwardMove = Mathf.Lerp(boat.position.x, boat.position.x + boatSpeed, updater * Time.time);
            //Vector3 nextPos = transform.forward * forwardMove;
            
        }


        Vector3 nextRot = new Vector3( boat.eulerAngles.x, turnSpeed + boat.eulerAngles.y,  boat.eulerAngles.z);
        boat.eulerAngles = Vector3.Lerp(boat.eulerAngles, nextRot, updater * Time.time);

        //input area
        if (Input.GetMouseButton(0))
            isMoving = true;
        else
            isMoving = false;

        //turn right
        if (Input.GetKey(KeyCode.RightArrow))
        {
            isTurning = true;
            if(turnSpeed < maxTurnSpeed)
                turnSpeed += turnIncrement;
        }
        //turn left
        else if(Input.GetKey(KeyCode.LeftArrow))
        {
            isTurning = true;
            if(turnSpeed > maxTurnSpeed * -1)
                turnSpeed -= turnIncrement;
        }
        else
        {
            isTurning = false;
        }

	}
}
