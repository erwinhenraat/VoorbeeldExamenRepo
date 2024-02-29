using UnityEngine;
using UnityEngine.UI;
using UntitledCube.Advertisements;

namespace UntitledCube.Maze.Generation
{
    public class GenerationMediator : MonoBehaviour
    {
        [SerializeField] private string _seed;
        [SerializeField] private Button _button;

        private bool _initialized = false;

        private void OnEnable() => _button.onClick.AddListener(GenerateMaze);

        private void OnDisable() => _button.onClick.RemoveListener(GenerateMaze);

        private void Start() => GenerateMaze();

        /// <summary>
        /// Generate maze.
        /// </summary>
        public void GenerateMaze()
        {
            if (_initialized)
                Advertising.Instance.ShowAd();

            _initialized = true;
            MazeGenerator.Generate(new(6, 6), _seed);
        }
    }
}