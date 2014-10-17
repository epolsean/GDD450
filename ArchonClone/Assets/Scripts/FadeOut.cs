using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeOut : MonoBehaviour {
    float delay = 1;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (delay < 0)
        {
            GetComponent<Text>().color = new Color(GetComponent<Text>().color.r, GetComponent<Text>().color.g, GetComponent<Text>().color.b, GetComponent<Text>().color.a - Time.deltaTime / 2);
            if (GetComponent<Text>().color.a < 0.20)
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            delay -= Time.deltaTime;
        }
	}
}
