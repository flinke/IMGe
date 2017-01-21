using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActimelGame : MonoBehaviour {

    public Transform ActimelBottle;
    private Vector3 _start;
    private Vector3 _end;
    private float _unterstrich = 0f;


    // Use this for initialization
    void Start() {
        _start = new Vector3(1.25f, 1.5f, 0f);
        _end = new Vector3(-1f, 1.5f, 0f);
    }

    // Update is called once per frame
    void Update() {
        _unterstrich += Time.deltaTime / 2;
        if (_unterstrich > 2.0f)
            _unterstrich = 0f;
        if (_unterstrich <= 1.0f) {
            ActimelBottle.transform.position = Vector3.Lerp(_start, _end, _unterstrich);
        } else {
            ActimelBottle.transform.position = Vector3.Lerp(_start, _end, 2 - _unterstrich);
        }


    }
}
