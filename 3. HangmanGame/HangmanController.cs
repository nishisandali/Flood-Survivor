using UnityEngine;
using UnityEngine.UI;
using Util;
using System;
public class HangmanController : MinigameController
{

    public string[] WordArray;

    //UI Elements
	public Text wordIndicator;
	public Text scoreIndicator;
	public Text letterIndicator;
	public Text messageIndicator;

    //Keyboard to enable
	public GameObject KeyBoard;
	[SerializeField]
	public LevelController levelController;

	public HangmanGameController hangman;
	private string word;
	private char[] revealed;
	private int score;
	private bool completed;
	private string[] wordset;
	private char firstLetter;

	int wordIndex = 0;
    int punish;
	char c;

    public int ScorePerLetter;
    public int ScorePerWord;
    public int ScoreWhenWon;
    public bool IsPhonetic;

    System.Random _random = new System.Random();


    override
    public void EndGame()
    {
        KeyBoard.SetActive(false);
        wordIndicator.transform.parent.gameObject.SetActive(false);
        messageIndicator.transform.parent.gameObject.SetActive(false);
    }

    override
	public void StartGame()
	{
		KeyBoard.SetActive(true);
	//	hangman = GameObject.FindGameObjectWithTag("Player").GetComponent<HangmanGameController>();
		reset();

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

    // Update is called once per frame


    public void alphebeticFunction(string alphabet)
	{
		wordIndex++;
		c = Convert.ToChar(alphabet);

		if (!check(c))
		{

			hangman.punish();
            levelController.livesLeft--;
            levelController.loseLifeNow();
     
			if (hangman.isDead)
			{
				wordIndicator.text = word;
				completed = true;
				levelController.EndLevel(false, "Oh no, the boy fell into the water :(");
				//Debug.Log("Game Over!!!");
			}
		}
             
	}

		public bool check(char c)
	{
		bool ret = false;
		int complete = 0;
		int score = 0;

		for (int i = 0; i < revealed.Length; i++)
		{
			if (c == word[i])
			{
				ret = true;
				if (revealed[i] == 0)
				{
					revealed[i] = c;
					score++;
				}
			}

			if (revealed[i] != 0)
			{
				complete++;
            
			}
		}

		if (score != 0)
		{
			this.score += score;
			if (complete == revealed.Length)
			{
				completed = true;
				this.score += revealed.Length;
                levelController.LevelBasedRewardCalculation = (revealed.Length * ScorePerLetter) + ScoreWhenWon;
                levelController.EndLevel(true, "Yay! the SES saved the boy just in time good work");

                //Debug.Log("You Won!");
            }
            updatewordIndicator();
			updateScoreIndicator();
		}

		return ret;
	}

	private void updatewordIndicator()
	{
		string displayed = "";

		for (int i = 0; i < revealed.Length; i++)
		{
			char c = revealed[i];
			if (c == 0)
			{
				c = '_';
			}
			displayed += ' ';
			displayed += c;
		}

		wordIndicator.text = displayed;
	}

	private void updateScoreIndicator()
	{
		scoreIndicator.text = "Score " + score;
	}

	private void setWord(string word)
	{
		word = word.ToUpper();
		this.word = word;
		firstLetter = word[0];
        if(IsPhonetic)
		    messageIndicator.text = "Guess the phonetic word for the letter " + firstLetter + "?";
        else
            messageIndicator.text = "Guess the natural disaster starting with the letter " + firstLetter + "?";
        revealed = new char[word.Length];
		updatewordIndicator();
	}



	public void next()
	{

		hangman.reset();
		completed = false;
        setWord(WordArray[_random.Next(0, WordArray.Length)]);
        
	}

	public void reset()
	{
		score = 0;
		updateScoreIndicator();
		next();
	}
}
