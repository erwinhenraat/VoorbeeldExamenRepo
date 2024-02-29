using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coinpickup : MonoBehaviour
{
    public int coinWorth = 2;
        
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Car")) ;
        {
            Destroy(gameObject);
            ScoreCounter.instance.IncreaseScore(coinWorth);
        }
    }
}
