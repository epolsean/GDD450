using UnityEngine;
using System.Collections;

public class SpawnBasicUnits : MonoBehaviour {

    public GameObject WhitePawn;
    public GameObject White02;
    public GameObject BlackPawn;
    public GameObject Black02;

    public GameObject BPawnStartTile;
    public GameObject WPawnStartTile;
    public GameObject B2StartTile;
    public GameObject W2StartTile;

	// Use this for initialization
	void Start () {
        SpawnWhitePiece(WhitePawn, WPawnStartTile);
        PiecePlaceScript.White01Tile = WPawnStartTile;
        SpawnWhitePiece(White02, W2StartTile);
        PiecePlaceScript.White02Tile = W2StartTile;
        SpawnBlackPiece(BlackPawn, BPawnStartTile);
        PiecePlaceScript.Black01Tile = BPawnStartTile;
        SpawnBlackPiece(Black02, B2StartTile);
        PiecePlaceScript.White02Tile = B2StartTile;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SpawnWhitePiece(GameObject piece, GameObject tile)
    {
        tile.GetComponent<TileProperties>().UnitOnTile = Instantiate(piece, tile.transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
        //tile.GetComponent<TileProperties>().Occupied = true;

        
    }

    public void SpawnBlackPiece(GameObject piece, GameObject tile)
    {
        tile.GetComponent<TileProperties>().UnitOnTile = Instantiate(piece, tile.transform.position, Quaternion.Euler(0, 180, 0)) as GameObject;
        //tile.GetComponent<TileProperties>().Occupied = true;
    }
}
