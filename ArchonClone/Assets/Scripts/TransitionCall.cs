using UnityEngine;
using System.Collections;

public class TransitionCall : MonoBehaviour {

    bool startTrans;
    float endTimer;
    GameObject Canvas;

	// Use this for initialization
	void Start () {
        Canvas = GameObject.Find("Canvas2");
        startTrans = false;
        endTimer = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (GameObject.Find("Player1(Clone)") != null || GameObject.Find("Player2(Clone)") != null)
        {
            if (GameObject.Find("Player1(Clone)").GetComponent<Player1MovementController>().win == true || GameObject.Find("Player2(Clone)").GetComponent<Player2MovementController>().win == true)
            {
                startTrans = true;
            }
        }
        if (startTrans == true)
        {
            if (endTimer <= 2.5f)
            {
                Debug.Log("We are in the Timer");
                endTimer += Time.deltaTime;
                if (endTimer >= 1.25f)
                {
                    Debug.Log("Should be drawing the Transitions");
                    Canvas.GetComponent<SceneTrans>().close = true;
                    Canvas.GetComponent<SceneTrans>().open = false;
                }
            }
        }
	}
}
