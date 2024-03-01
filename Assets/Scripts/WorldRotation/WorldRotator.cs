using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace UntitledCube.WorldRotation
{
    public class WorldRotator : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float _rotationDuration;

        private readonly Dictionary<WorldRotations, Quaternion> _directions = new()
        {
            { WorldRotations.UP, Quaternion.AngleAxis(-90f, Vector3.right) }, 
            { WorldRotations.DOWN, Quaternion.AngleAxis(90f, Vector3.right) },
            { WorldRotations.LEFT, Quaternion.AngleAxis(-90f, Vector3.up) },
            { WorldRotations.RIGHT, Quaternion.AngleAxis(90f, Vector3.up) }
        };

        public Action<WorldRotations> OnStartRotate;
        public Action OnFinishRotate;

        /// <summary>
        /// Starts the rotation of the world object to one of the neighbouring faces.
        /// </summary>
        /// <param name="direction"> The side the world object will rotate towards. </param>
        public void RotateWorld(WorldRotations direction)
        {
            OnStartRotate?.Invoke(direction);

            Quaternion startRotation = transform.rotation;
            Quaternion endRotation = startRotation * _directions[direction];

            StartCoroutine(LerpRotation(startRotation, endRotation));
        }

        private IEnumerator LerpRotation(Quaternion startRotation, Quaternion endRotation)
        {
            float startTime = Time.time;

            while (Time.time - startTime < _rotationDuration)
            {
                float time = (Time.time - startTime) / _rotationDuration;
                transform.rotation = Quaternion.Lerp(startRotation, endRotation, time);
                yield return null;
            }

            transform.rotation = endRotation;

            OnFinishRotate?.Invoke();
        }
    }
}