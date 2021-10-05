using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{

    private Vector3 moveVector;
    public float moveSpeed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        moveVector = new Vector3(0, 0, 1);
        moveSpeed = PlayerPrefs.GetFloat("SpeedVal");
        //Debug.Log("Speed Val: " + moveSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameManager.isPlaying && GameManager.gameManager.swipe.Tap) 
        {
            transform.Translate(moveVector * moveSpeed * Time.deltaTime);
        }        
    }
}
