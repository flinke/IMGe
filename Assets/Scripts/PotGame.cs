﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotGame : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.GetChild(0).GetComponent<Light>().intensity <= 9.0f)
            transform.GetChild(0).GetComponent<Light>().intensity += 0.01f;
    }


}
