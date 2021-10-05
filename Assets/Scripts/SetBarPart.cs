using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBarPart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.gameManager.SetBarPart(gameObject);
    }
}
