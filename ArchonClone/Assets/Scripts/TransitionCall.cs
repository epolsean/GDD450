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
                endTimer += Time.deltaTime;
                if (endTimer >= 1.5f && endTimer < 1.6f)
                {
                    Canvas.GetComponent<SceneTrans>().close = true;
                    Canvas.GetComponent<SceneTrans>().startTimer = 0;
                }
            }
            else
            {
                GameObject.Find("HexGrid").GetComponent<TileProperties>().cameBack = true;
            }
        }
	}
}
