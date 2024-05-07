using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;


public class GloveConnection : MonoBehaviour
{
    SerialPort stream;

    [SerializeField] TextMeshProUGUI warningText;

    GloveCallibration gloveCallibration;

    private void Start()
    {
        gloveCallibration = GetComponentInParent<GloveCallibration>();
        stream = gloveCallibration.stream;
    }


    public void OpenStream()
    {
        if (stream.IsOpen)
        {
            StartCoroutine(ShowWarning("Stream Already Open"));
            return;
        };

        try
        {
            stream.Open();
            stream.ReadTimeout = 50;
            StartCoroutine(gloveCallibration.StartCallibration());
        }
        catch (Exception e)
        {
            StartCoroutine(ShowWarning("Serial Port Error: " + e.Message));
        }
    }


    IEnumerator ShowWarning(string message)
    {
        warningText.text = message;
        warningText.gameObject.SetActive(true);

        yield return new WaitForSeconds(3);

        warningText.gameObject.SetActive(false);

    }

    void OnDestroy()
    {
        if (stream != null && stream.IsOpen)
        {
            stream.Close();
        }
    }

}