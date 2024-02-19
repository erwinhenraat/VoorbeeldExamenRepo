using System.Security.Cryptography.X509Certificates;
using UnityEngine;

namespace Car
{
    public class CarPlayerScript : MonoBehaviour
    {

        [Header("Car Controller: ")] 
        [SerializeField] private float moveSpeed = 5f;
        private CharacterController _characterController;

        // Start is called before the first frame update
        private void Start() => _characterController = GetComponent<CharacterController>();

        private void Update()
        {
            // Player automatically goes forward
            _characterController.Move(-transform.right * (moveSpeed * Time.deltaTime));
        }
        
        public void GoLeft()
        {
            // Player goes left with the touch of an button that is in the UI from the game
            _characterController.Move(-transform.parent.forward * (moveSpeed * Time.deltaTime));
        }

        public void GoRight()
        {
            // Player goes Right with the touch of an button that is in the UI from the game
            _characterController.Move(transform.parent.forward * (moveSpeed * Time.deltaTime));
        }
    }
}
