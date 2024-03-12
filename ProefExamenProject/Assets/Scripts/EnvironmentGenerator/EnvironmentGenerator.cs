using UnityEngine;

namespace EnvironmentGenerator
{
    public class EnvironmentGenerator : MonoBehaviour
    {

        [Header("Generator Settings: ")] 
        [SerializeField] private GameObject[] environmentTiles;
        [SerializeField] private Transform[] spawnPoints;
        
        // Start is called before the first frame update
        private void Start() => Generate();

        private void Generate()
        {
            foreach (var t in spawnPoints)
                Instantiate(environmentTiles[Random.Range(0, environmentTiles.Length)], t.position, Quaternion.Euler(90,0,0), transform);
        }
    }
}
