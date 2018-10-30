using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelUIElementTracker : MonoBehaviour {

    public GameObject[] LockObjects;
    public GameObject[] UnlockObjects;

    public void UnlockGame()
    {
        foreach(GameObject g in UnlockObjects)
        {
            g.SetActive(true);
        }
        foreach (GameObject g in LockObjects)
        {
            g.SetActive(false);
        }
    }

   public void LockGame()
    {
        foreach(GameObject g in LockObjects)
        {
            g.SetActive(true);
        }
        foreach(GameObject g in UnlockObjects)
        {
            g.SetActive(false);
        }
    }

}
