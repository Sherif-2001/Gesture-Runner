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
    SerialPort stream = new SerialPort("COM6", 9600);

    [SerializeField] TextMeshProUGUI warningText;

    GloveCallibration gloveCallibration;

    //public string strReceived;
    //private string[] strData = new string[5];

    //private int[] sensorData = new int[5]; // Array to hold sensor data vectors

    private void Start()
    {
        gloveCallibration = GetComponentInParent<GloveCallibration>();
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

            StartCoroutine(gloveCallibration.StartCallibration());
        }
        catch
        {
            StartCoroutine(ShowWarning("Serial Port Not Connected"));
        }
    }


    IEnumerator ShowWarning(string message)
    {
        warningText.text = message;
        warningText.gameObject.SetActive(true);

        yield return new WaitForSeconds(3);

        warningText.gameObject.SetActive(false);

    }

}