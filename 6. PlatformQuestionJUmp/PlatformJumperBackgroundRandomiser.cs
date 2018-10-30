using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformJumperBackgroundRandomiser : MonoBehaviour {

    public GameObject[] Backgrounds;

    private void Start()
    {
        Backgrounds[Random.Range(0, Backgrounds.Length)].SetActive(true);
    }
}
