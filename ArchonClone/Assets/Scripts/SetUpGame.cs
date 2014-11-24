using UnityEngine;
using System.Collections;

public class SetUpGame : MonoBehaviour {

    public GameObject player1;
    public GameObject player2;

    public GameObject hotSeatCamera;
    public GameObject p1TopDownCamera;
    public GameObject p1ThirdCamera;
    public GameObject p2TopDownCamera;
    public GameObject p2ThirdCamera;

    public GameObject laser1;
    public GameObject laser2;
    public GameObject ff1;
    public GameObject ff2;

	// Use this for initialization
	void Awake () {
        if (BattleStats.hotSeat == false)
        {
            if (BattleStats.player1Pref == "third")
            {
                //p1ThirdCamera.GetComponent<Camera>().rect = new Rect(0,0,1,1);
                p1TopDownCamera.SetActive(false);
                p1ThirdCamera.SetActive(true);
                player1.GetComponent<Player1MovementController>().topDownView = false;
            }
            else
            {
                p1ThirdCamera.SetActive(false);
                p1TopDownCamera.SetActive(true);
                player1.GetComponent<Player1MovementController>().topDownView = true;
            }

            if (BattleStats.player2Pref == "third")
            {
                //p2ThirdCamera.GetComponent<Camera>().rect = new Rect(0, 0, 1, 1);
                p2TopDownCamera.SetActive(false);
                p2ThirdCamera.SetActive(true);
                player2.GetComponent<Player2MovementController>().topDownView = false;
            }
            else
            {
                p2ThirdCamera.SetActive(false);
                p2TopDownCamera.SetActive(true);
                player2.GetComponent<Player2MovementController>().topDownView = true;
            }

        }
        else
        {
            if (BattleStats.player1Pref == "third")
            {
                if (BattleStats.singlePlayer)
                {
                    p1ThirdCamera.GetComponent<Camera>().rect = new Rect(0, 0, 1, 1);
                }
                hotSeatCamera.SetActive(false);
                p1TopDownCamera.SetActive(false);
                p2TopDownCamera.SetActive(false);
                p1ThirdCamera.SetActive(true);
                p2ThirdCamera.SetActive(true);
                player1.GetComponent<Player1MovementController>().topDownView = false;
                player2.GetComponent<Player2MovementController>().topDownView = false;
            }
            else
            {
                p1TopDownCamera.SetActive(false);
                p2TopDownCamera.SetActive(false);
                p2ThirdCamera.SetActive(false);
                p1ThirdCamera.SetActive(false);
                player1.GetComponent<Player1MovementController>().topDownView = true;
                player2.GetComponent<Player2MovementController>().topDownView = true;
            }
        }
        if (laser1 != null)
        {
            if (Random.Range(0, 100) < 50)
            {
                laser1.SetActive(true);
            }
        }
        if (laser2 != null)
        {
            if (Random.Range(0, 100) < 50)
            {
                laser2.SetActive(true);
            }
        }
        if (ff1 != null)
        {
            if (Random.Range(0, 100) < 50)
            {
                ff1.SetActive(true);
            }
        }
        if (ff2 != null)
        {
            if (Random.Range(0, 100) < 50)
            {
                ff2.SetActive(true);
            }
        }
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
