using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActimelGame : MonoBehaviour {

    public Transform ActimelBottle;
    public Transform ParentZones;
    private Transform[] _zones = new Transform[5];
    private Vector3 _start;
    private Vector3 _end;
    private float _unterstrich = 0f;
    private float _speed = 2.5f;
    private Transform _bottle;
    private Transform _zone;
    // Use this for initialization
    void Start() {
        if (_bottle == null)
        _bottle = Instantiate(ActimelBottle);
        if (_zone == null)
        _zone = Instantiate(ParentZones);

        _bottle.gameObject.SetActive(true);
        _zone.gameObject.SetActive(true);
        _start = new Vector3(1.25f, 1.5f, 0f);
        _end = new Vector3(-1f, 1.5f, 0f);

        for (int i = 0; i < _zone.childCount; i++)
            _zones[i] = _zone.GetChild(i);
        Debug.Log(_zone.GetChild(1) + " " + _zones[1]);

        CancelInvoke();
        InvokeRepeating("SetZones", 0f, 1.5f);
    }

    // Update is called once per frame
    void Update() {
        _unterstrich += Time.deltaTime / _speed;
        if (_unterstrich > 2.0f) {
            _unterstrich = 0f;
            _speed = Random.Range(1f, 2.5f);
        }
        if (_unterstrich <= 1.0f) {
            _bottle.transform.position = Vector3.Lerp(_start, _end, _unterstrich);
        } else {
            _bottle.transform.position = Vector3.Lerp(_start, _end, 2 - _unterstrich);
        }
    }

    void OnDisable() {
        CancelInvoke();
        _bottle.gameObject.SetActive(false);
        _zone.gameObject.SetActive (false);
    }
    
    void OnEnable() {
        Start();
    }

    void SetZones() {
        int tmp = Random.Range(0, _zone.childCount);
        int tmp2;
        do
            tmp2 = Random.Range(0, _zone.childCount);
        while (tmp == tmp2);
        _zones[tmp].GetComponent<Renderer>().material.color = Color.green;
        _zones[tmp].tag = "good";
        Debug.Log(_zones[tmp]);
        _zones[tmp2].GetComponent<Renderer>().material.color = Color.green;
        _zones[tmp2].tag = "good";

        for (int i = 0; i < _zone.childCount; i++) {
            if (tmp != i && tmp2 != i) {
                _zones[i].GetComponent<Renderer>().material.color = Color.red;
                _zones[i].tag = "bad";
            }
        }
    }
}
