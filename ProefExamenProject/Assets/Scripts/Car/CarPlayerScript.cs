using System.Security.Cryptography.X509Certificates;
using Input.Script;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

namespace Car
{
    public class CarPlayerScript : MonoBehaviour
    {

        [Header("Car Controller: ")] 
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private InputHandler handler;
        private CharacterController _characterController;
        
        private bool _isGoingLeft;
        private bool _isGoingRight;
        private float _rotSpeed = 90f;
        private float _maxRotAngle = 30f;

        [Header("Assign Buttons: ")] 
        [SerializeField] private Button leftButton;
        [SerializeField] private Button rightButton;

        // Start is called before the first frame update
        private void Start()
        { 
            _characterController = GetComponent<CharacterController>();
            
            // Add listeners for button press events
            leftButton.onClick.AddListener(() => {
                _isGoingLeft = true;
                _isGoingRight = false;
            });

            rightButton.onClick.AddListener(() => {
                _isGoingRight = true;
                _isGoingLeft = false;
            });

            // Add listeners for button release events
            var leftTrigger = leftButton.gameObject.AddComponent<EventTrigger>();
            var leftEntry = new EventTrigger.Entry {eventID = EventTriggerType.PointerUp};
            leftEntry.callback.AddListener(_ => {
                _isGoingLeft = false;
            });
            leftTrigger.triggers.Add(leftEntry);

            var rightTrigger = rightButton.gameObject.AddComponent<EventTrigger>();
            var rightEntry = new EventTrigger.Entry {eventID = EventTriggerType.PointerUp};
            rightEntry.callback.AddListener(_ => {
                _isGoingRight = false;
            });
            rightTrigger.triggers.Add(rightEntry);
        }

        private void Update()
        {
            // Player automatically goes forward
            var transform1 = transform;
            
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity))
            {
                var distanceToGround = hit.distance;
                transform.position -= new Vector3(0, distanceToGround - 0.439f, 0f);
            }
            
            _characterController.Move(-transform1.right * (moveSpeed * Time.deltaTime));

            // Move left or right while the corresponding button is held down
            if (_isGoingLeft || handler.phoneInput == InputHandler.InputState.Left)
            {
                _characterController.Move(-transform.forward * (moveSpeed * Time.deltaTime));
                RotateCar(-1);
            }
            else if (_isGoingRight || handler.phoneInput == InputHandler.InputState.Right)
            {
                _characterController.Move(transform.forward * (moveSpeed * Time.deltaTime));
                RotateCar(1);
            }
        }
 
        private void RotateCar(int dir)
        {
            var rotAngle = _rotSpeed * Time.deltaTime * dir;
            
            if (Mathf.Abs(transform.localEulerAngles.y) < _maxRotAngle)
                transform.Rotate(0,rotAngle,0);
        }
    }
}
