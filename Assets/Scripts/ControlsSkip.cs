using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlsSkip : MonoBehaviour
{

    SerialPort stream = new SerialPort("COM6", 9600);

    public string strReceived;
    private string[] strData = new string[5];

    private int[] sensorData = new int[5]; // Array to hold sensor data 

    private double[] sensors_difference = new double[5]; // Array to hold sensor data vectors
    private bool[] isFingersClose = new bool[5]; // Array to hold sensor data vectors

    private double[] openAverage = new double[5];

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            stream.Open();
        }
        catch
        {
            print("Stream Not Opened");
        }
        for (int i = 0; i < 5; i++)
        {
            string key = $"openAvg{i}";
            openAverage[i] = PlayerPrefs.GetFloat(key);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (stream.IsOpen)
        {
            try
            {
                GetStreamData();
                CheckHandGesture();
            }
            catch (Exception e)
            {
                Debug.Log($"We have a problem: {e.Message}");
            }
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(3);
        }
    }

    void GetStreamData()
    {
        strReceived = stream.ReadLine();
        strData = strReceived.Split(',');


        if (strData[0] != "" && strData[1] != "" && strData[2] != "" && strData[3] != "" && strData[4] != "")
        {
            Debug.Log($"{strData[0]}, {strData[1]} {strData[2]} {strData[3]} {strData[4]}");
        }
        for (int i = 0; i < 5; i++)
        {
            sensorData[i] = int.Parse(strData[i]);
        }


        for (int i = 0; i < 5; i++)
        {
            // Claculate abosulte difference between incoming and min
            sensors_difference[i] = Math.Abs(openAverage[i] - sensorData[i]);
            isFingersClose[i] = sensors_difference[i] > 10;
        }
        //Debug.Log($" Difference  {sensors_difference[0]}, {sensors_difference[1]} {sensors_difference[2]} {sensors_difference[3]} {sensors_difference[4]}");
        Debug.Log($" Difference  {Math.Round(sensors_difference[0])}, {Math.Round(sensors_difference[1])}, {Math.Round(sensors_difference[2])}, {Math.Round(sensors_difference[3])}, {Math.Round(sensors_difference[4])}");

    }

    void CheckHandGesture()
    {
        if (!isFingersClose[0] && isFingersClose[1] && isFingersClose[2] && isFingersClose[3] && isFingersClose[4])
        {
            print("Done");
            SceneManager.LoadScene(3);
        }

    }
}
