using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public class SwipeInteraction : MonoBehaviour
    {
        private InputAction _swipeAction;
        private Vector2 _startPosition = Vector2.zero;    
        private Vector2 _endPosition = Vector2.zero;    

        private void OnEnable()
        {
            InputSystem.SubscribeToAction("Swipe", Swipe, out _swipeAction);  
        }

        private void Swipe(InputAction.CallbackContext callbackContext)
        {
            Vector2 touchPosition = callbackContext.ReadValue<Vector2>();
            if(_startPosition == Vector2.zero)
            {
                _startPosition = touchPosition;
            }
            /*else if(_endPosition < touchPosition)
            {

            }*/
            //here comes shift function
        }

        private void OnDisable()
        {
            InputSystem.UnsubscribeToAction(_swipeAction, Swipe);
        }
    }
}
