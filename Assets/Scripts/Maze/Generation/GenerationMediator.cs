using UnityEngine;
using UnityEngine.UI;
using UntitledCube.Advertisements;
using UntitledCube.Timer;

namespace UntitledCube.Maze.Generation
{
    public class GenerationMediator : MonoBehaviour
    {
        [SerializeField] private string _seed;
        [SerializeField] private Button _button;
        [SerializeField] private Text _seedDisplay;

        private bool _initialized = false;

        private void OnEnable()
        {
            _button.onClick.AddListener(GenerateMaze);
            MazeGenerator.OnGenerated += DisplaySeed;
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(GenerateMaze);
            MazeGenerator.OnGenerated -= DisplaySeed;
        }

        /// <summary>
        /// Generates maze based on given seed
        /// </summary>
        /// <param name="newSeed">The string that contains the seed you want to generate</param>
        public void GenerateMaze(string newSeed)
        {
            _seed = newSeed;
            GenerateMaze();
        }

        private void GenerateMaze()
        {
            if (_initialized)
                Advertising.Instance.ShowAd();

            _initialized = true;
            MazeGenerator.Generate(new(6, 6), _seed);

            Stopwatch.Instance.StartStopWatch();

            _seed = default;
        }

        private void DisplaySeed(string seed) => _seedDisplay.text = seed;
    }
}