using UnityEngine;
using System.Collections;

public class BlackPawnProperties : MonoBehaviour {

    public static bool isAlive = true;

    public static int health = 50;
    
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (isAlive == false)
        {
            Destroy(this.gameObject);
        }
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
