using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarXPos : MonoBehaviour
{
    public Transform wheelY;
    public bool inFixUp = false;
    Vector3 pos;
    float two = 2;
    private void Start()
    {
        pos = transform.localScale;
    }
    // Update is called once per frame
    void Update()
    {
        if (inFixUp)
        {
            pos.x = wheelY.transform.localPosition.x / two;
            transform.localScale = pos;
        }
        else
        {
            pos.x = -wheelY.transform.localPosition.x / two;
            transform.localScale = pos;
        }
        
    }
}
