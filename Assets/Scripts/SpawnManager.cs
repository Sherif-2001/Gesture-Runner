using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject[] obstaclesPrefabs;

    float delayTime = 3f;
    float repeatRate = 10f;

    /// <summary>
    /// Start invokes for spawning enemies and powerups
    /// </summary>
    void Start()
    {
        InvokeRepeating(nameof(SpawnObstacle), delayTime, repeatRate);
    }

    /// <summary>
    /// Spawn enemy outside the camera border in a specific height
    /// </summary>
    void SpawnObstacle()
    {
        GameObject randomObstacle = obstaclesPrefabs[Random.Range(0, obstaclesPrefabs.Length)];
        randomObstacle.transform.position = new Vector3(OutsideCameraViewX(), randomObstacle.transform.position.y, randomObstacle.transform.position.z);
        Instantiate(randomObstacle);

    }

    /// <summary>
    /// Get the position outside the camera view point
    /// </summary>
    float OutsideCameraViewX()
    {
        float distanceFromBorder = 4f;
        Camera mainCamera = Camera.main;

        float cameraHeight = mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        // Calculate spawn position only to the right side of the camera
        float spawnX = Random.Range(cameraWidth, cameraWidth + distanceFromBorder);

        return spawnX;
    }
}
