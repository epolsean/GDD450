using UnityEngine;
using System.Collections;

public class PiecePropScript : MonoBehaviour {

	//Stats of a piece
    public int Health;
    public int MaxHealth;
    public float Movement;
    public float Damage;
    public double PowerLevel; 

    
    // Use this for initialization
	void Start () {
        if(this.name == "BlackScout(Clone)" || this.name == "WhiteScout(Clone)")//stats for Scout
        {
            MaxHealth = 50;
            Health = 50;
            Movement = 10;
            Damage = 15;
            PowerLevel = 150;
        }
        else if(this.name == "BlackTank(Clone)" || this.name == "WhiteTank(Clone)")//stats for Tank
        {
            MaxHealth = 100;
            Health = 100;
            Movement = 6;
            Damage = 20;
            PowerLevel = 250;
        }
        else if(this.name == "BlackRunner(Clone)" || this.name == "WhiteRunner(Clone)")//stats for Runner
        {
            MaxHealth = 40;
            Health = 40;
            Movement = 12;
            Damage = 6;
            PowerLevel = 80;
        }
        else//stats for Grunt
        {
            MaxHealth = 70;
            Health = 70;
            Movement = 8;
            Damage = 8;
            PowerLevel = 100; 
        }


	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
