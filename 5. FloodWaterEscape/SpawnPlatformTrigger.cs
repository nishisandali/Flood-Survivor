using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlatformTrigger : MonoBehaviour {



    private void OnTriggerEnter(Collider collision)
    {
        if(collision.transform.root == BoyFlashFloodEscapeController.BoyTransform)
        {
            BoyFlashFloodEscapeController.BoyTransform.GetComponent<BoyFlashFloodEscapeController>().SpawnNextPlatformPrefab(transform.root);
            Destroy(gameObject);
        }
    }
}
