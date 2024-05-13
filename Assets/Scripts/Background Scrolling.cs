using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundScrolling : MonoBehaviour
{
    public float speedx = 0.3f;
    float repeatRate = 5f;
    RawImage backgroundImage;

    private void Start()
    {
        InvokeRepeating(nameof(SpeedUpScrolling), 5f, repeatRate);
        backgroundImage = GetComponent<RawImage>();
    }

    void Update()
    {
        backgroundImage.uvRect = new Rect(backgroundImage.uvRect.position + new Vector2(speedx, 0) * Time.deltaTime, backgroundImage.uvRect.size);
    }

    void SpeedUpScrolling()
    {
        if (speedx > 0.4f)
        {
            speedx += 0.005f;
        }
        else
        {
            speedx *= 1.05f;
        }
        //print($"Background Speed: {speedx}");
    }
}
