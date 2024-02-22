using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Car
{
    public class CarPlayerScript : MonoBehaviour
    {

        [Header("Car Controller: ")] 
        [SerializeField] private float moveSpeed = 5f;
        private CharacterController _characterController;
        
        private bool _isGoingLeft;
        private bool _isGoingRight;

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
            if (_isGoingLeft)
            {
                _characterController.Move(-transform.forward * (moveSpeed * Time.deltaTime));
                transform.localRotation = Quaternion.Euler(0f, 75f, 0);
            }
            else if (_isGoingRight)
            {
                _characterController.Move(transform.forward * (moveSpeed * Time.deltaTime));
                transform.localRotation = Quaternion.Euler(0f, 105f, 0);
            }
        }
    }
}
