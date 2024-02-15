using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public class SwipeInteraction : MonoBehaviour
    {
        [SerializeField] private Vector2 maximumDirection;
        [SerializeField] private float minimumMagnitude;

        private int _isGoingSideways = 2;
        private InputAction _swipeAction;
        private InputAction _touchAction;
        private Vector2 _swipeDirection;

        private void Start()
        {
            InputSystem.SubscribeToAction("Swipe", Swipe, out _swipeAction);
            _touchAction = InputSystem.TryGetAction("Touch");

            _touchAction.Enable();
            _touchAction.canceled += StopSwipe;
        }

        private void Swipe(InputAction.CallbackContext callbackContext)
        {
            _swipeDirection = callbackContext.ReadValue<Vector2>();

       
     /*       float x = Mathf.Abs(_swipeDirection.x);
            float y = Mathf.Abs(_swipeDirection.y);
            float newX = Mathf.Abs(_newSwipeDirection.x);
            float newY = Mathf.Abs(_newSwipeDirection.y);

            float directionX = newX - x; 
            float directionY = newY - y; 

            if(directionX > directionY && _isGoingSideways != 1)
            {
                  
            }*/

        }

        private void StopSwipe(InputAction.CallbackContext callbackContext)
        {
            if (_swipeDirection.magnitude < minimumMagnitude) return;

            float x = Mathf.Abs(_swipeDirection.x);
            float y = Mathf.Abs(_swipeDirection.y);

           if(x > y)
            {
                int direction = _swipeDirection.x > 0 ? 1 : -1;
                //rotate sideways
                print("sideways");
            }
           else if(x < y)
            {
                int direction = _swipeDirection.y > 0 ? 1 : -1;
                //rotate upwards
                print("upwards");
            }

        }

        private void OnDisable()
        {
            InputSystem.UnsubscribeToAction(_swipeAction, Swipe);
            _touchAction.canceled -= StopSwipe;
        }
    }
}
