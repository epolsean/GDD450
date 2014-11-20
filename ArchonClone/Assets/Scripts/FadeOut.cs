using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeOut : MonoBehaviour {

    GameObject Canvas;
    float delay = 3;
    float startTimer;
    bool endTrans;

	// Use this for initialization
	void Start () {
        Canvas = GameObject.Find("Canvas2");
	}
	
	// Update is called once per frame
	void Update () {
        if (endTrans == false)
        {
            if (startTimer <= 2.5)
            {
                startTimer += Time.deltaTime;
                if (startTimer >= 1.25f)
                {
                    Canvas.GetComponent<SceneTrans>().open = true;
                }
            }
            else
            {
                endTrans = true;
            }
        }
        else
        {
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
}
