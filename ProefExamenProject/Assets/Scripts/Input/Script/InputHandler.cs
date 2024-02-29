using UnityEngine;

namespace Input.Script
{
    public class InputHandler : MonoBehaviour
    {
        
        [Header("Phone Input Settings: ")]
        public InputState phoneInput;
        private bool _leftTouched;
        private bool _rightTouched;


        private void Update()
        {
            switch (UnityEngine.Input.touchCount)
            {
                case > 0:
                    TouchHandler();
                    break;
                case < 1:
                    phoneInput = InputState.None;
                    break;
            }
        }


        private void TouchHandler()
        {
            for (var i = 0; i < UnityEngine.Input.touchCount; i++)
            {
                var touch = UnityEngine.Input.touches[i];
                if (touch.position.x < Screen.width / 2)
                {
                    phoneInput = InputState.Left;
                    if (touch.phase == TouchPhase.Began)
                        _leftTouched = true;
                    if (touch.phase == TouchPhase.Ended)
                        _leftTouched = false;

                }
                else
                {
                    phoneInput = InputState.Right;
                    if (touch.phase == TouchPhase.Began)
                        _rightTouched = true;
                    if (touch.phase == TouchPhase.Ended)
                        _rightTouched = false;
                }
                if (_leftTouched && _rightTouched)
                {
                    phoneInput = InputState.Both;
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
}


