using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Deals with ingame sound effects and background music
public class UISoundController : MonoBehaviour {


      [SerializeField]
    private AudioSource mainSource;
    [SerializeField]
    private AudioClip clickSound;
    [SerializeField]
    private AudioClip menuMusic;
    [SerializeField]
    private AudioClip gameMusic;
    [SerializeField]
    private AudioClip loseSound;
    [SerializeField]
    private AudioClip winSound;
    [SerializeField]
    private AudioClip inGameUIClickSound;
    [SerializeField]
    private AudioClip getPointsSound;
    [SerializeField]
    private AudioClip swipeSound;
    [SerializeField]
    private AudioClip tapSound;
    [SerializeField]
    private AudioClip steerSound;


	// Use this for initialization
	void Start () {

        if (GameController.SoundController == null)
        {
            GameController.SoundController = this;
        }
        if (GameController.SoundController != this)
        {
            Destroy(gameObject);
        }
        mainSource = GetComponent<AudioSource>();
	}

    public void PlayClickSound()
    {
        mainSource.PlayOneShot(clickSound);
    }
    public void PlayWinSound()
    {
        mainSource.PlayOneShot(winSound);
    }
	
    public void PlayLoseSound()
    {
        mainSource.PlayOneShot(loseSound);
    }

    public void PlayInGameUIClickSound()
    {
        mainSource.PlayOneShot(inGameUIClickSound);
    }

    public void PlayGetPointsSound()
    {
        mainSource.PlayOneShot(getPointsSound);
    }

    public void PlaySwipeSound()
    {
        mainSource.PlayOneShot(swipeSound);
    }
    public void PlayTapSound()
    {
        mainSource.PlayOneShot(tapSound);
    }
    public void PlaySteerSound()
    {
        mainSource.PlayOneShot(steerSound);
    }
}
