using UnityEngine;
using System.Collections;

public class TutorialTransition : MonoBehaviour {
    bool endTrans;
    float startTimer;
    GameObject Canvas;

    void Start()
    {
        Canvas = GameObject.Find("Canvas2");
    }
	
	// Update is called once per frame
	void Update () {
        if (endTrans == false)
        {
            if (startTimer <= 2.5f)
            {
                startTimer += Time.deltaTime;
                if (startTimer >= 1.5f && startTimer < 1.6f)
                {
                    Canvas.GetComponent<SceneTrans>().open = true;
                    Canvas.GetComponent<SceneTrans>().startTimer = 0;
                }
            }
            else
            {
                endTrans = true;
            }
        }
    }
}
