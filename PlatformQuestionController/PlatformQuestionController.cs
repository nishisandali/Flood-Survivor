using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlatformQuestionController : MinigameController
{

    //Track of UI elements
    //The answer options text array to set
    [SerializeField]
    private Text[] AnswerTextList;
    [SerializeField]
    private GameObject QuestionHolder;
    //The correct answer's index in the array
    [SerializeField]
    private int CorrectAnswerNum;
    [SerializeField]
    private int amountOfQuestions;
    //Reference to UI element to set question text
    [SerializeField]
    private Text QuestionText;

    [SerializeField]
    private QuestionClass[] Questions;


    //Reference to boy's animators
    Animator Anim;
    Animation Anim2;
    //animator parameter which initiates boy jump animation sequence
    public string jumpAnimParameter = "Jump";

    //Force of the jumps (adjusted in editor)
    public float JumpForce = 0.5f;
    //Direction of the jumps (adjusted in editor)
    public Vector3 JumpDirection;

    //amount of times jumped and also how many correct answers
    [SerializeField]
    private int CurrentScore = 0;
    [SerializeField]
    private int destroyWait = 2;
    [SerializeField]
    private int PlatformAmount = 10; //Amount of questions in the level
    [SerializeField]
    private GameObject Platform;//Platform object to instantiate (change to array then spawn random)
    [SerializeField]
    private GameObject WinningHill;
    [SerializeField]
    private Queue<GameObject> PlatformList = new Queue<GameObject>();//Store the platforms in a Queue to destroy when not in camera view
    [SerializeField]
    private Vector3 PlatformDistance;//Distance between platforms
    [SerializeField]
    private Vector3 HillDistance; //Vector position of the hill relative to the previously instantiated platform
    [SerializeField]
    private Vector3 LastPlatformSpot; //Store the spot of the last platform

    private bool canPlayGame;
    private bool HasGeneratedHill;

    private void Start()
    {
        Anim = GetComponent<Animator>();
        Anim2 = GetComponent<Animation>();
    }

    private void Update()
    {
        if (canPlayGame && Input.GetKeyDown(KeyCode.Space))
        {
            JumpNow();
        }
    }

    private void JumpNow()
    {
        Anim.SetBool(jumpAnimParameter, true);
        GetComponent<Rigidbody>().AddForce(JumpForce * JumpDirection);
        CurrentScore++;
        destroyPlatform();
        createPlatform();
    }

    private void createPlatform()
    {
        if (CurrentScore < CorrectAnswerNum - 1)
        {
            Vector3 newPlatformSpot = LastPlatformSpot + PlatformDistance;
            GameObject newPlatform = Instantiate(Platform, newPlatformSpot, Quaternion.identity);
            PlatformList.Enqueue(newPlatform);
            LastPlatformSpot = newPlatformSpot;
        }
        else
        {
            if (!HasGeneratedHill)
            {
                generateWinHillObject();
            }
        }
    }

    private void destroyPlatform()
    {
        //Dont start destroying platforms until jumped twice
        if (CurrentScore > destroyWait)
        {
            Destroy(PlatformList.Peek());
            PlatformList.Dequeue();
        }

    }

    public void ChooseAnswer(int answerNum)
    {

        DisableQuestionUI();
    }

    private void CorrectResponse()
    {

    }
    private void IncorrectResponse()
    {

    }

    private void DisableQuestionUI()
    {
        QuestionHolder.SetActive(false);
    }

    public void EnableQuestionUI()
    {
        QuestionHolder.SetActive(true);
    }

    private void generateWinHillObject()
    {
        Vector3 newPlatformSpot = LastPlatformSpot + HillDistance;
        GameObject newPlatform = Instantiate(WinningHill, newPlatformSpot, Quaternion.identity);
        newPlatform.transform.eulerAngles = new Vector3(-90, 0, 0);
        HasGeneratedHill = true;
    }

    //private void

    public override void StartGame()
    {
        canPlayGame = true;
        EnableQuestionUI();
    }

    public override void EndGame()
    {
        canPlayGame = false;
        DisableQuestionUI();
    }

    public override void PauseGame()
    {
        throw new NotImplementedException();
    }

    public override void ResumeGame()
    {
        throw new NotImplementedException();
    }
}

