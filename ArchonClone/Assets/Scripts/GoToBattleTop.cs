using UnityEngine;
using System.Collections;

public class GoToBattleTop : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //Application.LoadLevel("BattleTest");
        Application.LoadLevelAdditive("BattleTest");
        BattleStats.player1Pref = "top";
        Destroy(GameObject.Find("CameraAddative"));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
