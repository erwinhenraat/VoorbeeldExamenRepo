using UnityEngine;

namespace UntitledCube.Transport
{
    public class TransportTrigger : MonoBehaviour
    {
        [SerializeField] private Sides _transportSide;
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
            currentObject.GetComponent<Rigidbody>().isKinematic = true;

            _transportMover.TransportToSide(_transportSide, currentObject, gameObject);
        }
    }
}