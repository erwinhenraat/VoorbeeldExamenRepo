using UnityEngine;

namespace Car
{
    public class CamMovement : MonoBehaviour
    {

        [Header("Camera Follows Car Settings: ")] 
        [SerializeField] private Transform playerTransform;
        [SerializeField] private float smoothSpeed;
        
        // Update is called once per frame
        private void FixedUpdate()
        {
            // Calculate the desired x-position of the camera
            var desiredXPosition = playerTransform.position.z;

            // Smoothly move the camera towards the desired x-position
            var smoothedXPosition = Mathf.Lerp(transform.position.z, desiredXPosition, smoothSpeed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, transform.position.y, smoothedXPosition);
        }
    }
}
