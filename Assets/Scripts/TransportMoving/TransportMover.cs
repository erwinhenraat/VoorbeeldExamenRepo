using UnityEngine;
using UntitledCube.WorldRotation;

namespace UntitledCube.Transport
{
    public class TransportMover : MonoBehaviour
    {
        [SerializeField] private float _triggerOffset = 0.6f;
        [SerializeField] private float _placementOffset = 0.25f;

        private GameObject _currentObject;

        private WorldRotator _rotator;

        private void Awake() 
        {
            _rotator = FindObjectOfType<WorldRotator>();
            _rotator.OnFinishRotate += UnStickToWorld; 
        }

        public void TransportToSide(WorldRotations side, GameObject currentObject, GameObject currentTrigger)
        {
            _currentObject = currentObject;

            Vector3 transportPosition = new();

            Vector3 objectPosition = currentObject.transform.position;
            Vector3 triggerPosition = currentTrigger.transform.position;

            float placementOffset = _placementOffset;
            placementOffset = (side == WorldRotations.DOWN || side == WorldRotations.RIGHT)
                ? -_placementOffset
                : _placementOffset;

            if (side == WorldRotations.LEFT || side == WorldRotations.RIGHT)
            {
                transportPosition.x = triggerPosition.x + _triggerOffset;
                transportPosition.y = objectPosition.y;
                transportPosition.z = triggerPosition.z + placementOffset;
            }

            if (side == WorldRotations.UP || side == WorldRotations.DOWN)
            {
                transportPosition.x = objectPosition.x;
                transportPosition.y = triggerPosition.y + placementOffset;
                transportPosition.z = triggerPosition.z + _triggerOffset;
            }

            _currentObject.transform.position = transportPosition;

            StickToWorld(side);
        }

        private void StickToWorld(WorldRotations side)
        {
            _currentObject.transform.SetParent(transform);
            _currentObject.GetComponent<Rigidbody>().isKinematic = true;
            _currentObject.GetComponent<BoxCollider>().enabled = false;

            _rotator.RotateWorld(side);
        }
        private void UnStickToWorld()
        {
            _currentObject.GetComponent<BoxCollider>().enabled = true;
            _currentObject.GetComponent<Rigidbody>().isKinematic = false;
            _currentObject.transform.SetParent(null);
        }
    }
}