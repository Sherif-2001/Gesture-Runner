using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class ControllerScript : MonoBehaviour
{
    SerialPort stream = new SerialPort("COM6", 9600);

    [SerializeField] GameObject iceParticles;
    [SerializeField] GameObject fireParticles;

    private ScoreManager scoreManager;

    [SerializeField] GameObject obstacleMessage;

    public string strReceived;
    private string[] strData = new string[5];

    private int[] sensorData = new int[5]; // Array to hold sensor data 

    private double[] sensors_difference = new double[5]; // Array to hold sensor data vectors
    private bool[] isFingersClose = new bool[5]; // Array to hold sensor data vectors

    private double[] openAverage = new double[5];

    private bool enteredVictoryJump = false;
    private bool enteredFireGun = false;
    private bool enteredWaterGun = false;


    private PlayerScript playerScript;



    private void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();

        scoreManager = GetComponent<ScoreManager>();
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


        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            DestroyObstacles("Ice");
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            DestroyObstacles("Fire");
        }
        else if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            playerScript.Jump();

        }
    }

    void DestroyObstacles(string tag)
    {
        try
        {
            GameObject obstacle = GameObject.FindGameObjectWithTag(tag);
            GameObject particles = null;

            if (tag == "Fire")
            {
                particles = Instantiate(fireParticles, obstacle.transform.position, Quaternion.identity);
            }
            else if (tag == "Ice")
            {
                particles = Instantiate(iceParticles, obstacle.transform.position, Quaternion.identity);
            }
            else
            {
                return;
            }

            particles.GetComponent<ParticleSystem>().Play();
            Destroy(particles, 2f);

            scoreManager.IncreaseScore();
            Destroy(obstacle);
            obstacleMessage.SetActive(false);
        }
        catch
        {
            print($"No {tag} Obstacle Found");
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
        // Open hand
        if (!(isFingersClose[0] || isFingersClose[1] || isFingersClose[2] || isFingersClose[3] || isFingersClose[4]))
        {
            print("Open");
            enteredVictoryJump = false;
            enteredFireGun = false;
            enteredWaterGun = false;
        }
        // Victory Gesture
        else if (!(isFingersClose[1] || isFingersClose[2]) && isFingersClose[0] && isFingersClose[3] && isFingersClose[4])
        {
            print("Victory Jump");
            if (!enteredVictoryJump)
            {
                enteredVictoryJump = true;
                playerScript.Jump();
            }
        }
        // Gun 1 Finger Gesture
        else if (!(isFingersClose[0] || isFingersClose[1]) && isFingersClose[2] && isFingersClose[3] && isFingersClose[4])
        {
            print("Water Gun");
            if (!enteredWaterGun)
            {
                enteredWaterGun = true;
                DestroyObstacles("Fire");
            }
        }
        // Gun 2 Fingers Gesture
        else if (!(isFingersClose[0] || isFingersClose[1] || isFingersClose[2]) && isFingersClose[3] && isFingersClose[4])
        {
            print("Fire Gun");
            if (!enteredFireGun)
            {
                enteredFireGun = true;
                DestroyObstacles("Ice");
            }
        }
    }


    void OnDestroy()
    {
        if (stream != null && stream.IsOpen)
        {
            stream.Close();
        }

    }

}
