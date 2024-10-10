using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;     // Reference to the enemy prefab
    public GameObject minibossPrefab;   // Reference to the miniboss prefab
    public Transform player;            // Reference to the player
    public float spawnRadius = 20f;     // Distance around the player where enemies spawn
    public float spawnInterval = 5f;     // Time between spawning regular enemies
    private float nextSpawnTime;         // Time for the next regular enemy spawn

    private float minibossSpawnTime = 60f; // Time interval to spawn miniboss (1 minute)
    private float nextMinibossSpawnTime;   // Timer for miniboss spawn

    void Start()
    {
        nextMinibossSpawnTime = Time.time + minibossSpawnTime; // Initialize miniboss spawn timer
    }

    void Update()
    {
        // Check if it's time to spawn a new regular enemy
        if (Time.time > nextSpawnTime)
        {
            Vector3 spawnPosition = GetSpawnPosition();
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity); // Spawn the enemy
            nextSpawnTime = Time.time + spawnInterval; // Set next spawn time
        }

        // Check if it's time to spawn a miniboss
        if (Time.time > nextMinibossSpawnTime)
        {
            Vector3 spawnPosition = GetSpawnPosition();
            Instantiate(minibossPrefab, spawnPosition, Quaternion.identity); // Spawn the miniboss
            nextMinibossSpawnTime = Time.time + minibossSpawnTime; // Set next miniboss spawn time
            Debug.Log("Miniboss spawned!"); // Log miniboss spawn
        }
    }

    // Get a random spawn position outside the player's view
    Vector3 GetSpawnPosition()
    {
        Vector3 randomDirection = Random.insideUnitCircle.normalized * spawnRadius;
        Vector3 spawnPosition = player.position + randomDirection;

        // Ensure spawn position is always outside the player's immediate vision
        if (Vector3.Distance(spawnPosition, player.position) < spawnRadius)
        {
            spawnPosition += randomDirection * spawnRadius;
        }

        return spawnPosition;
    }
}
