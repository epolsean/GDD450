using UnityEngine;
using System.Collections;

public class PiecePlaceScript : MonoBehaviour {

    public static GameObject White01Tile;
    public static GameObject White02Tile;
    public static GameObject Black01Tile;
    public static GameObject Black02Tile;

    public GameObject SpawnController;

    // Use this for initialization
	void Start () {
        SpawnController = GameObject.Find("PieceSpwnController");
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void SpawnPieces()
    {
        //Spawn 2 White Units at their tiles
        SpawnController.GetComponent<SpawnBasicUnits>().SpawnWhitePiece(SpawnController.GetComponent<SpawnBasicUnits>().WhitePawn, White01Tile);
        SpawnController.GetComponent<SpawnBasicUnits>().SpawnWhitePiece(SpawnController.GetComponent<SpawnBasicUnits>().White02, White02Tile);
        //Spawn 2 Black Unity at their tiles
        SpawnController.GetComponent<SpawnBasicUnits>().SpawnBlackPiece(SpawnController.GetComponent<SpawnBasicUnits>().BlackPawn, Black01Tile);
        SpawnController.GetComponent<SpawnBasicUnits>().SpawnBlackPiece(SpawnController.GetComponent<SpawnBasicUnits>().Black02, Black02Tile);
    }
}
