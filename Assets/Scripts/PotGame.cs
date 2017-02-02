using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotGame : MonoBehaviour
{
    private float _timeToNextMiniGame;
    private float _cooldown = 10f;
    private Controller _controller;
    private bool clickedAButton

    // Use this for initialization
    void Start()
    {
        _controller = GetComponent<Controller>();
    }

    // Update is called once per frame
    void Update() {
        if (!clickedAButton && _controller.isClicked()) {
            _controller.OnButtonClick();
            clickedAButton = true;
        } else if (clickedAButton && !_controller.isClicked())
            clickedAButton = false;
        if (transform.GetChild(0).GetComponent<Light>().intensity <= 9.0f)
            transform.GetChild(0).GetComponent<Light>().intensity += 0.01f;
    }




}
