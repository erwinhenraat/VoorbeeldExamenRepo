using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCar : MonoBehaviour
{
    [SerializeField] private float speed;

    private void Start()
    {
        Destroy(this.gameObject, 10);
    }

    void Update()
    {
        transform.Translate(0 , 0, -speed * Time.deltaTime);
    }
}
