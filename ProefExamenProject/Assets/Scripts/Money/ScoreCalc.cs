using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ScoreCalc : MonoBehaviour
{
    public GameObject car;
    private Vector3 startPos;
    private Vector3 currentPos;
    private Vector3 endPos;
    private bool notDead = true;
    
    void Start()
    {
        startPos = car.transform.position;
        Debug.Log(startPos);
    }

    void Update()
    {
        currentPos = car.transform.position;
        
        if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
        {
            Die();
            notDead = false;
        }
    }

    void Die()
    {
        endPos = car.transform.position;
    }
}
