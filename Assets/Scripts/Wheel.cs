using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{

    private Vector3 moveVector;/*, moveHoriVector;*/
    private Vector3 transBridge;


    [Header("PlayerMovement")]
    public float speedX = 10f;
    public bool canMoveLeft = false, canMoveRight = false;
    public bool isRight;
    //public float wheelSpeed = 10.0f;
    public float min = 0.5f, max = 4.5f;    
    bool canMove = true;
    Vector3 correctPos;



    // Start is called before the first frame update
    void Start()
    {
        moveVector = new Vector3(1, 0, 0);
        correctPos = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);

        speedX = PlayerPrefs.GetFloat("SwipleVal");
        //Debug.Log("Swipe Speed: " + speedX);
    }

    // Update is called once per frame
    void Update()
    {        
        if (GameManager.gameManager.isPlaying)
        {
            CheckCanMoveLocal();
            if (canMove)
            {
                if (GameManager.gameManager.swipe.SwipeLeft || Input.GetKey(KeyCode.LeftArrow))
                {
                    MoveLeft();
                    //NewBridge();
                }
                if (GameManager.gameManager.swipe.SwipeRight || Input.GetKey(KeyCode.RightArrow))
                {
                    MoveRight();
                    //NewBridge();
                }
                CheckCanMoveLocal();
            }
        }
    }


   

    void CheckCanMoveLocal()
    {
        if (isRight)
        {
            if (transform.localPosition.x > GameManager.gameManager.GetBarSize())
            {
                canMoveRight = false;
            }
            else
            {
                canMoveRight = true;
            }

            if (transform.localPosition.x < GameManager.gameManager.minMove)
            {
                canMoveLeft = false;
            }
            else
            {
                canMoveLeft = true;
            }
        }
        else
        {
            if (transform.localPosition.x > -GameManager.gameManager.minMove)
            {
                canMoveRight = false;
            }
            else
            {
                canMoveRight = true;
            }

            if (transform.localPosition.x < -GameManager.gameManager.GetBarSize())
            {
                canMoveLeft = false;
            }
            else
            {
                canMoveLeft = true;
            }
        }
    }

    void MoveLeft()
    {
        if (isRight)
        {
            if (canMoveLeft)
            {
                transform.localPosition += moveVector * -speedX * Time.deltaTime;
            }

            //if (!canMoveRight)
            //{
                
            //}
        }
        else
        {
            if (canMoveRight)
            {
                transform.localPosition += moveVector * speedX * Time.deltaTime;
            }

            //if (!canMoveLeft)
            //{
                
            //}
        }
    }

    void MoveRight()
    {
        if (isRight)
        {
            if (canMoveRight)
            {
                transform.localPosition += moveVector * speedX * Time.deltaTime;
            }

            //if (!canMoveLeft)
            //{
                
            //}
        }
        else
        {
            if (canMoveLeft)
            {
                transform.localPosition += moveVector * -speedX * Time.deltaTime;                
            }

            //if (!canMoveRight)
            //{
                
            //}
        }
    }

    void GetPos()
    {
        if (isRight)
        {
            correctPos.x = GameManager.gameManager.GetBarSize();
        }
        else
        {
            correctPos.x = -GameManager.gameManager.GetBarSize();
        }        
        correctPos.y = transform.localPosition.y;
        correctPos.z = transform.localPosition.z;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("BarLast"))
        {
            EnterLast();
        }
        else if (other.gameObject.tag.Equals("BarZero"))
        {
            EnterZero();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("BarLast"))
        {
            ExitLast();
        }
        else if (other.gameObject.tag.Equals("BarZero"))
        {
            ExitZero();
        }
    }
    void EnterLast()
    {
        if (isRight)
        {
            canMoveRight = false;
        }
        else
        {
            canMoveLeft = false;
        }
    }

    void ExitLast()
    {
        if (isRight)
        {
            canMoveRight = true;
        }
        else
        {
            canMoveLeft = true;
        }
    }
    void EnterZero()
    {
        if (isRight)
        {
            canMoveLeft = false;
        }
        else
        {
            canMoveRight = false;
        }
    }
    void ExitZero()
    {
        if (isRight)
        {
            canMoveLeft = true;
        }
        else
        {
            canMoveRight = true;
        }
    }
}

