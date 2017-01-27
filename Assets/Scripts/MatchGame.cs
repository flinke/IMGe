using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchGame : MonoBehaviour
{

    private bool _matchIsInPosition = false;
    private bool _counterBegins = false;
    private float velocity = 3;
    private float counter = 0;
    private float counterSlide = 0;
    private Vector3 actualPosition = new Vector3(0, 0, 0);
    private float w = 0;

    private float slideBefore = 0;

    float sliderRight;

    // Use this for initialization
    void Start()
    {
        GetComponentInChildren<Renderer>().enabled = false;
        transform.GetChild(1).GetComponent<Renderer>().enabled = false;
        GetComponentInChildren<Renderer>().enabled = false;
        StartMatchFly();
        MatchFlyToWall();
    }

    public void StartMatchFly()
    {
        transform.GetChild(2).GetComponent<Renderer>().enabled = true;
        transform.GetChild(3).GetComponent<Renderer>().enabled = true;
        MatchFlyToStartPosition();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.GetComponentInParent<Animator>().runtimeAnimatorController.animationClips[0]

        //Debug.Log(transform.GetComponentInParent<Animator>().runtimeAnimatorController.animationClips[0].length);
        if (transform.GetComponentInParent<Animator>().runtimeAnimatorController.animationClips[0].length <= counter)
        {
            _matchIsInPosition = true;
            //Debug.Log("hier");
            transform.GetComponentInParent<Animator>().enabled = false;
            //transform.parent.GetChild(3).gameObject.AddComponent<Controller>();
            //transform.parent.GetChild(3).gameObject.AddComponent<Streichholz>();            
            counter = 0;
            _counterBegins = false;
            //Destroy(this);          
        }

        if (_counterBegins)
        {
            counter += Time.deltaTime;
        }

        //Debug.Log(transform.parent);
        //&& !(transform.GetComponentInParent<Animator>().IsPlaying("MatchesFly")
        if (_matchIsInPosition)
        {
            //Debug.Log("hier");
            this.transform.localPosition = new Vector3(30.8f, -3.6f, -0.9f);
            //this.transform.localRotation = new Quaternion(-14.695f, -122.912f, 25.473f, 1f);
            //this.transform.localScale = new Vector3(0.3767294f, 0.3767294f, 0.3767294f);
            //transform.GetComponentInParent<Animator>().Stop();
            //transform.GetComponentInParent<Animation>().Stop("MatchesFly");
            MoveMatch();
        }

        if (w >= 1.0f)
        {
            MatchFlyToWall();
        }


    }

    //man registriert dass jetzt streichholz getan wird, indem ein boolean gesetzt wird bei einer oberklassemanager

    //controller vibiriert und neues spiel fängt an:
    // das streichholz fliegt hin

    public void MatchFlyToStartPosition()
    {
        transform.GetComponentInParent<Animator>().SetBool("GameBegins", true);
        _counterBegins = true;

    }

    public void OnTriggerEnter(Collider other)
    {
        transform.GetChild(2).GetComponent<Renderer>().enabled = false;
        transform.GetChild(3).GetComponent<Renderer>().enabled = false;
        //transform.GetComponentInParent<Animator>().speed = -1;
        transform.GetComponentInParent<Animator>().SetBool("MatchLitOn", false);

        transform.GetComponentInParent<Animator>().SetBool("GameBegins", false);
    }

    public void MatchFlyToWall()
    {
        Debug.Log("hallo");

        transform.GetComponentInParent<Animator>().SetBool("MatchLitOn", true);
        Debug.Log(GetComponent<Renderer>().enabled);

        //transform.GetComponentInParent<Animator>().speed = -1;
        //transform.GetComponentInParent<Animator>().SetBool("MatchLitOn", false);
        //transform.GetComponentInParent<Animator>().SetBool("GameBegins", false);
    }

    //Methode laesst streichholzschachtel hinfliegen


    //bewegung des streicholz mithilfe des controllers
    public void MoveMatch()
    {
        //information, wo slider
        sliderRight = GetComponent<Controller>().getSlider()[3];
        //Debug.Log(sliderRight);
        //start: 30.8, -3.6, -0.9
        //ende: 31.2, -3.5, 27.6
        //richtungsvektor: (0.4, 0.1, 28.5)
        //bei 0000 ende, bei 0FFF start
        //this.transform.localPosition = new Vector3(30.8f, -3.6f, -0.9f) + (new Vector3(0.4f, 0.1f, 28.5f) * sliderRight);
        this.transform.localPosition = Vector3.Lerp(new Vector3(31.2f, -3.5f, 27.6f), new Vector3(30.8f, -3.6f, -0.9f), (sliderRight));


        if (slideBefore + 0.07f < -sliderRight && counterSlide < counterSlide + Time.deltaTime)
        {
            counterSlide += Time.deltaTime * 1.3f;
            Mathf.Clamp(counterSlide, 0, 1);
            Debug.Log(transform.GetChild(1));
            transform.GetChild(1).GetComponent<Renderer>().enabled = true;
        }
        else
        {
            counterSlide -= Time.deltaTime * 0.09f;
            transform.GetChild(1).GetComponent<Renderer>().enabled = true;
            // Mathf.Clamp(counterSlide, 0, 1);
        }

        slideBefore = -sliderRight;

        Debug.Log(counterSlide);

        if (counterSlide >= 1.0f)
        {
            if (actualPosition == new Vector3(0, 0, 0))
            {
                actualPosition = transform.localPosition;
                w = slideBefore;
            }

            Debug.Log(":)");
            transform.GetChild(1).GetComponent<Renderer>().enabled = false;
            GetComponentInChildren<Renderer>().enabled = true;
            //Lerpe zum endpunkt
            counterSlide = 20f;

            this.transform.localPosition = Vector3.Lerp(actualPosition, new Vector3(31.2f, -3.5f, 27.6f), w);
            w += Time.deltaTime;
        }

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

