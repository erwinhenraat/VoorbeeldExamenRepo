using System.Collections;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;
using UntitledCube.Advertisements;
using UntitledCube.Gravity;
using UntitledCube.Timer;

namespace UntitledCube.Maze.Generation
{
    public class GenerationMediator : MonoBehaviour
    {
        [SerializeField] private string _seed;
        [SerializeField] private Button _button;
        [SerializeField] private Text _seedDisplay;

        private bool _initialized = false;
        private bool _gravityChanged;

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

        private void Start() => GenerateMaze(); //todo: Add this functionality to the play button when input gets to be working in develop

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
            Stopwatch.Instance.ResetStopWatch();

            if (_initialized)
                Advertising.Instance.ShowAd();

            _initialized = true;
            MazeGenerator.Generate(new(6, 6), _seed);

            _seed = default;

            StartCoroutine(StartStopWatch());
        }

        private IEnumerator StartStopWatch()
        {
            _gravityChanged = false;
            GravityManager.Instance.OnGravityChanged += OnGravityChangedHandler;
            yield return new WaitUntil(() => _gravityChanged);
            Stopwatch.Instance.StartStopWatch();
            GravityManager.Instance.OnGravityChanged -= OnGravityChangedHandler;
        }

        private void OnGravityChangedHandler(Vector3 newGravity) => _gravityChanged = true;
        
        private void DisplaySeed(string seed) => _seedDisplay.text = seed;
    }
}