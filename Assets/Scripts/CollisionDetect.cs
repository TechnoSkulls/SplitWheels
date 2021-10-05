using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CollisionDetect : MonoBehaviour
{
    Rigidbody rb;
    Vector3 pos;

    bool canDec = false, decInit = true;
    public bool isRight;
    //bool fall = false;
    bool dead = false;
    string noHarm = "NoHarm";
    bool firstNumber = true;
    bool isInit = false;
    public GameObject leftObj;
    public CollisionDetect left;

    public GameObject[] psGOs;

    ParticleSystem psInit1, psInit2, psInit3, psInit4;
    public ParticleSystem tyreSmoke, smoke;

    int retVal = 1;
    float adjustY = 2.25f;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        pos = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z);
        Invoke("InitBool", 1.0f);
        if (isRight)
        {
            GameManager.gameManager.SetRight(gameObject);
            left = leftObj.GetComponent<CollisionDetect>();
            psInit1 = psGOs[0].GetComponent<ParticleSystem>();
            psInit2 = psGOs[1].GetComponent<ParticleSystem>();
            psInit3 = psGOs[2].GetComponent<ParticleSystem>();
            psInit4 = psGOs[3].GetComponent<ParticleSystem>();
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) 
        {
            FallDown();
        }
    }
    void InitBool()
    {
        isInit = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (isInit && !dead)
        {
            if (other.gameObject.tag.Equals("Inc"))
            {
                GameManager.gameManager.IncBarSize();
                other.gameObject.SetActive(false);
                AddScore();
            }
            else if (other.gameObject.tag.Equals("Dec"))
            {
                if (decInit)
                {
                    SetTimer();
                    InvokeRepeating("DecTimer", 0, GameManager.gameManager.barDecTime);
                    decInit = false;
                }
            }
            else if (other.gameObject.tag.Equals("OffDec"))
            {
                CancelInvoke("DecTimer");
                canDec = false;
                decInit = true;
            }
            else if (other.gameObject.tag.Equals("Die"))
            {
                Die();
            }
            else if (other.gameObject.tag.Equals("Out"))
            {
                Out();
            }
            else if (other.gameObject.tag.Equals("FallDown"))
            {
                FallDown();
            }
            else if (other.gameObject.tag.Equals("Platform"))
            {
                FallOff();
                pos.x = gameObject.transform.localPosition.x;
                pos.y = other.gameObject.transform.position.y + adjustY;
                pos.z = gameObject.transform.localPosition.z;
                transform.localPosition = pos;
            }                      
            else if (other.gameObject.tag.Equals("Number"))
            {
                if (isRight)
                {
                    MulScore(Convert.ToInt32(other.gameObject.name));
                    other.gameObject.tag = noHarm;
                    LevelEnd();
                }
                FallOff();
                pos.x = gameObject.transform.localPosition.x;
                pos.y = gameObject.transform.localPosition.y + 0.5f;
                pos.z = gameObject.transform.localPosition.z;
                transform.localPosition = pos;
            }
            if (isRight)
            {
                if (other.gameObject.tag.Equals("LevelClear"))
                {
                    LevelClear();
                }                
            }
        }
        
    }



    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag.Equals("Dec") && !dead)
        {
            Dec();
        }
    }
    void Out()
    {
        dead = true;
        GameManager.gameManager.isPlaying = false;
        GameManager.gameManager.isDead = true;
        //smoke.Play();
        if (isRight)
        {
            LevelEnd();
        }        
    }
    void Die()
    {
        dead = true;
        GameManager.gameManager.isPlaying = false;
        GameManager.gameManager.SetCamFalse();
        rb.isKinematic = false;
        Invoke("StopFall", 2.0f);
        //smoke.Play();
        GameManager.gameManager.isDead = true;
        if (isRight)
        {
            LevelEnd();
        }
    }
    void StopFall()
    {
        rb.isKinematic = true;
    }
    void FallOff()
    {
        rb.isKinematic = true;
        GameManager.gameManager.isFalling = false;
    }
    void FallDown()
    {
        GameManager.gameManager.isFalling = true;
        rb.isKinematic = false;
    }
    
    void Dec()
    {
        
        if (canDec)
        {
            canDec = false;
            //Debug.Log("Decreased");
            retVal = GameManager.gameManager.DecBarSize();
            if (isRight && retVal == 0) 
            {
                CallLevelEnd();
            }
        }
    }
    void DecTimer()
    {
        canDec = true;
        SetTimer();
    }
    void SetTimer()
    {
        if (GameManager.gameManager.barPart.transform.localScale.y > 2.5f && GameManager.gameManager.barPart.transform.localScale.y < 4.6f)
        {
            GameManager.gameManager.barDecTime = 0.30f;
        }
        else if (GameManager.gameManager.barPart.transform.localScale.y < 2.6f)
        {
            GameManager.gameManager.barDecTime = 0.50f;
        }
        else if (GameManager.gameManager.barPart.transform.localScale.y > 4.5f && GameManager.gameManager.barPart.transform.localScale.y < 6.5f)
        {
            GameManager.gameManager.barDecTime = 0.25f;
        }
        else if (GameManager.gameManager.barPart.transform.localScale.y > 6.4f)
        {
            GameManager.gameManager.barDecTime = 0.15f;
        }
    }
    void LevelClear()
    {
        GameManager.gameManager.levelClear = true;
        psInit1.Play();
        psInit2.Play();
        psInit3.Play();
    }

    void LevelEnd()
    {
        TyreSmokeStop();
        left.TyreSmokeStop();
        if (isRight)
        {
            GameManager.gameManager.isDead = true;
            GameManager.gameManager.isPlaying = false;
            
            if (GameManager.gameManager.levelClear)
            {
                psInit1.Play();
                psInit2.Play();
                psInit3.Play();
                psInit4.Play();
                Invoke("ShowEndScreen", 3.0f);  
            }
            else
            {
                PlaySmoke();
                left.PlaySmoke();
                Invoke("ShowEndScreen", 1.0f);
            }
        }
    }
    public void CallLevelEnd()
    {
        DeadTrue();
        left.DeadTrue();
        GameManager.gameManager.isPlaying = false;
        GameManager.gameManager.isDead = true;
        LevelEnd();
    }
    public void DeadTrue()
    {
        dead = true;
    }
    void ShowEndScreen()
    {
        GameManager.gameManager.ScoreX();
        GameManager.gameManager.ShowEndScreen();
    }
    void MulScore(int i)
    {
        GameManager.gameManager.scoreX = i;     
    }
    void AddScore()
    {
        GameManager.gameManager.AddToScore();
    }



    public void PlaySmoke()
    {
        smoke.Play();
    }
    public void TyreSmokeStop()
    {
        tyreSmoke.Stop();
    }

    public void TyreSmokePlay()
    {
        tyreSmoke.Play();
        if (isRight)
        {
            left.TyreSmokePlay();
        }        
    }
}
