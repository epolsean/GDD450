using UnityEngine;
using System.Collections;

public class TileProperties : MonoBehaviour {

    public GameObject UnitOnTile = null;
    public GameObject UnitMoveController;
	// Use this for initialization
	void Start () {
        //UnitOnTile = null;
        UnitMoveController = GameObject.Find("MovementController");
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

    void OnMouseDown()
    {
        UnitMoveController.GetComponent<WhitePawnMove>().SelectedPiece = UnitOnTile;
    }
}
