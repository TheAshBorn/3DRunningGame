using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningTiles : MonoBehaviour
{
    public GameObject tileToSpawn;
    public GameObject referenceObject;
    public float timeOffset = 0.4f;
    public float distanceBetweenTiles = 5.0f;
    public float randomValue = 0.8f;
    private Vector3 previousTilePosition;
    private float startTime;
    private Vector3 direction, mainDirection = new Vector3(0, 0, 1), otherDirection = new Vector3(1, 0, 0);

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        previousTilePosition = referenceObject.transform.position;
        startTime = Time.time;
        player = GameObject.FindWithTag("Player"); // Assuming the player has a tag "Player"
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - startTime > timeOffset)
        {
            if (Random.value < randomValue)
                direction = mainDirection;
            else
            {
                Vector3 temp = direction;
                direction = otherDirection;
                mainDirection = direction;
                otherDirection = temp;
            }
            Vector3 spawnPos = previousTilePosition + distanceBetweenTiles * direction;
            startTime = Time.time;
            Instantiate(tileToSpawn, spawnPos, Quaternion.Euler(0, 0, 0));
            previousTilePosition = spawnPos;

            // Check and destroy tiles behind the player
            CheckAndDestroyTiles();
        }
    }

    void CheckAndDestroyTiles()
    {
        if (player != null)
        {
            GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile"); // Assuming the tiles have a tag "Tile"
            foreach (var tile in tiles)
            {
                if (tile.transform.position.z < player.transform.position.z - distanceBetweenTiles)
                {
                    Destroy(tile);
                }
            }
        }
    }
}
