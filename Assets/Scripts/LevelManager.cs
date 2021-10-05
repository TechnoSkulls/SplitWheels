using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject empytyGameObj;
    [Header("Platforms")]
    public GameObject[] levels;

    GameObject platformObj;

    int temp = 0, levelToLoad = 1;

    public int levelAval = 5, minus = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void InitLevel(int i)
    {
        //GameObject tempObj;
        //if (platformObj)
        //{
        //    tempObj = platformObj;
        //    Destroy(tempObj);
        //}

        //if (i > levelAval)
        //{
        //    GetIn10(i);
        //}
        //else
        //{
        //    levelToLoad = i;
        //}
        //if (i < 1)
        //{
        //    levelToLoad = 1;
        //}

        //platformObj = Instantiate(levels[levelToLoad - 1]);        
        levelToLoad = PlayerPrefs.GetInt("obsComp");
        //Debug.Log(levelToLoad);
        platformObj = Instantiate(levels[levelToLoad]);        
        GameManager.gameManager.InitPlayer();        

    }


    void GetIn10(int i)
    {
        
        temp = i - minus;
        
        if (temp > levelAval)
        {
            GetIn10(temp);
        }
        else
        {
            levelToLoad = temp;
        }        
    }
}
