using UnityEngine;
using System.Collections;

public class OnTileActions : MonoBehaviour {

    public GameObject PieceOnTile = null;
    public GameObject AtkPiece = null; 
    public GameObject TileNode;
    public Texture AlienTexture;
    public Texture SynthTexture;
    public Texture NuturalTexture = null; 
    public int TilePowerBooster = 0;
    public enum TileType { Alien, Synth, Nutural };
    public TileType TileState; 
    public bool IsNutural = false;
    
    // Use this for initialization
	void Start () {
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
}
