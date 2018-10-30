using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlickItSwipeInput : MonoBehaviour
{


    [SerializeField]
    private Text debugtxt;
    [SerializeField]
    private float minSwipeDistY;
    [SerializeField]
    private float minSwipeDistX;
    [SerializeField]
    private Vector2 startPos;
    [SerializeField]
    private CarRoadMove currcar;


    //Input for Swiping cars left or right
    void Update()
    {
       /* Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            debugtxt.text = hit.transform.gameObject.name;
            if (hit.collider.GetComponent<CarRoadMove>())
            {
                debugtxt.text = "hit car";
                currcar = hit.collider.GetComponent<CarRoadMove>();
            }
        }*/
        Touch[] touches = Input.touches;
        foreach (Touch touch1 in touches)
        {
            Touch touch = Input.touches[0];
            if (touch.phase == TouchPhase.Began)
            {
                debugtxt.text = "touchphasebegun";
                // Construct a ray from the current touch coordinates
                var ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    debugtxt.text = hit.transform.gameObject.name;
                    if (hit.collider.GetComponent<CarRoadMove>())
                    {
                        debugtxt.text = "hit car";
                        currcar = hit.collider.GetComponent<CarRoadMove>();
                    }
                }
            }
                switch (touch.phase)

                {
                    case TouchPhase.Began:

                        startPos = touch.position;
                        break;

                    case TouchPhase.Ended:
                        float swipeDistVertical = (new Vector3(0, touch.position.y, 0) - new Vector3(0, startPos.y, 0)).magnitude;
                        float swipeDistHorizontal = (new Vector3(touch.position.x, 0, 0) - new Vector3(startPos.x, 0, 0)).magnitude;
                        if (swipeDistHorizontal > minSwipeDistX)
                        {

                            float swipeValue = Mathf.Sign(touch.position.x - startPos.x);

                            if (swipeValue > 0 && currcar != null)//right swipe
                                currcar.swipeDivert(false);

                            else if (swipeValue < 0 && currcar != null)//left swipe
                                currcar.swipeDivert(true);
                            currcar = null;
                        }
                        break;
                }
            }
   /**     var ray1 = Camera.main.ScreenPointToRay(touch1.position);
        RaycastHit hit1;
        if (Physics.Raycast(ray1, out hit))
        {
            debugtxt.text = hit.transform.gameObject.name;
            if (hit.collider.tag == "car" && hit.collider.GetComponent<CarRoadMove>())
            {
                debugtxt.text = "hit car";
                currcar = hit.collider.GetComponent<CarRoadMove>();
            }
        }*/
        /* if (Input.touchCount > 0)

         {
             var ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
             RaycastHit hit;

             if (Physics.Raycast(ray, out hit))
             {
                 debugtxt.text = hit.transform.gameObject.name;
                 //Checks to see if the initial touch spot hits a car object with the CarScript    
                 if (hit.collider.tag == "car" && hit.collider.GetComponent<CarRoadMove>())
                 {
                     currcar = hit.collider.GetComponent<CarRoadMove>();
                 }
             }
             Touch touch = Input.touches[0];
             switch (touch.phase)

             {

                 case TouchPhase.Began:

                     startPos = touch.position;

                     break;

                 case TouchPhase.Ended:

                     float swipeDistVertical = (new Vector3(0, touch.position.y, 0) - new Vector3(0, startPos.y, 0)).magnitude;



                     float swipeDistHorizontal = (new Vector3(touch.position.x, 0, 0) - new Vector3(startPos.x, 0, 0)).magnitude;


                     if (swipeDistHorizontal > minSwipeDistX)
                     {

                         float swipeValue = Mathf.Sign(touch.position.x - startPos.x);

                         if (swipeValue > 0 && currcar != null)//right swipe
                             currcar.swipeDivert(true);

                         else if (swipeValue < 0 && currcar != null)//left swipe
                             currcar.swipeDivert(false);
                             currcar = null;
                     }
                     break;
             }
         }
     }*/
    }
}
