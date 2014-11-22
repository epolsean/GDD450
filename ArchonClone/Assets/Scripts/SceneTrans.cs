using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneTrans : MonoBehaviour
{

    public GameObject slider1;
    public GameObject slider2;

    public bool close;
    public bool open;

    public float fracJourney;
    public float distCovered;
    public float startTime;

    public bool S1N2;
    public bool S1N2R;
    public bool S1N2Started;
    public float startTimer;
    string level;
    bool nextScene;
    bool startedScene;

	// Use this for initialization
	void Start () {
        close = false;
        open = false;
        S1N2 = false;
        S1N2R = false;
        S1N2Started = false;
	}
	
	// Update is called once per frame
    void Update()
    {
        if (close == true)
        {
            S1N2 = true;
            sliderOneAndTwoMove();
        }
        if (open == true)
        {
            S1N2R = true;
            sliderOneAndTwoMove();
        }
        /*
        if (nextScene == true)
        {
            if (startTimer <= 2.5f)
            {
                startTimer += Time.deltaTime;
                if (startTimer >= 1.5f)
                {
                    close = true;
                }
            }
            else
            {
                Application.LoadLevel(level);
            }
        }

        if (startedScene == true)
        {
            if (startTimer <= 2.5f)
            {
                startTimer += Time.deltaTime;
                if (startTimer >= 1.5f)
                {
                    open = true;
                }
            }
            else
            {
                startedScene = false;
                open = false;
                S1N2R = false;
                S1N2Started = false;
                startTimer = 0;
            }
        }*/
    }

    void sliderOneAndTwoMove()
    {
        if (S1N2Started == false)
        {
            Debug.Log("Time Started");
            startTime = Time.time;
            S1N2Started = true;
        }

        distCovered = (Time.time - startTime) * 10;
        fracJourney = distCovered / 10;

        if (S1N2 == true)
        {
            Debug.Log("Closing");
            slider1.GetComponent<Image>().fillAmount = Mathf.Lerp(0.075f, 0.5f, fracJourney);
            slider2.GetComponent<Image>().fillAmount = Mathf.Lerp(0.075f, 0.5f, fracJourney);
        }
        else if (S1N2R == true)
        {
            Debug.Log("Open");
            slider1.GetComponent<Image>().fillAmount = Mathf.Lerp(0.5f, 0.075f, fracJourney);
            slider2.GetComponent<Image>().fillAmount = Mathf.Lerp(0.5f, 0.075f, fracJourney);
        }

        if (slider1.GetComponent<Image>().fillAmount >= .499f && close == true)
        {
            close = false;
            S1N2 = false;
            distCovered = 0;
            fracJourney = 0;
            S1N2Started = false;
        }
        if (slider1.GetComponent<Image>().fillAmount <= .076f && open == true)
        {
            open = false;
            S1N2R = false;
            distCovered = 0;
            fracJourney = 0;
            S1N2Started = false;
        }
    }

    public void NextScene()
    {
        if (Application.loadedLevelName == "TestTrans1")
        {
            level = "TestTrans2";
            nextScene = true;
        }
        else if (Application.loadedLevelName == "TestTrans2")
        {
            level = "TestTrans1";
            nextScene = true;
        }
    }
}
