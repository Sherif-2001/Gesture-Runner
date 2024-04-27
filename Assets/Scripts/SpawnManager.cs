using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject[] obstaclesPrefabs;
    [SerializeField] TextMeshProUGUI obstacleText;

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
        StartCoroutine(ShowObstacleWarning(randomObstacle));
    }

    IEnumerator ShowObstacleWarning(GameObject obstacle)
    {
        obstacleText.gameObject.SetActive(true);
        obstacleText.text = $"Incoming....\n{obstacle.name}".ToUpper();
        yield return new WaitForSeconds(5);

        obstacleText.gameObject.SetActive(false);
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
