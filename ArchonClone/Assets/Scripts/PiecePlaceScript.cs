using UnityEngine;
using System.Collections;

public class PiecePlaceScript : MonoBehaviour {

    public static GameObject White01Tile;
    public static GameObject White02Tile;
    public static GameObject White03Tile;
    public static GameObject White04Tile;
    public static GameObject White05Tile;
    public static GameObject White06Tile;
    public static GameObject Black01Tile;
    public static GameObject Black02Tile;
    public static GameObject Black03Tile;
    public static GameObject Black04Tile;
    public static GameObject Black05Tile;
    public static GameObject Black06Tile;

    public GameObject TestWhite01Tile;
    

    public static bool setPieces = false;
    public static bool isStart = true;

    public GameObject SpawnController;

    void Awake()
    {
        //DontDestroyOnLoad(this);
        isStart = true; 
    }
    
    // Use this for initialization
	void Start () {
        SpawnController = GameObject.Find("PieceSpwnController");
        //setPieces = true; 
	}
	
	// Update is called once per frame
	void Update () {
	    if(setPieces)
        {
            setPieces = false;
            SpawnPieces();
        }
        TestWhite01Tile = White01Tile;
	}

    public void SpawnPieces()
    {
        //Spawn 2 White Units at their tiles
        SpawnController.GetComponent<SpawnBasicUnits>().SpawnWhitePiece(SpawnController.GetComponent<SpawnBasicUnits>().WhitePawn, White01Tile);
        SpawnController.GetComponent<SpawnBasicUnits>().SpawnWhitePiece(SpawnController.GetComponent<SpawnBasicUnits>().White02, White02Tile);
        SpawnController.GetComponent<SpawnBasicUnits>().SpawnWhitePiece(SpawnController.GetComponent<SpawnBasicUnits>().WhiteGrunt, White03Tile);
        SpawnController.GetComponent<SpawnBasicUnits>().SpawnWhitePiece(SpawnController.GetComponent<SpawnBasicUnits>().WhiteRunner, White04Tile);
        //Spawn 2 Black Unity at their tiles
        SpawnController.GetComponent<SpawnBasicUnits>().SpawnBlackPiece(SpawnController.GetComponent<SpawnBasicUnits>().BlackPawn, Black01Tile);
        SpawnController.GetComponent<SpawnBasicUnits>().SpawnBlackPiece(SpawnController.GetComponent<SpawnBasicUnits>().Black02, Black02Tile);
        SpawnController.GetComponent<SpawnBasicUnits>().SpawnBlackPiece(SpawnController.GetComponent<SpawnBasicUnits>().BlackScout, Black03Tile);
        SpawnController.GetComponent<SpawnBasicUnits>().SpawnBlackPiece(SpawnController.GetComponent<SpawnBasicUnits>().BlackRunner, Black04Tile);
    }
}
