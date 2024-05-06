using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GloveCallibration : MonoBehaviour
{

    SerialPort stream = new SerialPort("COM6", 9600);

    public string strReceived;
    private string[] strData = new string[5];

    private int[] sensorData = new int[5]; // Array to hold sensor data vectors
    // LinkedList<int> myLinkedListSensor1 = new LinkedList<int>();
    // LinkedList<int> myLinkedListSensor2 = new LinkedList<int>();
    // LinkedList<int> myLinkedListSensor3 = new LinkedList<int>();
    // LinkedList<int> myLinkedListSensor4 = new LinkedList<int>();
    // LinkedList<int> myLinkedListSensor5 = new LinkedList<int>();

    // Create an array of LinkedLists
    LinkedList<int>[] sensorsMin = new LinkedList<int>[5];
    LinkedList<int>[] sensorsMax = new LinkedList<int>[5];

    private double[] minAverage = new double[5]; // Array to hold sensor data vectors
    private double[] maxAverage = new double[5]; // Array to hold sensor data vectors
    private double[] minMode = new double[5]; // Array to hold sensor data vectors
    private double[] maxMode = new double[5]; // Array to hold sensor data vectors
    private double[] sensors_difference = new double[5]; // Array to hold sensor data vectors
    private double[] sensors_mapping = new double[5]; // Array to hold sensor data vectors


    private bool isCalibratingOpen = false;
    private bool isCalibratingClose = false;
    private bool isMappinng = false;

    [SerializeField] TextMeshProUGUI callibrationText;
    [SerializeField] GameObject openHandImage;
    [SerializeField] GameObject closedHandImage;
    [SerializeField] GameObject callibrationPanel;


    void Start()
    {
        // Initialize each element of the array with a new LinkedList
        for (int i = 0; i < sensorsMin.Length; i++)
        {
            sensorsMin[i] = new LinkedList<int>();
            sensorsMax[i] = new LinkedList<int>();
        }

    }

    //public Transform hand, index, middle, ring, pinky, thumb;



    void Update()
    {
        if (stream.IsOpen)
        {
            try
            {
                strReceived = stream.ReadLine();
                strData = strReceived.Split(',');

                if (!isMappinng)
                {
                    if (strData[0] != "" && strData[1] != "" && strData[2] != "" && strData[3] != "" && strData[4] != "")
                    {
                        Debug.Log($"{strData[0]}, {strData[1]} {strData[2]} {strData[3]} {strData[4]}");
                    }
                }

                for (int i = 0; i < 5; i++)
                {
                    sensorData[i] = int.Parse(strData[i]);
                }
                if (isCalibratingOpen)
                {
                    // store min values
                    for (int i = 0; i < 5; i++)
                    {
                        sensorsMin[i].AddLast(sensorData[i]);
                    }
                    // myLinkedListSensor1.AddLast(sensorData[0]);
                    // myLinkedListSensor2.AddLast(sensorData[1]);
                    // myLinkedListSensor3.AddLast(sensorData[2]);
                    // myLinkedListSensor4.AddLast(sensorData[3]);
                    // myLinkedListSensor5.AddLast(sensorData[4]);
                }
                if (isCalibratingClose)
                {
                    // store min values
                    for (int i = 0; i < 5; i++)
                    {
                        sensorsMax[i].AddLast(sensorData[i]);
                    }
                }

                if (isMappinng)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        // Claculate abosulte difference between incoming and min
                        sensors_difference[i] = Math.Abs(minAverage[i] - sensorData[i]);

                        // Map the input numbers between 0 and 180
                        sensors_mapping[i] = MapToRange(sensorData[i], Math.Min(minAverage[i], maxAverage[i]), Math.Max(minAverage[i], maxAverage[i]), 0, 180);

                        // sensorsMax[i].AddLast(sensorData[i]);
                    }
                }
                Debug.Log($" Difference  {sensors_difference[0]}, {sensors_difference[1]} {sensors_difference[2]} {sensors_difference[3]} {sensors_difference[4]}");
                Debug.Log($" Mapping  {sensors_mapping[0]}, {sensors_mapping[1]} {sensors_mapping[2]} {sensors_mapping[3]} {sensors_mapping[4]}");



            }
            catch (Exception)
            {
                Debug.Log("We have a problem");
            }
        }
    }


    static double MapToRange(double value, double originalMin, double originalMax, double newMin, double newMax)
    {
        if (value < originalMin)
        {
            return newMin;
        }
        else if (value > originalMax)
        {
            return newMax;
        }
        else
        {
            return ((value - originalMin) / (originalMax - originalMin)) * (newMax - newMin) + newMin;
        }
    }


    public IEnumerator StartCallibration()
    {
        callibrationPanel.SetActive(true);
        callibrationText.SetText("Open your hand");

        yield return new WaitForSeconds(3);
        isCalibratingOpen = true;


        yield return new WaitForSeconds(5);
        isCalibratingOpen = false;
        // Claculate Open Parameters

        Debug.Log($"Count 1: {sensorsMin[0].Count}");
        // Calculate and print the average
        for (int i = 0; i < 5; i++)
        {
            minAverage[i] = CalculateAverage(sensorsMin[i]);
            // Create a dynamic key name using string interpolation
            string key = $"minAvg{i}";
            PlayerPrefs.SetFloat(key, (float)minAverage[0]);

            Debug.Log($"Minimum Average {i}: {minAverage[i]}");
        }

        // Calculate and print the Mode
        for (int i = 0; i < 5; i++)
        {
            minMode[i] = FindMode(sensorsMin[i]);
            // Create a dynamic key name using string interpolation
            string key = $"minMode{i}";
            PlayerPrefs.SetFloat(key, (float)minMode[0]);

            Debug.Log($"Minimum Mode {i}: {minMode[i]}");
        }
        PlayerPrefs.Save();

        callibrationText.SetText("Close your hand");
        openHandImage.SetActive(false);
        closedHandImage.SetActive(true);

        yield return new WaitForSeconds(3);
        isCalibratingClose = true;


        yield return new WaitForSeconds(5);
        isCalibratingClose = false;
        // Claculate Close Parameters

        Debug.Log($"Count 1: {sensorsMax[0].Count}");
        // Calculate and print the average
        for (int i = 0; i < 5; i++)
        {
            maxAverage[i] = CalculateAverage(sensorsMax[i]);
            // Create a dynamic key name using string interpolation
            string key = $"maxAvg{i}";
            PlayerPrefs.SetFloat(key, (float)maxAverage[0]);

            Debug.Log($"Maximum Average {i}: {maxAverage[i]}");
        }
        // Calculate and print the Mode
        for (int i = 0; i < 5; i++)
        {
            maxMode[i] = FindMode(sensorsMax[i]);
            // Create a dynamic key name using string interpolation
            string key = $"maxMode{i}";
            PlayerPrefs.SetFloat(key, (float)maxMode[0]);

            Debug.Log($"Maximum Mode {i}: {maxMode[i]}");
        }
        PlayerPrefs.Save();


        callibrationText.SetText("Thank you! Enjoy the game");
        closedHandImage.SetActive(false);

        // start mapping
        isMappinng = true;

        yield return new WaitForSeconds(3);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    static double CalculateAverage(LinkedList<int> linkedList)
    {
        if (linkedList == null || linkedList.Count == 0)
        {
            throw new ArgumentException("The linked list is null or empty.");
        }

        double sum = 0;

        foreach (int value in linkedList)
        {
            sum += value;
        }

        return sum / linkedList.Count;
    }

    static int FindMode(LinkedList<int> linkedList)
    {
        if (linkedList == null || linkedList.Count == 0)
        {
            throw new ArgumentException("The linked list is null or empty.");
        }

        // Create a dictionary to count occurrences of each number
        Dictionary<int, int> frequency = new Dictionary<int, int>();

        foreach (int number in linkedList)
        {
            if (frequency.ContainsKey(number))
            {
                frequency[number]++;
            }
            else
            {
                frequency[number] = 1;
            }
        }

        // Find the number with the highest frequency
        int mode = frequency.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;

        return mode;
    }
}
