using UnityEngine;
using System.Collections;

public class White02Properties : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider Tile)
    {
        if(Tile.tag == "WhiteTile" || Tile.tag == "BlackTile")
        {
            Tile.GetComponent<TileProperties>().UnitOnTile = this.gameObject;
        }
    }
    void OnTriggerExit(Collider Tile)
    {
        Tile.GetComponent<TileProperties>().UnitOnTile = null;
    }
}
