using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

public class Enemy : MonoBehaviour
{
    private float currentSpeed;
    [SerializeField] private float baseSpeed;
    private bool isInView;
    private bool isNearPlayer;
    private bool atMaxDistance;
    [SerializeField] private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        
    }
    
    
}
