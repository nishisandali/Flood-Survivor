using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AboutController : MonoBehaviour {

    public GameObject AboutUsObject;


    public void ShowAboutUs()
    {
        AboutUsObject.SetActive(true);
    }

    public void UnshowAboutUs()
    {
        AboutUsObject.SetActive(false);
    }
}
