using UnityEngine;


namespace AIBERG.ParkourMode{
public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstacle;
    public float maxX;
    public float maxY;
    public float minX;
    public float minY;
    public float spawnInterval;
    private float spawnTime;
    public bool canSpawn = false;


        // Update is called once per frame
    void Update()
    {
        if (canSpawn && Time.time > spawnTime) // Check if canSpawn is true
        {
            //Debug.Log(canSpawn);
            Spawn();
            spawnTime = Time.time + spawnInterval;
        }
    }
    void Spawn()
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        Instantiate(obstacle, transform.position + new Vector3(randomX, randomY, 0), transform.rotation);//MIGHT FACE ISSUES ON COLLISION WITH Z AXIS
    }
}

}
