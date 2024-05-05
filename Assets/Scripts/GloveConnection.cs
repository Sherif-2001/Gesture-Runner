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

    [SerializeField] GameObject callibrationPanel;
    [SerializeField] TextMeshProUGUI callibrationText;

    [SerializeField] GameObject openHandImage;
    [SerializeField] GameObject closedHandImage;


    public string strReceived;
    private string[] strData = new string[5];

    private int[] sensorData = new int[5]; // Array to hold sensor data vectors
    LinkedList<int> myLinkedListSensor1 = new LinkedList<int>();
    LinkedList<int> myLinkedListSensor2 = new LinkedList<int>();
    LinkedList<int> myLinkedListSensor3 = new LinkedList<int>();
    LinkedList<int> myLinkedListSensor4 = new LinkedList<int>();
    LinkedList<int> myLinkedListSensor5 = new LinkedList<int>();

    private bool isCalibratingOpen = false;

    //public Transform hand, index, middle, ring, pinky, thumb;

    void Update()
    {
        if (stream.IsOpen)
        {
            try
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
                if (isCalibratingOpen)
                {
                    myLinkedListSensor1.AddLast(sensorData[0]);
                    myLinkedListSensor2.AddLast(sensorData[1]);
                    myLinkedListSensor3.AddLast(sensorData[2]);
                    myLinkedListSensor4.AddLast(sensorData[3]);
                    myLinkedListSensor5.AddLast(sensorData[4]);
                }

            }
            catch (Exception)
            {
                Debug.Log("We have a problem");
            }
        }
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
            StartCoroutine(StartCallibration());
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

    IEnumerator StartCallibration()
    {
        isCalibratingOpen = true;

        callibrationText.SetText("Open your hand");
        callibrationPanel.SetActive(true);

        yield return new WaitForSeconds(5);
        isCalibratingOpen = false;
        // Calculate and print the average
        double average1 = CalculateAverage(myLinkedListSensor1);
        PlayerPrefs.SetFloat("avg1", (float)average1);

        double average2 = CalculateAverage(myLinkedListSensor2);
        PlayerPrefs.SetFloat("avg2", (float)average2);

        double average3 = CalculateAverage(myLinkedListSensor3);
        PlayerPrefs.SetFloat("avg3", (float)average3);

        double average4 = CalculateAverage(myLinkedListSensor4);
        PlayerPrefs.SetFloat("avg4", (float)average4);

        double average5 = CalculateAverage(myLinkedListSensor5);
        PlayerPrefs.SetFloat("avg5", (float)average5);
        PlayerPrefs.Save();

        Debug.Log($"Count 1: {myLinkedListSensor1.Count}");


        Debug.Log($"Average1: {average1}");
        Debug.Log($"Average2: {average2}");
        Debug.Log($"Average3: {average3}");
        Debug.Log($"Average4: {average4}");
        Debug.Log($"Average5: {average5}");

        int mode1 = FindMode(myLinkedListSensor1);
        int mode2 = FindMode(myLinkedListSensor2);
        int mode3 = FindMode(myLinkedListSensor3);
        int mode4 = FindMode(myLinkedListSensor4);
        int mode5 = FindMode(myLinkedListSensor5);

        Debug.Log($"mode1: {mode1}");
        Debug.Log($"mode2: {mode2}");
        Debug.Log($"mode3: {mode3}");
        Debug.Log($"mode4: {mode4}");
        Debug.Log($"mode5: {mode5}");

        callibrationText.SetText("Close your hand");
        openHandImage.SetActive(false);
        closedHandImage.SetActive(true);
        yield return new WaitForSeconds(5);

        callibrationText.SetText("Thank you! Enjoy the game");
        closedHandImage.SetActive(false);

        yield return new WaitForSeconds(3);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}