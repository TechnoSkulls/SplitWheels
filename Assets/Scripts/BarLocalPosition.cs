using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarLocalPosition : MonoBehaviour
{
    public Transform wheelY;
    public bool inFixUp = false;
    Vector3 pos;
    public float yOffset = 0.85f;
    public float zOffset = 0;
    private void Start()
    {
        pos = new Vector3(gameObject.transform.localPosition.x, wheelY.transform.localPosition.y + yOffset, gameObject.transform.localPosition.z + zOffset);
    }
    // Update is called once per frame
    void Update()
    {
        if (!inFixUp)
        {
            pos.y = wheelY.localPosition.y + yOffset;            
            transform.localPosition = pos;
        }        
    }

    private void FixedUpdate()
    {
        if (inFixUp)
        {
            pos.y = wheelY.localPosition.y + yOffset;
            transform.localPosition = pos;
        }
    }
}
