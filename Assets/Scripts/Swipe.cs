using UnityEngine;

public class Swipe : MonoBehaviour
{

    // Use this for initialization
    private bool tap, swipeLeft, swipeRight, swipeUp, swipeDown, isDragging = false;
    private Vector2 startTouch, swipeDelta;

    public Vector2 SwipeDelta { get { return swipeDelta; } }
    public bool SwipeLeft { get { return swipeLeft; } }
    public bool SwipeRight { get { return swipeRight; } }
    public bool SwipeUp { get { return swipeUp; } }
    public bool SwipeDown { get { return swipeDown; } }
    public bool Tap { get { return tap; } }

    public float swipeMag = 2.0f;
    //   void Start ()
    //   {

    //}

    float x;
    float y;

    // Update is called once per frame
    private void Update()
    {
        /*tap = */swipeLeft = swipeRight = swipeUp = swipeDown = false;

        #region Standalone Input
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.LogError("Tapped");
            tap = true;
            isDragging = true;
            startTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            Reset();
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

        // Calculate the distance 
        swipeDelta = Vector2.zero;
        if (isDragging)
        {
            if (Input.touches.Length > 0)
            {
                swipeDelta = Input.touches[0].position - startTouch;
                startTouch = Input.touches[0].position;
            }
            else if (Input.GetMouseButton(0))
            {
                swipeDelta = (Vector2)Input.mousePosition - startTouch;
                startTouch = Input.mousePosition;
            }
        }
        //if (swipeDelta.magnitude > 25)
        //{
        //    Debug.Log("Swipe ka Magnitue greater than 25: " + swipeDelta.magnitude);
        //}

        //Did we cross the deadzone?
        if (swipeDelta.magnitude > swipeMag) 
        {
            //Which Direction
            //Debug.Log("Swipe ka Magnitue which moved: " + swipeDelta.magnitude);
            x = swipeDelta.x;
            y = swipeDelta.y;
            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                //Either left or right
                if (x < 0)
                {
                    swipeLeft = true;
                }
                else
                {
                    swipeRight = true;
                }
            }
            //else
            //{
            //    //Either up or down
            //    if (y < 0)
            //    {
            //        swipeDown = true;
            //    }
            //    else
            //    {
            //        swipeUp = true;
            //    }
            //}
            //Reset();
        }
    }

    private void Reset()
    {
        tap = swipeLeft = swipeRight = swipeUp = swipeDown = false;
        startTouch = swipeDelta = Vector2.zero;
        isDragging = false;
    }
}
