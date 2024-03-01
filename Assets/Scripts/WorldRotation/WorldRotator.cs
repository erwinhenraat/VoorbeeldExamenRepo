using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using static UnityEngine.GridBrushBase;

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

        private readonly Dictionary<WorldRotations, Quaternion> _directions = new()
        {
            { WorldRotations.UP, Quaternion.AngleAxis(-90f, Vector3.right) }, 
            { WorldRotations.DOWN, Quaternion.AngleAxis(90f, Vector3.right) },
            { WorldRotations.LEFT, Quaternion.AngleAxis(-90f, Vector3.up) },
            { WorldRotations.RIGHT, Quaternion.AngleAxis(90f, Vector3.up) }
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
            //StartCoroutine(RotateStep(direction));
            Rotate(direction);
        }

        private IEnumerator RotatingSides()
        {
            foreach (WorldRotations rotation in _rotationDirections)
            {
                RotateWorld(rotation);
                yield return new WaitForSeconds(_timebetweenRotations);
            }
        }

        /*private IEnumerator RotationSteps(WorldRotations rotationDiraction)
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

            Debug.Log(startRotation.eulerAngles);
            Vector3 testPosition = new Vector3(startRotation.eulerAngles.x + endRotation.x, startRotation.eulerAngles.y + endRotation.y, 0f);
            transform.rotation = Quaternion.Euler(testPosition);

            OnFinishRotate?.Invoke();
        }

        private IEnumerator RotateStep(WorldRotations direction) 
        {
            Vector3 startRotation = transform.rotation.eulerAngles;
            _directions.TryGetValue(direction, out Vector3 endRotation);


            for (float i = 0; i <= 1; i += _stepAmount)
            {
                Vector3 midRotation = new
                (
                    Mathf.Lerp(startRotation.x, endRotation.x, i), 
                    Mathf.Lerp(startRotation.y, endRotation.y, i),
                    0f
                );

                transform.rotation = Quaternion.Euler(startRotation + midRotation);

                yield return new WaitForSeconds(_betweenSteps);
            }

            transform.rotation = Quaternion.Euler(endRotation);

            OnFinishRotate?.Invoke();
        }*/

        private void Rotate(WorldRotations direction)
        {
            Quaternion startRotation = transform.rotation;
            Quaternion endRotation = _directions[direction];
            Quaternion newRotation = startRotation * endRotation;

            transform.rotation = newRotation;

            Debug.Log($"{startRotation} + {endRotation} = {newRotation}");

            OnFinishRotate?.Invoke();
        }

    }
}