using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{

    public InputField width, height, levelValue;
    int _width, _height, _levelValue, temp;
    public GameObject loaderCanvas;
    bool isTrue = false;
    // Start is called before the first frame update
    void Start()
    {
#if UNITY_STANDALONE
        Screen.SetResolution(1080, 1920, true);//For 1080p Recording
        loaderCanvas.SetActive(true);
#endif
    }
    public void LoadScene()
    {
        SceneManager.LoadScene(1);
    }

    public void Set()
    {
        isTrue = int.TryParse(levelValue.text, out temp);
        if (isTrue)
        {
            _levelValue = int.Parse(levelValue.text);
            if (_levelValue > 0)
            {
                PlayerPrefs.SetInt("CurrentLevel", int.Parse(levelValue.text));
            }
        }
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
