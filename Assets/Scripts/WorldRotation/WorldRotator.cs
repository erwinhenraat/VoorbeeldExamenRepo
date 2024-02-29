using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UntitledCube.WorldRotation
{
    public class WorldRotator : MonoBehaviour
    {
        [Header ("Rotation Testing")]
        [SerializeField] private List<WorldRotations> _rotationDirections = new();
        [SerializeField] private float _timebetweenRotations = 2f;

        [Header("Settings")]
        [SerializeField] private float _stepAmount = 0.01f;
        [SerializeField] private float _betweenSteps = 0.01f;

        private Dictionary<WorldRotations, Vector3> _directions = new Dictionary<WorldRotations, Vector3>()
        {
            { WorldRotations.UP, new Vector3(-90f, 0f, 0f) },
            { WorldRotations.DOWN, new Vector3(90f, 0f, 0f) },
            { WorldRotations.LEFT, new Vector3(0f, -90f, 0f) },
            { WorldRotations.RIGHT, new Vector3(0f, 90f, 0f) }
        };

        public Action<WorldRotations> OnStartRotate;
        public Action OnFinishRotate;

        private void Start() => StartCoroutine(RotatingSides());

        /// <summary>
        /// Starts the rotation of the world object to one of the neighbouring faces.
        /// </summary>
        /// <param name="direction"> The side the world object will rotate towards. </param>
        public void RotateWorld(WorldRotations direction)
        {
            OnStartRotate?.Invoke(direction);
            StartCoroutine(RotationSteps(direction));
        }

        private IEnumerator RotatingSides()
        {
            foreach (WorldRotations rotation in _rotationDirections)
            {
                RotateWorld(rotation);
                yield return new WaitForSeconds(_timebetweenRotations);
            }
        }

        private IEnumerator RotationSteps(WorldRotations rotationDiraction)
        {
            Quaternion startRotation = transform.rotation;
            _directions.TryGetValue(rotationDiraction, out Vector3 endRotation);

            for (float i = 0; i <= 1; i += _stepAmount)
            {
                Quaternion midRotation = Quaternion.Euler(
                    Mathf.Lerp(startRotation.x, endRotation.x, i),
                    Mathf.Lerp(startRotation.y, endRotation.y, i),
                    0f
                );
                transform.rotation = startRotation * midRotation;

                yield return new WaitForSeconds(_betweenSteps);
            }

            OnFinishRotate?.Invoke();
        }
    }
}