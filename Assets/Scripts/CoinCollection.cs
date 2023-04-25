using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollection : MonoBehaviour
{
    float second = 0f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        second += 1 * Time.deltaTime;
        Debug.Log(second);
    }
}
