using UntitledCube.Maze.Generation;
using UnityEngine;

namespace UntitledCube.Spawning
{
    public class PlayerSpawnPoint : MonoBehaviour
    {
        [SerializeField] private GameObject _player;
        [SerializeField] private Transform _spawnPoint;

        private void OnEnable() => MazeGenerator.OnGenerated += SpawnPlayer;

        private void OnDisable() => MazeGenerator.OnGenerated -= SpawnPlayer;

        /// <summary>
        /// Moves Player position to spawn position.
        /// </summary>
        public void SpawnPlayer(string _) => _player.transform.position = _spawnPoint.position;
    }
}