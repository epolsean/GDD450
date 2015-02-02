using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DominationController : MonoBehaviour {

    public GameObject alienPercent;
    public GameObject robotPercent;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void UpdatePercentUI()
    {
        int TotalTiles = 0;
        int neutralTiles = 0;
        int alienTiles = 0;
        int robotTiles = 0;

        for (int i = 0; i < OnTurnActions.allTiles.Length; i++)
        {
            if(OnTurnActions.allTiles[i].GetComponent<OnTileActions>().TileState == OnTileActions.TileType.Alien)
            {
                alienTiles++;
            }
            else if(OnTurnActions.allTiles[i].GetComponent<OnTileActions>().TileState == OnTileActions.TileType.Synth)
            {
                robotTiles++;
            }
            else if(OnTurnActions.allTiles[i].GetComponent<OnTileActions>().TileState == OnTileActions.TileType.Nutural)
            {
                neutralTiles++;
            }
            TotalTiles++;
        }
        alienPercent.GetComponent<Slider>().value = (float)alienTiles / TotalTiles;
        robotPercent.GetComponent<Slider>().value = (float)robotTiles / TotalTiles;
        if (alienPercent.GetComponent<Slider>().value >= 0.85f)
        {
            BattleStats.winner = "Aliens";
        }
        else if (robotPercent.GetComponent<Slider>().value >= 0.85f)
        {
            BattleStats.winner = "Robots";
        }
    }
}
