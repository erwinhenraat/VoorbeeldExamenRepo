// TESTING ONLY

using UnityEngine;

public class LevelShowCase : MonoBehaviour
{
    public bool rotate = false;
    public float rotationSpeed = 45f;

    void Update()
    {
        if (!rotate)
            return;
        transform.Rotate(
            rotationSpeed * Time.deltaTime,
            rotationSpeed * Time.deltaTime,
            rotationSpeed * Time.deltaTime
        );
    }
}
