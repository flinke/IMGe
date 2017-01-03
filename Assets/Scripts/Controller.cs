using UnityEngine;
using System.IO.Ports;
using System;
using System.Collections;

public class Controller : MonoBehaviour {

    SerialPort stream = new SerialPort("COM3", 115200);
    private string receivedData = "EMPTY";
    private string[] sliders;
    private string[] accelerometer;
    public float[] sliderVals;
    public float accelValsX;
    public float accelValsY;
    public float accelValsZ;
    public float alcoholLevel;

    void Start() {
        stream.Open();
        Debug.Log("Serial port opened");
    }

    public void getSlider() {
        stream.Write("4");
        receivedData = stream.ReadLine();
        sliders = receivedData.Split(' ');
        sliderVals[1] = Convert.ToInt32(sliders[1], 16);
        sliderVals[2] = Convert.ToInt32(sliders[2], 16);
        sliderVals[3] = Convert.ToInt32(sliders[3], 16);
        sliderVals[4] = Convert.ToInt32(sliders[4], 16);
    }

    public void getAlcoholLevel() {
        stream.Write("s");
        receivedData = stream.ReadLine();
        alcoholLevel = float.Parse( receivedData.Split(' ')[1], System.Globalization.CultureInfo.InvariantCulture);
    }

    public void getAccel() {
        stream.Write("a");
        receivedData = stream.ReadLine();
        accelerometer = receivedData.Split(' ');
        accelValsX = (float)Convert.ToInt32(accelerometer[1], 16) / 128;
        accelValsY = (float)Convert.ToInt32(accelerometer[2], 16) / 128;
        accelValsZ = (float)Convert.ToInt32(accelerometer[3], 16) / 127;
        if (accelValsX > 1.0f)
            accelValsX -= 2.0f;
        if (accelValsY > 1.0f)
            accelValsY -= 2.0f;
        if (accelValsZ > 1.0f)
            accelValsZ -= 2.0f;
    }

    public bool isPressed(int button) {
        int receivedData;
        stream.Write("1");
        receivedData = Convert.ToInt32(stream.ReadLine(), 16);

        int bitmask = 1 << (5 + button);
        receivedData &= bitmask;
        return receivedData != 0;
    }

    public void setLED(int id, int status) {
        stream.Write("l " + id + " " + status + "\r\n");
        receivedData = stream.ReadLine();
    }

    public void setMetreLED(int number) {
        for (int i = 3; i > 3 - number; i--) {
            stream.Write("l " + i + " 1\r\n");
            receivedData = stream.ReadLine();
        }
        for (int i = 0; i < 4 - number; i++) {
            stream.Write("l " + i + " 0\r\n");
            receivedData = stream.ReadLine();
        }
    }

    public void setMultipleLED(string led) {
        for (int i = 0; i < 4; i++) {
            if (led[i] == '0' || led[i] == '1' || led[i] == '2') {
                stream.Write("l " + i + " " + led[i] + "\r\n");
                receivedData = stream.ReadLine();
            } else if (led[i] == '4') {
                StartCoroutine(blinkLED(i));
            }
        }
    }

    IEnumerator blinkLED(int id, int frequency = 2, int blinkCount = 10) {
        while (blinkCount > 0) {
            stream.Write("l " + id + "2\r\n");
            receivedData = stream.ReadLine();
            blinkCount--;
            yield return new WaitForSeconds(1.0f / frequency);
        }
    }

    public void setMotor(int value) {
        stream.Write("m " + value + "\r\n");
        receivedData = stream.ReadLine();
    }
}
