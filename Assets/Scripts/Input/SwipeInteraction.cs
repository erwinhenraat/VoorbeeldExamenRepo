using UnityEngine;
using UnityEngine.InputSystem;
using UntitledCube.Gravity;

namespace UntitledCube.Input
{
    public class SwipeInteraction : MonoBehaviour
    {
        [SerializeField] private float _moveThreshold;

        [SerializeField] private GravityManager GravityManager; // Switch to Singleton once implemented

        private InputAction _swipeAction;

        private Vector2 _swipePosition;
        private Vector2 _swipeDirection;

        private void OnEnable() => InputSystem.SubscribeToAction("Swipe", Swipe, out _swipeAction);

        private void Start() => Application.targetFrameRate = 120;
        
        private void Swipe(InputAction.CallbackContext callbackContext)
        {
            _swipePosition = callbackContext.ReadValue<Vector2>();

            Vector2 _newSwipeDirection = CalculateDirection();

            if (_newSwipeDirection == Vector2.zero) 
                return;

            if(_swipeDirection != _newSwipeDirection ) 
                GravityManager.SetGravityDirection(_newSwipeDirection);

            _swipeDirection = _newSwipeDirection;
        }

        private Vector2 CalculateDirection()
        {
            float x = Mathf.Abs(_swipePosition.x);
            float y = Mathf.Abs(_swipePosition.y);

            if (x > _moveThreshold && x > y) 
                return _swipePosition.x > 0 ? Vector2.right : Vector2.left;
            else if (y > _moveThreshold && x < y) 
                return _swipePosition.y > 0 ? Vector2.up : Vector2.down;
            
            return Vector2.zero;
        }

        private void OnDisable() => InputSystem.UnsubscribeToAction(_swipeAction, Swipe);
        
    }
}
