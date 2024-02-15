using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UntitledCube
{
    public enum RotationDirection
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }

    public class WorldRotator : MonoBehaviour
    {
        [Header ("Rotation Testing")]
        [SerializeField]
        private List<RotationDirection> _rotationDiractions = new();

        [SerializeField]
        private float _timebetweenRotations = 2f;

        [Header("Settings")]
        [SerializeField]
        private float _stepAmount = 20;

        [SerializeField]
        private float _stepSpeed = 0.05f;

        private Dictionary<RotationDirection, Vector3> directions = new Dictionary<RotationDirection, Vector3>();
        private Vector3 currentRotation;

        public Action<RotationDirection> OnStartRotate;
        public Action OnFinishRotate;

        private void Awake() => SetValues();
        private void Start() => StartCoroutine(RotatingSides());

        /// <summary>
        /// Starts the rotation of the world object to one of the neighbouring faces.
        /// </summary>
        /// <param name="direction"> The side the world object will rotate towards. </param>
        public void RotateWorld(RotationDirection direction)
        {
            OnStartRotate?.Invoke(direction);

            directions.TryGetValue(direction, out Vector3 rotationValue);
            Vector3 step = rotationValue / _stepAmount;

            StartCoroutine(RotationSteps(step));
        }

        private void SetValues()
        {
            directions.Add(RotationDirection.UP, new Vector3(-90f, 0f, 0f));
            directions.Add(RotationDirection.DOWN, new Vector3(90f, 0f, 0f));
            directions.Add(RotationDirection.LEFT, new Vector3(0f, 90f, 0f));
            directions.Add(RotationDirection.RIGHT, new Vector3(0f, -90f, 0f));
        }

        IEnumerator RotatingSides()
        {
            foreach (var rotation in _rotationDiractions)
            {
                RotateWorld(rotation);
                yield return new WaitForSeconds(_timebetweenRotations);
            }
        }

        IEnumerator RotationSteps(Vector3 step)
        {
            for (int i = 0; i < _stepAmount; i++)
            {
                currentRotation += step;
                transform.eulerAngles = currentRotation;
                yield return new WaitForSeconds(_stepSpeed);
            }

            OnFinishRotate?.Invoke();
        }
    }
}
