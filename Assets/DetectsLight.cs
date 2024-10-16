using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectsLight : MonoBehaviour
{
    public bool isLit = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isLit)
        {
            Debug.Log("I am lit");
        }
        isLit = false;
    }
}
