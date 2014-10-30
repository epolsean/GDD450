using UnityEngine;
using System.Collections;

public class GoToBattleThird : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //Application.LoadLevel("BattleTest");
        Application.LoadLevelAdditive("BattleTest");
        BattleStats.player1Pref = "third";
        Destroy(GameObject.Find("CameraAddative"));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
