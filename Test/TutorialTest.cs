using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        print("Hello Test");
	}
	
	// Update is called once per frame
	void Update () {
        newMethod();
	}

    public void newMethod() {
        print("Hello, World");
    }
      
}
