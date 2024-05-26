using System.Collections.Generic;
using UnityEngine;

namespace AIBERG
{
    public class CollectibleSpawner : MonoBehaviour
    {
        public List<GameObject> healthCollectibles;
        public GameObject cooldownCollectible;
        public List<GameObject> scoreCollectibles;
        public float healthSpawnInterval = 30f;
        public float cooldownSpawnInterval = 45f;
        public float scoreSpawnInterval = 3f;
        public float minY = -3f;
        public float maxY = 3f;
        public float healthSpawnTimer;
        public float cooldownSpawnTimer;
        public float scoreSpawnTimer;

        private void Start()
        {
            healthSpawnTimer = healthSpawnInterval;
            cooldownSpawnTimer = cooldownSpawnInterval;
            scoreSpawnTimer = scoreSpawnInterval;
        }
        void Update()
        {
            healthSpawnTimer -= Time.deltaTime;
            cooldownSpawnTimer -= Time.deltaTime;
            scoreSpawnTimer -= Time.deltaTime;
            if (healthSpawnTimer <= 0)
            {
                SpawnHealthCollectible();
                healthSpawnTimer = healthSpawnInterval;
            }
            if (cooldownSpawnTimer <= 0)
            {
                SpawnCooldownCollectible();
                cooldownSpawnTimer = cooldownSpawnInterval;
            }
            if (scoreSpawnTimer <= 0)
            {
                SpawnScoreCollectible();
                scoreSpawnTimer = scoreSpawnInterval;
            }
        }

        void SpawnHealthCollectible()
        {
            GameObject collectibleToSpawn = GetRandomCollectible(healthCollectibles);
            if (collectibleToSpawn != null)
            {
                Vector3 spawnPosition = new Vector3(15f, Random.Range(minY, maxY), transform.position.z);
                GameObject spawnedCollectible = Instantiate(collectibleToSpawn, this.transform.parent);
                spawnedCollectible.transform.localPosition = spawnPosition;
            }
        }
        void SpawnScoreCollectible()
        {
            GameObject collectibleToSpawn = GetRandomCollectible(scoreCollectibles);
            if (collectibleToSpawn != null)
            {
                Vector3 spawnPosition = new Vector3(15f, Random.Range(minY, maxY), transform.position.z);
                GameObject spawnedCollectible = Instantiate(collectibleToSpawn, this.transform.parent);
                spawnedCollectible.transform.localPosition = spawnPosition;
            }
        }

        void SpawnCooldownCollectible()
        {
            Vector3 spawnPosition = new Vector3(15f, Random.Range(minY, maxY), transform.position.z);
            GameObject spawnedCollectible = Instantiate(cooldownCollectible, this.transform.parent);
            spawnedCollectible.transform.localPosition = spawnPosition;
        }

        GameObject GetRandomCollectible(List<GameObject> collectibleChoices)
        {
            float totalRarity = 0;
            foreach (var collectible in collectibleChoices)
            {
                totalRarity += collectible.GetComponent<Collectible>().rarity;
            }

            float randomValue = Random.Range(0, totalRarity);
            float cumulativeRarity = 0;
            foreach (var collectible in collectibleChoices)
            {
                cumulativeRarity += collectible.GetComponent<Collectible>().rarity;
                if (randomValue < cumulativeRarity)
                {
                    return collectible;
                }
            }
            return null;
        }
    }
}
