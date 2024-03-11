using MarkUlrich.StateMachine.States;
using MarkUlrich.StateMachine;
using UnityEngine;
using UnityEngine.UI;
using UntitledCube.Advertisements;
using UntitledCube.Gravity;
using UntitledCube.Spawning;
using UntitledCube.Timer;
using UntitledCube.WorldRotation;

namespace UntitledCube.UI.Buttons
{
    public class RetryButton : MonoBehaviour
    {
        [SerializeField] private Button _retryButton;

        private WorldRotator _rotator;
        private PlayerSpawnPoint _spawnPoint;
        private GravityManager _gravityManager;
        private Stopwatch _stopwatch;
        private Advertising _advertising;

        private void Start()
        {
            _rotator = FindAnyObjectByType<WorldRotator>();
            _spawnPoint = FindAnyObjectByType<PlayerSpawnPoint>();
            _gravityManager = GravityManager.Instance;
            _stopwatch = Stopwatch.Instance;
            _advertising = Advertising.Instance;

            _retryButton.onClick.AddListener(ResetLevel);
        }

        private void ResetLevel()
        {
            if (GameStateMachine.Instance.CurrentState is PausedState) return;

             _rotator.ResetRotation();
            _spawnPoint.SpawnPlayer("");
            _gravityManager.ResetGravity("");
            _stopwatch.StartStopWatch();
            _advertising.ShowAd();
        }
    }
}
 
