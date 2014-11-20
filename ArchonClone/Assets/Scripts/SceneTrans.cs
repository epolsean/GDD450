using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneTrans : MonoBehaviour
{

    public GameObject slider1;
    public GameObject slider2;
    public GameObject slider3;

    public bool trigger;
    public bool trigger2;

    float fracJourney;
    float distCovered;
    float startTime;

    bool S1N2;
    bool S3;
    bool S1N2R;
    bool S3R;
    bool S1N2Started = false;
    bool S3Started = false;

	// Use this for initialization
	void Start () {
        trigger = false;
        trigger2 = false;
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
        if (trigger2 == true)
        {
            if (slider3.GetComponent<Image>().fillAmount != 0f)
            {
                S1N2R = false;
                S3R = true;
            }
            else if (slider3.GetComponent<Image>().fillAmount == 0f)
            {
                S1N2R = true;
                S3R = false;
            }

            if (S1N2R == true)
            {
                sliderOneAndTwoMove();
            }

            if (S3R == true)
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

        if (S1N2 == true)
        {
            slider1.GetComponent<Image>().fillAmount = Mathf.Lerp(0, 0.5f, fracJourney);
            slider2.GetComponent<Image>().fillAmount = Mathf.Lerp(0, 0.5f, fracJourney);
        }
        else if (S1N2R == true)
        {
            slider1.GetComponent<Image>().fillAmount = Mathf.Lerp(0.5f, 0, fracJourney);
            slider2.GetComponent<Image>().fillAmount = Mathf.Lerp(0.5f, 0, fracJourney);
        }
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

        if (S3 == true)
        {
            slider3.GetComponent<Image>().fillAmount = Mathf.Lerp(0, 1f, fracJourney);
        }
        else if (S3R == true)
        {
            slider3.GetComponent<Image>().fillAmount = Mathf.Lerp(1f, 0, fracJourney);
        }
    }
}
