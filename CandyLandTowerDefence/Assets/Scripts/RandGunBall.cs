using UnityEngine;
using System.Collections;

public class RandGunBall : MonoBehaviour {

    public int RanGunBallColor = 0;

	// Use this for initialization
	void Start () {
        RanGunBallColor = Random.Range(0, 3);
	}
	
	// Update is called once per frame
	void Update () {
	    if(RanGunBallColor>=3)
        {
            this.renderer.material.color = Color.red;
        }
        else if (RanGunBallColor >= 2)
        {
            this.renderer.material.color = Color.green;
        }
        else if (RanGunBallColor >= 1)
        {
            this.renderer.material.color = Color.yellow;
        }
        else
        {
            this.renderer.material.color = Color.blue;
        }
	}
}
