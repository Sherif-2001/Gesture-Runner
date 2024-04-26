using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    float score = 0;


    /// <summary>
    /// Update the score as time passes
    /// </summary>
    void Update()
    {
        score += Time.deltaTime * 60;
        scoreText.text = "Score\n" + Mathf.Round(score);
    }

}
