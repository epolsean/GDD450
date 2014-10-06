using UnityEngine;
using System.Collections;

public class TileProperties : MonoBehaviour {

    public GameObject UnitOnTile = null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseOver()
    {
        renderer.material.color = Color.green;
    }

    void OnMouseExit()
    {
        if(this.tag == "WhiteTile")
        {
            renderer.material.color = Color.white;
        }
        if (this.tag == "BlackTile")
        {
            renderer.material.color = Color.black;
        }
    }
}
