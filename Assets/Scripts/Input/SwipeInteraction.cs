using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public class SwipeInteraction : MonoBehaviour
    {
        [SerializeField] private float minimumMagnitude;

        private InputAction _swipeAction;
        private InputAction _touchAction;
        private Vector2 _swipePosition;

        private void Start()
        {
            InputSystem.SubscribeToAction("Swipe", Swipe, out _swipeAction);
            _touchAction = InputSystem.TryGetAction("Touch");

            _touchAction.Enable();
            _touchAction.canceled += StopSwipe;
        }

        private void Swipe(InputAction.CallbackContext callbackContext)
        {
            _swipePosition = callbackContext.ReadValue<Vector2>();

        }

        private void StopSwipe(InputAction.CallbackContext callbackContext)
        {
            print("here");
            if (_swipePosition.magnitude < minimumMagnitude) return;

            Vector2 direction = CalculateDirection();

            if(direction != Vector2.zero)
            {
                Debug.Log(direction);
                //GravityShift(direction)
            }

        }

        private Vector2 CalculateDirection()
        {
            float x = Mathf.Abs(_swipePosition.x);
            float y = Mathf.Abs(_swipePosition.y);

            if (x > y)
            {
                return _swipePosition.x > 0 ? Vector2.right : Vector2.left;
            }
            else if (x < y)
            {
                return _swipePosition.y > 0 ? Vector2.up : Vector2.down;
            }

            return Vector2.zero;
        }

        private void OnDisable()
        {
            InputSystem.UnsubscribeToAction(_swipeAction, Swipe);
            _touchAction.canceled -= StopSwipe;
        }
    }
}
