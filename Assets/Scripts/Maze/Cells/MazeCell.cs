using System.Collections.Generic;
using UnityEngine;

namespace UntitledCube.Maze.Cell
{
    public class MazeCell : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer[] wallRenderers;
        [SerializeField] private SpriteRenderer floorRenderer;

        private Dictionary<Vector2, SpriteRenderer> _walls = new Dictionary<Vector2, SpriteRenderer>();
        private readonly Vector2[] _directions = { Vector2.right, Vector2.left, Vector2.up, Vector2.down };
        private CellState _cellState;

        public CellState GetCellState => _cellState;

        private void Awake()
        {
            // Adding wall renderers to walls dictonary with there direction
            for (int i = 0; i < wallRenderers.Length; i++)
            {
                _walls.Add(_directions[i], wallRenderers[i]);
            }
        }

        public void SetState(CellState state)
        {
            // Setting cell state and updating the floor renderer
            _cellState = state;
            switch (state)
            {
                case CellState.Available:
                    floorRenderer.color = Color.green;
                    break;
                case CellState.Completed:
                    floorRenderer.enabled = false;
                    break;
                case CellState.Current:
                    floorRenderer.color = Color.red;
                    break;
            }
        }

        public void RemoveWall(Vector2 wallToRemove)
        {
            // Remove given wall
            _walls[wallToRemove].enabled = false;
        }

        public void SetWallsActive(bool active)
        {
            // Set walls active
            for (int i = 0; i < wallRenderers.Length; i++)
            {
                wallRenderers[i].enabled = active;
            }

            floorRenderer.enabled = active;
        }
    }
}