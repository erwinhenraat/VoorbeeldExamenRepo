using System.Collections.Generic;
using UnityEngine;
using UntitledCube.Timer;

namespace UntitledCube.Maze.Cell
{
    public class MazeCell : MonoBehaviour
    {
        [Header("Structure")]
        [SerializeField] private GameObject[] _wallObjects;
        [SerializeField] private MeshRenderer _floorRenderer;

        [Header("Coin")]
        [Range(0,100)]
        [SerializeField] private int _coinSpawnchance;
        [SerializeField] private Transform _coinSpawnpoint;
        [SerializeField] private GameObject _coinPrefab;

        [Header("Materials")]
        [SerializeField] private Material _startMaterial;
        [SerializeField] private Material _endMaterial;

        [Header("Collider")]
        [SerializeField] private BoxCollider _boxCollider;

        private readonly Dictionary<Vector2, GameObject> _walls = new();
        private readonly Vector2[] _directions = { Vector2.right, Vector2.left, Vector2.up, Vector2.down };

        private bool _isStart;
        private bool _isEnd;

        public CellState State { get; set; }

        public Vector2 Position { get; set; }

        public bool IsStart
        {
            set
            {
                _isStart = value;
                SetWallsActive(false);
                _floorRenderer.gameObject.SetActive(value);
                _floorRenderer.material = _startMaterial;
            }
            get => _isStart; 
        }

        public bool IsEnd
        {
            set
            {
                _isEnd = value;
                SetWallsActive(false);
                _floorRenderer.gameObject.SetActive(value);
                _floorRenderer.material = _endMaterial;
                _boxCollider.enabled = value;
            }
            get => _isEnd;
        }

        private void Awake()
        {
            for (int i = 0; i < _wallObjects.Length; i++)
                _walls.Add(_directions[i], _wallObjects[i]);

            SpawnCoin();
        }

        private void OnTriggerEnter(Collider other) => Stopwatch.Instance.Stop();

        private void SpawnCoin()
        {
            int chance = Mathf.Clamp(_coinSpawnchance, 0, 100);
            float randomValue = Random.Range(0.0f, 100.0f);

            if (randomValue <= chance)
                Instantiate(_coinPrefab, _coinSpawnpoint);
        }

        /// <summary>
        /// Deactivates a specific wall object.
        /// </summary>
        /// <param name="wallToRemove">The Vector2 representing the wall's coordinates or identifier.</param>
        public void RemoveWall(Vector2 wallToRemove) => _walls[wallToRemove].SetActive(false);

        /// <summary>
        /// Sets the active state of all wall objects in the game.
        /// </summary>
        /// <param name="active">True to activate walls, false to deactivate them.</param>
        public void SetWallsActive(bool active)
        {
            for (int i = 0; i < _wallObjects.Length; i++)
                _wallObjects[i].SetActive(active);
        }

        /// <summary>
        /// Resets the game state, reactivating walls and hiding the floor.
        /// </summary>
        public void Reset()
        {
            IsEnd = false;
            IsStart = false;
            SetWallsActive(true);
            _floorRenderer.gameObject.SetActive(false);
        }
    }
}