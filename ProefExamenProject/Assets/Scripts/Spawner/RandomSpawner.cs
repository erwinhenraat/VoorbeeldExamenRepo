using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

namespace Spawner
{
    public class RandomSpawner : MonoBehaviour
    {

        [Header("Spawner Settings: ")] 
        [SerializeField] private GameObject[] randomObjects;
        [SerializeField] private Transform randomSpawn;

        private const float MinSpawnTime = 0.5f;
        private const float MaxSpawnTime = 5.5f;
        private int _maxSpawnedIn = 10;

        private Queue<GameObject> _spawnedObjects;

        // Start is called before the first frame update
        private void Start() => InvokeRepeating(nameof(SpawnIn), Random.Range(MinSpawnTime, MaxSpawnTime),MaxSpawnTime);

        private void Update()
        {
            // ReSharper disable once InvertIf
            if (_spawnedObjects.Count > _maxSpawnedIn)
            {
                var oldestObject = _spawnedObjects.Dequeue();
                Destroy(oldestObject);
            }
        }
        
        private void SpawnIn()
        {
            var objectToSpawn = randomObjects[Random.Range(0, randomObjects.Length)];
            var randomPoint = Random.insideUnitCircle * 2.5f;
            var spawnPos = randomSpawn.position + new Vector3(randomPoint.x, 0f, randomPoint.y);
            var rotation = randomSpawn.rotation;
            
            var newObject = Instantiate(objectToSpawn, spawnPos, Quaternion.Euler(rotation.x, rotation.y, rotation.z));
            _spawnedObjects.Enqueue(newObject);
        }
    }
}
