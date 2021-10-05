using System.Collections;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [Header("Direction")]
    public bool riglef = false;
    public bool updown = false, frobac = false;
    [Header("Max Move Distance")]
    public float maxDistToMove = 0.5f;
    public bool front = true;
    private bool move = false;
    private Vector3 moveVector, startPos;
    public float moveSpeed = 0.5f;
    float val;
    Mover mover;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;        
        if (riglef)
        {
            moveVector = new Vector3(1, 0, 0);
            move = true;
            val = startPos.x;
        }
        else if (updown) 
        {
            moveVector = new Vector3(0, 1, 0);
            move = true;
            val = startPos.y;
        }
        else if (frobac)
        {
            moveVector = new Vector3(0, 0, 1);
            move = true;
            val = startPos.z;
        }
        else
        {
            mover = GetComponent<Mover>();
            mover.enabled = false;
        }
    }

    private void FixedUpdate()
    {
        if (front)
        {
            transform.Translate(moveVector * moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(-moveVector * moveSpeed * Time.deltaTime);
        }
    }

    private void Update()
    {
        if (move)
        {
            CheckBounds();
        }        
    }

    void CheckBounds()
    {
        if (riglef)
        {
            if (front)
            {
                if (transform.position.x > startPos.x + maxDistToMove)
                {
                    front = false;
                }                
            }
            else
            {
                if (transform.position.x < startPos.x - maxDistToMove)
                {
                    front = true;
                }
            }            
        }
        else if (updown)
        {
            if (front)
            {
                if (transform.position.y > startPos.y + maxDistToMove)
                {
                    front = false;
                }
            }
            else
            {
                if (transform.position.y < startPos.y - maxDistToMove)
                {
                    front = true;
                }
            }
        }
        else if (frobac)
        {
            if (front)
            {
                if (transform.position.z > startPos.z + maxDistToMove)
                {
                    front = false;
                }
            }
            else
            {
                if (transform.position.z < startPos.z - maxDistToMove)
                {
                    front = true;
                }
            }
        }
    }
}
