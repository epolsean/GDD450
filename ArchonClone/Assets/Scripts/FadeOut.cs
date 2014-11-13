using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeOut : MonoBehaviour {
    float delay = 3;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (delay <= 0)
        {
            GetComponent<Text>().text = "FIGHT!";
            GetComponent<Text>().color = new Color(GetComponent<Text>().color.r, GetComponent<Text>().color.g, GetComponent<Text>().color.b, GetComponent<Text>().color.a - Time.deltaTime);
            if (GetComponent<Text>().color.a < 0.5)
            {
                Destroy(this.gameObject);
            }
        }
        else if (delay >= 1)
        {
            GetComponent<Text>().text = delay.ToString("0");
            delay -= Time.deltaTime;
        }
        else
        {
            delay -= Time.deltaTime;
        }
	}
}
