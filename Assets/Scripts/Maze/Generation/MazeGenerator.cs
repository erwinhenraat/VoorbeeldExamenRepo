using System.Collections.Generic;
using UntitledCube.Maze.Cell;
using System.Linq;
using UnityEngine;
using System;

namespace UntitledCube.Maze.Generation
{
    public static class MazeGenerator
    {
        private static Vector2 _mazeSize;

        private static int _nextCellItteration;
        private static int _completedCells;

        private static readonly List<int> _seed = new();
        private static readonly List<string> _fullSeed = new();

        private static readonly List<MazeCell> _currentPath = new();
        private static readonly Dictionary<MazeCell, Vector2> _possibleNext = new();
        private static readonly Vector2[] _directions = { Vector2.right, Vector2.left, Vector2.down, Vector2.up };

        public static Action<string> OnGenerated;

        public static void Generate(Vector2 size, Vector3 worldPosition, Vector3 rotation, string seed = "")
        {
            List<int> decryptedSeed = SeedCodec.Decode(SeedCodec.Decrypt(seed));
            
            ResetCells();

            _fullSeed.Clear();

            GenerateGrid(size, worldPosition, rotation);

            foreach (GameObject grid in GridGenerator.Grids.Keys)
            {
                ResetCells();

                MazeCell[,] cells = GridGenerator.Grids[grid];

                _mazeSize = new Vector2(cells.GetLength(0), cells.GetLength(1));

                SetStartingCell(cells, decryptedSeed);
                SetExitCells(cells, decryptedSeed);

                GenerateMaze(cells, decryptedSeed);
            }

            string fullSeed = SeedCodec.Assemble(_fullSeed);
            Debug.Log(fullSeed);
            OnGenerated?.Invoke(fullSeed);
        }

        private static void ResetCells()
        {
            _currentPath.Clear();
            _completedCells = 0;
            _seed.Clear();
            _nextCellItteration = 0;
        }

        private static void GenerateGrid(Vector2 size, Vector3 worldPosition, Vector3 rotation)
        {
            GridGenerator.Generate(6, size);
        }

        private static void SetStartingCell(MazeCell[,] cells, List<int> seed)
        {
            int startPoint = seed.Count == 0 ? UnityEngine.Random.Range(0, (int)_mazeSize.x) : seed[0];
            _currentPath.Add(cells[startPoint, 0]);
            _currentPath[0].State = CellState.Current;
            _currentPath[0].RemoveWall(Vector2.down);
            _seed.Add(startPoint);
        }

        private static void SetExitCells(MazeCell[,] cells, List<int> seed)
        {
            int topExitPoint = seed.Count == 0 ? UnityEngine.Random.Range(0, (int)_mazeSize.x) : seed[1];
            cells[topExitPoint, (int)_mazeSize.y - 1].RemoveWall(Vector2.up);
            _seed.Add(topExitPoint);

            int leftExitPoint = seed.Count == 0 ? UnityEngine.Random.Range(0, (int)_mazeSize.y) : seed[2];
            cells[0, leftExitPoint].RemoveWall(Vector2.left);
            _seed.Add(leftExitPoint);

            int rightExitPoint = seed.Count == 0 ? UnityEngine.Random.Range(0, (int)_mazeSize.y) : seed[3];
            cells[(int)_mazeSize.x - 1, rightExitPoint].RemoveWall(Vector2.right);
            _seed.Add(rightExitPoint);
        }

        private static void GenerateMaze(MazeCell[,] cells, List<int> seed)
        {
            while (_completedCells < cells.Length)
            {
                _possibleNext.Clear();

                MazeCell currentCell = _currentPath[^1];
                Vector2 currentCellPos = new(currentCell.Position.x, currentCell.Position.y);

                GetPossibleDirections(cells, currentCellPos);

                if (_possibleNext.Count > 0)
                    GoToNextCell(seed);
                else
                    Backtrack();
            }

            string encryptedSeed = SeedCodec.Encrypt(SeedCodec.Encode(_seed));
            Debug.Log(encryptedSeed);
            _fullSeed.Add(encryptedSeed);
        }

        private static void GetPossibleDirections(MazeCell[,] cells, Vector2 position)
        {
            for (int i = 0; i < _directions.Length; i++)
            {
                Vector2 currentDirection = _directions[i];

                if (IsOutOfBounds(position + currentDirection))
                    continue;

                int targetX = (int)(position.x + currentDirection.x);
                int targetY = (int)(position.y + currentDirection.y);
                MazeCell currentCheckingCell = cells[targetX, targetY];

                if (currentCheckingCell.State == CellState.Completed || currentCheckingCell.State == CellState.Current)
                    continue;

                _possibleNext.Add(currentCheckingCell, currentDirection);
            }
        }

        private static void GoToNextCell(List<int> seed)
        {
            int randomIndex = seed.Count == 0 
                ? UnityEngine.Random.Range(0, _possibleNext.Count) 
                : seed[_nextCellItteration + 4];

            _seed.Add(randomIndex);

            MazeCell nextCell = _possibleNext.ElementAt(randomIndex).Key;

            Vector2 nextDirection = _possibleNext[nextCell];

            nextCell.RemoveWall(-nextDirection);
            _currentPath[^1].RemoveWall(nextDirection);

            _currentPath.Add(nextCell);
            nextCell.State = CellState.Current;

            _nextCellItteration++;
        }

        private static void Backtrack()
        {
            _completedCells++;

            _currentPath[^1].State = CellState.Completed;
            _currentPath.RemoveAt(_currentPath.Count - 1);
        }

        private static bool IsOutOfBounds(Vector2 potentialPosition)
        {
            return potentialPosition.x >= _mazeSize.x ||
                   potentialPosition.x < 0 ||
                   potentialPosition.y >= _mazeSize.y ||
                   potentialPosition.y < 0;
        }
    }
}