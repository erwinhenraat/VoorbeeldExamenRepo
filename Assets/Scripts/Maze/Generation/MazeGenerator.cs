using System.Collections.Generic;
using UnityEngine.Events;
using System.Collections;
using UnityEngine;
using UntitledCube.Maze.Cell;

namespace UntitledCube.Maze.Generation
{
    public class MazeGenerator : MonoBehaviour
    {
        [SerializeField] private GridGenerator gridGenerator;

        private Coroutine _mazeGeneration;
        private MazeCell[,] _cells;
        private Vector2 _mazeSize;
        private bool _instantGeneration = true;
        private bool _isGenerating;

        private List<MazeCell> _currentPath = new List<MazeCell>();
        private int _completedCells;
        private Dictionary<MazeCell, Vector2> _possibleNext = new Dictionary<MazeCell, Vector2>();
        private readonly Vector2[] _directions = { Vector2.right, Vector2.left, Vector2.down, Vector2.up };

        [Header("Unity Events")]
        [SerializeField] private UnityEvent OnGenerate = new UnityEvent();

        public void Generate()
        {
            OnGenerate?.Invoke();

            // Clear current Path and completed cells
            _currentPath.Clear();
            _cells = new MazeCell[0, 0];
            _completedCells = 0;

            // Set cells and maze size
            _cells = gridGenerator.Cells;
            _mazeSize = new Vector2(_cells.GetLength(0), _cells.GetLength(1));

            PickStartAndEnd();

            // Checking if it is already generating and turning it off
            if (_isGenerating) StopCoroutine(_mazeGeneration);
            _mazeGeneration = StartCoroutine(GenerateMaze());
        }

        private IEnumerator GenerateMaze()
        {
            _isGenerating = true;

            while (_completedCells < _cells.Length)
            {
                _possibleNext.Clear();

                // Settings current cell and its position
                MazeCell currentCell = _currentPath[^1];
                Vector2 currentCellPos = new Vector2(currentCell.transform.position.x, currentCell.transform.position.y);

                GetPossibleDirections(currentCellPos);

                if (_possibleNext.Count > 0) GoToNextCell(); // Check if there are any possible cells
                else Backtrack(); // If there are no possible next cells back track to the previous cell

                if (!_instantGeneration) yield return new WaitForSeconds(0.01f);
            }

            _isGenerating = false;
            yield return null;
        }

        private void PickStartAndEnd()
        {
            // Pick random points
            int randomStartPoint = Random.Range(0, (int)_mazeSize.x); // Start Point
            int randomEndPoint = Random.Range(0, (int)_mazeSize.x); // End Point

            // Settings cell as starting point
            _currentPath.Add(_cells[randomStartPoint, 0]);
            _currentPath[0].SetState(CellState.Current);
            _currentPath[0].RemoveWall(Vector2.down);

            // Removing end cell wall
            _cells[randomEndPoint, (int)_mazeSize.y - 1].RemoveWall(Vector2.up);
        }

        private void GetPossibleDirections(Vector2 pos)
        {
            for (int i = 0; i < _directions.Length; i++)
            {
                Vector2 currentDirection = _directions[i];

                // Check if current checking cell is outside maze
                if (pos.x + currentDirection.x >= _mazeSize.x || pos.x + currentDirection.x < 0 ||
                    pos.y + currentDirection.y >= _mazeSize.y || pos.y + currentDirection.y < 0) continue;

                MazeCell currentCheckingCell = _cells[(int)(pos.x + currentDirection.x), (int)(pos.y + currentDirection.y)];

                // Check if current checking cell is already been or being used
                if (currentCheckingCell.GetCellState == CellState.Completed || currentCheckingCell.GetCellState == CellState.Current) continue;

                _possibleNext.Add(currentCheckingCell, currentDirection);
            }
        }

        private void GoToNextCell()
        {
            MazeCell nextCell = null;
            int randomInt = Random.Range(0, _possibleNext.Count);
            int forCount = 0;

            // Looping through dictonary to get a random entry
            foreach (KeyValuePair<MazeCell, Vector2> cell in _possibleNext)
            {
                if (randomInt == forCount)
                {
                    nextCell = cell.Key;
                }
                forCount++;
            }

            // Remove wall between this and next cell
            Vector2 nextDirection = _possibleNext[nextCell];
            nextCell.RemoveWall(-nextDirection);
            _currentPath[^1].RemoveWall(nextDirection);

            // Setting next cell
            _currentPath.Add(nextCell);
            nextCell.SetState(CellState.Current);
        }

        private void Backtrack()
        {
            _completedCells++;

            // Set state of current cell and remove it so it can contine with the next one
            _currentPath[^1].SetState(CellState.Completed);
            _currentPath.RemoveAt(_currentPath.Count - 1);
        }

        // Toggle the instant generation
        public void ToggleInstantGeneration(bool instatnGen)
        {
            _instantGeneration = instatnGen;
        }
    }
}