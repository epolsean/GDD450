using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneTrans : MonoBehaviour
{

    public GameObject slider1;
    public GameObject slider2;
    public GameObject slider3;

    public bool trigger;

    float fracJourney;
    float distCovered;
    float startTime;

    bool S1N2;
    bool S3;
    bool S1N2Started = false;
    bool S3Started = false;

	// Use this for initialization
	void Start () {
	    slider1.GetComponent<Image>().fillAmount = 0;
        slider2.GetComponent<Image>().fillAmount = 0;
        slider3.GetComponent<Image>().fillAmount = 0;
        trigger = false;
	}
	
	// Update is called once per frame
    void Update()
    {
        if (trigger == true)
        {
            if (slider2.GetComponent<Image>().fillAmount != .5f)
            {
                S1N2 = true;
                S3 = false;
            }
            else if (slider2.GetComponent<Image>().fillAmount == .5f)
            {
                S1N2 = false;
                S3 = true;
            }

            if (S1N2 == true)
            {
                sliderOneAndTwoMove();
            }

            if (S3 == true)
            {
                sliderThreeMove();
            }
        }
    }

    void sliderOneAndTwoMove()
    {
        if (S1N2Started == false)
        {
            startTime = Time.time;
            S1N2Started = true;
        }

        distCovered = (Time.time - startTime) * 10;
        fracJourney = distCovered / 10;

        slider1.GetComponent<Image>().fillAmount = Mathf.Lerp(0, 0.5f, fracJourney);
        slider2.GetComponent<Image>().fillAmount = Mathf.Lerp(0, 0.5f, fracJourney);
    }

    void sliderThreeMove()
    {
        if (S3Started == false)
        {
            startTime = Time.time;
            S3Started = true;
        }

        distCovered = (Time.time - startTime) * 10;
        fracJourney = distCovered / 4;

        slider3.GetComponent<Image>().fillAmount = Mathf.Lerp(0, 1f, fracJourney);
    }
}
