using UnityEngine;
using System.IO.Ports;
using System;
using System.Collections;

public class Controller : MonoBehaviour {
    SerialPort _stream = new SerialPort("COM8", 115200);
    private string receivedData = "EMPTY";
    private string[] sliders = new string[5] { "test", "t", "tes", "ass", "df" };
    private string[] accelerometer;
    private float[] sliderVals = new float[5] { 1.0f, 0f, 0f, 0f, 0f };
    private float accelValsX;
    private float accelValsY;
    private float accelValsZ;
    private float alcoholLevel;
    private int pressedLastFrame = -1;


    void Start() {
        _stream.Open();
        Debug.Log("Serial port opened");
        //StartCoroutine(blinkLED(4,10,31));
        setMultipleLED("0000");
    }

    void Update() {
        getAccel();
        Debug.Log(accelValsX + " " + accelValsY + " " + accelValsZ);
    }

    //get the slide/rotate values
    public float[] getSlider() {
        _stream.Write("4");
        receivedData = _stream.ReadLine();
        sliders = receivedData.Split(' ');
        sliderVals[0] = (float)Convert.ToInt32(sliders[1], 16) / 4096;
        sliderVals[1] = (float)Convert.ToInt32(sliders[2], 16) / 4096;
        sliderVals[2] = (float)Convert.ToInt32(sliders[3], 16) / 4096;
        sliderVals[3] = (float)Convert.ToInt32(sliders[4], 16) / 4096;
        return sliderVals;
    }

    //get the blow-value
    public float getAlcoholLevel() {
        _stream.Write("s");
        receivedData = _stream.ReadLine();
        alcoholLevel = float.Parse(receivedData.Split(' ')[1], System.Globalization.CultureInfo.InvariantCulture);
        return alcoholLevel;
    }

    //maybe change to array
    public void getAccel() {
        _stream.Write("a");
        receivedData = _stream.ReadLine();
        accelerometer = receivedData.Split(' ');
        accelValsX = (float)Convert.ToInt32(accelerometer[1], 16) / 128;
        accelValsY = (float)Convert.ToInt32(accelerometer[2], 16) / 128;
        accelValsZ = (float)Convert.ToInt32(accelerometer[3], 16) / 128;
        if (accelValsX > 1.0f)
            accelValsX -= 2.0f;
        if (accelValsY > 1.0f)
            accelValsY -= 2.0f;
        if (accelValsZ > 1.0f)
            accelValsZ -= 2.0f;
    }

    public float getAccelX() {
        getAccel();
        return accelValsX;
    }

    // returns the pressed button
    public int OnButtonClick() {
        Debug.Log(pressedLastFrame);
        for (int i = 0; i < 6; i++) {
            if (isPressed(i) && pressedLastFrame != i) {
                pressedLastFrame = i;
                return i;
            }
        }
        pressedLastFrame = -1;
        return -1;
    }

    //check if a button (1-6) is pressed 
    public bool isPressed(int button) {
        int receivedData;
        _stream.Write("1");
        receivedData = Convert.ToInt32(_stream.ReadLine(), 16);

        int bitmask = 1 << (6 + button);
        receivedData &= bitmask;
        return receivedData != 0;
    }

    //set the LED (0-3) to off (0), on (1) or switch (2)
    public void setLED(int id, int status) {
        _stream.Write("l " + id + " " + status + "\r\n");
        receivedData = _stream.ReadLine();
    }

    //set up a "Level"
    public void setMetreLED(int number) {
        for (int i = 3; i > 3 - number; i--) {
            _stream.Write("l " + i + " 1\r\n");
            receivedData = _stream.ReadLine();
        }
        for (int i = 0; i < 4 - number; i++) {
            _stream.Write("l " + i + " 0\r\n");
            receivedData = _stream.ReadLine();
        }
    }

    //needs a 4 character long string to set up the LED in this way
    // 0 for turn off, 1 for turn 1, 2 for swap and 4 for blink
    public void setMultipleLED(string led) {
        for (int i = 0; i < 4; i++) {
            if (led[i] == '0' || led[i] == '1' || led[i] == '2') {
                _stream.Write("l " + (3 - i) + " " + led[i] + "\r\n");
                receivedData = _stream.ReadLine();
            } else if (led[i] == '4') {
                StartCoroutine(blinkLED(i));
            }
        }
    }

    //let the LED blink 
    IEnumerator blinkLED(int id, int frequency = 5, int blinkCount = 20) {
        while (blinkCount > 0) {
            _stream.Write("l " + (3 - id) + " 2\r\n");
            receivedData = _stream.ReadLine();
            blinkCount--;
            yield return new WaitForSeconds(1.0f / frequency);
        }
    }

    public void setMotor(int value) {
        _stream.Write("m " + value + "\r\n");
        receivedData = _stream.ReadLine();
    }
}
