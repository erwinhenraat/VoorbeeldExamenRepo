using System;
using System.Collections;
using System.Collections.Generic;
using Car;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private GameOverScreen gameOverScreen;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            gameOverScreen.MenuOpen();
        }
    }
}
