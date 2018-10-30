using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BoyFlashFloodEscapeController : MinigameController {


    [SerializeField]
    private GameObject GameCamera;
    
    [SerializeField]
    private GameObject TapToJumpObject;
    [SerializeField]
    private Canvas UICanvas;
    //Track of boy
    [SerializeField]
    public static Transform BoyTransform;
    [SerializeField]
    private Rigidbody BoyRigidbody;
    [SerializeField]
    private Animator BoyAnimator;

    [SerializeField]
    private Transform PlatformFollowBoy;
  


    [SerializeField]
    private Text SESCoinsScore;
    [SerializeField]
    private Text MetresRanText;
    [SerializeField]
    private int metresRan;
    [SerializeField]
    private int StartMetres;
    [SerializeField]
    private Vector3 RunDirection;
    [SerializeField]
    private float RunSpeed;
    [SerializeField]
    private float SpeedIncrease = 0.02f;
    [SerializeField]
    private Vector3 JumpDirection;
    [SerializeField]
    private float JumpForce;

    [SerializeField]
    private int currentScore;

    //Maintaining never ending main platform
    [SerializeField]
    private GameObject MainPlatformSpawn;
    [SerializeField]
    private GameObject[] OtherPlatformSpawns;


    [SerializeField]
    private GameObject[] PlatformPrefab;
    [SerializeField]
    private GameObject[] FloodPlatformPrefab;
    [SerializeField]
    private GameObject CoinPrefab;

    [SerializeField]
    private float PrefabSpawnDistance;
    [SerializeField]
    private float LastPrefabSpawnPoint;

    [SerializeField]
    private Vector3 PrefabSpawnSpotDistance;
    [SerializeField]
    private Vector3 PrefabSpawnSpotFloodingDistance;
    [SerializeField]
    private Vector3 LastPrefabSpawnSpot;
    [SerializeField]
    private GameObject NextPlatformToDelete;


    //Platforms spawning 
    [SerializeField]
    private int MaxPlatforms;
    [SerializeField]
    private int LastPlatformIndex;
    [SerializeField]
    private int NextPlatformIndex;
    [SerializeField]
    private float PlatformSpawnInterval;
    [SerializeField]
    private float NextPlatformSpawnTime;
    [SerializeField]
    private bool hasPlatformInView;


    //Flash flood & coins spawns
    [SerializeField]
    private bool FlashFloodAlert;
    [SerializeField]
    private float TimeUntilFlashFlood;
    [SerializeField]
    private float FlashFloodDuration;
    [SerializeField]
    private float NextFlashFloodTime;
    [SerializeField]
    private Transform FlashFloodObject;
    [SerializeField]
    private GameObject CoinSpawn;
    [SerializeField]
    private float MinCoinSpawnInterval;
    [SerializeField]
    private float MaxCoinSpawnInterval;
  

    //Points
    [SerializeField]
    private int FlashFloodsAvoided;
    [SerializeField]
    private float DistanceTravelled;

    [SerializeField]
    private bool CanPlayGame = false;
    [SerializeField]
    private float PositionLock;

    [SerializeField]
    private string CollectCoinAnimation;
    [SerializeField]
    private GameObject CollectCoinUI;
    [SerializeField]
    private Transform CollectCoinHolder;
    [SerializeField]
    private float CollectCoinAnimationTime = 0.7f;
    [SerializeField]
    private float FloodTimerCountdown;
    [SerializeField]
    private float FlashFloodMinInterval;
    [SerializeField]
    private float FlashFloodMaxInterval;
    [SerializeField]
    private float currentFlashFloodInterval;
    [SerializeField]
    private float FlashFloodAlertTime = 7;
    [SerializeField]
    private float FlashFloodAlertDelay = 7;
    [SerializeField]
    private float FlashFloodCountDownTime = 7;
    [SerializeField]
    private float FlashFloodAlertStartTime;
    [SerializeField]
    private bool MakeNextPlatformFlood;
    [SerializeField]
    private bool FloodingState;
    [SerializeField]
    private bool IsGrounded;
    [SerializeField]
    private float FlashFloodBoySpeed;
    [SerializeField]
    private int FlashFloodLevel;
    [SerializeField]
    private int CurrentBoyLevel;
    [SerializeField]
    private Animation FlashFloodScreenAlert;
    [SerializeField]
    private string FlashFloodAlertAnim = "AlertFlood";
    [SerializeField]
    private Text FlashFloodAlertText;
    [SerializeField]
    private Text FlashFloodTimerText;
    [SerializeField]
    private Image FlashFloodDangerIcon;
    [SerializeField]
    private bool isNextPlatformFlashFlood;
    [SerializeField]
    private bool isFlashFlooding;
    [SerializeField]
    private bool LastPlatformFlood;

    [SerializeField]
    private GameObject FlashFloodAlertObject;
    [SerializeField]
    private Text FlashFloodAlertTextObject;
    [SerializeField]
    private Vector3[] FlashFloodLevelsYPosition;
    [SerializeField]
    private string[] FlashFloodAnimNames;
    [SerializeField]
    private int flashfloodLevel;

    [SerializeField]
    private AudioClip JumpSound;
    [SerializeField]
    private AudioClip FlashFloodSound;
    [SerializeField]
    private AudioClip CoinSound;

   // [SerializeField]
   // private int currentCollectCoinUI;
   public void SetLevelScore()
    {
        GameController.thisLevelController.LevelBasedRewardCalculation = currentScore;
    }

    // Use this for initialization
    void Start() {
        BoyTransform = transform;
        BoyRigidbody = GetComponent<Rigidbody>();
        BoyAnimator = GetComponent<Animator>();
        PositionLock = transform.position.z;
        GetNewFlashFloodInterval();
    }

    // Update is called once per frame
    void Update() {
        if (CanPlayGame)
        {
            MoveRight();
            PlatformFollowBoy.transform.position = new Vector3(BoyTransform.position.x, -5.11f, 2.88f);
        }
        metresRan = (int)BoyTransform.position.x;
        MetresRanText.text = (metresRan - StartMetres).ToString();
        RaycastHit hit;
        if (Physics.Raycast(BoyTransform.position + new Vector3(0, 0f, 0), Vector3.down, 0.4f))
        {
            IsGrounded = true;
            BoyAnimator.SetBool("EndJumpEarly", true);
        }
        else
        {
            IsGrounded = false;
            BoyAnimator.SetBool("EndJumpEarly", false);
        }
        if (isFlashFlooding)
        {
            FlashFloodAlertTime = FlashFloodAlertStartTime +(FlashFloodCountDownTime)- Time.time;
            FlashFloodTimerText.text = FlashFloodAlertTime.ToString("F2");
            if (FlashFloodAlertTime < 0)
            {
                DisableAlert();
            }
        }
    }

    public void DisableAlert()
    {
        FlashFloodAlertObject.SetActive(false);
        isFlashFlooding = false;
    }
    public void GetNewFlashFloodInterval()
    {
        currentFlashFloodInterval = Random.Range(FlashFloodMinInterval, FlashFloodMaxInterval);
    }

    public void SetFlashFloodNow()
    {
        FloodingState = true;
        StartCoroutine(EnableFlashFloodAfterTime(FlashFloodAlertDelay));
    }
    IEnumerator EnableFlashFloodAfterTime (float floodArivalTime)
    {
        yield return new WaitForSeconds(floodArivalTime);
        EnableFlashFloodAlert();
        isFlashFlooding = true;
        FlashFloodAlertObject.SetActive(true);
        FlashFloodAlertStartTime = Time.time;
        StartCoroutine(FlashFloodAfterTime());

    }
    IEnumerator FinishFlashFlood()
    {
        yield return new WaitForSeconds(3);
        isFlashFlooding = false;
        FloodingState = false;
        CalculateNextFlashFlood();
    }
    IEnumerator FlashFloodAfterTime()
    {
        yield return new WaitForSeconds(FlashFloodCountDownTime);
        //FLOODNOW
        PlatformFollowBoy.GetComponent<Animation>().Play(FlashFloodAnimNames[flashfloodLevel], PlayMode.StopAll);
        GameController.thisLevelController.PlaySound(FlashFloodSound);
        FlashFloodAlertObject.SetActive(false);
        StartCoroutine(FinishFlashFlood());
    }

    public void CalculateNextFlashFlood()
    {
        NextFlashFloodTime =Time.time + Random.Range(FlashFloodMinInterval, FlashFloodMaxInterval);
        print(NextFlashFloodTime);
    }

    public void EnableFloodNow()
    {
        FlashFloodAlertObject.SetActive(false);
        ResetFloodAlert();
    }
    public void ResetFloodAlert()
    {
        FlashFloodCountDownTime = FlashFloodAlertDelay;
    }

    public void EnableFlashFloodAlert()
    {

  //      FlashFloodScreenAlert.Play(FlashFloodAlertAnim, PlayMode.StopAll);
    }
   
    public void FloodNow()
    {

    }

    public void MoveRight()
    {
        //BoyRigidbody.AddForce(RunDirection * RunSpeed * Time.deltaTime);
        transform.Translate(RunDirection * RunSpeed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, transform.position.y, PositionLock);
    }

       
    public void Jump()
    {
        if (IsGrounded)
        {
            BoyAnimator.SetBool("Jump", true);
            
            GetComponent<Rigidbody>().AddForce(JumpForce * JumpDirection);
            GameController.thisLevelController.PlaySound(JumpSound);
        }
    }

    public void GetCoin(int CoinType, int score)
    {
        GameObject i = Instantiate(CollectCoinUI,CollectCoinHolder);
        GameController.thisLevelController.PlaySound(CoinSound);
        StartCoroutine(AddScoreAfterTime(score));
    }

    IEnumerator AddScoreAfterTime(int toAdd)
    {
        yield return new WaitForSeconds(CollectCoinAnimationTime);
        AddScoreNow(toAdd);
    }

    public void AddScoreNow( int score)
    {
        currentScore+=score;
        SESCoinsScore.text = currentScore.ToString();
    }



    private void EnableGameStartObjects()
    {
        GameCamera.SetActive(true);
        TapToJumpObject.SetActive(true);
        UICanvas.worldCamera = GameCamera.GetComponent<Camera>();
    }

     public void SpawnNextPlatformPrefab(Transform platformHit)
    {
    
        if (NextPlatformToDelete != null)
        {
            Destroy(NextPlatformToDelete);
        }
        NextPlatformToDelete = platformHit.gameObject;
        GameController.thisLevelController.AdditionalObjectToDisable = NextPlatformToDelete;
        if (!FloodingState && isNextPlatformFlashFlood)
        {
            Vector3 nextPrefabSpawnSpot = LastPrefabSpawnSpot + PrefabSpawnSpotDistance;
            int rnd = Random.Range(0, 1);
            print(rnd);
            flashfloodLevel = rnd;
            GameObject b =Instantiate(FloodPlatformPrefab[rnd], nextPrefabSpawnSpot, Quaternion.identity);
            GameController.thisLevelController.AdditionalObjectToDisable2 = b;
            FlashFloodAlertTextObject.text = "Level " + (rnd + 1).ToString() + " Flash Flood Incoming";
            LastPrefabSpawnSpot = nextPrefabSpawnSpot;
            RunSpeed = RunSpeed + SpeedIncrease;
            LastPlatformFlood = true;
            isNextPlatformFlashFlood = false;
            SetFlashFloodNow();
        }
        else
        {
            Vector3 nextPrefabSpawnSpot;
            if (LastPlatformFlood)
            {
                 nextPrefabSpawnSpot = LastPrefabSpawnSpot + PrefabSpawnSpotFloodingDistance;
                LastPlatformFlood = false;
            }
            else
            {
                nextPrefabSpawnSpot = LastPrefabSpawnSpot + PrefabSpawnSpotDistance;
            }

            GameObject b= Instantiate(PlatformPrefab[(int)Random.Range(0, PlatformPrefab.Length)], nextPrefabSpawnSpot, Quaternion.identity);
            GameController.thisLevelController.AdditionalObjectToDisable2 = b;
            LastPrefabSpawnSpot = nextPrefabSpawnSpot;
            RunSpeed = RunSpeed + SpeedIncrease;
        }
        if (!FloodingState && CheckIfCanFlood())
        {
            TryToTriggerFlood();
        }
    }

    public void TriggerFlood()
    {
        isNextPlatformFlashFlood = true;

    }

    public void TryToTriggerFlood()
    {
        float rand = Random.Range(0.0f, 1.0f);
        if (rand < 0.80f)
        {
            TriggerFlood();
        }
    }

    public bool CheckIfCanFlood()
    {
        if (NextFlashFloodTime < Time.time)
        {
            return true;
        }
        return false;
    }

    //private void

    public override void StartGame()
    {
        CanPlayGame = true;
        EnableGameStartObjects();
    }

    public override void EndGame()
    {
        CanPlayGame = false;
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
