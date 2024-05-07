using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timeText;
    float time = 0;

    [SerializeField] TextMeshProUGUI scoreText;
    int score = 0;


    /// <summary>
    /// Update the score as time passes
    /// </summary>
    void Update()
    {
        time += Time.deltaTime;
        DisplayTime(time);
    }

    void DisplayTime(float timeInSeconds)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(timeInSeconds);

        // Format the TimeSpan as "mm:ss"
        string formattedTime = string.Format("{0:00}:{1:00}", timeSpan.Minutes, timeSpan.Seconds);

        timeText.text = "Time\n" + formattedTime;

    }

    public void IncreaseScore()
    {
        score += 5;
        scoreText.text = "Score\n" + score;
    }

}
