using UnityEngine;

namespace UntitledCube.Loading
{
    public class LoadIcon : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed = 90f;

        private void Update() => transform.Rotate(_rotationSpeed * Time.deltaTime * Vector3.forward);
    }
}