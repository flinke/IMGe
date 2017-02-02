using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LifeBar : MonoBehaviour
{

    GUIText lifebar;
    float hp = 5;
    public GameObject[] lifeboxes = new GameObject[6];
    private float Countdown = 300f;
    
    // Update is called once per frame
    void Update()
    {
        toggle((int)hp);
        Debug.Log(Countdown);
        if (Countdown > 0f) {
            Countdown -= Time.deltaTime;
        } else { 
            Debug.Log("win");
            SceneManager.LoadScene(2);
        }
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
        hp -= dmg;
        if (hp <= 0) {
            SceneManager.LoadScene(1);
        }
    }
}
