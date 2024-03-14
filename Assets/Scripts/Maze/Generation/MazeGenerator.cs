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

        private static int _mazeCount = 0;
        private static int randomEnd;
        private static bool _startSet = false;

        public static Action OnGenerate;
        public static Action<string> OnGenerated;

        public static string Seed => SeedCodec.Assemble(_fullSeed, randomEnd);

        /// <summary>
        /// Generates a maze based on the given seed and size.
        /// </summary>
        /// <param name="size">The size the maze will be.</param>
        /// <param name="seed">The seed to generate a specific meaze.</param>
        public static void Generate(Vector2 size, string seed = "")
        {
            OnGenerate?.Invoke();

            (List<List<int>> decryptedSeed, int endPoint) = SeedCodec.ProcessSeed(seed);
            
            ResetCells();

            _fullSeed.Clear();
            _mazeCount = 0;

            _startSet = false;

            randomEnd = decryptedSeed == null ? UnityEngine.Random.Range(1, 5) : endPoint; 

            GridGenerator.Generate(6, size);

            foreach (GameObject grid in GridGenerator.Grids.Keys)
            {
                ResetCells();

                MazeCell[,] cells = GridGenerator.Grids[grid];

                _mazeSize = new Vector2(cells.GetLength(0), cells.GetLength(1));

                SetStartingCell(cells, decryptedSeed);
                SetExitCells(cells, decryptedSeed);

                GenerateMaze(cells, decryptedSeed);

                GenerateStartRoom(cells);
                GenerateEndRoom(cells, randomEnd);

                _mazeCount++;
            }

            OnGenerated?.Invoke(SeedCodec.Assemble(_fullSeed, randomEnd));
        }

        private static void ResetCells()
        {
            _currentPath.Clear();
            _completedCells = 0;
            _seed.Clear();
            _nextCellItteration = 0;
        }

        private static void SetStartingCell(MazeCell[,] cells, List<List<int>> seed)
        {
            int startPoint = CalculateStartPoint(seed);
            _currentPath.Add(cells[startPoint, 0]);
            _currentPath[0].State = CellState.Current;
            _currentPath[0].RemoveWall(Vector2.down);
            _seed.Add(startPoint);
        }

        private static void SetExitCells(MazeCell[,] cells, List<List<int>> seed)
        {
            (int Top, int Left, int Right) = CalculateExitPoints(seed);

            cells[Top, (int)_mazeSize.y - 1].RemoveWall(Vector2.up);
            _seed.Add(Top);

            cells[0, Left].RemoveWall(Vector2.left);
            _seed.Add(Left);

            cells[(int)_mazeSize.x - 1, Right].RemoveWall(Vector2.right);
            _seed.Add(Right);
        }

        private static int CalculateStartPoint(List<List<int>> seed)
        {
            if (seed != null)
                return seed[_mazeCount][0];

            if (_mazeCount >= 1 && _mazeCount <= 5)
            {
                if (_mazeCount == 5)
                    return 5 - SeedCodec.StartCell(_fullSeed[3], 3);

                int index = _mazeCount == 4 ? 3 : _mazeCount - 1;
                int factor = _mazeCount == 4 ? 2 : 1;
                return SeedCodec.StartCell(_fullSeed[index], factor);
            }

            return UnityEngine.Random.Range(0, (int)_mazeSize.x);
        }

        private static (int Top, int Left, int Right) CalculateExitPoints(List<List<int>> seed)
        {
            int topExitPoint = seed != null ? seed[_mazeCount][1] :
            _mazeCount switch
            {
                3 => SeedCodec.StartCell(_fullSeed[0], 0),
                4 => 5 - SeedCodec.StartCell(_fullSeed[1], 2),
                5 => SeedCodec.StartCell(_fullSeed[1], 3),
                _ => UnityEngine.Random.Range(0, (int)_mazeSize.x),
            };

            int leftExitPoint = seed != null ? seed[_mazeCount][2] :
            _mazeCount switch
            {
                4 => 5 - SeedCodec.StartCell(_fullSeed[2], 2),
                5 => SeedCodec.StartCell(_fullSeed[0], 3),
                _ => UnityEngine.Random.Range(0, (int)_mazeSize.y),
            };

            int rightExitPoint = seed != null ? seed[_mazeCount][3] :
            _mazeCount switch
            {
                4 => SeedCodec.StartCell(_fullSeed[0], 2),
                5 => 5 - SeedCodec.StartCell(_fullSeed[2], 3),
                _ => UnityEngine.Random.Range(0, (int)_mazeSize.y),
            };

            return (topExitPoint, leftExitPoint, rightExitPoint);
        }

        private static void GenerateMaze(MazeCell[,] cells, List<List<int>> seed)
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

            _fullSeed.Add(SeedCodec.Encrypt(SeedCodec.Encode(_seed)));
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

        private static void GoToNextCell(List<List<int>> seed)
        {
            int randomIndex = seed == null 
                ? UnityEngine.Random.Range(0, _possibleNext.Count) 
                : seed[_mazeCount][_nextCellItteration + 4];

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

        private static void GenerateStartRoom(MazeCell[,] cells)
        {
            if (_startSet)
                return;

            cells[2, 2].IsStart = true;
            cells[2, 3].IsStart = true;
            cells[3, 2].IsStart = true;
            cells[3, 3].IsStart = true;

            _startSet = true;
        }

        private static void GenerateEndRoom(MazeCell[,] cells, int endCount)
        {
            if (endCount != _mazeCount)
                return;

            cells[3, 2].SetEnd(0);
            cells[3, 3].SetEnd(1);
            cells[2, 2].SetEnd(2);
            cells[2, 3].SetEnd(3);
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