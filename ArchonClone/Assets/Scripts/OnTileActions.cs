using UnityEngine;
using System.Collections;

public class OnTileActions : MonoBehaviour {

    public GameObject PieceOnTile = null;
    public GameObject AtkPiece = null; 
    public GameObject TileNode;
    public GameObject TurnController; 
    public Texture AlienTexture;
    public Texture SynthTexture;
    public Texture NuturalTexture = null; 
    public int TilePowerLevel = 0;
    public enum TileType { Alien, Synth, Nutural };
    public TileType TileState; 
    public bool IsNutural = false;
    public bool isSelected = false;
    public int RandomRotation; 
    
    // Use this for initialization
	void Start () {
        RandomRotation = Random.Range(1, 4);
        TilePowerLevel = 1; 
        if(RandomRotation == 1)
        {
            this.transform.eulerAngles = new Vector3(0, 30, 0); 
        }
        else if (RandomRotation == 2)
        {
            this.transform.eulerAngles = new Vector3(0, 90, 0);
        }
        else if (RandomRotation == 3)
        {
            this.transform.eulerAngles = new Vector3(0, 150, 0);
        }
        else if (RandomRotation == 4)
        {
            this.transform.eulerAngles = new Vector3(0, 210, 0);
        }
        
        if (TileState == TileType.Alien)
        {
            this.renderer.material.mainTexture = AlienTexture;
        }
        else if (TileState == TileType.Synth)
        {
            this.renderer.material.mainTexture = SynthTexture;
        }
        else
        {
            this.renderer.material.mainTexture = NuturalTexture;
        }

        TurnController = GameObject.Find("TurnController"); 
	}
	
	// Update is called once per frame
	void Update () {
        if(TileState == TileType.Alien)
        {
            this.renderer.material.mainTexture = AlienTexture;
        }
        else if( TileState == TileType.Synth)
        {
            this.renderer.material.mainTexture = SynthTexture;
        }
        else
        {
            this.renderer.material.mainTexture = NuturalTexture;
        }

        if(IsNutural)
        {
            this.renderer.material.mainTexture = null;
        }
	}
    /*called when you hover over a tile, it will assign the tile 
     * to the TurnController's OnHoverTile and OnHoverPiece variables
     * as well as change the color of the entire GameObject to cyan/blue
     */ 
    void OnMouseOver()
    {
        TileNode.SetActive(true);
        GridManager.rescan = true;
        TurnController.GetComponent<OnTurnActions>().OnHoverTile = this.gameObject;
        TurnController.GetComponent<OnTurnActions>().OnHoverPiece = this.PieceOnTile;
        if(isSelected == false)
        {
            if(TurnController.GetComponent<OnTurnActions>().hasSelectedPiece == false)
            {
                this.renderer.material.color = Color.cyan; 
            }
            
            if(TurnController.GetComponent<OnTurnActions>().hasSelectedPiece)
            {
                 
                if(TurnController.GetComponent<OnTurnActions>().isGenPath == false)
                {
                    Invoke("GenHoverPath", 1/100);
                    
                }
                else
                {
                    TurnController.GetComponent<OnTurnActions>().drawnPath = false; 
                    if (TurnController.GetComponent<OnTurnActions>().SelectedPiece.GetComponent<pieceMovementScript>().path.vectorPath.Count <= TurnController.GetComponent<OnTurnActions>().SelectedPiece.GetComponent<pieceMovementScript>().MaxPathNodes)
                    {
                        this.renderer.material.color = Color.green;
                    }
                    else
                    {
                        this.renderer.material.color = Color.red;
                    }
                }
                
            }
        }
    }

    /*called when you move your mouse cursor off of a game object, it
     * will set the TurnControllers variables back to null as well 
     * as reset the color of the entire GameObject to white
     */ 
    void OnMouseExit()
    {
        if(PieceOnTile != null)
        {
            if(isSelected == false)
            {
                TileNode.SetActive(false);
                GridManager.rescan = true;
            }
        }
        TurnController.GetComponent<OnTurnActions>().isGenPath = false; 
        TurnController.GetComponent<OnTurnActions>().OnHoverTile = null;
        TurnController.GetComponent<OnTurnActions>().OnHoverPiece = null;
        if (isSelected == false)
        {
            this.renderer.material.color = Color.white;
        } 
        if(TurnController.GetComponent<OnTurnActions>().hasSelectedPiece)
        {
            TurnController.GetComponent<OnTurnActions>().resetAllTiles(); 
        }
    }

    public void ResetTile()
    {
        this.renderer.material.color = Color.white; 
    }

    public void GenHoverPath()
    {
        TurnController.GetComponent<OnTurnActions>().GenPath();
    }

    public void updateTileLevel()
    {
        print("updatingTile");
        print("TileState: " + TileState.ToString());
        if (PieceOnTile != null)
        {
            //begin checking upgrade/downgrade options
            print("there is a unit on the tile");

            if (TileState.ToString() == "Alien")
            {
                print("tile is of type: Alien");
                if (PieceOnTile.tag == "Black")
                {
                    if (TilePowerLevel < 4)
                    {
                        TilePowerLevel++;
                        print("alien TIle up");
                    }
                }
                else
                {
                    if (TilePowerLevel > 1)
                    {
                        TilePowerLevel--;
                        print("alien Tile down");
                    }
                    else
                    {
                        TileState = TileType.Nutural;
                    }
                }
            }
            else if (TileState.ToString() == "Synth")
            {
                print("tile is of type: Synth");
                if (PieceOnTile.tag == "White")
                {
                    if (TilePowerLevel < 4)
                    {
                        TilePowerLevel++;
                        print("Robot Tile Up");
                    }
                }
                else
                {
                    if (TilePowerLevel > 1)
                    {
                        TilePowerLevel--;
                        print("Robot Tile Down");
                    }
                    else
                    {
                        TileState = TileType.Nutural;
                    }
                }
            }
            else//structure if tile is in a nutural state
            {
                if (PieceOnTile.tag == "Black")
                {
                    TileState = TileType.Alien;
                    TilePowerLevel++;
                }
                else
                {
                    TileState = TileType.Synth;
                    TilePowerLevel++;
                }
            }
        }
    }
}
