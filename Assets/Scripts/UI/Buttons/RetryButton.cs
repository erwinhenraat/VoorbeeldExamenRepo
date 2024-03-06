
using UnityEngine;
using UnityEngine.UI;
using UntitledCube.Gravity;
using UntitledCube.Spawning;
using UntitledCube.Timer;
using UntitledCube.WorldRotation;

public class RetryButton : MonoBehaviour
{
    [SerializeField] private Button _retryButton;

    private WorldRotator _rotator;
    private PlayerSpawnPoint _spawnPoint;
    private Stopwatch _stopwatch;
    private GravityManager _gravityManager;

    private void Start()
    {
        _rotator = FindAnyObjectByType<WorldRotator>();
        _spawnPoint = FindAnyObjectByType<PlayerSpawnPoint>();

        _stopwatch = Stopwatch.Instance;
        _gravityManager = GravityManager.Instance;

        _retryButton.onClick.AddListener(ResetLevel);
    }

    private void ResetLevel()
    {
        _rotator.ResetRotation();
        _spawnPoint.SpawnPlayer("");
        _stopwatch.StartStopWatch();
        _gravityManager.ResetGravity("");
    }
}
 
