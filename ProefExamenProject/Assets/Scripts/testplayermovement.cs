using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testplayermovement : MonoBehaviour
{
    public float speed;
    void Update()
    {
        transform.Translate(0 , 0, speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(5f * Time.deltaTime, 0f, 0f);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(-5f * Time.deltaTime, 0f, 0f);
        }
    }

    public float GetSpeed()
    {
        return speed;
    }
}
