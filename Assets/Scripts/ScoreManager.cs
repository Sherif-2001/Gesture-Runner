using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timeText;
    float time = 0;


    /// <summary>
    /// Update the score as time passes
    /// </summary>
    void Update()
    {
        time += Time.deltaTime;
        timeText.text = "Score\n" + Mathf.Round(time);
    }

}
