using UnityEngine;
using System.Collections;

public class SpawnBasicUnits : MonoBehaviour {

    public static int BlackPieceCount;
    public static int WhitePieceCount;
    
    public GameObject WhitePawn;
    public GameObject White02;
    public GameObject WhiteGrunt;
    public GameObject WhiteRunner; 

    public GameObject BlackPawn;
    public GameObject Black02;
    public GameObject BlackRunner;
    public GameObject BlackScout; 

    public GameObject BPawnStartTile;
    public GameObject WPawnStartTile;
    public GameObject B2StartTile;
    public GameObject W2StartTile;
    public GameObject B3StartTile;
    public GameObject W3StartTile;
    public GameObject B4StartTile;
    public GameObject W4StartTile;
    public GameObject B5StartTile;
    public GameObject W5StartTile;
    public GameObject B6StartTile;
    public GameObject W6StartTile;

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
        //SpawnWhitePiece(WhiteX, W3StartTile);
        PiecePlaceScript.White03Tile = W3StartTile;
        //SpawnWhitePiece(WhiteX, W4StartTile);
        PiecePlaceScript.White04Tile = W4StartTile;
        //SpawnWhitePiece(WhiteX, W5StartTile);
        PiecePlaceScript.White05Tile = W5StartTile;
        //SpawnWhitePiece(WhiteX, W6StartTile);
        PiecePlaceScript.White06Tile = W6StartTile;
        //SpawnBlackPiece(BlackPawn, BPawnStartTile);
        PiecePlaceScript.Black01Tile = BPawnStartTile;
        //SpawnBlackPiece(Black02, B2StartTile);
        PiecePlaceScript.Black02Tile = B2StartTile;
        //SpawnBlackPiece(BlackX, B3StartTile);
        PiecePlaceScript.Black03Tile = B3StartTile;
        //SpawnBlackPiece(BlackX, B4StartTile);
        PiecePlaceScript.Black04Tile = B4StartTile;
        //SpawnBlackPiece(BlackX, B5StartTile);
        PiecePlaceScript.Black05Tile = B5StartTile;
        //SpawnBlackPiece(BlackX, B6StartTile);
        PiecePlaceScript.Black06Tile = B6StartTile;
    }

    public void SpawnWhitePiece(GameObject piece, GameObject tile)
    {

        if (!Network.isServer && !Network.isClient)
        {
            tile.GetComponent<TileProperties>().UnitOnTile = Instantiate(piece, tile.transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
            //tile.GetComponent<TileProperties>().Occupied = true;
            WhitePieceCount++;
        }
        else
        {
            tile.GetComponent<TileProperties>().UnitOnTile = Network.Instantiate(piece, tile.transform.position, Quaternion.Euler(0, 0, 0), 1) as GameObject;
            networkView.RPC("addPieceCount", RPCMode.AllBuffered, WhitePieceCount);
        }
    }

    public void SpawnBlackPiece(GameObject piece, GameObject tile)
    {
        if (!Network.isServer && !Network.isClient)
        {
            tile.GetComponent<TileProperties>().UnitOnTile = Instantiate(piece, tile.transform.position, Quaternion.Euler(0, 180, 0)) as GameObject;
            //tile.GetComponent<TileProperties>().Occupied = true;
            BlackPieceCount++;
        }
        else
        {
            tile.GetComponent<TileProperties>().UnitOnTile = Network.Instantiate(piece, tile.transform.position, Quaternion.Euler(0, 0, 0), 1) as GameObject;
            networkView.RPC("addPieceCount", RPCMode.AllBuffered, WhitePieceCount);
        }
    }

    [RPC]
    void addPieceCount(int pieceCount)
    {
        pieceCount++;
    }
}
