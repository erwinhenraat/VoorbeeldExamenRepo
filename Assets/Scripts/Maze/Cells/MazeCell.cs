using System.Collections.Generic;
using UnityEngine;

namespace UntitledCube.Maze.Cell
{
    public class MazeCell : MonoBehaviour
    {
        [SerializeField] private GameObject[] _wallObjects;

        private readonly Dictionary<Vector2, GameObject> _walls = new();
        private readonly Vector2[] _directions = { Vector2.right, Vector2.left, Vector2.up, Vector2.down };

        public CellState State { get; set; }

        private void Awake()
        {
            for (int i = 0; i < _wallObjects.Length; i++)
                _walls.Add(_directions[i], _wallObjects[i]);
        }

        public void RemoveWall(Vector2 wallToRemove) => _walls[wallToRemove].SetActive(false);

        public void SetWallsActive(bool active)
        {
            for (int i = 0; i < _wallObjects.Length; i++)
                _wallObjects[i].SetActive(active);
        }
    }
}