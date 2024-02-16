using System.Collections.Generic;
using UntitledCube.Maze.Cell;
using UnityEngine;

namespace UntitledCube.Maze.Generation
{
    public static class GridGenerator
    {
        private static MazeCell[,] _cells;
        private static bool _isGenerated;
        private static readonly Dictionary<GameObject, List<MazeCell>> _mazeHolders = new();
        
        private static readonly List<MazeCell> _usedCells = new();

        public static MazeCell[,] Cells => _cells;

        public static Dictionary<GameObject, List<MazeCell>> Mazes => _mazeHolders;

        public static void Generate(int amount, Vector2 size)
        {
            if(_isGenerated) 
                ResetCells();

            List<MazeCell> preLoadedCells = PreLoader.Instance.PreLoadedCells;

            int cellCount = 0;

            for (int i = 0; i < amount; i++)
            {
                _cells = new MazeCell[(int)size.x, (int)size.y];

                GameObject mazeHolder = new($"Maze Holder {i}");
                _mazeHolders.Add(mazeHolder, new());

                for (int x = 0; x < size.x; x++)
                {
                    for (int y = 0; y < size.y; y++)
                    {
                        Vector2 position = new(x, y);
                        MazeCell avaibleCell = preLoadedCells[cellCount];

                        _cells.SetValue(avaibleCell, x, y);

                        avaibleCell.Position = position;

                        avaibleCell.transform.gameObject.SetActive(true);
                        avaibleCell.transform.position = position;
                        avaibleCell.transform.parent = mazeHolder.transform;
                        avaibleCell.transform.rotation = Quaternion.Euler(Vector3.zero);

                        _mazeHolders[mazeHolder].Add(avaibleCell);
                        _usedCells.Add(avaibleCell);
                        cellCount++;
                    }
                }

                float xpos = mazeHolder.transform.position.x == 0 ? 1 : mazeHolder.transform.position.x;
                Vector2 positon = new(xpos * (i*6), 0);
                mazeHolder.transform.position = positon;
            }

            ShapeMazes(_mazeHolders);

            _isGenerated = true;
        }

        private static void ShapeMazes(Dictionary<GameObject, List<MazeCell>> mazes)
        {
            GameObject[] spawnPoints = PreLoader.Instance.SpawnPoints;

            int index = 0;
            foreach(GameObject maze in mazes.Keys)
            {
                maze.transform.position = spawnPoints[index].transform.position;
                maze.transform.rotation = spawnPoints[index].transform.rotation;
                index++;
            }
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

            foreach (GameObject maze in _mazeHolders.Keys)
                GameObject.Destroy(maze);

            _mazeHolders.Clear();
        }
    }
}