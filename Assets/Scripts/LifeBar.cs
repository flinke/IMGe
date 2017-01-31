using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBar : MonoBehaviour
{

    GUIText lifebar;
    float hp = 5;
    public GameObject[] lifeboxes = new GameObject[6];

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        toggle((int)hp);
    }

    void toggle(int i)
    {
        foreach (GameObject obj in lifeboxes)
        {
            obj.SetActive(false);
        }
        lifeboxes[i].SetActive(true);
    }

    public void getDamage(float dmg)
    {
        Debug.Log(hp);
        hp -= dmg;
    }
}
