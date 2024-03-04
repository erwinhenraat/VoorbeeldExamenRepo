using System;
using MarkUlrich.Utils;
using UnityEngine;

namespace UntitledCube.Gravity
{
    public class GravityManager : SingletonInstance<GravityManager>
    {
        [SerializeField] private float _gravityMultiplier = 1f;
        
        [SerializeField] private float _gravityAmount = 9.81f;

        public Action<Vector3> OnGravityChanged;

        /// <summary>
        /// Sets the gravity direction to the specified direction.
        /// </summary>
        /// <param name="direction">The direction of the gravity.</param>
        public void SetGravityDirection(Vector3 direction)
        {
            Physics.gravity = direction * _gravityAmount * _gravityMultiplier;
            OnGravityChanged?.Invoke(direction);
        }
    }
}
