using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PrototypeEnemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float baseSpeed;
    private GameObject _player;
    // private testplayermovement playerMovement;
    private float _playerPosX;
    public GameObject model;
    [SerializeField] private float maxDistance;
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        // playerMovement = _player.GetComponent<testplayermovement>();
    }
    
    void Update()
    {
        _playerPosX = _player.gameObject.transform.position.x;
        transform.Translate(0 , 0, speed * Time.deltaTime);
        FollowPlayer();
    }

    void FollowPlayer()
    {
        float distance = Vector3.Distance(_player.transform.position, this.gameObject.transform.position);
        transform.position = new Vector3(_playerPosX, transform.position.y, transform.position.z);
        if (distance > maxDistance)
        {
            // speed = playerMovement.GetSpeed();
        }
        else
        {
            speed = baseSpeed;
        }
        
    }
}
