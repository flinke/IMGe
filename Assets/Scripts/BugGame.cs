using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BugGame : MonoBehaviour {
    public GameObject ParentBee;
    private Transform[] _beeArray;
    private Dictionary<int, bool> _isBeeUp;


    // Use this for initialization
    void Start() {
        SetBumbleBeeArray(ParentBee);
        SpawnBee();
    }

    // Update is called once per frame
    void Update() {
        for (int i = 0; i < _beeArray.Length; i++) {
            Debug.Log(i + " " + _isBeeUp[i] + " " + _beeArray[i].transform.position.y);
            if (_isBeeUp[i] && _beeArray[i].transform.position.y < 0.47f) {
                _beeArray[i].transform.position = Vector3.Lerp(
                    new Vector3(_beeArray[i].transform.position.x, 0.2f, _beeArray[i].transform.position.z),
                    new Vector3(_beeArray[i].transform.position.x, 0.47f, _beeArray[i].transform.position.z), 10000 / Time.deltaTime);
            }
        }
    }

    // add transform components of children to array
    void SetBumbleBeeArray(GameObject parentBee) {
        int beeCount = parentBee.transform.childCount;
        _beeArray = new Transform[beeCount];
        _isBeeUp = new Dictionary<int, bool>(beeCount);
        for (int i = 0; i < beeCount; i++) {
            _beeArray[i] = parentBee.transform.GetChild(i);
            _isBeeUp[i] = false;
        }

    }

    // spawn random bee
    void SpawnBee() {
        int beeNum;
        do {
            beeNum = Random.Range(0, _beeArray.Length - 1);
            _isBeeUp[beeNum] = true;
            StartCoroutine(DespawnBee(beeNum));
        } while (!_isBeeUp[beeNum]);
    }

    // bling bling and reset bee
    IEnumerator BlinkBee(int beeNum) {
        for (int i = 0; i < 10; i++) {
            _beeArray[beeNum].GetComponent<Renderer>().enabled
                = !_beeArray[beeNum].GetComponent<Renderer>().enabled;
            yield return new WaitForSeconds(0.05f);
        }
        _beeArray[beeNum].transform.position =
                    new Vector3(_beeArray[beeNum].transform.position.x, 0.2f, _beeArray[beeNum].transform.position.z);
    }

    // despawn bee
    IEnumerator DespawnBee(int beeNum) {
        yield return new WaitForSeconds(3f);
        if (_isBeeUp[beeNum]) {
            _beeArray[beeNum].transform.position = Vector3.Lerp(
    new Vector3(_beeArray[beeNum].transform.position.x, 0.2f, _beeArray[beeNum].transform.position.z),
    new Vector3(_beeArray[beeNum].transform.position.x, 0.47f, _beeArray[beeNum].transform.position.z), 10000 / Time.deltaTime);
            _isBeeUp[beeNum] = false;
        }
    }

    // kill bee if hit


    // lower HP if hit false button

}
