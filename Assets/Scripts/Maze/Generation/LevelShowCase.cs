using UnityEngine;

namespace UntitledCube.Maze
{
    public class LevelShowCase : MonoBehaviour
    {
        [SerializeField] private bool shouldRotate = false;
        [SerializeField] private float rotationSpeed = 45f;

        public bool ShouldRotate
        {
            get => shouldRotate;
            set => shouldRotate = value;
        }

        private void Update()
        {
            if (!shouldRotate)
                return;

            transform.Rotate
            (
                rotationSpeed * Time.deltaTime,
                rotationSpeed * Time.deltaTime,
                rotationSpeed * Time.deltaTime
            );
        }
    }
}