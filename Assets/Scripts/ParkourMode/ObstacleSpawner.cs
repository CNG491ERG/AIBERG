using AIBERG.Core;
using UnityEngine;

namespace AIBERG.ParkourMode
{
    public class ObstacleSpawner : MonoBehaviour
    {
        public GameEnvironment environment;
        public GameObject[] obstaclePrefabs;
        public ParallaxController parallaxController;
        public float spawnYMin;
        public float spawnYMax;
        public float spawnX;
        public float spawnInterval = 0.5f; // Decrease the interval for more frequent spawning
        public int minObstaclesPerSpawn = 1; // Minimum number of obstacles to spawn at once
        public int maxObstaclesPerSpawn = 3; // Maximum number of obstacles to spawn at once
        public float obstacleMovementSpeedX = 10f;
        public float spawnRadiusCheck = 1f; // Radius to check for nearby obstacles
        private float timer;
        private bool canSpawnObstacles = false;

        void Start()
        {
            timer = spawnInterval;
            environment = Utilities.ComponentFinder.FindComponentInParents<GameEnvironment>(this.transform);
        }

        void Update()
        {
            if (canSpawnObstacles)
            {
                timer -= Time.deltaTime;
                if (timer <= 0f)
                {
                    SpawnObstacles();
                    timer = Random.Range(spawnInterval, spawnInterval+0.5f);
                }
            }
        }

        void SpawnObstacles()
        {
            int obstaclesToSpawn = Random.Range(minObstaclesPerSpawn, maxObstaclesPerSpawn + 1);

            for (int i = 0; i < obstaclesToSpawn; i++)
            {
                Vector2 spawnPosition;
                int attempts = 0;
                bool positionFound = false;

                do
                {
                    // Generate a random Y position within the confined area
                    float y = Random.Range(spawnYMin, spawnYMax);
                    spawnPosition = new Vector2(spawnX, y);

                    // Check for nearby obstacles
                    Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnPosition, spawnRadiusCheck);
                    if (colliders.Length == 0)
                    {
                        positionFound = true;
                    }

                    attempts++;
                } while (!positionFound && attempts < 10);

                if (positionFound)
                {
                    // Randomly select an obstacle prefab
                    int randomIndex = Random.Range(0, obstaclePrefabs.Length);
                    GameObject selectedObstaclePrefab = obstaclePrefabs[randomIndex];

                    GameObject newObstacle = Instantiate(selectedObstaclePrefab, spawnPosition, Quaternion.identity);
                    newObstacle.GetComponent<Obstacle>().parallaxController = parallaxController;
                    environment.AddObjectToEnvironmentList(newObstacle);
                }
                else
                {
                    Debug.LogWarning("Failed to find a suitable position to spawn an obstacle.");
                }
            }
        }

        public void StartSpawningObstacles()
        {
            canSpawnObstacles = true;
        }

        public void StopSpawningObstacles()
        {
            canSpawnObstacles = false;
        }
    }
}
