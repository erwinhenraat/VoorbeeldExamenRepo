using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UntitledCube.Maze.Generation;

namespace UntitledCube.WorldRotation
{
    public class WorldRotator : MonoBehaviour
    {
        [SerializeField] private GameObject _gyroscope;
        [SerializeField] private float _rotationDuration;

        private Coroutine _rotateRoutine;

        private readonly Dictionary<WorldRotations, Quaternion> _directions = new()
        {
            { WorldRotations.UP, Quaternion.AngleAxis(-90f, Vector3.right) }, 
            { WorldRotations.DOWN, Quaternion.AngleAxis(90f, Vector3.right) },
            { WorldRotations.LEFT, Quaternion.AngleAxis(-90f, Vector3.up) },
            { WorldRotations.RIGHT, Quaternion.AngleAxis(90f, Vector3.up) }
        };

        public Action<WorldRotations> OnStartRotate;
        public Action OnFinishRotate;

        private void Awake() => transform.parent = _gyroscope.transform;

        private void OnEnable() => MazeGenerator.OnGenerate += ResetRotation;

        private void OnDisable() => MazeGenerator.OnGenerate -= ResetRotation;

        /// <summary>
        /// Starts the rotation of the world object to one of the neighbouring faces.
        /// </summary>
        /// <param name="direction"> The side the world object will rotate towards. </param>
        public void RotateWorld(WorldRotations direction)
        {
            OnStartRotate?.Invoke(direction);

            Quaternion startRotation = _gyroscope.transform.rotation;
            Quaternion endRotation = startRotation * _directions[direction];

            _rotateRoutine = StartCoroutine(LerpRotation(startRotation, endRotation));
        }

        private IEnumerator LerpRotation(Quaternion startRotation, Quaternion endRotation)
        {
            float startTime = Time.time;

            while (Time.time - startTime < _rotationDuration)
            {
                float time = (Time.time - startTime) / _rotationDuration;
                _gyroscope.transform.rotation = Quaternion.Lerp(startRotation, endRotation, time);
                yield return null;
            }

            _gyroscope.transform.rotation = endRotation;

            FinishRotate();
        }

        private void FinishRotate()
        {
            transform.parent = null;
            _gyroscope.transform.rotation = Quaternion.identity;
            transform.parent = _gyroscope.transform;
            _rotateRoutine = null;
            OnFinishRotate?.Invoke();
        }

        /// <summary>
        /// Returns the cube to default rotation.
        /// </summary>
        public void ResetRotation()
        {
            if(_rotateRoutine != null)
            {
                StopCoroutine(_rotateRoutine);
                FinishRotate();
            }

            _rotateRoutine = null;
            transform.parent = null;

            transform.rotation = Quaternion.identity;
            _gyroscope.transform.rotation = Quaternion.identity;

            transform.parent = _gyroscope.transform;
        }
    }
}