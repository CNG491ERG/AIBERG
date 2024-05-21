using AIBERG.Core;
using Codice.Client.BaseCommands.CheckIn.Progress;
using Codice.CM.Common;
using UnityEngine;


namespace AIBERG.ParkourMode{
    public class ObstacleSpawner : MonoBehaviour
    {
        public GameObject obstacle1;
        public GameObject obstacle2;
        public float maxX1;
        public float maxY1;
        public float minX1;
        public float minY1;
        public float spawnInterval1;
        private float spawnTime1;
        public bool canSpawn1 = false;

        public float maxX2;
        public float maxY2;
        public float minX2;
        public float minY2;
        public float spawnInterval2;
        private float spawnTime2;
        public bool canSpawn2 = false;
        public float speed = -15.0f;


        // Update is called once per frame
        void Update()
        {
            if (canSpawn1 && Time.time > spawnTime1) // Check if canSpawn is true
            {
                //Debug.Log(canSpawn);
                Spawn1();
                spawnTime1 = Time.time + spawnInterval1;
            }
            if(canSpawn2 && Time.time > spawnTime2)
            {
                Spawn2();
                spawnTime2 = Time.time + spawnInterval2;
            }
        }
        void Spawn1()
        {
            float randomX = Random.Range(minX1, maxX1);
            float randomY = Random.Range(minY1, maxY1);

            GameObject newObstacle = Instantiate(obstacle1, transform.position + new Vector3(randomX, randomY, 0), transform.rotation);//MIGHT FACE ISSUES ON COLLISION WITH Z AXIS
            Rigidbody2D obstacleRigidbody = newObstacle.GetComponent<Rigidbody2D>();
            obstacleRigidbody.velocity = transform.right * -speed;

        }
        void Spawn2()
        {
            float randomX = Random.Range(minX2, maxX2);
            float randomY = Random.Range(minY2, maxY2);

            GameObject newObstacle = Instantiate(obstacle2, transform.position + new Vector3(randomX, randomY, 0), transform.rotation);//MIGHT FACE ISSUES ON COLLISION WITH Z AXIS
            Rigidbody2D obstacleRigidbody = newObstacle.GetComponent<Rigidbody2D>();
            obstacleRigidbody.velocity = transform.right * -speed;

        }
    }

}
