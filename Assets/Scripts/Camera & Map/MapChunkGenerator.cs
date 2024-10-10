using UnityEngine;
using System.Collections.Generic;

public class MapChunkGenerator : MonoBehaviour
{
    public GameObject chunkPrefab;
    public Transform player;
    public int chunkWidth = 24;  // Width in tiles
    public int chunkHeight = 10;  // Height in tiles
    public float chunkRemovalDistance = 40f; // Distance to remove chunks
    public int bufferZone = 2; // Number of chunks to generate ahead of the player

    private Vector2 playerCurrentChunk;
    private Dictionary<Vector2, GameObject> generatedChunks = new Dictionary<Vector2, GameObject>();

    void Start()
    {
        playerCurrentChunk = GetChunkCoords(player.position);
        GenerateSurroundingChunks(playerCurrentChunk);
    }

    void Update()
    {
        Vector2 playerChunkPos = GetChunkCoords(player.position);

        if (playerChunkPos != playerCurrentChunk)
        {
            playerCurrentChunk = playerChunkPos;
            GenerateSurroundingChunks(playerCurrentChunk);
            RemoveDistantChunks();
        }
    }

    Vector2 GetChunkCoords(Vector3 position)
    {
        int chunkX = Mathf.FloorToInt(position.x / chunkWidth);
        int chunkY = Mathf.FloorToInt(position.y / chunkHeight);
        return new Vector2(chunkX, chunkY);
    }

    void GenerateChunk(Vector2 chunkCoords)
    {
        if (!generatedChunks.ContainsKey(chunkCoords))
        {
            Vector3 chunkPosition = new Vector3(chunkCoords.x * chunkWidth, chunkCoords.y * chunkHeight, 0);
            GameObject newChunk = Instantiate(chunkPrefab, chunkPosition, Quaternion.identity);
            generatedChunks[chunkCoords] = newChunk;
        }
    }

    void GenerateSurroundingChunks(Vector2 centerChunk)
    {
        for (int x = -bufferZone; x <= bufferZone; x++)
        {
            for (int y = -bufferZone; y <= bufferZone; y++)
            {
                GenerateChunk(centerChunk + new Vector2(x, y));
            }
        }
    }

    void RemoveDistantChunks()
    {
        List<Vector2> chunksToRemove = new List<Vector2>();

        foreach (var chunk in generatedChunks)
        {
            if (Vector3.Distance(player.position, chunk.Value.transform.position) > chunkRemovalDistance)
            {
                chunksToRemove.Add(chunk.Key);
            }
        }

        foreach (var chunkKey in chunksToRemove)
        {
            Destroy(generatedChunks[chunkKey]);
            generatedChunks.Remove(chunkKey);
        }
    }
}
