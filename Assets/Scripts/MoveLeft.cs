using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    [SerializeField] float speed;

    // Update is called once per frame
    void Update()
    {
        transform.position += speed * Time.deltaTime * Vector3.left;

        if (transform.position.x < -OutsideCameraViewX())
        {
            Destroy(gameObject);
        }
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
