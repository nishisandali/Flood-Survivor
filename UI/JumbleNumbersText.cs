using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class JumbleNumbersText : MonoBehaviour {

    [SerializeField]
    public int realNumber = 10000;
    [SerializeField]
    private float jumbleTime = 3;
    [SerializeField]
    private Text TextToJumble;
    [SerializeField]
    private Text lastTextJumbled;
    [SerializeField]
    private bool isJumbling;
    [SerializeField]
    private float StartTime; 

    private void Start()
    {
        //JumbleTextNow();
       /* if (GameController.Maincontroller != null)
        {
            GameController.saveController.PointsText = GetComponent<Text>();
            GameController.saveController.SetPoints();
        }
        */

    }

    void FixedUpdate()
    {
        if (isJumbling)
        {
            float jumbleLength = realNumber.ToString().Length;
            float maxNum = (Mathf.Pow(10.0f, jumbleLength)) - 1;
            float minNum = (maxNum/10) + 1;
            float currentJumbled = Random.Range(minNum, maxNum);
            int currentJumbled2 = (int)currentJumbled;
            TextToJumble.text = currentJumbled2.ToString();
           if(StartTime< Time.time)
            {
                TextToJumble.text = realNumber.ToString();
                isJumbling = false;
            }
        }
    }

    public void JumbleTextNow()
    {
        StartTime = Time.time + jumbleTime ;
        isJumbling = true;
    }

    public void JumbleSpecificTextNow(Text toJumble, int realNum)
    {
        lastTextJumbled = TextToJumble;
        TextToJumble = toJumble;
        realNumber = realNum;
        JumbleTextNow();
    }
    public void JumbleTextForTime(Text toJumble, int realNum, float time)
    {
        lastTextJumbled = TextToJumble;
        jumbleTime = time;
        TextToJumble = toJumble;
        realNumber = realNum;
        JumbleTextNow();
    }



}
