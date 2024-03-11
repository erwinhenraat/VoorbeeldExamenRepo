using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ScoreCalc : MonoBehaviour
{
    [SerializeField] private GameObject car;
    private Vector3 startPos;
    private Vector3 currentPos;
    private Vector3 endPos;
    
    void Start()
    {
        startPos = car.transform.position;
    }

    void Update()
    {
        currentPos = car.transform.position;
        
        if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
        {
            Die();
        }
    }

    void Die()
    {
        endPos = car.transform.position;
    }
}
