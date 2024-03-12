using UntitledCube.Maze.Generation;
using UnityEngine;

namespace UntitledCube.Spawning
{
    public class PlayerSpawnPoint : MonoBehaviour
    {
        [SerializeField] private GameObject _player;
        [SerializeField] private Transform _spawnPoint;

        private Rigidbody _playerRigidbody;

        private void OnEnable() => MazeGenerator.OnGenerated += SpawnPlayer;

        private void OnDisable() => MazeGenerator.OnGenerated -= SpawnPlayer;

        private void SpawnPlayer(string _)
        {
            _player.transform.SetPositionAndRotation(_spawnPoint.position, Quaternion.identity);

            if (_playerRigidbody == null)
                _playerRigidbody = _player.GetComponent<Rigidbody>();

            _playerRigidbody.velocity = Vector3.zero;
            _playerRigidbody.freezeRotation = true;
            _playerRigidbody.freezeRotation = false;
        }   
    }
}