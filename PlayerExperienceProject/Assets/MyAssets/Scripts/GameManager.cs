using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    bool gameBegin;
    // Start is called before the first frame update
    void Start()
    {
        gameBegin = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameBegin)
        {
            CheckStart();
        }
    }

    void CheckStart()
    {
        if(Input.GetKey(KeyCode.Space))
        {
        }    


    }
}
