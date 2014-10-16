using UnityEngine;
using System.Collections;

public class GoToBattleThird : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Application.LoadLevel("BattleTest");
        BattleStats.player1Pref = "third";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
