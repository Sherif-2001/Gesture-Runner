using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundScrolling : MonoBehaviour
{
    public float speedx = 1f;
    RawImage backgroundImage;

    private void Start()
    {
        backgroundImage = GetComponent<RawImage>();
    }

    void Update()
    {
        backgroundImage.uvRect = new Rect(backgroundImage.uvRect.position + new Vector2(speedx, 0) * Time.deltaTime, backgroundImage.uvRect.size);
    }
}
