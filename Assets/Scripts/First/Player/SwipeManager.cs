using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeManager : MonoBehaviour
{
    public static bool tap, swipeLeft, swipeRight, swipeUp;
    private bool isDragging = false;
    private Vector2 startTouch, swipeDelta;

    Rigidbody myRigid;
    void Update()
    {
        tap = swipeLeft = swipeRight = swipeUp = false;
        #region Stand Inptuts
        if (Input.GetMouseButtonDown(0))
        {
            tap = true;
            isDragging = true;
            startTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;

        }
        #endregion


        #region Mobile Inputs
        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                tap = true;
                isDragging = true;
                startTouch = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                isDragging = false;
                Reset();
            }
        }
        #endregion


        // 거리 계산
        swipeDelta = Vector2.zero;
        if (isDragging)
        {
            if (Input.touches.Length > 0) // ******* 이거 < 였던거를 > 로 바꿧음
                swipeDelta = Input.touches[0].position - startTouch;
            else if (Input.GetMouseButton(0))
                swipeDelta = (Vector2)Input.mousePosition - startTouch;
        }

        // corss the distance?
        if(swipeDelta.magnitude > 125)
        {
            // which direction?
            float x = swipeDelta.x;
            float y = swipeDelta.y;
            if(Mathf.Abs(x) > Mathf.Abs(y))
            {
                // left or right
                if(x > 0)
                    swipeRight = true;
                else 
                    swipeLeft =  true; 
                
                    
            }
            else // up or down
            {
                if (y > 0)
                {
                    swipeUp = true;
                }
            }

            Reset();
               
        }

    }

    private void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
        isDragging = false;
    }
}
