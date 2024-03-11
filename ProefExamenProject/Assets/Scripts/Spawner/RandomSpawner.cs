using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spawner
{
    public class RandomSpawner : MonoBehaviour
    {

        [Header("Spawner Settings: ")] 
        [SerializeField] private GameObject[] randomObjects;
        [SerializeField] private GameObject boundsObject;
        [SerializeField] private int maxSpawnedIn = 10;
        
        [Header("Time Spawn Settings: ")]
        [SerializeField] private float minSpawnTime = 0.5f;
        [SerializeField] private float maxSpawnTime = 2.5f;

        [Header("Grid Settings: ")] 
        [SerializeField] [Range(0, 500)] private int gridRows;
        [SerializeField] [Range(0, 500)] private int gridCols;

        private Queue<GameObject> _spawnedObjects;
        private HashSet<Vector3> _occupiedPositions;

        // Start is called before the first frame update
        private void Start()
        {
            _spawnedObjects = new Queue<GameObject>();
            _occupiedPositions = new HashSet<Vector3>();
            
            InvokeRepeating(nameof(SpawnIn), Random.Range(minSpawnTime, maxSpawnTime),maxSpawnTime);
        } 

        private void SpawnIn()
        {
            if (boundsObject == null)
            {
                Debug.LogError("Bounds Object isn't set");
                return;
            }

            // Choose a random object to spawn
            var objectToSpawn = randomObjects[Random.Range(0, randomObjects.Length)];
            var bounds = boundsObject.GetComponent<Renderer>().bounds;
            var spawnPos = FindValidSpawnPosition(bounds);
            
            var newObject = Instantiate(objectToSpawn, spawnPos, Quaternion.identity);
            _spawnedObjects.Enqueue(newObject);
            
            // ReSharper disable once InvertIf
            if (_spawnedObjects is { } queue && queue.Count > maxSpawnedIn)
            {
                var oldestObject = _spawnedObjects.Dequeue();
                Destroy(oldestObject);
            }
        }
        
        private Vector3 FindValidSpawnPosition(Bounds bounds)
        {
            var cellSizeX = bounds.size.x / (gridCols - 1);
            var cellSizeZ = bounds.size.z / (gridRows - 1);
            
            var maxAttempts = gridRows * gridCols;
            var attempts = 0;
            Vector3 spawnPos;
            
            do
            {
                var gridX = Random.Range(0, gridCols);
                var gridZ = Random.Range(0, gridRows);
                
                spawnPos = boundsObject.transform.position + new Vector3(gridX * cellSizeX, 0.95f, gridZ * cellSizeZ) - bounds.extents;
                if (!_occupiedPositions.Contains(spawnPos))
                {
                    _occupiedPositions.Add(spawnPos);
                    break;
                }
                
                attempts++;
            } 
            
            while (attempts < maxAttempts);
            return spawnPos;
        }
    }
}
