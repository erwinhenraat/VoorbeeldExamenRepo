using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using UntitledCube.Maze.Cell;

namespace UntitledCube.Maze.Generation
{
    public class GridGenerator : MonoBehaviour
    {
        [SerializeField] private PreLoader preLoader;

        private MazeCell[,] _cells;
        private bool _isGenerated;
        private List<MazeCell> _usedCells = new List<MazeCell>();

        public MazeCell[,] Cells => _cells;

        [Header("Unity Events")]
        [SerializeField] private UnityEvent<Vector2> OnGenerate = new UnityEvent<Vector2>();

        public void Generate(Vector2 mazeSize)
        {
            OnGenerate?.Invoke(mazeSize);

            if (_isGenerated) ResetCells();

            List<MazeCell> preLoadedCells = preLoader.PreLoadedCells;
            int cellCount = 0;

            // Setting _cells size
            _cells = new MazeCell[(int)mazeSize.x, (int)mazeSize.y];

            // Generate Grid
            for (int x = 0; x < mazeSize.x; x++)
            {
                for (int y = 0; y < mazeSize.y; y++)
                {
                    Vector2 position = new Vector2(x, y);
                    MazeCell avaibleCell = preLoadedCells[cellCount];

                    _cells.SetValue(avaibleCell, x, y);

                    // Settings position and settings cell active
                    avaibleCell.transform.position = position;
                    avaibleCell.transform.gameObject.SetActive(true);

                    _usedCells.Add(avaibleCell);
                    cellCount++;
                }
            }

            _isGenerated = true;
        }

        private void ResetCells()
        {
            // Reseting all the cells to there starting state/position
            for (int i = 0; i < _usedCells.Count; i++)
            {
                MazeCell currentCell = _usedCells[i];

                currentCell.SetWallsActive(true);
                currentCell.gameObject.SetActive(false);
                currentCell.SetState(CellState.Available);
                currentCell.transform.position = Vector3.zero;
            }

            _usedCells.Clear();
        }
    }
}