using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerPrefsSW : MonoBehaviour
{
    public bool clearPrefs;
    public bool loadScene;
    void Start()
    {
        SetPlayerPrefs();
        if (loadScene)
        {
            Invoke("LoadGame", 1.0f);
        }        
    }

    void SetPlayerPrefs()
    {
        if (clearPrefs)
        {
            PlayerPrefs.DeleteAll();
        }


        if (!PlayerPrefs.HasKey("CurrentLevel"))
        {
            PlayerPrefs.SetInt("CurrentLevel", 1);
        }
        
        if (!PlayerPrefs.HasKey("SpeedDef"))
        {
            PlayerPrefs.SetFloat("SpeedDef", 7.5f);

            PlayerPrefs.SetFloat("SpeedVal", PlayerPrefs.GetFloat("SpeedDef"));
        }
        
        if (!PlayerPrefs.HasKey("SwipeDef"))
        {
            PlayerPrefs.SetFloat("SwipeDef", 3.0f);

            PlayerPrefs.SetFloat("SwipleVal", PlayerPrefs.GetFloat("SwipeDef"));
        }

        if (!PlayerPrefs.HasKey("obsDef"))
        {
            PlayerPrefs.SetInt("obsDef", 1);

            PlayerPrefs.SetInt("obsComp", PlayerPrefs.GetInt("obsDef"));
        }



        if (!PlayerPrefs.HasKey("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", 0);
        }

    }

    void LoadGame()
    {
#if UNITY_IOS || UNITY_ANDROID
        SceneManager.LoadScene(1);
#endif
    }
}
