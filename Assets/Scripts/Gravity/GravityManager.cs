using System;
using MarkUlrich.Utils;
using UnityEngine;
using UntitledCube.Maze.Generation;

namespace UntitledCube.Gravity
{
    public class GravityManager : SingletonInstance<GravityManager>
    {
        [SerializeField] private float _gravityMultiplier = 1f;
        
        [SerializeField] private float _gravityAmount = 9.81f;

        protected override void OnEnable()
        {
            base.OnEnable();
            MazeGenerator.OnGenerated += ResetGravity;
        }

        private void OnDisable() => MazeGenerator.OnGenerated -= ResetGravity;
        
        public Action<Vector3> OnGravityChanged;

        /// <summary>
        /// Sets the gravity direction to the specified direction.
        /// </summary>
        /// <param name="direction">The direction of the gravity.</param>
        public void SetGravityDirection(Vector3 direction)
        {
            Physics.gravity = _gravityAmount * _gravityMultiplier * direction;
            OnGravityChanged?.Invoke(direction);
        }

        /// <summary>
        /// Returns gravity to its original state.
        /// </summary>
        public void ResetGravity(string _) => SetGravityDirection(Vector3.down);
    }
}
