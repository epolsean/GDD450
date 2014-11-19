using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneTrans : MonoBehaviour
{

    public GameObject slider1;
    public GameObject slider2;
    public GameObject slider3;
    public GameObject slider4;

    float startTime;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
    void Update()
    {
        float distCovered = (Time.time - startTime) * 10;
        float fracJourney = distCovered / 10;

        if (slider2.GetComponent<Image>().fillAmount != .5f)
        {
            slider1.GetComponent<Image>().fillAmount = Mathf.Lerp(0, 0.5f, fracJourney);
            slider2.GetComponent<Image>().fillAmount = Mathf.Lerp(0, 0.5f, fracJourney);
        }

        if (slider2.GetComponent<Image>().fillAmount == .5f && slider3.GetComponent<Image>().fillAmount == 0)
        {
            startTime = Time.time;
            distCovered = (Time.time - startTime) * 10;
            slider3.GetComponent<Image>().fillAmount = Mathf.Lerp(0, 1f, fracJourney);
        }
        else if (slider2.GetComponent<Image>().fillAmount == .5f)
        {
            float fracJourney2 = distCovered / 6;
            slider3.GetComponent<Image>().fillAmount = Mathf.Lerp(0, 1f, fracJourney2);
        }
        
        if (slider3.GetComponent<Image>().fillAmount == 1f && slider4.GetComponent<Image>().fillAmount == 0)
        {
            startTime = Time.time;
            distCovered = (Time.time - startTime) * 10;
            slider4.GetComponent<Image>().fillAmount = Mathf.Lerp(0, 1f, fracJourney);
        }
        else if (slider3.GetComponent<Image>().fillAmount == 1f)
        {
            float fracJourney3 = distCovered / 15;
            slider4.GetComponent<Image>().fillAmount = Mathf.Lerp(0, 1f, fracJourney3);
        }
    }
}
