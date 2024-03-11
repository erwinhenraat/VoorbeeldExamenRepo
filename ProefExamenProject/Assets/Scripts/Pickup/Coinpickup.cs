using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coinpickup : MonoBehaviour
{
    private int coinWorth = 1;
        
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Car")) ;
        {
            Destroy(gameObject);
            ScoreCounter.instance.IncreaseScore(coinWorth);
        }
    }
}
