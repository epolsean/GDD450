using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public GameObject SelectRandP(string TagName)
    {
        GameObject selectedPiece = null;
        //collect all pieces with given tag...
        
        //randomly select one of these pieces...

        return selectedPiece;
    }

    public GameObject FindNearestPlayerP(GameObject AIPiece, string PlayerTag)
    {
        GameObject PlayerPiece = null;
        //collect all pieces with given tag...

        //find the player piece that is closest to selected AIPiece...

        return PlayerPiece; 
    }

    public void SetTargetAI()//only called if PlayerPiece within radius
    {
        //this sets the target for the AI Opponent and moves initiates combat...
    }
}
