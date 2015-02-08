using UnityEngine;
using System.Collections;

public class PiecePropScript : MonoBehaviour {

	//Stats of a piece
    public float Movement;
    public float PowerLevel;
    public string unitType;
    public string PieceName;
    
    // Use this for initialization
	void Start () {
        if(this.name == "BlackScout(Clone)" || this.name == "WhiteScout(Clone)")//stats for Scout
        {
            unitType = "Scout";
            Movement = 4;
            PowerLevel = 150;
            if(this.name == "BlackScout(Clone)")
            {
                PieceName = "Organic Scout";
            }
            else
            {
                PieceName = "Synthetic Scout";
            }
        }
        else if(this.name == "BlackTank(Clone)" || this.name == "WhiteTank(Clone)")//stats for Tank
        {
            unitType = "Tank";
            Movement = 2;
            PowerLevel = 250;
            if (this.name == "BlackTank(Clone)")
            {
                PieceName = "Organic Tank";
            }
            else
            {
                PieceName = "Synthetic Tank";
            }
        }
        else if(this.name == "BlackRunner(Clone)" || this.name == "WhiteRunner(Clone)")//stats for Runner
        {
            unitType = "Runner";
            Movement = 5;
            PowerLevel = 80;
            if (this.name == "BlackRunner(Clone)")
            {
                PieceName = "Organic Runner";
            }
            else
            {
                PieceName = "Synthetic Runner";
            }
        }
        else if (this.name == "BlackBomber(Clone)" || this.name == "WhiteBomber(Clone)")//stats for Bomber
        {
            unitType = "Bomber";
            Movement = 2;
            PowerLevel = 125;
            if (this.name == "BlackBomber(Clone)")
            {
                PieceName = "Organic Bomber";
            }
            else
            {
                PieceName = "Synthetic Bomber";
            }
        }
        else//stats for Grunt
        {
            unitType = "Grunt";
            Movement = 3;
            PowerLevel = 100;
            if (this.name == "BlackGrunt(Clone)")
            {
                PieceName = "Organic Grunt";
            }
            else
            {
                PieceName = "Synthetic Grunt";
            }
        }


	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
