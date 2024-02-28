using System.Collections.Generic;
using UntitledCube.Maze.Cell;
using MarkUlrich.Utils;
using UnityEngine;

namespace UntitledCube.Maze.Generation
{
    public class PreLoader : SingletonInstance<PreLoader>
    {
        [SerializeField] private int _preloadAmount;
        [SerializeField] private MazeCell _cellPrefab;
        [SerializeField] private GameObject[] _mazeSpawnPoints;

        private readonly List<MazeCell> _preLoadedCells = new();

        public List<MazeCell> PreLoadedCells => _preLoadedCells;

        public GameObject[] SpawnPoints => _mazeSpawnPoints;

        private void Awake() => PreLoad();

        private void PreLoad()
        {
            for (int i = 0; i < _preloadAmount; i++)
            {
                MazeCell newCell = Instantiate(_cellPrefab, Vector3.zero, Quaternion.identity, transform);
                newCell.transform.gameObject.SetActive(false);
                newCell.State = CellState.Available;
                _preLoadedCells.Add(newCell);
            }
        }
    }
}