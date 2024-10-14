using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public byte numberOfRows = 4;
    public byte numberOfColumns = 4;
    private Vector3 arenaSize;
    public Transform arena;

    // Start is called before the first frame update
    void Start()
    {
        arenaSize = new Vector3(arena.localScale.x *5f, arena.localScale.y, arena.localScale.z * 5f);
        Debug.Log(arenaSize);
        SpawnObstacles();
        //SpawnTest();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnObstacles()
    {
        float arenaWidth = arenaSize.x;
        float arenaLength = -arenaSize.z;
        float cellWidth = arenaWidth / 4f;   // Divide arena width into 4 columns
        float cellLength = arenaLength / 4f; // Divide arena length into 3 rows

        
        Vector3 arenaPosition = arena.position;  // The world position of the arena

        // Loop through rows and columns to spawn cubes
        for (int row = 0; row < 3; row++)      // 3 rows
        {
            for (int col = 0; col < 4; col++)  // 4 columns
            {
                // Calculate the X and Z position for each cube
                float xPosition = arenaPosition.x - (arenaWidth / 2f) + (col * cellWidth) + (cellWidth / 2f);
                float zPosition = arenaPosition.z - (arenaLength / 2f) + (row * cellLength) + (cellLength / 2f);
                
                // Set the position for the cube (Y is set to 0 assuming the arena is at y = 0)
                Vector3 cubePosition = new Vector3(xPosition, arenaPosition.y, zPosition);
                Quaternion rotation = Quaternion.Euler(0, 0, 0);
                // Spawn the cube
                //Instantiate(obstaclePrefab, cubePosition, Quaternion.identity);
                Instantiate(obstaclePrefab, cubePosition, rotation, arena);
            }
        
        }
    }

    private void SpawnTest() {
        Vector3 position = new Vector3(arenaSize.x * 5f, 0.5f, arenaSize.z * 5f);
        Quaternion rotation = Quaternion.Euler(0, 90, 0);
        Instantiate(obstaclePrefab, position, rotation);
    }
}
