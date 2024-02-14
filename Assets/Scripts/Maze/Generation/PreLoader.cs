using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UntitledCube.Maze.Cell;

namespace UntitledCube.Maze.Generation
{
    public class PreLoader : MonoBehaviour
    {
        [SerializeField] private int preloadAmount;
        [SerializeField] private MazeCell cellPrefab;
        private List<MazeCell> _preLoadedCells = new List<MazeCell>();

        public List<MazeCell> PreLoadedCells => _preLoadedCells;

        private void Awake() => PreLoad();

        private void PreLoad()
        {
            // Preloading the preload amount
            for (int i = 0; i < preloadAmount; i++)
            {
                MazeCell newCell = Instantiate(cellPrefab, Vector3.zero, Quaternion.identity, this.transform);
                newCell.transform.gameObject.SetActive(false);
                newCell.SetState(CellState.Available);
                _preLoadedCells.Add(newCell);
            }
        }
    }
}