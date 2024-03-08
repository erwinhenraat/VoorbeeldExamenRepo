using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

namespace Spawner
{

    public enum RoadTiles
    {
        Straight = 0,
        Right = 1,
        Left = 2
    }
    
    public class GridsSpawner : MonoBehaviour
    {
        [Header("Grid Spawner Settings: ")] 
        [SerializeField] private int horGrid;
        [SerializeField] private int verGrid;
        
        [Header("Lists of Tiles: ")]
        [SerializeField] private List<GameObject> decorTiles;
        [SerializeField] private List<GameObject> streetTiles;
        
        private int _currentTile;
        private GameObject[,] _gridTiles;
        private bool[] _roadInRow;
        
        private void Start()
        {
            _roadInRow = new bool[verGrid];
            for (var i = 0; i < verGrid; i++)
                _roadInRow[i] = false; 
            
            MakeGrid();
        } 

        private void MakeGrid()
        {
            if (horGrid <= 0 || verGrid <= 0)
            {
                Debug.LogError("Grid dimensions are invalid!");
                return;
            }

            _gridTiles = new GameObject[horGrid, verGrid];

            // Loop through the grid dimensions and instantiate tiles
            for (var i = 0; i < horGrid; i++)
            {
                for (var j = 0; j < verGrid; j++)
                {
                    // Calculate the position for the current tile
                    var tilePosition = new Vector3(i, 0f, j);
                    var tileToSpawn = GetTileForPos(i, j);

                    // Instantiate the tile at the calculated position
                    var spawnedTile = Instantiate(tileToSpawn, tilePosition, Quaternion.identity, transform);
                    _gridTiles[i, j] = spawnedTile;
                }
            }
        }

        private GameObject GetTileForPos(int x, int y)
        {
            // Ensure _gridTiles is not null
            if (_gridTiles == null || _gridTiles.GetLength(0) != horGrid || _gridTiles.GetLength(1) != verGrid)
            {
                Debug.LogError("_gridTiles is not properly initialized!");
                return null;
            }
            
            if (_roadInRow[y])
            {
                // Determine direction of the road (left, right, straight)
                var deviation = Random.Range(-1, 2);
                var newX = Mathf.Clamp(x + deviation, 0, horGrid - 1);

                // Ensure the new position is within the grid bounds and not occupied by another road tile
                while (newX == x || (_gridTiles[newX, y] != null && _gridTiles[newX, y].CompareTag("StreetTile")))
                {
                    deviation = Random.Range(-1, 2);
                    newX = Mathf.Clamp(x + deviation, 0, horGrid - 1);
                }

                // Mark the new position as a road tile in the row
                _roadInRow[y] = true;

                // Return the tile at the new position
                return streetTiles[0];
            }

            // Check if this is the designated column for a road tile
            // ReSharper disable once InvertIf
            if (x == 2)
            {
                // Mark the current row as containing a road tile
                _roadInRow[y] = true;

                // Return the road tile for this column
                return streetTiles[0];
            }
            
            return GetRandomTile(decorTiles);
        }

        private static GameObject GetRandomTile(IReadOnlyList<GameObject> tileList)
        {
            if (tileList.Count == 0)
            {
                Debug.LogError("The Tile List is Empty");
                return null;
            }

            var randomIndex = Random.Range(0, tileList.Count);
            return tileList[randomIndex];
        }
    }
}
