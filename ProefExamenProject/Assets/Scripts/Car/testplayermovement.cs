using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testplayermovement : MonoBehaviour
{
    public float speed;
    private void Update()
    {
        transform.Translate(0 , 0, speed * Time.deltaTime);

        if (UnityEngine.Input.GetKeyDown(KeyCode.A))
            transform.Translate(5f * Time.deltaTime, 0f, 0f);
        if (UnityEngine.Input.GetKeyDown(KeyCode.D))
            transform.Translate(-5f * Time.deltaTime, 0f, 0f);
    }

    public float GetSpeed()
    {
        return speed;
    }
}
