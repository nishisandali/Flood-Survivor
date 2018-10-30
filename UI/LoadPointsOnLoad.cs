using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LoadPointsOnLoad : MonoBehaviour {



	// Use this for initialization
	void Start () {
        GameController.saveController.PointsText = GetComponent<Text>();
        GetComponent<Text>().text = GameController.saveController.GetPoints().ToString();

    }
	

}
