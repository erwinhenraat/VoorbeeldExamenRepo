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

        private void Start() => _retryButton.onClick.AddListener(ResetLevel);

        private void ResetLevel()
        {
            if (GameStateMachine.Instance.CurrentState is not GameState)
                return;

            Stopwatch.Instance.ResetStopWatch();
            Advertising.Instance.ShowAd();
            MazeGenerator.Generate(new(6, 6), MazeGenerator.Seed);

            Stopwatch.Instance.StartWithInteractionSync();
        }
    }
}