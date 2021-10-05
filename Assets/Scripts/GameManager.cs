//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{

    public static GameManager gameManager;//GameManager Instance
    
    
    [Header("Dummy Variables")]
    public bool clearPrefs = false;
    public int dummyLevel = 0, currentLevel = 1;
    public CollisionDetect rightCollider;
    public GameObject canvas;



    [Header("Prefabs")]
    public GameObject swipeGO;
    public GameObject mainBodyGO;
    public GameObject levelManagerGO;
    public CameraFollower cameraFollower;

    [Header("Initiated GOs")]
    public Swipe swipe;
    public GameObject player;
    public LevelManager levelManager;

    public bool isPlaying = true;

    [Header("Wheels MinMax Movement")]
    public float minMove = 0.30f;
    public float maxMove = 4.5f;

    [Header("Bar Object")]
    public GameObject barPart;

    [Header("Bar Size Increment Value")]
    public float barIncVal = 0.1f;

    [Header("Bar Size Decrement Value")]
    public float barDecVal = 0.1f;
    public float barDecTime = 0.35f;

    [Header("Camera Follower")]
    public CameraFollower cam;
    public bool isFalling = false;

    public bool levelClear = false, isDead = false;

    [Header("UI Elements - Texts")]
    public Text scoreText;
    public Text mainMenuBestText, endMenuScoreText, endMenuMultiText, endMenuStatusText, levelValue;

    [Header("UI Elements - Game Objects")]
    public GameObject mainMenu;
    public GameObject playingMenu, endMenu, loadingMenu, playerAnim, up, down;
    public GameObject next, reload;
    public GameObject vibrateOn, vibrateOff;

    [Header("Score Values")]
    public int scoreX = 1;
    public int score = 0;
    [Header("Current Preset")]
    public int currentPreset = 0;

   

    decimal multipliedScore = 0;


    int highScore = 0, vibrate = 0, one = 1, finalScore = 0;

    bool playing = false;
   
    private void Awake()
    {
        if (gameManager == null)
        {
            gameManager = this;
        }
        else
        {
            DestroyImmediate(gameManager.gameObject);
            gameManager = this;
        }

        if (clearPrefs)
        {
            PlayerPrefs.DeleteAll();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        SetPlayerPrefs();
        InitScene();
        InitLevel(currentLevel);
    }

    void SetPlayerPrefs()
    {
        currentLevel = GetLevelValue();


        if (dummyLevel > 0)
        {
            currentLevel = dummyLevel;
        }

        highScore = GetHighScore();
    }

    void SetLevelValue(int i)
    {
        PlayerPrefs.SetInt("CurrentLevel", i);
        currentLevel = GetLevelValue();
    }
    int GetLevelValue()
    {
        return PlayerPrefs.GetInt("CurrentLevel");
    }

    void SetHighScore(int i)
    {
        PlayerPrefs.SetInt("HighScore", i);
        highScore = GetHighScore();
    }

    int GetHighScore()
    {
        return PlayerPrefs.GetInt("HighScore");
    }
    void InitScene()
    {
        cam = Camera.main.GetComponent<CameraFollower>();
        swipe = Instantiate(swipeGO).GetComponent<Swipe>();
        levelManager = Instantiate(levelManagerGO).GetComponent<LevelManager>();
        SetCurrentPreset();
    }

    void SetCurrentPreset()
    {
        if (SceneManager.GetActiveScene().name.Equals("preset1"))
        {
            currentPreset = 0;
        }
        else if (SceneManager.GetActiveScene().name.Equals("preset2"))
        {
            currentPreset = 1;
        }
        else if (SceneManager.GetActiveScene().name.Equals("preset3"))
        {
            currentPreset = 2;
        }
    }
    public void InitLevel(int i)
    {
        levelManager.InitLevel(i);
        levelValue.text = "Level " + currentLevel;
    }
    public void InitPlayer()
    {
        player = Instantiate(mainBodyGO);
    }

    void SetMenuBest()
    {
        canvas.SetActive(true);
        if (highScore > 0)
        {
            mainMenuBestText.text = "BEST " + highScore.ToString();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            IncBarSize();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            DecBarSize();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            isPlaying = true;
        }

        if (!playing && (swipe.SwipeLeft || Input.GetKey(KeyCode.LeftArrow) || swipe.SwipeRight || Input.GetKey(KeyCode.RightArrow))) 
        {
            playing = true;
            swipe.swipeMag = 0.0f;
            Play();
        }
    }

    public void SetBarPart(GameObject _barPart)
    {
        barPart = _barPart;
    }

    public float GetBarSize()
    {
        maxMove = barPart.transform.localScale.y;
        if (maxMove <= 4.5f && maxMove > 0.1f)
        {
            return maxMove;
        }
        else if (maxMove <= 0.1f)
        {
            return maxMove = 0.1f;
        }
        else
        {
            return maxMove = 4.5f;
        }        
    }

    public void IncBarSize()
    {
        barPart.transform.localScale = new Vector3(barPart.transform.localScale.x, barPart.transform.localScale.y + barIncVal, barPart.transform.localScale.z);
    }

    public int DecBarSize()
    {
        if (barPart.transform.localScale.y > 0.1f)
        {
            barPart.transform.localScale = new Vector3(barPart.transform.localScale.x, barPart.transform.localScale.y - barDecVal, barPart.transform.localScale.z);
            return 1;
        }
        else
        {
            return 0;
        }        
    }

    public void SetCameraTargets(Transform _camTarget, Transform _lookTarget)
    {
        cam = Camera.main.GetComponent<CameraFollower>();
        cam.SetTargets(_camTarget, _lookTarget);
        cam.Assign(true);
    }

    public void SetCamFalse()
    {
        cam.Assign(false);
    }

    public void SetRight(GameObject go)
    {
        rightCollider = go.GetComponent<CollisionDetect>();
    }

    public void Restart()
    {
#if UNITY_STANDALONE
        SceneManager.LoadScene(0);
#elif UNITY_IOS || UNITY_ANDROID
        //SceneManager.LoadScene(1);
        SceneManager.LoadScene(0);
#endif
    }
    public void AddToScore()
    {
        score += 1;
        scoreText.text = score.ToString();
    }


    public void CheckHighScore()
    {
        if (multipliedScore > highScore)
        {
            SetHighScore(finalScore);
            SetMenuBest();
        }
    }

    public void ShowEndScreen()
    {
        isPlaying = false;
        if (levelClear)
        {
            next.SetActive(true);
            currentLevel+=1;
            SetLevelValue(currentLevel);
        }
        else
        {
            reload.SetActive(true);            
        }        
        endMenuScoreText.text = multipliedScore.ToString();
        playingMenu.SetActive(false);
        endMenu.SetActive(true);
    }
    public void ScoreX()
    {
        multipliedScore = score * scoreX;
        CheckHighScore();
    }
    public void Play()
    {
        mainMenu.SetActive(false);
        playingMenu.SetActive(true);
        rightCollider.TyreSmokePlay();
        isPlaying = true;       
    }
}
