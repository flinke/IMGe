using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchGame : MonoBehaviour {
    public GameObject match;

    private bool _matchIsInPosition = false;
    private bool _counterBegins = false;
    private float velocity = 3;
    private float counter = 0;
    private float counterSlide = 0;
    private Vector3 actualPosition = new Vector3(0, 0, 0);
    private float w = 0;
    private bool _isPlaying = false;
    private float time_gone;
    private float slideBefore = 0;


    float sliderRight;

    // Use this for initialization
    void Start() {
        match.GetComponentInChildren<Renderer>().enabled = false;
        match.transform.GetChild(1).GetComponent<Renderer>().enabled = false;
        match.GetComponentInChildren<Renderer>().enabled = false;
        StartMatchFly();
    }

    public void StartMatchFly() {
        _isPlaying = true;
        match.transform.GetChild(2).GetComponent<Renderer>().enabled = true;
        match.transform.GetChild(3).GetComponent<Renderer>().enabled = true;
        MatchFlyToStartPosition();
        time_gone = Time.time;
    }

    // Update is called once per frame
    void Update() {
        if (match.transform.GetComponentInParent<Animator>().runtimeAnimatorController.animationClips[0].length <= counter) {
            _matchIsInPosition = true;
            match.transform.GetComponentInParent<Animator>().enabled = false;
            counter = 0;
            _counterBegins = false;
        }

        if (_counterBegins) {
            counter += Time.deltaTime;
        }

        if (_matchIsInPosition) {
            //match.transform.localPosition = new Vector3(30.8f, -3.6f, -0.9f);
            MoveMatch();
        }

        //        if (w >= 0.5f) {
        //            MatchFlyToWall();
        //        }


        if (_isPlaying && (Time.time - time_gone) >= 6.0f) {
            _isPlaying = false;
            GameObject.Find("LifeBar").GetComponent<LifeBar>().getDamage(1.0f);
            time_gone = Time.time;
        }
    }


    public void MatchFlyToStartPosition() {
        match.transform.GetComponentInParent<Animator>().SetBool("GameBegins", true);
        _counterBegins = true;
    }

    public void MatchFlyToWall() {
        match.transform.GetComponentInParent<Animator>().enabled = true;
        match.transform.GetComponentInParent<Animator>().SetBool("GameBegins", false);
        match.transform.GetComponentInParent<Animator>().SetBool("MatchLitOn", true);
        _isPlaying = false;
    }

    //bewegung des streicholz mithilfe des controllers
    public void MoveMatch() {
        //information, wo slider
        sliderRight = GetComponent<Controller>().getSlider()[3];
        match.transform.localPosition = Vector3.Lerp(new Vector3(31.2f, -3.5f, 27.6f), new Vector3(30.8f, -3.6f, -0.9f), (sliderRight));


        if ((slideBefore - sliderRight) / Time.deltaTime >= 25f && Random.Range(0, 9) > 3)
        {
            Mathf.Clamp(counterSlide, 0, 1);
            match.transform.GetChild(1).GetComponent<Renderer>().enabled = true;
            //if (counterSlide >= 1.0f)
            {
                //if (actualPosition == new Vector3(0, 0, 0)) {
                //    actualPosition = match.transform.localPosition;
                //    w = slideBefore;
                //}

                match.transform.GetChild(1).GetComponent<Renderer>().enabled = false;
                match.GetComponentInChildren<Renderer>().enabled = true;
                //Lerpe zum endpunkt
                counterSlide = 20f;

                match.transform.localPosition = Vector3.Lerp(actualPosition, new Vector3(31.2f, -3.5f, 27.6f), w);
                //w += Time.deltaTime;
                Debug.Log(Random.Range(0, 9));
                    MatchFlyToWall();
            }
        } else {
            counterSlide -= Time.deltaTime * 0.09f;
            //match.transform.GetChild(1).GetComponent<Renderer>().enabled = true;
        }

        slideBefore = sliderRight;
    }

    //dabei auch eine rauchschwade erscheinen lassen

    //wenn schnell genug, dann feuer, mit counter schauen, ob unterhalb einer grenze

    //streichholz wird auch wand geworfen und wand geht in feuer auf 
    //wenn wand collider von streichholz berührt, dann wird das particle system aktiviert

    //schachtel fliegt wieder zurück

    //wenn in zehn sekunden nicht schafft -> bild wechselt -> bleibt beim Bild
    //wie viele leben: 0=bluescreen. 3/4 Leben startet. 
    //jede drei bis fünf bugs geht hoch
    //actimel: gehts hoich, wenn trifft 

}

//0 bis 5 intensity hoch, wenn spiel beginnt 

