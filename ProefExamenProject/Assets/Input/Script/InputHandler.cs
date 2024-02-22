using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public InputState PhoneInput;
    private bool leftTouched;
    private bool rightTouched;


    private void Update()
    {
        if (Input.touchCount > 0)
            TouchHandler();
        if (Input.touchCount < 1)
            PhoneInput = InputState.None;
    }


    private void TouchHandler()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            var touch = Input.touches[i];
            if (touch.position.x < Screen.width / 2)
            {
                Debug.Log("Left");
                PhoneInput = InputState.Left;
                if (touch.phase == TouchPhase.Began)
                    leftTouched = true;
                if (touch.phase == TouchPhase.Ended)
                    leftTouched = false;

            }
            else
            {
                Debug.Log("Right");
                PhoneInput = InputState.Right;
                if (touch.phase == TouchPhase.Began)
                    rightTouched = true;
                if (touch.phase == TouchPhase.Ended)
                    rightTouched = false;
            }
            if (leftTouched && rightTouched)
            {
                Debug.Log("Both");
                PhoneInput = InputState.Both;
            }

        }
    }

    public enum InputState
    {
        None = 0,
        Left = 1,
        Right = 2,
        Both = 4
    }
}


