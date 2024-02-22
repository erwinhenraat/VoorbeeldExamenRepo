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

            
            var position = transform.position;
            
            // Smoothly move the camera towards the desired x-position
            var smoothedXPosition = Mathf.Lerp(position.z, desiredXPosition, smoothSpeed * Time.deltaTime);
            position = new Vector3(position.x, position.y, smoothedXPosition);
            transform.position = position;
        }
    }
}
