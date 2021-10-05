using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float wheelSpeed = 350f;

    public bool right = false, up = false, forward = false;
    public bool wheel;
    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    void Update()
    {
        //if (GameManager.gameManager.isPlaying)
        //{

        //}

        if (wheel)
        {
            if (GameManager.gameManager.swipe.Tap && GameManager.gameManager.isPlaying)
            {
                if (right)
                {
                    transform.Rotate(Vector3.right * Time.deltaTime * wheelSpeed);
                }
                else if (up)
                {
                    transform.Rotate(Vector3.up * Time.deltaTime * wheelSpeed);
                }
                else if (forward)
                {
                    transform.Rotate(Vector3.forward * Time.deltaTime * wheelSpeed);
                }
            }
        }
        else
        {
            if (right)
            {
                transform.Rotate(Vector3.right * Time.deltaTime * wheelSpeed);
            }
            else if (up)
            {
                transform.Rotate(Vector3.up * Time.deltaTime * wheelSpeed);
            }
            else if (forward)
            {
                transform.Rotate(Vector3.forward * Time.deltaTime * wheelSpeed);
            }
        }
    }
}
