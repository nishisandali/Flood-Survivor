using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class FourPicsOneWordController : MinigameController {

    public Button[] ButtonKeys;
    public Button reset;
    public GameObject Keys;
    public Image[] pics;

    public Text WordIndicator;
    public Text WordHintIndicator;
    public Text MessageIndicator;
    public string currentWord;
    public char[] wordarray;
    public char[] currentWordarray;
    private char[] revealed;
    private string letter_word;
   
    [SerializeField]
    private FourPicsSprites[] fourPicsSprites;
    [SerializeField]
    private string[] stringArray;
    [SerializeField]
    public LevelController levelController;

    int letterIndex = 0;
    private string word;
    private int buttonPressed = 0;
    //public Color CorrectColor;
    //public Color IncorrectColor;

    public float time = 2;

    System.Random _random = new System.Random();

    override
    public void EndGame() {
        Keys.SetActive(false);
    }


    public override void StartGame()
    {
        // throw new NotImplementedException();
        Keys.SetActive(true);
        reset.gameObject.SetActive(true);
        next();
    }

    public void Start() {
        /*Keys.SetActive(true);
        reset.gameObject.SetActive(true);
        next();*/
	}

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

    //Shows the next letter of the word when hint button is pressed
    private void DisplayMessage(char Letter)
    {
        StartCoroutine(ShowAndHide(WordHintIndicator, 2));
        WordHintIndicator.text = "The Next Letter is " + Letter;
        reset.gameObject.SetActive(false);
    }

    //Get the letter of the button to the text field when pressed
    public void alphebeticFunction(string letter, int buttonKeyPressedIndex)
    {
        currentWord = currentWord.ToUpper();
        currentWordarray = currentWord.ToCharArray();
        char[] array = WordIndicator.text.ToCharArray();
        wordarray = array;
        //If the length of the given word is greater than the letters in the text field
        //Write into the text
        if (wordarray.Length <= currentWordarray.Length)
        {
            char b = Convert.ToChar(letter);
            if (b == currentWordarray[letterIndex])
            {
                //Debug.Log(buttonKeyPressedIndex);
                letter_word = letter_word + letter;
                WordIndicator.text = letter_word;
                letterIndex++;
                reset.onClick.AddListener(() => DisplayMessage(currentWordarray[letterIndex]));
                SetButtonKeyPressed(true, buttonKeyPressedIndex);
            }
            else{
                SetButtonKeyPressed(false, buttonKeyPressedIndex);
                levelController.livesLeft--;
                levelController.loseLifeNow();
            }
        }

        string word_Text = WordIndicator.text;
        if (word_Text == currentWord)
        {
            levelController.EndLevel(true, "Yay! the SES saved the boy just in time good work");
        }

    }

    //Set the word
    private void SetWord(string words){
        this.word = words;
        revealed = new char[words.Length];
        currentWord = words;
    }

    //Set Letters in the words to buttons randomly
    private void SetButton(string button_word){
        button_word = button_word.ToUpper();
        this.word = button_word;
        ArrayList wordList = new ArrayList(button_word.ToCharArray());
        ArrayList randomLetterArray = new ArrayList();
        System.Random rnd = new System.Random();
        int randomIndex = 0;
        while (wordList.Count > 0)
        {
            randomIndex = rnd.Next(0, wordList.Count); //Choose a random object in the list
            randomLetterArray.Add(wordList[randomIndex]); //add it to the new, random list
            wordList.RemoveAt(randomIndex); //remove to avoid duplicates
        }
        //Get the letters of the word to buttons in a random order
        for (int i = 0; i < randomLetterArray.Count ; i++)
        {
            //Debug.Log(randomLetterArray[i]);
            string letter1 = randomLetterArray[i].ToString();
            ButtonKeys[i].gameObject.SetActive(true);
            ButtonKeys[i].GetComponentInChildren<Text>().text = letter1;
            ButtonKeys[i].onClick.AddListener(() => alphebeticFunction(letter1, i));
            //Debug.Log(currentWord[i]);
        }
        reset.onClick.AddListener(() => DisplayMessage(currentWord[0]));
    }

    //Get pictures of the relevant word
    public void SetPic(FourPicsSprites picture)
    {
        for (int k = 0; k < pics.Length; k++)
        {
            Sprite sprite = picture.Pictures[k];
            pics[k].GetComponent<Image>().sprite = sprite;
        }

    }

    public void SetButtonKeyPressed(bool isCorrect, int Key){
        if (!isCorrect)
        {
            StartCoroutine(ShowAndHide(MessageIndicator, 1));
            MessageIndicator.text = "You Pressed a wrong letter!!";
        }
    }

    //Set buttons, Pictures and a relevant word
    public void next()
    {
        int n = _random.Next(0, stringArray.Length);
        string new_word = stringArray[n];
        FourPicsSprites pic = fourPicsSprites[n];
        SetWord(new_word);
        SetPic(pic);
        SetButton(new_word);
    }

    //Shows a message for a ceratin amount of time and disappears 
    IEnumerator ShowAndHide(Text message, float delay)
    {
        message.gameObject.SetActive(true);
        yield return new WaitForSeconds(delay);
        message.gameObject.SetActive(false);
    }
}