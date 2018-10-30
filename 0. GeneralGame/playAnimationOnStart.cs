
using UnityEngine;

public class playAnimationOnStart : MonoBehaviour {

    public string AnimName;


	// Use this for initialization
	void Start () {
        GetComponent<Animation>().Play(AnimName);
	}
	
	
}
