using System.Collections.Generic;
using UntitledCube.Maze.Cell;
using UnityEngine;

namespace UntitledCube.Maze.Generation
{
    public static class GridGenerator
    {
        private static bool _isGenerated;
        private static readonly Dictionary<GameObject, MazeCell[,]> _gridHolders = new();
        
        private static readonly List<MazeCell> _usedCells = new();

        public static Dictionary<GameObject, MazeCell[,]> Grids => _gridHolders;

        /// <summary>
        /// Generates a grid of MazeCell objects, organized into "Maze Holder" GameObjects. 
        /// Optionally uses preloaded cells and shapes the resulting maze. 
        /// </summary>
        /// <param name="amount">The number of "Maze Holder" GameObjects to generate.</param>
        /// <param name="size">The dimensions (width and height) of each maze grid.</param>
        public static void Generate(int amount, Vector2 size)
        {
            if(_isGenerated) 
                ResetCells();

            List<MazeCell> preLoadedCells = PreLoader.Instance.PreLoadedCells;

            int cellCount = 0;

            for (int i = 0; i < amount; i++)
            {
                GameObject gridHolder = new($"Maze Holder {i}");
                _gridHolders.Add(gridHolder, new MazeCell[(int)size.x, (int)size.y]);

                for (int x = 0; x < size.x; x++)
                {
                    for (int y = 0; y < size.y; y++)
                    {
                        Vector2 position = new(x, y);
                        MazeCell avaibleCell = preLoadedCells[cellCount];

                        _gridHolders[gridHolder].SetValue(avaibleCell, x, y);

                        avaibleCell.Position = position;

                        avaibleCell.transform.gameObject.SetActive(true);
                        avaibleCell.transform.position = position;
                        avaibleCell.transform.parent = gridHolder.transform;
                        avaibleCell.transform.rotation = Quaternion.Euler(Vector3.zero);

                        _usedCells.Add(avaibleCell);
                        cellCount++;
                    }
                }
            }

            ShapeMazes(_gridHolders);

            _isGenerated = true;
        }

        private static void ShapeMazes(Dictionary<GameObject, MazeCell[,]> mazes)
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

                currentCell.Reset();
                currentCell.gameObject.SetActive(false);
                currentCell.State = CellState.Available;
                currentCell.transform.position = Vector3.zero;
                currentCell.transform.parent = PreLoader.Instance.transform;
            }

            _usedCells.Clear();

            foreach (GameObject maze in _gridHolders.Keys)
                GameObject.Destroy(maze);

            _gridHolders.Clear();
        }
    }
}