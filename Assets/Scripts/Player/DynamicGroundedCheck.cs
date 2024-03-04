using UnityEngine;
using UntitledCube.Gravity;

public class DynamicGroundedCheck : MonoBehaviour
{
    [SerializeField] private float _raycastDistance = 0.2f;
    private Vector3 _raycastDirection = Vector3.down;

    public bool IsGrounded => Physics.Raycast(transform.position, _raycastDirection, _raycastDistance);

    private void Awake() => GravityManager.Instance.OnGravityChanged += SetRayCastDirection;
    private void SetRayCastDirection(Vector3 direction) => _raycastDirection = direction.normalized;

    private void OnDestroy() 
    {
        if (GravityManager.Instance != null)
            GravityManager.Instance.OnGravityChanged -= SetRayCastDirection;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, _raycastDirection * _raycastDistance);
    }
}
