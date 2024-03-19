using UnityEngine;

namespace Car
{
    public class CamMovement : MonoBehaviour
    {

        [Header("Camera Follows Car Settings: ")] 
        [SerializeField] private Transform playerTransform;
        [SerializeField] private float smoothSpeed;
        [SerializeField] private float distanceFromPlayer;

        // Offset of current Pos
        private Vector3 _offset;

        private void Start() => _offset = transform.position - playerTransform.position;
        
        // Update is called once per frame
        private void FixedUpdate()
        {
            // Calculate the desired x-position of the camera
            var desiredXPosition = playerTransform.position + _offset;
            desiredXPosition.z -= distanceFromPlayer;
            
            // Smoothly move the camera towards the desired x-position
            var smoothedPos = Vector3.Lerp(transform.position, desiredXPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPos;
        }
    }
}
