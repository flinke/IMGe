using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotGame : MonoBehaviour {

    private bool _gameStarted = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (_gameStarted)
        {
            transform.GetChild(0).GetComponent<Light>().intensity += 0.01f;
        }

	}

    public void StartPotGame()
    {
        _gameStarted = true;
    }
}
