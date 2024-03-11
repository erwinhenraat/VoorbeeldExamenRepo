using System;
using Input.Script;
using Spawner;
using UnityEngine;

namespace Car
{
    public class CarPlayerScript : MonoBehaviour
    {

        [Header("Car Controller: ")] 
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private InputHandler handler;
        [SerializeField] private ConnectGenerator despawnAfterTime;
        private CharacterController _characterController;
        
        private bool _isGoingLeft;
        private bool _isGoingRight;
        private const float RotSpeed = 10f;
        private float _currentRotationSpeed;

        // Start is called before the first frame update
        private void Start() => _characterController = GetComponent<CharacterController>();

        private void Update()
        {
            // Player automatically goes forward
            var transform1 = transform;
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
            
            // ReSharper disable once SwitchStatementMissingSomeEnumCasesNoDefault
            switch (handler.phoneInput)
            {
                case InputHandler.InputState.None:
                {
                    RotateCar(0);
                    if (moveSpeed < 7.5f)
                        ReturnSpeed();
                    break;
                }
                case InputHandler.InputState.Both:
                    Brake();
                    break;
            }
        }

        private void Brake()
        {
            moveSpeed = Mathf.Lerp(moveSpeed, 0, Time.deltaTime * 2f);

            if (moveSpeed < 0.01f)
                moveSpeed = 0;
        }

        private void ReturnSpeed()
        {
            moveSpeed = Mathf.Lerp(moveSpeed, 7.5f, Time.deltaTime * 2f);

            if (moveSpeed > 7.4f)
                moveSpeed = 7.5f;
        }
        
        private void RotateCar(int dir)
        {
            var targetAngle = dir > 0 ? 115f : dir < 0 ? 65f : 90f;
            var currentAngle = transform.localEulerAngles.y;
            _currentRotationSpeed = Mathf.Lerp(_currentRotationSpeed, RotSpeed, Time.deltaTime);
            var newAngle = Mathf.LerpAngle(currentAngle, targetAngle, _currentRotationSpeed * Time.deltaTime);

            // Apply the new rotation to the car
            transform.localEulerAngles = new Vector3(0, newAngle, 0);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("StreetTile"))
                despawnAfterTime.getChunks += 1;
        }
    }
}
