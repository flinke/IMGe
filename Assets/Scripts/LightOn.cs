using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOn : MonoBehaviour {

    public GameObject match;

	// Use this for initialization
	void Start () {
        transform.GetChild(0).GetComponent<Renderer>().enabled = false;
        transform.GetChild(1).GetComponent<Renderer>().enabled = false;
	}
	
    public void OnTriggerEnter()
    {
        transform.GetChild(0).GetComponent<Renderer>().enabled = true;
        transform.GetChild(1).GetComponent<Renderer>().enabled = true;

        Debug.Log("triggerenter");
        match.transform.GetChild(2).GetComponent<Renderer>().enabled = false;
        match.transform.GetChild(3).GetComponent<Renderer>().enabled = false;
        //transform.GetComponentInParent<Animator>().speed = -1;
        match.transform.GetComponentInParent<Animator>().SetBool("MatchLitOn", false);

        match.transform.GetComponentInParent<Animator>().SetBool("GameBegins", false);
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
