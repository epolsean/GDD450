using UnityEngine;
using System.Collections;

public class SpawnBasicUnits : MonoBehaviour {

    public static int BlackPieceCount;
    public static int WhitePieceCount;
    
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
        if(PiecePlaceScript.isStart)
        {
            PiecePlaceScript.isStart = false; 
            StartSpawn();
            PiecePlaceScript.setPieces = true; 
        }
        else
        {
            //PiecePlaceScript.setPieces = true; 
        }
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log("BlackCount: " + BlackPieceCount);
        Debug.Log("WhiteCount: " + WhitePieceCount);
        if(BlackPieceCount <= 0 || WhitePieceCount <= 0)
        {
            EndControllerScript.isEnd = true; 
        }
	}

    void StartSpawn()
    {
        //SpawnWhitePiece(WhitePawn, WPawnStartTile);
        PiecePlaceScript.White01Tile = WPawnStartTile;
        //SpawnWhitePiece(White02, W2StartTile);
        PiecePlaceScript.White02Tile = W2StartTile;
        //SpawnBlackPiece(BlackPawn, BPawnStartTile);
        PiecePlaceScript.Black01Tile = BPawnStartTile;
        //SpawnBlackPiece(Black02, B2StartTile);
        PiecePlaceScript.Black02Tile = B2StartTile;
    }

    public void SpawnWhitePiece(GameObject piece, GameObject tile)
    {
        tile.GetComponent<TileProperties>().UnitOnTile = Instantiate(piece, tile.transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
        //tile.GetComponent<TileProperties>().Occupied = true;
        WhitePieceCount++;

        
    }

    public void SpawnBlackPiece(GameObject piece, GameObject tile)
    {
        tile.GetComponent<TileProperties>().UnitOnTile = Instantiate(piece, tile.transform.position, Quaternion.Euler(0, 180, 0)) as GameObject;
        //tile.GetComponent<TileProperties>().Occupied = true;
        BlackPieceCount++;
    }
}
