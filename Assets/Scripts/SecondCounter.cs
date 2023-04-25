using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondCounter : MonoBehaviour
{
    public int globalSeconds = 0;
    void Start()
    {
        StartCoroutine(Timer());
    }
    IEnumerator Timer() 
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(1);
            globalSeconds += 1;
            Debug.Log(globalSeconds);
        }
    }
}
