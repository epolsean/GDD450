using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpawnBasicUnits : MonoBehaviour {

    public static int BlackPieceCount;
    public static int WhitePieceCount;

    public static int SynthGruntCount;
    public static int SynthScoutCount;
    public static int SynthTankCount;
    public static int SynthRunnerCount;
    public static int SynthBomberCount; 

    public static int AlienGruntCount;
    public static int AlienScoutCount;
    public static int AlienTankCount;
    public static int AlienRunnerCount;
    public static int AlienBomberCount; 

    public GameObject SGtext;
    public GameObject SStext;
    public GameObject STtext;
    public GameObject SRtext;
    public GameObject SBtext; 

    public GameObject AGtext;
    public GameObject AStext;
    public GameObject ATtext;
    public GameObject ARtext;
    public GameObject ABtext; 

    
    public GameObject WhiteTank;
    public GameObject WhiteScout;
    public GameObject WhiteGrunt;
    public GameObject WhiteRunner;
    public GameObject WhiteBomber; 

    public GameObject BlackTank;
    public GameObject BlackGrunt;
    public GameObject BlackRunner;
    public GameObject BlackScout;
    public GameObject BlackBomber; 

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
    public GameObject BBomber01Tile;
    public GameObject BBomber02Tile;
    public GameObject BBomber03Tile;
    public GameObject BBomber04Tile;
    public GameObject WBomber01Tile;
    public GameObject WBomber02Tile;
    public GameObject WBomber03Tile;
    public GameObject WBomber04Tile;

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

    float startTime;

	// Use this for initialization
	void Start () {
        startTime = Time.time;

        EndControllerScript.OrgVic = false;
        EndControllerScript.SynthVic = false;

        StartSpawn();

        SGtext = GameObject.Find("SynthGruntCountText");
        SStext = GameObject.Find("SynthScoutCountText");
        STtext = GameObject.Find("SynthTankCountText");
        SRtext = GameObject.Find("SynthRunnerCountText");

        AGtext = GameObject.Find("AlienGruntCountText");
        AStext = GameObject.Find("AlienScoutCountText");
        ATtext = GameObject.Find("AlienTankCountText");
        ARtext = GameObject.Find("AlienRunnerCountText");
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log("BlackCount: " + BlackPieceCount);
        //Debug.Log("WhiteCount: " + WhitePieceCount);

        //assign values to the text.text
        SGtext.GetComponent<Text>().text = SynthGruntCount.ToString();
        SStext.GetComponent<Text>().text = SynthScoutCount.ToString();
        STtext.GetComponent<Text>().text = SynthTankCount.ToString();
        SRtext.GetComponent<Text>().text = SynthRunnerCount.ToString();

        AGtext.GetComponent<Text>().text = AlienGruntCount.ToString();
        AStext.GetComponent<Text>().text = AlienScoutCount.ToString();
        ATtext.GetComponent<Text>().text = AlienTankCount.ToString();
        ARtext.GetComponent<Text>().text = AlienRunnerCount.ToString();

        if(BlackPieceCount <= 0 && (Time.time - startTime) > 2)
        {
            EndControllerScript.SynthVic = true; 
            EndControllerScript.isEnd = true; 
        }
        else if (WhitePieceCount <= 0 && (Time.time - startTime) > 2)
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
            SynthTankCount++;
        }
        if (WTank02Tile != null)
        {
            SpawnWhitePiece(WhiteTank, WTank02Tile);
            SynthTankCount++;
        }
        if (WTank03Tile != null)
        {
            SpawnWhitePiece(WhiteTank, WTank03Tile);
            SynthTankCount++;
        }
        if (WTank04Tile != null)
        {
            SpawnWhitePiece(WhiteTank, WTank04Tile);
            SynthTankCount++;
        }
        //Spawn 4 SynthScout
        if(WScout01Tile != null)
        {
            SpawnWhitePiece(WhiteScout, WScout01Tile);
            SynthScoutCount++;
        }
        if (WScout02Tile != null)
        {
            SpawnWhitePiece(WhiteScout, WScout02Tile);
            SynthScoutCount++;
        }
        if (WScout03Tile != null)
        {
            SpawnWhitePiece(WhiteScout, WScout03Tile);
            SynthScoutCount++;
        }
        if (WScout04Tile != null)
        {
            SpawnWhitePiece(WhiteScout, WScout04Tile);
            SynthScoutCount++;
        }
        //Spawn 4 SynthRunner
        if (WRunner01Tile != null)
        {
            SpawnWhitePiece(WhiteRunner, WRunner01Tile);
            SynthRunnerCount++;
        }
        if (WRunner02Tile != null)
        {
            SpawnWhitePiece(WhiteRunner, WRunner02Tile);
            SynthRunnerCount++;
        }
        if (WRunner03Tile != null)
        {
            SpawnWhitePiece(WhiteRunner, WRunner03Tile);
            SynthRunnerCount++;
        }
        if (WRunner04Tile != null)
        {
            SpawnWhitePiece(WhiteRunner, WRunner04Tile);
            SynthRunnerCount++;
        }
        //Spawn 4 SynthGrunts
        if (WGrunt01Tile != null)
        {
            SpawnWhitePiece(WhiteGrunt, WGrunt01Tile);
            SynthGruntCount++;
        }
        if (WGrunt02Tile != null)
        {
            SpawnWhitePiece(WhiteGrunt, WGrunt02Tile);
            SynthGruntCount++;
        }
        if (WGrunt03Tile != null)
        {
            SpawnWhitePiece(WhiteGrunt, WGrunt03Tile);
            SynthGruntCount++;
        }
        if (WGrunt04Tile != null)
        {
            SpawnWhitePiece(WhiteGrunt, WGrunt04Tile);
            SynthGruntCount++;
        }

        //Spawn 4 SynthBombers
        if (WBomber01Tile != null)
        {
            SpawnWhitePiece(WhiteBomber, WBomber01Tile);
            SynthBomberCount++;
        }
        if (WBomber02Tile != null)
        {
            SpawnWhitePiece(WhiteBomber, WBomber02Tile);
            SynthBomberCount++;
        }
        if (WBomber03Tile != null)
        {
            SpawnWhitePiece(WhiteBomber, WBomber03Tile);
            SynthBomberCount++;
        }
        if (WBomber04Tile != null)
        {
            SpawnWhitePiece(WhiteBomber, WBomber04Tile);
            SynthBomberCount++;
        }

        //Spawn 2 AlienTanks
        if (BTank01Tile != null)
        {
            SpawnBlackPiece(BlackTank, BTank01Tile);
            AlienTankCount++;
        }
        if (BTank02Tile != null)
        {
            SpawnBlackPiece(BlackTank, BTank02Tile);
            AlienTankCount++;
        }
        if (BTank03Tile != null)
        {
            SpawnBlackPiece(BlackTank, BTank03Tile);
            AlienTankCount++;
        }
        if (BTank04Tile != null)
        {
            SpawnBlackPiece(BlackTank, BTank04Tile);
            AlienTankCount++;
        }
        //Spawn 4 AlienScout
        if (BScout01Tile != null)
        {
            SpawnBlackPiece(BlackScout, BScout01Tile);
            AlienScoutCount++;
        }
        if (BScout02Tile != null)
        {
            SpawnBlackPiece(BlackScout, BScout02Tile);
            AlienScoutCount++;
        }
        if (BScout03Tile != null)
        {
            SpawnBlackPiece(BlackScout, BScout03Tile);
            AlienScoutCount++;
        }
        if (BScout04Tile != null)
        {
            SpawnBlackPiece(BlackScout, BScout04Tile);
            AlienScoutCount++;
        }
        //Spawn 4 AlienRunner
        if (BRunner01Tile != null)
        {
            SpawnBlackPiece(BlackRunner, BRunner01Tile);
            AlienRunnerCount++;
        }
        if (BRunner02Tile != null)
        {
            SpawnBlackPiece(BlackRunner, BRunner02Tile);
            AlienRunnerCount++;
        }
        if (BRunner03Tile != null)
        {
            SpawnBlackPiece(BlackRunner, BRunner03Tile);
            AlienRunnerCount++;
        }
        if (BRunner04Tile != null)
        {
            SpawnBlackPiece(BlackRunner, BRunner04Tile);
            AlienRunnerCount++;
        }
        //Spawn 4 AlienGrunts
        if (BGrunt01Tile != null)
        {
            SpawnBlackPiece(BlackGrunt, BGrunt01Tile);
            AlienGruntCount++;
        }
        if (BGrunt02Tile != null)
        {
            SpawnBlackPiece(BlackGrunt, BGrunt02Tile);
            AlienGruntCount++;
        }
        if (BGrunt03Tile != null)
        {
            SpawnBlackPiece(BlackGrunt, BGrunt03Tile);
            AlienGruntCount++;
        }
        if (BGrunt04Tile != null)
        {
            SpawnBlackPiece(BlackGrunt, BGrunt04Tile);
            AlienGruntCount++;
        }
        //Spawn 4 AlienBombers
        if (BBomber01Tile != null)
        {
            SpawnBlackPiece(BlackBomber, BBomber01Tile);
            AlienBomberCount++;
        }
        if (BBomber02Tile != null)
        {
            SpawnBlackPiece(BlackBomber, BBomber02Tile);
            AlienBomberCount++;
        }
        if (BBomber03Tile != null)
        {
            SpawnBlackPiece(BlackBomber, BBomber03Tile);
            AlienBomberCount++;
        }
        if (BBomber04Tile != null)
        {
            SpawnBlackPiece(BlackBomber, BBomber04Tile);
            AlienBomberCount++;
        }
        
    }

    public void SpawnWhitePiece(GameObject piece, GameObject tile)
    {

        if (!Network.isServer && !Network.isClient)
        {
            tile.GetComponent<OnTileActions>().PieceOnTile = Instantiate(piece, tile.transform.position, Quaternion.Euler(0, 90, 0)) as GameObject;
            tile.GetComponent<OnTileActions>().PieceOnTile.GetComponent<pieceMovementScript>().datTile = tile; 
            WhitePieceCount++;
        }
        else if (Network.isServer)
        {
            tile.GetComponent<OnTileActions>().PieceOnTile = Network.Instantiate(piece, tile.transform.position, Quaternion.Euler(0, 90, 0), 1) as GameObject;
            tile.GetComponent<OnTileActions>().PieceOnTile.GetComponent<pieceMovementScript>().datTile = tile;
            networkView.RPC("addPieceCount", RPCMode.AllBuffered, WhitePieceCount, 0);
        }
        else if (Network.isClient)
        {
            tile.GetComponent<OnTileActions>().PieceOnTile.GetComponent<pieceMovementScript>().datTile = tile;
        }
    }

    public void SpawnBlackPiece(GameObject piece, GameObject tile)
    {
        if (!Network.isServer && !Network.isClient)
        {
            tile.GetComponent<OnTileActions>().PieceOnTile = Instantiate(piece, tile.transform.position, Quaternion.Euler(0, 270, 0)) as GameObject;
            tile.GetComponent<OnTileActions>().PieceOnTile.GetComponent<pieceMovementScript>().datTile = tile;
            //tile.GetComponent<TileProperties>().Occupied = true;
            BlackPieceCount++;
        }
        else if (Network.isServer)
        {
            tile.GetComponent<OnTileActions>().PieceOnTile = Network.Instantiate(piece, tile.transform.position, Quaternion.Euler(0, 270, 0), 1) as GameObject;
            tile.GetComponent<OnTileActions>().PieceOnTile.GetComponent<pieceMovementScript>().datTile = tile;
            networkView.RPC("addPieceCount", RPCMode.AllBuffered, BlackPieceCount, 1);
        }
        else if(Network.isClient)
        {
            tile.GetComponent<OnTileActions>().PieceOnTile.GetComponent<pieceMovementScript>().datTile = tile;
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
