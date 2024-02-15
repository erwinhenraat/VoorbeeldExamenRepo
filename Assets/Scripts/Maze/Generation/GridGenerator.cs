using System.Collections.Generic;
using UntitledCube.Maze.Cell;
using UnityEngine;

namespace UntitledCube.Maze.Generation
{
    public static class GridGenerator
    {
        private static MazeCell[,] _cells;
        private static bool _isGenerated;
        private static GameObject _mazeHolder;
        
        private static readonly List<MazeCell> _usedCells = new();

        public static MazeCell[,] Cells => _cells;

        public static void Generate(Vector2 mazeSize, Vector3 worldPosition, Vector3 rotation)
        {
            if (_isGenerated) 
                ResetCells();

            List<MazeCell> preLoadedCells = PreLoader.Instance.PreLoadedCells;
            int cellCount = 0;

            _cells = new MazeCell[(int)mazeSize.x, (int)mazeSize.y];

            _mazeHolder = new("Maze Holder");

            for (int x = 0; x < mazeSize.x; x++)
            {
                for (int y = 0; y < mazeSize.y; y++)
                {
                    Vector2 position = new(x, y);
                    MazeCell avaibleCell = preLoadedCells[cellCount];

                    _cells.SetValue(avaibleCell, x, y);

                    avaibleCell.transform.position = position;
                    avaibleCell.Position = position;
                    avaibleCell.transform.gameObject.SetActive(true);
                    avaibleCell.transform.parent = _mazeHolder.transform;
                    avaibleCell.transform.rotation = Quaternion.Euler(Vector3.zero);

                    _usedCells.Add(avaibleCell);
                    cellCount++;
                }
            }

            _mazeHolder.transform.SetPositionAndRotation(worldPosition, Quaternion.Euler(rotation));

            _isGenerated = true;
        }

        private static void ResetCells()
        {
            for (int i = 0; i < _usedCells.Count; i++)
            {
                MazeCell currentCell = _usedCells[i];

                currentCell.SetWallsActive(true);
                currentCell.gameObject.SetActive(false);
                currentCell.State = CellState.Available;
                currentCell.transform.position = Vector3.zero;
                currentCell.transform.parent = PreLoader.Instance.transform;
            }

            _usedCells.Clear();
            GameObject.Destroy(_mazeHolder);
        }
    }
}