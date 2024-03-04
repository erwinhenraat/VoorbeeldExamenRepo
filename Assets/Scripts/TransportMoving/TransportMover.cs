using System;
using UnityEngine;
using UntitledCube.WorldRotation;
using System.Collections.Generic;

namespace UntitledCube.Transport
{
    public class TransportMover : MonoBehaviour
    {
        [SerializeField] private float _triggerOffset = 0.6f;
        [SerializeField] private OffsetDirection[] _offsetDirections;

        private Dictionary<WorldRotations, OffsetDirection> _placementOffset = new();
        private GameObject _currentObject;
        private WorldRotator _rotator;

        private void Awake() 
        {
            foreach (OffsetDirection offset in _offsetDirections)
                _placementOffset.Add(offset.Side, offset);
            
            _rotator = FindObjectOfType<WorldRotator>();
            _rotator.OnFinishRotate += UnStickToWorld; 
        }

        /// <summary>
        /// Transports the specified GameObject to the specified side, aligning it with the corresponding trigger object.
        /// </summary>
        /// <param name="side">The side to which the object should be transported (e.g., UP, DOWN, LEFT, RIGHT).</param>
        /// <param name="currentObject">The GameObject to be transported.</param>
        /// <param name="currentTrigger">The trigger object that determines the destination alignment.</param>
        public void TransportToSide(WorldRotations side, GameObject currentObject, GameObject currentTrigger)
        {
            _currentObject = currentObject;

            Vector3 transportPosition = new();

            Vector3 objectPosition = currentObject.transform.position;
            Vector3 triggerPosition = currentTrigger.transform.position;

            Vector3 placementOffset = _placementOffset[side].Offset;

            transportPosition.x = triggerPosition.x + _triggerOffset + placementOffset.x;
            transportPosition.y = objectPosition.y + placementOffset.y;
            transportPosition.z = triggerPosition.z + placementOffset.z;

            _currentObject.transform.position = transportPosition;

            StickToWorld(side);
        }

        private void StickToWorld(WorldRotations side)
        {
            _currentObject.transform.SetParent(transform);
            _currentObject.GetComponent<Rigidbody>().isKinematic = true;
            _currentObject.GetComponent<Collider>().enabled = false;

            _rotator.RotateWorld(side);
        }
        private void UnStickToWorld()
        {
            _currentObject.GetComponent<Collider>().enabled = true;
            _currentObject.GetComponent<Rigidbody>().isKinematic = false;
            _currentObject.transform.SetParent(null);
            _currentObject.transform.rotation = Quaternion.identity;
        }
    }

    [Serializable]
    public struct OffsetDirection
    {
        [SerializeField] private WorldRotations _side;
        [SerializeField] private Vector3 _offset;

        public readonly WorldRotations Side => _side;
        public readonly Vector3 Offset => _offset;
    }
}