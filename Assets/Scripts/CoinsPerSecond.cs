using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using static SecondCounter;

public class CoinsPerSecond : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI CoinDisplay;
    [SerializeField] TextMeshProUGUI CPS;
    int coins;
    int cps = 2;
    void Start()
    {
        StartCoroutine(CoinGain());
    }
    void Update()
    {
        
    }
    IEnumerator CoinGain()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(1);
            CPS.text = "" + cps;
            coins += cps;
            CoinDisplay.text = "" + coins;
        }
    }
}
