
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class BoyPlatformerController : MinigameController {

    //Track of UI elements
    //The answer options text array to set
    [SerializeField]
    private Text[] AnswerTextList;
    [SerializeField]
    private GameObject QuestionHolder;
   
    private int CurrentCorrectAnswer;
    //how many questions to ask
    [SerializeField]
    private int CorrectAnswerNum;
    [SerializeField]
    private int amountOfQuestions;
    //Reference to UI element to set question text
    [SerializeField]
    private Text QuestionText;
    [SerializeField]
    private Image AnswerStateImage;
    [SerializeField]
    private Sprite CorrectSprite;
    [SerializeField]
    private Sprite IncorrectSprite;
  

    [SerializeField]
    private QuestionClass[] Questions;
    [SerializeField]
    private QuestionClass[] CurrentQuestions;
    [SerializeField]
    private int[] QuestionIndexes = new int[4];
    [SerializeField]
    private float[] PlatformYSpots = new float[4] ;
    
    //Reference to boy's animators
    private Animator Anim;
    private Animation Anim2;
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
    private float UnshowTime = 1.5f;
    [SerializeField]
    private float ShowTime = 2.2f;
    [SerializeField]
    private int PlatformAmount = 10; //Amount of questions in the level
    
    //Game state visual objects and variables
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
    [SerializeField]
    private Transform RisingWater;
    [SerializeField]
    private Vector3 WaterRiseAmount;
    [SerializeField]
    private Vector3 RisingWaterEndSpot;
    [SerializeField]
    private Vector3 RisingSpeed = Vector3.zero;
    [SerializeField]
    private float RisingWaterTime = 2;
    [SerializeField]
    private bool WaterIsRising = false;

    [SerializeField]
    private Vector3 WaterStartSpot;
   
    private bool canPlayGame;
    private bool HasGeneratedHill;
    [SerializeField]
    private string correctUIAnim = "CorrectUIAnim";
    [SerializeField]
    private string incorrectUIAnim = "IncorrectUIAnim";
    [SerializeField]
    private string AnswerStateAnim = "AnswerStateAnim";
    [SerializeField]
    private Color InitialBgColor;

    [SerializeField]
    private int RewardPerQuestion = 25;
    [SerializeField]
    private int RewardWin = 100;
    [SerializeField]
    private int livesLost = 0;
    [SerializeField]
    private int CorrectAnswers = 0;
    private Vector3 StartRisePos;
    private float t;

    [SerializeField]
    private AudioClip CorrectSound;
    [SerializeField]
    private AudioClip IncorrectSound;
    [SerializeField]
    private AudioClip JumpSound;
   

    private void Start () {
        Anim = GetComponent<Animator>();
        Anim2 = GetComponent<Animation>();
        GenerateQuestionArray();
        MixQuestionAnswers();
        SetCurrentQuestionUI();
        SetRisingWaterSpot();
	}

    private void Update()
    {
        if (canPlayGame && Input.GetKeyDown(KeyCode.Space))
        {
            JumpNow();
        }

        if (WaterIsRising)
        {
            t += Time.deltaTime / RisingWaterTime;
            // RisingWater.position = Vector3.SmoothDamp(RisingWater.position, RisingWaterEndSpot, ref RisingSpeed, RisingWaterTime);
            RisingWater.position = Vector3.Lerp(StartRisePos, RisingWaterEndSpot, t);
        }

    }

    private void SetRisingWaterSpot()
    {
        RisingWater.position = WaterStartSpot;
    }

    private void RiseRisingWater()
    {

            // RisingWaterEndSpot = RisingWater.position + (PlatformDistance) + new Vector3(0,0.5f,0) ;
            RisingWaterEndSpot = new Vector3(0 , PlatformYSpots[CorrectAnswers] - (1.57f + (1.57f/livesLost)), 0);
        StartRisePos = RisingWater.position;
        t = 0;
            WaterIsRising = true;
            StartCoroutine(StopRisingWaterAfterTime());
        RisingWaterEndSpot = RisingWater.position + (PlatformDistance);
        WaterIsRising = true;
        StartCoroutine(StopRisingWaterAfterTime());
    }
    IEnumerator StopRisingWaterAfterTime()
    {
        yield return new WaitForSeconds(RisingWaterTime);
        WaterIsRising = false;
        //RisingWater.position = RisingWaterEndSpot;
        
    }
    IEnumerator StopRisingWaterAfterTimeAndLoseLife()
    {
        yield return new WaitForSeconds(RisingWaterTime);
        WaterIsRising = false;
        GameController.thisLevelController.loseLifeNow();
    }


    private void RiseRisingWaterLostLife()
    {
        t = 0;
        StartRisePos = RisingWater.position;
        RisingWaterEndSpot = RisingWater.position + (new Vector3(0, 1.57f / livesLost, 0));
        WaterIsRising = true;
        StartCoroutine(StopRisingWaterAfterTimeAndLoseLife());
    }

    private void JumpNow()
    {
        Anim.SetBool(jumpAnimParameter, true);
        PlayJumpSound();
        GetComponent<Rigidbody>().AddForce(JumpForce * JumpDirection);
        CurrentScore++;
        destroyPlatform();
        createPlatform();
        StartCoroutine(ShowNextQuestionAfterTime());
    }

    private void createPlatform()
    {
        if (CurrentScore < CorrectAnswerNum -1)
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
      if (CheckAnswer(answerNum))
        {
            CorrectResponse(answerNum);
        }
      else
        {
            IncorrectResponse(answerNum);
        }
      
     // DisableQuestionUI();
    }

    private bool CheckAnswer(int Answer)
    {
        if (CurrentCorrectAnswer == Answer)
        {
            return true;
        }
        return false;
    }

    private void CorrectResponse(int index)
    {
        CorrectAnswers++;
        AnswerTextList[index].transform.parent.GetComponent<Animation>().Play(correctUIAnim, PlayMode.StopAll);
        BeginCorrectUIAnimations();
    }
    private void IncorrectResponse(int index)
    {
        livesLost--;
        AnswerTextList[index].transform.parent.GetComponent<Animation>().Play(incorrectUIAnim, PlayMode.StopAll);
        BeginIncorrectUIAnimations();
   
    }
    private void BeginCorrectUIAnimations()
    {
        AnswerStateImage.sprite = CorrectSprite;
        PlayCorrectSound();
        StartCoroutine(UnshowUIAfterTime(true));
    }
    private void BeginIncorrectUIAnimations()
    {
        AnswerStateImage.sprite = IncorrectSprite;
        PlayIncorrectSound();
        StartCoroutine(UnshowUIAfterTime(false));
    }


    IEnumerator UnshowUIAfterTime(bool Correct)
    {
        yield return new WaitForSeconds(UnshowTime);
        DisableQuestionUI();
        ResetQuestionsBackground();
        AnswerStateImage.gameObject.GetComponent<Animation>().Play(AnswerStateAnim, PlayMode.StopAll);
        yield return new WaitForSeconds(0.5f);

        if (Correct)
        {
            JumpNow();
            RiseRisingWater();
        }
        else
        {
            WaterRise();
            RiseRisingWaterLostLife();
        }
    }

    IEnumerator ShowNextQuestionAfterTime()
    {
        
        yield return new WaitForSeconds(ShowTime);
        if (GameController.thisLevelController.livesLeft > -1)
        {
            if (CurrentScore < amountOfQuestions)
            {
                SetCurrentQuestionUI();
                EnableQuestionUI();
            }
            else if (CurrentScore >= amountOfQuestions)
            {
                GameController.thisLevelController.EndLevel(true, "Congratulations, You made it to safety");
            }
        }
    }

    private void WaterRise()
    {     
        
        GameController.thisLevelController.LoseLife();
        StartCoroutine(ShowNextQuestionAfterTime());
    }

    public void GenerateQuestionArray()
    {
        for(int i = 0; i < CorrectAnswerNum; i++)
        {
            int rand = 0;
            bool makeIntDistinct = true;
            while (makeIntDistinct)
            {
                rand = Random.Range(0, Questions.Length);
                makeIntDistinct = false;
                for (int i2 = 0; i2 < CorrectAnswerNum; i2++)
                {

                    if (rand != 0 && rand == QuestionIndexes[i2])
                    {
                        makeIntDistinct = true;
                    }
                }
            }
            CurrentQuestions[i] = Questions[rand];
            QuestionIndexes[i] = rand;
        }
    }

    private void MixQuestionAnswers()
    {
        for(int i = 0; i < CurrentQuestions.Length; i++)
        {
            CurrentQuestions[i].MixAnswers();
        }
    }

    private void ResetQuestionsBackground()
    {
        for (int i = 0; i < AnswerTextList.Length; i++)
        {
            AnswerTextList[i].transform.parent.GetComponent<Image>().color = InitialBgColor;
        }
    }

    private void SetCurrentQuestionUI()
    {
        QuestionText.text = CurrentQuestions[CurrentScore].GetQuestion();
        for(int i  = 0; i < AnswerTextList.Length; i++)
        {
            AnswerTextList[i].text = CurrentQuestions[CurrentScore].GetAnswer(i);
        }

        CurrentCorrectAnswer = CurrentQuestions[CurrentScore].GetCorrectAnswer();
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

    private void PlayJumpSound()
    {
        GameController.thisLevelController.PlaySound(JumpSound);
    }

    private void PlayCorrectSound()
    {
        GameController.thisLevelController.PlaySound(CorrectSound);
    }

    private void PlayIncorrectSound()
    {
        GameController.thisLevelController.PlaySound(IncorrectSound);
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
   //     throw new NotImplementedException();
    }

    public override void ResumeGame()
    {
      //  throw new NotImplementedException();
    }
}
