using UnityEngine;
using System.Collections;

public class SpawnBasicUnits : MonoBehaviour {

    public static int BlackPieceCount;
    public static int WhitePieceCount;
    
    public GameObject WhiteTank;
    public GameObject WhiteScout;
    public GameObject WhiteGrunt;
    public GameObject WhiteRunner; 

    public GameObject BlackTank;
    public GameObject BlackGrunt;
    public GameObject BlackRunner;
    public GameObject BlackScout; 

    public GameObject BTank01Tile;
    public GameObject WTank01Tile;
    public GameObject BTank02Tile;
    public GameObject WTank02Tile;
    public GameObject BTank03Tile;
    public GameObject WTank03Tile;
    public GameObject BTank04Tile;
    public GameObject WTank04Tile;
    public GameObject BRunner01Tile;
    public GameObject WRunner01Tile;
    public GameObject BRunner02Tile;
    public GameObject WRunner02Tile;
    public GameObject BRunner03Tile;
    public GameObject WRunner03Tile;
    public GameObject BRunner04Tile;
    public GameObject WRunner04Tile;
    public GameObject BGrunt01Tile;
    public GameObject WGrunt01Tile;
    public GameObject BGrunt02Tile;
    public GameObject WGrunt02Tile;
    public GameObject BGrunt03Tile;
    public GameObject WGrunt03Tile;
    public GameObject BGrunt04Tile;
    public GameObject WGrunt04Tile;
    public GameObject BScout01Tile;
    public GameObject WScout01Tile;
    public GameObject BScout02Tile;
    public GameObject WScout02Tile;
    public GameObject BScout03Tile;
    public GameObject WScout03Tile;
    public GameObject BScout04Tile;
    public GameObject WScout04Tile;

    //Set up all the alien Tiles
    public GameObject AlienGrunt1StartTile;
    public GameObject AlienGrunt2StartTile;
    public GameObject AlienGrunt3StartTile;
    public GameObject AlienGrunt4StartTile;
    public GameObject AlienGrunt5StartTile;
    public GameObject AlienGrunt6StartTile;

    public GameObject AlienTank1StartTile;
    public GameObject AlienTank2StartTile;

    public GameObject AlienScout1StartTile;
    public GameObject AlienScout2StartTile;

    public GameObject AlienRunner1StartTile;
    public GameObject AlienRunner2StartTile;

    //Set up all the robot Tiles
    public GameObject RobotGrunt1StartTile;
    public GameObject RobotGrunt2StartTile;
    public GameObject RobotGrunt3StartTile;
    public GameObject RobotGrunt4StartTile;
    public GameObject RobotGrunt5StartTile;
    public GameObject RobotGrunt6StartTile;

    public GameObject RobotTank1StartTile;
    public GameObject RobotTank2StartTile;

    public GameObject RobotScout1StartTile;
    public GameObject RobotScout2StartTile;

    public GameObject RobotRunner1StartTile;
    public GameObject RobotRunner2StartTile;

	// Use this for initialization
	void Start () {
        EndControllerScript.OrgVic = false;
        EndControllerScript.SynthVic = false;
        StartSpawn();
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log("BlackCount: " + BlackPieceCount);
        //Debug.Log("WhiteCount: " + WhitePieceCount);
        if(BlackPieceCount <= 0)
        {
            EndControllerScript.SynthVic = true; 
            EndControllerScript.isEnd = true; 
        }
        else if(WhitePieceCount <= 0)
        {
            EndControllerScript.OrgVic = true;
            EndControllerScript.isEnd = true; 
        }
	}

    void StartSpawn()
    {
        //Spawn 4 SynthTanks
        if(WTank01Tile != null)
        {
            SpawnWhitePiece(WhiteTank, WTank01Tile);
        }
        if (WTank02Tile != null)
        {
            SpawnWhitePiece(WhiteTank, WTank02Tile);
        }
        if (WTank03Tile != null)
        {
            SpawnWhitePiece(WhiteTank, WTank03Tile);
        }
        if (WTank04Tile != null)
        {
            SpawnWhitePiece(WhiteTank, WTank04Tile);
        }
        //Spawn 4 SynthScout
        if(WScout01Tile != null)
        {
            SpawnWhitePiece(WhiteScout, WScout01Tile);
        }
        if (WScout02Tile != null)
        {
            SpawnWhitePiece(WhiteScout, WScout02Tile);
        }
        if (WScout03Tile != null)
        {
            SpawnWhitePiece(WhiteScout, WScout03Tile);
        }
        if (WScout04Tile != null)
        {
            SpawnWhitePiece(WhiteScout, WScout04Tile);
        }
        //Spawn 2 SynthRunner
        if (WRunner01Tile != null)
        {
            SpawnWhitePiece(WhiteRunner, WRunner01Tile);
        }
        if (WRunner02Tile != null)
        {
            SpawnWhitePiece(WhiteRunner, WRunner02Tile);
        }
        if (WRunner03Tile != null)
        {
            SpawnWhitePiece(WhiteRunner, WRunner03Tile);
        }
        if (WRunner04Tile != null)
        {
            SpawnWhitePiece(WhiteRunner, WRunner04Tile);
        }
        //Spawn 4 SynthGrunts
        if (WGrunt01Tile != null)
        {
            SpawnWhitePiece(WhiteGrunt, WGrunt01Tile);
        }
        if (WGrunt02Tile != null)
        {
            SpawnWhitePiece(WhiteGrunt, WGrunt02Tile);
        }
        if (WGrunt03Tile != null)
        {
            SpawnWhitePiece(WhiteGrunt, WGrunt03Tile);
        }
        if (WGrunt04Tile != null)
        {
            SpawnWhitePiece(WhiteGrunt, WGrunt04Tile);
        }

        //Spawn 2 AlienTanks
        if (BTank01Tile != null)
        {
            SpawnBlackPiece(BlackTank, BTank01Tile);
        }
        if (BTank02Tile != null)
        {
            SpawnBlackPiece(BlackTank, BTank02Tile);
        }
        if (BTank03Tile != null)
        {
            SpawnBlackPiece(BlackTank, BTank03Tile);
        }
        if (BTank04Tile != null)
        {
            SpawnBlackPiece(BlackTank, BTank04Tile);
        }
        //Spawn 4 AlienScout
        if (BScout01Tile != null)
        {
            SpawnBlackPiece(BlackScout, BScout01Tile);
        }
        if (BScout02Tile != null)
        {
            SpawnBlackPiece(BlackScout, BScout02Tile);
        }
        if (BScout03Tile != null)
        {
            SpawnBlackPiece(BlackScout, BScout03Tile);
        }
        if (BScout04Tile != null)
        {
            SpawnBlackPiece(BlackScout, BScout04Tile);
        }
        //Spawn 4 AlienRunner
        if (BRunner01Tile != null)
        {
            SpawnBlackPiece(BlackRunner, BRunner01Tile);
        }
        if (BRunner02Tile != null)
        {
            SpawnBlackPiece(BlackRunner, BRunner02Tile);
        }
        if (BRunner03Tile != null)
        {
            SpawnBlackPiece(BlackRunner, BRunner03Tile);
        }
        if (BRunner04Tile != null)
        {
            SpawnBlackPiece(BlackRunner, BRunner04Tile);
        }
        //Spawn 4 AlienGrunts
        if (BGrunt01Tile != null)
        {
            SpawnBlackPiece(BlackGrunt, BGrunt01Tile);
        }
        if (BGrunt02Tile != null)
        {
            SpawnBlackPiece(BlackGrunt, BGrunt02Tile);
        }
        if (BGrunt03Tile != null)
        {
            SpawnBlackPiece(BlackGrunt, BGrunt03Tile);
        }
        if (BGrunt04Tile != null)
        {
            SpawnBlackPiece(BlackGrunt, BGrunt04Tile);
        }
        
    }

    public void SpawnWhitePiece(GameObject piece, GameObject tile)
    {

        if (!Network.isServer && !Network.isClient)
        {
            tile.GetComponent<TileProperties>().UnitOnTile = Instantiate(piece, tile.transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
            tile.GetComponent<TileProperties>().UnitOnTile.GetComponent<pieceMove>().datTile = tile;
            //tile.GetComponent<TileProperties>().Occupied = true;
            WhitePieceCount++;
        }
        else
        {
            tile.GetComponent<TileProperties>().UnitOnTile = Network.Instantiate(piece, tile.transform.position, Quaternion.Euler(0, 0, 0), 1) as GameObject;
            networkView.RPC("addPieceCount", RPCMode.AllBuffered, WhitePieceCount, 0);
        }
    }

    public void SpawnBlackPiece(GameObject piece, GameObject tile)
    {
        if (!Network.isServer && !Network.isClient)
        {
            tile.GetComponent<TileProperties>().UnitOnTile = Instantiate(piece, tile.transform.position, Quaternion.Euler(0, 180, 0)) as GameObject;
            tile.GetComponent<TileProperties>().UnitOnTile.GetComponent<pieceMove>().datTile = tile;
            //tile.GetComponent<TileProperties>().Occupied = true;
            BlackPieceCount++;
        }
        else
        {
            tile.GetComponent<TileProperties>().UnitOnTile = Network.Instantiate(piece, tile.transform.position, Quaternion.Euler(0, 0, 0), 1) as GameObject;
            networkView.RPC("addPieceCount", RPCMode.AllBuffered, BlackPieceCount, 1);
        }
    }

    [RPC]
    void addPieceCount(int pieceCount, int type)
    {
        pieceCount++;
        if (type == 0)
        {
            WhitePieceCount = pieceCount;
        }
        else if (type == 1)
        {
            BlackPieceCount = pieceCount;
        }
    }
}
