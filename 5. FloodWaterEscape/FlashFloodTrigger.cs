using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashFloodTrigger : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.root.gameObject.tag == "Boy")
        {
            collision.transform.root.GetComponent<BoyFlashFloodEscapeController>().SetLevelScore();
            GameController.thisLevelController.EndLevel(true, "You got wiped out, Better luck next time!");
        }
    }
}
