using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Sides
{
    LEFT,
    RIGHT,
    TOP,
    Down,
}

public class TransportTrigger : MonoBehaviour
{
    [SerializeField] private Sides _transportSide;
    [SerializeField] private LayerMask[] _TransportableLayer;

    private void OnTriggerEnter(Collider other)
    {
        foreach (LayerMask layerMask in _TransportableLayer)
        {
            if ((layerMask.value & (1 << other.gameObject.layer)) == 0)
                return;
        }

        GameObject currentObject = other.gameObject;
        currentObject.GetComponent<Rigidbody>().isKinematic = true;

        TransportToSide(_transportSide, currentObject);
    }

    private void TransportToSide(Sides side ,GameObject currentObject)
    {
        var playerPos = currentObject.transform.position;
        var transporterCenter = this.transform.position;
    }
}
