using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpCoinOnColliderHit : MonoBehaviour {

    public int coinType;
    public int Score = 1;

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.transform.root == BoyFlashFloodEscapeController.BoyTransform)
        {
            //PickUpCoin;;
            BoyFlashFloodEscapeController.BoyTransform.GetComponent<BoyFlashFloodEscapeController>().GetCoin(coinType, Score);
            Destroy(gameObject);
        }
    }
}
