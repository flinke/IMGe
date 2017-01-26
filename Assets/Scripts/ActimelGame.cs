using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActimelGame : MonoBehaviour {

    public Transform ActimelBottle;
    public Transform ParentZones;
    private Transform[] _zones;
    private Vector3 _start;
    private Vector3 _end;
    private float _unterstrich = 0f;
    private float _speed = 2.5f;

    // Use this for initialization
    void Start(){
        _start = new Vector3(1.25f, 1.5f, 0f);
        _end = new Vector3(-1f, 1.5f, 0f);
        for (int i = 0; i < ParentZones.childCount; i++) 
            _zones[i] = ParentZones.GetChild(i);
        InvokeRepeating("SetZones", 0f, 1.5f);
        }

    // Update is called once per frame
    void Update(){
        _unterstrich += Time.deltaTime / _speed;
        if (_unterstrich > 2.0f) {
            _unterstrich = 0f;
            _speed = Random.Range(1f, 2.5f);
        }
        if (_unterstrich <= 1.0f) {
            ActimelBottle.transform.position = Vector3.Lerp(_start, _end, _unterstrich);
        } else {
            ActimelBottle.transform.position = Vector3.Lerp(_start, _end, 2 - _unterstrich);
        }
    }

    // 
    void SetZones() {

    }
}
