using UnityEngine;
using System.Collections;

public class StrategyBoardMusic : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (GetComponent<AudioSource>().isPlaying && GameObject.Find("BattleSceneAdditive") != null)
        {
            GetComponent<AudioSource>().Pause();
        }
        else if (!GetComponent<AudioSource>().isPlaying && GameObject.Find("BattleSceneAdditive") == null)
        {
            GetComponent<AudioSource>().Play();
        }
	}
}
