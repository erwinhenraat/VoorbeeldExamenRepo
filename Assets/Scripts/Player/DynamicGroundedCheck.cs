using UnityEngine;
using UntitledCube.Gravity;
using System;

namespace UntitledCube.Player
{
    public class DynamicGroundedCheck : MonoBehaviour
    {
        [SerializeField] private float _raycastDistance = 0.2f;
        private Vector3 _raycastDirection = Vector3.down;

        public event Action OnGrounded;
        public event Action OnUngrounded;

        private bool _wasGrounded = false;

        public bool IsGrounded => Physics.Raycast(transform.position, _raycastDirection, _raycastDistance);

        private void Awake() => GravityManager.Instance.OnGravityChanged += SetRayCastDirection;

        private void SetRayCastDirection(Vector3 direction) => _raycastDirection = direction.normalized;

        private void OnDestroy()
        {
            if (GravityManager.Instance != null)
                GravityManager.Instance.OnGravityChanged -= SetRayCastDirection;
        }

        private void Update()
        {
            bool currentlyGrounded = IsGrounded;

            if (currentlyGrounded && !_wasGrounded)
                OnGrounded?.Invoke();
            else if (!currentlyGrounded && _wasGrounded)
                OnUngrounded?.Invoke();

            _wasGrounded = currentlyGrounded;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, _raycastDirection * _raycastDistance);
        }
    }
}
