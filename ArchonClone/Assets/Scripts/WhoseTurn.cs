using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WhoseTurn : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (BattleStats.player1sTurn)
        {
            GetComponent<Text>().text = "Player 1's Turn";
        }
        else
        {
            GetComponent<Text>().text = "Player 2's Turn";
        }
	
	}
}
