using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverToSide : MonoBehaviour
{
    [SerializeField] LayerMask[] _movableLayers;

    private void OnTriggerEnter(Collider other)
    {
        foreach (LayerMask layerMask in _movableLayers)
        {
            if ((layerMask.value & (1 << other.gameObject.layer)) == 0)
                return;
        }

        GameObject currentObject = other.gameObject;
        currentObject.GetComponent<Rigidbody>().isKinematic = true;

        SetToOtherFace(currentObject);
    }

    private void SetToOtherFace(GameObject currentObject)
    {

    }

}
