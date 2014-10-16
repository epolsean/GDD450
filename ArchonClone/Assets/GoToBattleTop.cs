using UnityEngine;
using System.Collections;

public class GoToBattleTop : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Application.LoadLevel("BattleTest");
        BattleStats.player1Pref = "top";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
