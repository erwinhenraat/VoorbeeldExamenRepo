using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UntitledCube.WorldRotation
{
    public class WorldRotator : MonoBehaviour
    {
        [Header ("Rotation Testing")]
        [SerializeField] private List<RotationDirection> _rotationDirections = new();
        [SerializeField] private float _timebetweenRotations = 2f;

        [Header("Settings")]
        [SerializeField] private float _stepAmount = 200;
        [SerializeField] private float _stepSpeed = 0.005f;
        [SerializeField] private float _step;
        //[SerializeField] private float _step = 0;

        private Dictionary<RotationDirection, Vector3> _directions = new();
        private Vector3 _currentRotation;

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

            _directions.TryGetValue(direction, out Vector3 rotationValue);
            Vector3 step = rotationValue / _stepAmount;

            //_step = _stepAmount / 100;
            StartCoroutine(RotationSteps(direction));
        }

        private void SetValues()
        {
            _directions.Add(RotationDirection.UP, new Vector3(-90f, 0f, 0f));
            _directions.Add(RotationDirection.DOWN, new Vector3(90f, 0f, 0f));
            _directions.Add(RotationDirection.LEFT, new Vector3(0f, 90f, 0f));
            _directions.Add(RotationDirection.RIGHT, new Vector3(0f, -90f, 0f));
        }

        private IEnumerator RotatingSides()
        {
            foreach (RotationDirection rotation in _rotationDirections)
            {
                RotateWorld(rotation);
                yield return new WaitForSeconds(_timebetweenRotations);
            }
        }

        private IEnumerator RotationSteps(RotationDirection rotationDiraction)
        {
            var firstRotation = transform.rotation;
            var startRotationX = transform.rotation.x;
            var startRotationY = transform.rotation.y;

            _directions.TryGetValue(rotationDiraction, out Vector3 endRoationother);
            var endRotationX = endRoationother.x;
            var endRotationY = endRoationother.y;
            
            for (_step = 0; _step <= 1.1; _step += 0.1f)
            {
                Debug.Log("LOOPING");
                Quaternion midRotation = Quaternion.Euler(
                    Mathf.Lerp(startRotationX, endRotationX, _step),
                    Mathf.Lerp(startRotationY, endRotationY, _step),
                    0f
                );
                Debug.Log("Mid Rotation: " + midRotation.eulerAngles);

                transform.rotation = firstRotation * midRotation;

                yield return new WaitForSeconds(_stepSpeed);
            }

            OnFinishRotate?.Invoke();
        }
    }
}
