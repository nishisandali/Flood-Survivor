using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonStateChange : MonoBehaviour {

	public void changeButtonState()
	{

		GetComponent<Image>().color = new Color(0.16f, 0.28f, 0.95f);
		GetComponent<Image>().raycastTarget = false;
	}
}
