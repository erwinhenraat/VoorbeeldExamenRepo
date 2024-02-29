using UnityEngine;
using UntitledCube.WorldRotation;

namespace UntitledCube.Transport
{
    public class TransportTrigger : MonoBehaviour
    {
        [SerializeField] private WorldRotations _transportSide;
        [SerializeField] private LayerMask[] _TransportableLayer;

        private TransportMover _transportMover;

        private void Awake() => _transportMover = FindObjectOfType<TransportMover>();

        private void OnTriggerEnter(Collider other)
        {
            foreach (LayerMask layerMask in _TransportableLayer)
            {
                if ((layerMask.value & (1 << other.gameObject.layer)) == 0)
                    return;
            }

            GameObject currentObject = other.gameObject;

            _transportMover.TransportToSide(_transportSide, currentObject, gameObject);
        }
    }
}