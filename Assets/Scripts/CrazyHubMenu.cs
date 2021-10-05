using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class CrazyHubMenu : MonoBehaviour
{

    public InputField speed;
    public InputField swipe;
    public Text statusText;
    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    //void Update()
    //{

    //}


    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void Reset()
    {
        PlayerPrefs.SetFloat("SpeedVal", PlayerPrefs.GetFloat("SpeedDef"));
        
        PlayerPrefs.SetFloat("SwipleVal", PlayerPrefs.GetFloat("SwipeDef"));

        PlayerPrefs.SetInt("obsComp", PlayerPrefs.GetInt("obsDef"));

        statusText.text = "Values have been RESET.";
    }

    public void SetSwipe()
    {
        if (!swipe.text.Equals(""))
        {
            //Debug.Log("Swipe Text: " + swipe.text);
            PlayerPrefs.SetFloat("SwipleVal", float.Parse(swipe.text));
            statusText.text = "Swipe has been set to " + swipe.text;
            swipe.text = "";            
        }
    }
    public void SetSpeed()
    {
        if (!speed.text.Equals(""))
        {
            //Debug.Log("Speed Text: " + speed.text);
            PlayerPrefs.SetFloat("SpeedVal", float.Parse(speed.text));
            statusText.text = "Speed has been set to " + speed.text;
            speed.text = "";
        }        
    }

    public void SetObs(int i)
    {
        PlayerPrefs.SetInt("obsComp", i);

        if (i == 0)
        {
            statusText.text = "Obstacle set to easy.";
        }
        else if (i == 1)
        {
            statusText.text = "Obstacle set to medium.";
        }
        else if (i == 2)
        {
            statusText.text = "Obstacle set to hard.";
        }
    }
}
