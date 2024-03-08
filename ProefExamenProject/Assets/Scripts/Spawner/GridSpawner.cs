using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

namespace Spawner
{
    public class GridSpawner : MonoBehaviour
    {

        [Header("Spawner Settings: ")] 
        [SerializeField] private GameObject[] randomObjects;
        [SerializeField] private GameObject boundsObject;
        [SerializeField] private int maxSpawnedIn;

        [Header("Grid Spawner Settings: ")] 
        [SerializeField] private int gridRows;
        [SerializeField] private int gridCols;

        private Queue<GameObject> _spawnedObjects;
        private Vector2[,] _gridPos;
        private HashSet<Vector3> _occupiedPos;

        // Start is called before the first frame update
        private void Start()
        {
            _spawnedObjects = new Queue<GameObject>();
            _occupiedPos = new HashSet<Vector3>();
            
            InitializeGrid();
        }

        private void InitializeGrid()
        {
            var bounds = boundsObject.GetComponent<Renderer>().bounds;

            var cellSizeX = bounds.size.x / gridCols;
            var cellSizeZ = bounds.size.z / gridRows;

            _gridPos = new Vector2[gridRows, gridCols];

            for (var row = 0; row < gridRows; row++)
            {
                for (var col = 0; col < gridCols; col++)
                {
                    var posX = bounds.min.x + col * cellSizeX + cellSizeX / 2;
                    var posZ = bounds.min.z + row * cellSizeZ + cellSizeZ / 2;

                    _gridPos[row, col] = new Vector2(posX, posZ);
                }
            }
        }
        
        // Update is called once per frame
        private void Update()
        {
            if (_spawnedObjects.Count < maxSpawnedIn)
                SpawnInRandomTile();
        }

        private void SpawnInRandomTile()
        {
            var objectToSpawn = randomObjects[Random.Range(0, randomObjects.Length)];
            var spawnTilePos = GetRandomEmptyTilePos();

            // ReSharper disable once InvertIf
            if (spawnTilePos != Vector2.zero)
            {
                var newObject = Instantiate(objectToSpawn, new Vector3(spawnTilePos.x, boundsObject.transform.position.y, spawnTilePos.y), Quaternion.identity);
                _spawnedObjects.Enqueue(newObject);
            }
        }

        private Vector2 GetRandomEmptyTilePos()
        {
            var shuffledPos = _gridPos.Cast<Vector2>().ToList();
            shuffledPos.Shuffle();

            foreach (var pos in shuffledPos.Where(pos => !_occupiedPos.Contains(pos)))
            {
                _occupiedPos.Add(pos);
                return pos;
            }

            return Vector2.zero;
        }
        
    }
}
