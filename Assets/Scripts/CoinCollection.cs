using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinCollection : MonoBehaviour
{
    Collider2D myCollider2D;
    public int coinsCollected = 0;
    [SerializeField] TextMeshProUGUI coinCounter;

    void Start()
    {
        myCollider2D = FindObjectOfType<Collider2D>();
    }
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        Debug.Log("Coin Obtained");
        coinsCollected++;
        Destroy(other.gameObject, 0);
        if (coinsCollected > 99)
        {
            coinsCollected = 0;
        }
        else if (coinsCollected < 10)
        {
            coinCounter.text = "0" + coinsCollected;
        }
        else
        {
            coinCounter.text = "" + coinsCollected;  
        }
         
    }
}
