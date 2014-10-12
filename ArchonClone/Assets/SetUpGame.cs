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

	// Use this for initialization
	void Awake () {

        if (BattleStats.hotSeat == false)
        {
            hotSeatCamera.SetActive(false);
            if (BattleStats.player1Pref == "third")
            {
                p1ThirdCamera.GetComponent<Camera>().rect = new Rect(0,0,1,1);
                p1TopDownCamera.SetActive(false);
                player1.GetComponent<Player1MovementController>().enabled = false;
                
            }
            else
            {
                p1ThirdCamera.SetActive(false);
                player1.GetComponent<ThirdPersonCamera>().enabled = false;
                player1.GetComponent<ThirdPersonController>().enabled = false;
            }

            if (BattleStats.player2Pref == "third")
            {
                p1ThirdCamera.GetComponent<Camera>().rect = new Rect(0, 0, 1, 1);
                p1TopDownCamera.SetActive(false);
                player2.GetComponent<Player2MovementController>().enabled = false;
            }
            else
            {
                p2ThirdCamera.SetActive(false);
                player2.GetComponent<ThirdPersonCamera1>().enabled = false;
                player2.GetComponent<ThirdPersonController1>().enabled = false;
            }
        }
        else
        {
            if (BattleStats.player1Pref == "third")
            {
                hotSeatCamera.SetActive(false);
                p1TopDownCamera.SetActive(false);
                p2TopDownCamera.SetActive(false);
                player1.GetComponent<Player1MovementController>().enabled = false;
                player2.GetComponent<Player2MovementController>().enabled = false;
            }
            else
            {
                p1TopDownCamera.SetActive(false);
                p2TopDownCamera.SetActive(false);
                p2ThirdCamera.SetActive(false);
                p1ThirdCamera.SetActive(false);
                player1.GetComponent<ThirdPersonCamera>().enabled = false;
                player1.GetComponent<ThirdPersonController>().enabled = false;
                player2.GetComponent<ThirdPersonCamera1>().enabled = false;
                player2.GetComponent<ThirdPersonController1>().enabled = false;
            }
        }
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
