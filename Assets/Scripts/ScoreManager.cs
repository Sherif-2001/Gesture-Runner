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

    [SerializeField] TextMeshProUGUI yourScoreText;
    [SerializeField] TextMeshProUGUI yourTimeText;

    [SerializeField] GameObject GameOverPanel;


    /// <summary>
    /// Update the score as time passes
    /// </summary>
    void Update()
    {
        time += Time.deltaTime;
        timeText.text = DisplayTime(time);
    }

    string DisplayTime(float timeInSeconds)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(timeInSeconds);

        // Format the TimeSpan as "mm:ss"
        string formattedTime = string.Format("{0:00}:{1:00}", timeSpan.Minutes, timeSpan.Seconds);

        return "Time\n" + formattedTime;

    }

    public void IncreaseScore()
    {
        score += 1;
        scoreText.text = "Score\n" + score;
    }

    /// <summary>
    /// Save player score and max score
    /// </summary>
    public void SavePlayerScore()
    {
        GameOverPanel.SetActive(true);
        Time.timeScale = 0;
        yourScoreText.text = "Score\n" + score;
        yourTimeText.text = DisplayTime(time);
    }
}
