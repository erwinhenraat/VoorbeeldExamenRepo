using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

        [SerializeField] private int maxChunks;
        [SerializeField] private float chunkSpacing; // Minimum distance between chunks

        private List<GameObject> _spawnedChunks;
        private RoadTiling _currentRoadType = RoadTiling.SmallRoadTile;
        private const RoadTiling _previousRoadType = RoadTiling.SmallRoadTile;

        private void Start()
        {
            _spawnedChunks = new List<GameObject>();
            GenerateChunks();
        }

        private void GenerateChunks()
        {
            var currentZPosition = loadInTransform.position.z; // Starting Z position (adjust if needed)

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

        private GameObject GetNextRoadPrefab()
        {
            var randomValue = Random.Range(0, 100); 
            var canTransitionAgain = true;

            switch (_currentRoadType)
            {
                case RoadTiling.SmallRoadTile:
                    if (randomValue < 70) // 70% chance to stay small road
                        return roadPrefabs[(int)_currentRoadType];
                    else
                    {
                        // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                        if (canTransitionAgain && _previousRoadType != RoadTiling.SmallBecomesBig) // Allow transition only once and not after BigBecomesSmall
                        {
                            canTransitionAgain = false;
                            _currentRoadType = RoadTiling.SmallBecomesBig;
                            return roadPrefabs[(int)_currentRoadType];
                        }
                        return roadPrefabs[(int)_currentRoadType]; // Stay small road if last was transition
                    }
                case RoadTiling.BigRoadTile:
                    if (randomValue < 30) // 30% chance to become small road
                    {
                        // Transition to SmallRoadTile (if allowed)
                        if (canTransitionAgain && _previousRoadType != RoadTiling.BigBecomesSmall)
                        {
                            canTransitionAgain = false;
                            _currentRoadType = RoadTiling.SmallBecomesBig; // This will be handled in the SmallBecomesBig case
                            return roadPrefabs[(int)RoadTiling.SmallRoadTile]; // Return the small road prefab
                        }
                        return roadPrefabs[(int)_currentRoadType]; // Stay big road if last was transition
                    }
                    else
                        return roadPrefabs[(int)_currentRoadType]; // Use big road prefab
                case RoadTiling.SmallBecomesBig:  // Handle SmallBecomesBig case
                    if (_previousRoadType != RoadTiling.SmallBecomesBig) // Check for repetitive sequence
                    {
                        _currentRoadType = RoadTiling.SmallRoadTile;
                        canTransitionAgain = true;
                        return roadPrefabs[(int)RoadTiling.BigBecomesSmall];
                    }
                case RoadTiling.BigBecomesSmall:  // Handle BigBecomesSmall case
                    if (_previousRoadType != RoadTiling.BigBecomesSmall) // Check for repetitive sequence
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
