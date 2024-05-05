using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GloveConnection : MonoBehaviour
{
    SerialPort stream = new SerialPort("COM6", 9600);

    [SerializeField] TextMeshProUGUI warningText;

    [SerializeField] GameObject callibrationPanel;
    [SerializeField] TextMeshProUGUI callibrationText;

    [SerializeField] GameObject openHandImage;
    [SerializeField] GameObject closedHandImage;


    public string strReceived;
    private string[] strData = new string[9];

    //public Transform hand, index1, index2, index3, middle1, middle2, middle3, ring1, ring2, ring3, pinky0, pinky1, pinky2, pinky3, thumb1, thumb2, thumb3;

    void Update()
    {
        if (stream.IsOpen)
        {
            try
            {
                strReceived = stream.ReadLine();
                strData = strReceived.Split(',');

                if (strData[0] != "")
                {
                    Debug.Log(strData[0]);
                }

                //index1.transform.localRotation = Quaternion.Euler(-74, 106, 61-isaret);
                //middle1.transform.localRotation = Quaternion.Euler(-80, 18, 151-orta);
                //ring1.transform.localRotation = Quaternion.Euler(-69, -21, -169-yuzuk);
                //pinky1.transform.localRotation = Quaternion.Euler(-50, -26, 26);
                //thumb1.transform.localRotation = Quaternion.Euler(9, 156, 27-bas);

            }
            catch (Exception)
            {
                Debug.Log("We have a problem");
            }
        }
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
        callibrationText.SetText("Open your hand");
        callibrationPanel.SetActive(true);

        yield return new WaitForSeconds(5);

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