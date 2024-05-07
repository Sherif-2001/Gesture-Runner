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

    public SerialPort stream = new SerialPort("COM6", 9600);

    public string strReceived;
    private string[] strData = new string[5];
    // Array to hold sensor data 
    private int[] sensorData = new int[5];

    // Create an array of LinkedLists
    LinkedList<int>[] fingersOpen = new LinkedList<int>[5];
    LinkedList<int>[] fingersClose = new LinkedList<int>[5];

    private double[] openAverage = new double[5];
    //private double[] closeAverage = new double[5];
    //private double[] openMode = new double[5];
    //private double[] closeMode = new double[5];
    private double[] sensors_difference = new double[5];
    //private double[] sensors_mapping = new double[5];


    private bool isCalibratingOpen = false;
    //private bool isCalibratingClose = false;
    private bool isMappinng = false;

    [SerializeField] GameObject callibrationPanel;
    [SerializeField] TextMeshProUGUI callibrationText;
    [SerializeField] GameObject openHandImage;
    //[SerializeField] GameObject closedHandImage;


    void Start()
    {
        // Initialize each element of the array with a new LinkedList
        for (int i = 0; i < fingersOpen.Length; i++)
        {
            fingersOpen[i] = new LinkedList<int>();
            fingersClose[i] = new LinkedList<int>();
        }
    }

    void Update()
    {
        if (stream.IsOpen)
        {
            try
            {
                strReceived = stream.ReadLine();
                strData = strReceived.Split(',');
                Debug.Log(strReceived);

                if (strData[0] != "" && strData[1] != "" && strData[2] != "" && strData[3] != "" && strData[4] != "")
                {
                    Debug.Log($"{strData[0]}, {strData[1]} {strData[2]} {strData[3]} {strData[4]}");
                }

                for (int i = 0; i < 5; i++)
                {
                    sensorData[i] = int.Parse(strData[i]);
                }
                if (isCalibratingOpen)
                {
                    // store Open values
                    for (int i = 0; i < 5; i++)
                    {
                        fingersOpen[i].AddLast(sensorData[i]);
                    }
                }
                //if (isCalibratingClose)
                //{
                //    // store Open values
                //    for (int i = 0; i < 5; i++)
                //    {
                //        fingersClose[i].AddLast(sensorData[i]);
                //    }
                //}

                if (isMappinng)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        // Claculate abosulte difference between incoming and Open
                        sensors_difference[i] = Math.Abs(openAverage[i] - sensorData[i]);

                        // Map the input numbers between 0 and 180
                        //sensors_mapping[i] = MapToRange(sensorData[i], openAverage[i], closeAverage[i], 0, 180);
                    }
                    Debug.Log($" Difference  {Math.Round(sensors_difference[0])}, {Math.Round(sensors_difference[1])}, {Math.Round(sensors_difference[2])}, {Math.Round(sensors_difference[3])}, {Math.Round(sensors_difference[4])}");
                    //Debug.Log($" Mapping  {Math.Round(sensors_mapping[0])}, {Math.Round(sensors_mapping[1])}, {Math.Round(sensors_mapping[2])}, {Math.Round(sensors_mapping[3])}, {Math.Round(sensors_mapping[4])}");
                }
            }
            catch (Exception)
            {
                Debug.Log("We have a problem");
            }
        }
    }

    //static double MapToRange(double value, double originalMin, double originalMax, double newMin, double newMax)
    //{
    //    return ((value - originalMin) / (originalMax - originalMin)) * (newMax - newMin) + newMin;
    //}


    public IEnumerator StartCallibration()
    {
        callibrationPanel.SetActive(true);
        callibrationText.SetText("Open your hand");

        yield return new WaitForSeconds(3);
        isCalibratingOpen = true;


        yield return new WaitForSeconds(5);
        isCalibratingOpen = false;
        // Claculate Open Parameters

        Debug.Log($"Count 1: {fingersOpen[0].Count}");
        // Calculate and print the average
        for (int i = 0; i < 5; i++)
        {
            openAverage[i] = CalculateAverage(fingersOpen[i]);
            // Create a dynamic key name using string interpolation
            string key = $"openAvg{i}";
            PlayerPrefs.SetFloat(key, (float)openAverage[i]);

            Debug.Log($"Open Average {i}: {openAverage[i]}");
        }
        PlayerPrefs.Save();


        //// Calculate and print the Mode
        //for (int i = 0; i < 5; i++)
        //{
        //    openMode[i] = FindMode(fingersOpen[i]);
        //    // Create a dynamic key name using string interpolation
        //    string key = $"openMode{i}";
        //    PlayerPrefs.SetFloat(key, (float)openMode[i]);

        //    Debug.Log($"Open Mode {i}: {openMode[i]}");
        //}
        //PlayerPrefs.Save();

        //callibrationText.SetText("Close your hand");
        //openHandImage.SetActive(false);
        //closedHandImage.SetActive(true);

        //yield return new WaitForSeconds(3);
        //isCalibratingClose = true;


        //yield return new WaitForSeconds(5);
        //isCalibratingClose = false;
        // Claculate Close Parameters

        //Debug.Log($"Count 1: {fingersClose[0].Count}");
        //// Calculate and print the average
        //for (int i = 0; i < 5; i++)
        //{
        //    closeAverage[i] = CalculateAverage(fingersClose[i]);
        //    // Create a dynamic key name using string interpolation
        //    string key = $"closeAvg{i}";
        //    PlayerPrefs.SetFloat(key, (float)closeAverage[i]);

        //    Debug.Log($"Close Average {i}: {closeAverage[i]}");
        //}
        //// Calculate and print the Mode
        //for (int i = 0; i < 5; i++)
        //{
        //    closeMode[i] = FindMode(fingersClose[i]);
        //    // Create a dynamic key name using string interpolation
        //    string key = $"closeMode{i}";
        //    PlayerPrefs.SetFloat(key, (float)closeMode[i]);

        //    Debug.Log($"Close Mode {i}: {closeMode[i]}");
        //}
        //PlayerPrefs.Save();


        openHandImage.SetActive(false);
        callibrationText.SetText("Thank you! Glove is Calibrated");

        // start mapping
        isMappinng = true;

        yield return new WaitForSeconds(3);

        SceneManager.LoadScene(0);
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

    //static int FindMode(LinkedList<int> linkedList)
    //{
    //    if (linkedList == null || linkedList.Count == 0)
    //    {
    //        throw new ArgumentException("The linked list is null or empty.");
    //    }

    //    // Create a dictionary to count occurrences of each number
    //    Dictionary<int, int> frequency = new Dictionary<int, int>();

    //    foreach (int number in linkedList)
    //    {
    //        if (frequency.ContainsKey(number))
    //        {
    //            frequency[number]++;
    //        }
    //        else
    //        {
    //            frequency[number] = 1;
    //        }
    //    }

    //    // Find the number with the highest frequency
    //    int mode = frequency.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;

    //    return mode;
    //}
}
