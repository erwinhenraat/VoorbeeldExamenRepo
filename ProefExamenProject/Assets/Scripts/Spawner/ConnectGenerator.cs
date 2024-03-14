using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace Spawner
{

    public enum RoadTiling // This is the enum definition
    {
        SmallRoadTile = 0,
        BigRoadTile = 1,
        SmallBecomesBig = 2,
        BigBecomesSmall = 3
    }
    
    public class ConnectGenerator : MonoBehaviour
    {

        [Header("Chunk Settings: ")] 
        [SerializeField] private GameObject[] roadPrefabs;
        [SerializeField] private Transform loadInTransform;

        [Header("Chunk Settings: ")]
        [SerializeField] private int maxChunks;
        [SerializeField] private float chunkSpacing;

        public int getChunks;
        private bool _afterFirstGeneration;
        private bool _generateMore;
        private bool _generateOnce;

        // ReSharper disable once CollectionNeverQueried.Local
        private List<GameObject> _spawnedChunks;
        private RoadTiling _currentRoadType = RoadTiling.SmallRoadTile;
        private const RoadTiling PreviousRoadType = RoadTiling.SmallRoadTile;

        private void Start()
        {
            _spawnedChunks = new List<GameObject>();
            GenerateChunks();
        }

        private void Update()
        {
            if (getChunks >= 8 && !_generateOnce)
            {
                _generateOnce = true;
                _afterFirstGeneration = true;
                if (!_generateMore)
                {
                    var chunksToRemove = new List<GameObject>(_spawnedChunks.Take(2));
                    foreach (var chunk in chunksToRemove)
                    {
                        _spawnedChunks.Remove(chunk);
                        Destroy(chunk);
                    }
                }
                else
                {
                    var chunksToRemove = new List<GameObject>(_spawnedChunks.Take(3));
                    foreach (var chunk in chunksToRemove)
                    {
                        _spawnedChunks.Remove(chunk);
                        Destroy(chunk);
                    }
                }
                
                var getLast = _spawnedChunks.Last();
                loadInTransform = getLast.transform.GetChild(0);
                
                getChunks = 0;
                GenerateChunks();
            }
        }

        private void GenerateChunks()
        {
            var currentZPosition = loadInTransform.position.z; // Starting Z position (adjust if needed)

            if (!_afterFirstGeneration)
            {
                _generateOnce = false;
                for (var i = 0; i < maxChunks; i++)
                {
                    // Get the next road prefab based on current road type and restrictions
                    var chunkPrefab = GetNextRoadPrefab();

                    // Calculate spawn position with spacing along Z-axis
                    var spawnPosition = new Vector3(loadInTransform.position.x, 0f, currentZPosition);

                    // Adjust for pivot point offset (assuming offset is on Z-axis)
                    var meshRenderer = chunkPrefab.GetComponentInChildren<MeshRenderer>();
                    if (meshRenderer != null)
                        spawnPosition.z += meshRenderer.bounds.extents.z / 2f; // Adjust based on your pivot offset
                    else
                        Debug.LogWarning("Chunk prefab has no MeshRenderer. Consider using a collider for bounds check.");

                    // Spawn the chunk
                    var spawnedChunk = Instantiate(chunkPrefab, spawnPosition, loadInTransform.rotation, transform);
                    _spawnedChunks.Add(spawnedChunk);

                    // Update Z position for next chunk
                    currentZPosition += chunkSpacing + meshRenderer.bounds.extents.z; // Adjust based on your pivot offset
                }
            }
            else
            {
                _generateOnce = false;
                var value = !_generateMore ? 2 : 3;
                
                for (var i = 0; i < value; i++)
                {
                    // Get the next road prefab based on current road type and restrictions
                    var chunkPrefab = GetNextRoadPrefab();

                    // Calculate spawn position with spacing along Z-axis
                    var spawnPosition = new Vector3(loadInTransform.position.x, 0f, currentZPosition);

                    // Adjust for pivot point offset (assuming offset is on Z-axis)
                    var meshRenderer = chunkPrefab.GetComponentInChildren<MeshRenderer>();
                    
                    if (meshRenderer != null)
                        spawnPosition.z += meshRenderer.bounds.extents.z / 2f; // Adjust based on your pivot offset
                    else
                        Debug.LogWarning("Chunk prefab has no MeshRenderer. Consider using a collider for bounds check.");

                    // Spawn the chunk
                    var spawnedChunk = Instantiate(chunkPrefab, spawnPosition, loadInTransform.rotation, transform);
                    _spawnedChunks.Add(spawnedChunk);

                    // Update Z position for next chunk
                    currentZPosition += chunkSpacing + meshRenderer.bounds.extents.z; // Adjust based on your pivot offset
                    _generateMore = true;
                }
            }
        }

        private GameObject GetNextRoadPrefab()
        {
            var randomValue = Random.Range(0, 100); 
            var canTransitionAgain = true;

            switch (_currentRoadType)
            {
                case RoadTiling.SmallRoadTile:
                    if (randomValue < 70) 
                        return roadPrefabs[(int)_currentRoadType];
                    else
                    {
                        // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                        if (canTransitionAgain && PreviousRoadType != RoadTiling.SmallBecomesBig)
                        {
                            canTransitionAgain = false;
                            _currentRoadType = RoadTiling.SmallBecomesBig;
                            return roadPrefabs[(int)_currentRoadType];
                        }
                        return roadPrefabs[(int)_currentRoadType]; 
                    }
                case RoadTiling.BigRoadTile:
                    if (randomValue < 30) 
                    {
                        // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                        if (canTransitionAgain && PreviousRoadType != RoadTiling.BigBecomesSmall)
                        {
                            canTransitionAgain = false;
                            _currentRoadType = RoadTiling.SmallBecomesBig; 
                            return roadPrefabs[(int)RoadTiling.SmallRoadTile]; 
                        }
                        return roadPrefabs[(int)_currentRoadType]; 
                    }
                    else
                        return roadPrefabs[(int)_currentRoadType]; 
                case RoadTiling.SmallBecomesBig:  
                    if (PreviousRoadType != RoadTiling.SmallBecomesBig) 
                    {
                        _currentRoadType = RoadTiling.SmallRoadTile;
                        canTransitionAgain = true;
                        return roadPrefabs[(int)RoadTiling.BigBecomesSmall];
                    }
                case RoadTiling.BigBecomesSmall:  // Handle BigBecomesSmall case
                    if (PreviousRoadType != RoadTiling.BigBecomesSmall) // Check for repetitive sequence
                    {
                        _currentRoadType = RoadTiling.BigRoadTile;
                        canTransitionAgain = true;
                        return roadPrefabs[(int)RoadTiling.SmallBecomesBig];
                    }
                default:
                    Debug.LogError("Unexpected RoadTiling");
                    return null;
            }
        }
    }
}
