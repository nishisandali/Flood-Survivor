using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonScript : MonoBehaviour
{

    public void changeButtonState()
    {

       // GetComponent<Image>().color = new Color(0.74f, 0.91f, 1f);
        GetComponent<Image>().raycastTarget = false;
    }


}
