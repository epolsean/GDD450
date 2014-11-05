using UnityEngine;
using System.Collections;

public class PiecePropScript : MonoBehaviour {

	//Stats of a piece
    public int Health;
    public int Movement;
    public int Damage;
    public double SpecialStat; 

    
    // Use this for initialization
	void Start () {
        if(this.name == "BlackScout(Clone)" || this.name == "White02(Clone)")//stats for Scout
        {
            Health = 50;
            Movement = 3;
            Damage = 15;
        }
        else if(this.name == "BlackPawn(Clone)" || this.name == "WhitePawn(Clone)")//stats for Tank
        {
            Health = 100;
            Movement = 2;
            Damage = 20;
        }
        else if(this.name == "BlackRunner(Clone)" || this.name == "WhiteRunner(Clone)")//stats for Runner
        {
            Health = 40;
            Movement = 4;
            Damage = 6;
        }
        else//stats for Grunt
        {
            Health = 70;
            Movement = 2;
            Damage = 8;
        }


	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
