using UnityEngine;

public class TransportMover : MonoBehaviour
{
    [SerializeField] private float _triggerOffset;
    [SerializeField] private float _placementOffset;

    public void TransportToSide(Sides side, GameObject currentObject, GameObject currentTrigger)
    {
        Vector3 transportPosition = new();

        Vector3 objectPosition = currentObject.transform.position; 
        Vector3 triggerPosition = currentTrigger.transform.position;
        
        _placementOffset = (side == Sides.DOWN || side == Sides.RIGHT) ? -_placementOffset : _placementOffset;

        if (side == Sides.LEFT || side == Sides.RIGHT)
        {
            transportPosition.x = triggerPosition.x + _triggerOffset;
            transportPosition.y = objectPosition.y;
            transportPosition.z = triggerPosition.z + _placementOffset;
        }

        if (side == Sides.TOP || side == Sides.DOWN)
        {
            transportPosition.x = objectPosition.x;
            transportPosition.y = triggerPosition.y + _placementOffset;
            transportPosition.z = triggerPosition.z + _triggerOffset;
        }

        currentObject.transform.position = transportPosition;
    }
}
