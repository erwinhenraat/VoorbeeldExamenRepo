using MarkUlrich.StateMachine.States;
using MarkUlrich.StateMachine;
using UnityEngine;
using UnityEngine.UI;
using UntitledCube.Maze.Generation;
using UntitledCube.Advertisements;
using UntitledCube.Timer;
using UntitledCube.Gravity;
using System.Collections;

namespace UntitledCube.UI.Buttons
{
    public class RetryButton : MonoBehaviour
    {
        [SerializeField] private Button _retryButton;

        private bool _gravityChanged;

        private void Start() => _retryButton.onClick.AddListener(ResetLevel);

        private void ResetLevel()
        {
            if (GameStateMachine.Instance.CurrentState is not GameState)
                return;

            Stopwatch.Instance.ResetStopWatch();
            Advertising.Instance.ShowAd();
            MazeGenerator.Generate(new(6, 6), MazeGenerator.Seed);

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
    }
}