using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MinigameController : MonoBehaviour {

    public abstract void StartGame();
    public abstract void EndGame();
    public abstract void PauseGame();
    public abstract void ResumeGame();


}
