using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOn : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.GetChild(0).GetComponent<Renderer>().enabled = false;
        transform.GetChild(1).GetComponent<Renderer>().enabled = false;
	}
	
    public void OnTriggerEnter()
    {
        transform.GetChild(0).GetComponent<Renderer>().enabled = true;
        transform.GetChild(1).GetComponent<Renderer>().enabled = true;
    }

    public void LightsOff()
    {
        transform.GetChild(0).GetComponent<Renderer>().enabled = true;
        transform.GetChild(1).GetComponent<Renderer>().enabled = true;
    }

	// Update is called once per frame
	void Update () {
		
	}
}
