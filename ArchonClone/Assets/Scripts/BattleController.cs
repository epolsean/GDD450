using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BattleController : MonoBehaviour {

    public GameObject attackerIcon;
    public GameObject defenderIcon;
    public GameObject vsText;

    GameObject attackerTile;
    GameObject defenderTile;

    string Loser;
    bool BattleOver = false;
    //Vector2 attackerIconStarPos;
    //Vector2 defenderIconStarPos;
    enum BattleState {PreBattle,Battle,PostBattle};
    BattleState partOfBattle;

    int starting = 1;

    float attackerLevel;
    float defenderLevel;

	// Use this for initialization
	void Start () 
    {
        attackerLevel = attackerTile.GetComponent<OnTileActions>().TilePowerLevel + attackerTile.GetComponent<OnTileActions>().PieceOnTile.GetComponent<PiecePropScript>().Damage;
        defenderLevel = defenderTile.GetComponent<OnTileActions>().TilePowerLevel + defenderTile.GetComponent<OnTileActions>().PieceOnTile.GetComponent<PiecePropScript>().Damage;
        
        starting = 1;
        partOfBattle = BattleState.PreBattle;
        attackerIcon.GetComponent<RectTransform>().anchoredPosition = new Vector2(GetComponent<RectTransform>().sizeDelta.x / -4, GetComponent<RectTransform>().sizeDelta.y / 2 + attackerIcon.GetComponent<RectTransform>().sizeDelta.y/2);
        defenderIcon.GetComponent<RectTransform>().anchoredPosition = new Vector2(GetComponent<RectTransform>().sizeDelta.x / 4, -1*GetComponent<RectTransform>().sizeDelta.y / 2 - defenderIcon.GetComponent<RectTransform>().sizeDelta.y/2);
	}

    void OnEnable()
    {
        attackerLevel = attackerTile.GetComponent<OnTileActions>().TilePowerLevel + attackerTile.GetComponent<OnTileActions>().PieceOnTile.GetComponent<PiecePropScript>().Damage;
        defenderLevel = defenderTile.GetComponent<OnTileActions>().TilePowerLevel + defenderTile.GetComponent<OnTileActions>().PieceOnTile.GetComponent<PiecePropScript>().Damage;

        starting = 1;
        partOfBattle = BattleState.PreBattle;
        attackerIcon.GetComponent<RectTransform>().anchoredPosition = new Vector2(GetComponent<RectTransform>().sizeDelta.x / -4, GetComponent<RectTransform>().sizeDelta.y / 2 + attackerIcon.GetComponent<RectTransform>().sizeDelta.y / 2);
        defenderIcon.GetComponent<RectTransform>().anchoredPosition = new Vector2(GetComponent<RectTransform>().sizeDelta.x / 4, -1 * GetComponent<RectTransform>().sizeDelta.y / 2 - defenderIcon.GetComponent<RectTransform>().sizeDelta.y / 2);
    }
	
	// Update is called once per frame
	void Update () 
    {
        if (partOfBattle == BattleState.PreBattle)
        {
            if (starting == 1)
            {
                if (Vector2.Distance(attackerIcon.GetComponent<RectTransform>().anchoredPosition, new Vector2(attackerIcon.GetComponent<RectTransform>().anchoredPosition.x, (int)GetComponent<RectTransform>().sizeDelta.y / 4)) > 1)
                {
                    //vsText.GetComponent<Text>().fontSize += 1;
                    defenderIcon.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(defenderIcon.GetComponent<RectTransform>().anchoredPosition, new Vector2(defenderIcon.GetComponent<RectTransform>().anchoredPosition.x, (int)-GetComponent<RectTransform>().sizeDelta.y / 4), Time.deltaTime);
                    attackerIcon.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(attackerIcon.GetComponent<RectTransform>().anchoredPosition, new Vector2(attackerIcon.GetComponent<RectTransform>().anchoredPosition.x, (int)GetComponent<RectTransform>().sizeDelta.y / 4), Time.deltaTime);
                }
                else
                {
                    starting++;
                }
            }
            else if(starting == 2)
            {
                if (Vector2.Distance(attackerIcon.GetComponent<RectTransform>().anchoredPosition, new Vector2(attackerIcon.GetComponent<RectTransform>().anchoredPosition.x, (int)-GetComponent<RectTransform>().sizeDelta.y )) > 10)
                {
                    defenderIcon.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(defenderIcon.GetComponent<RectTransform>().anchoredPosition, new Vector2(defenderIcon.GetComponent<RectTransform>().anchoredPosition.x, (int)GetComponent<RectTransform>().sizeDelta.y ), Time.deltaTime);
                    attackerIcon.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(attackerIcon.GetComponent<RectTransform>().anchoredPosition, new Vector2(attackerIcon.GetComponent<RectTransform>().anchoredPosition.x, (int)-GetComponent<RectTransform>().sizeDelta.y ), Time.deltaTime);
                }
                else
                {
                    starting++;
                }
            }
            else
            {
                attackerIcon.SetActive(false);
                defenderIcon.SetActive(false);
                //vsText.SetActive(false);
                partOfBattle = BattleState.Battle;
            }
        }
        else if (partOfBattle == BattleState.Battle)
        {
            SimulateBattle();
        }
        else if (partOfBattle == BattleState.PostBattle)
        {
            if (Loser == "attacker")
            {
                Debug.Log("attacker lost the fight");
                Destroy(attackerTile.GetComponent<OnTileActions>().PieceOnTile);
                attackerTile.GetComponent<OnTileActions>().PieceOnTile = null;
            }
            else if (Loser == "defender")
            {
                Debug.Log("defender lost the fight");
                Destroy(defenderTile.GetComponent<OnTileActions>().PieceOnTile);
                defenderTile.GetComponent<OnTileActions>().PieceOnTile = attackerTile.GetComponent<OnTileActions>().PieceOnTile;
                attackerTile.GetComponent<OnTileActions>().PieceOnTile = null;
            }
            else if (Loser == "both")
            {
                Debug.Log("both lost the fight, everyone is dead");
                Destroy(attackerTile.GetComponent<OnTileActions>().PieceOnTile);
                Destroy(defenderTile.GetComponent<OnTileActions>().PieceOnTile);
                attackerTile.GetComponent<OnTileActions>().PieceOnTile = null;
                defenderTile.GetComponent<OnTileActions>().PieceOnTile = null;
            }
            this.enabled = false;
            GameObject.Find("TurnController").GetComponent<OnTurnActions>().isFighting = false;
            //GameObject.Find("TurnController").GetComponent<OnTurnActions>().ResetController();
            GameObject.Find("TurnController").GetComponent<OnTurnActions>().EndOfTurn();
            GameObject.Find("TurnController").GetComponent<OnTurnActions>().ResetController();
        }
    }

    void SimulateBattle()
    {
        //Add code to simulate battle and determine winner
        if (Random.Range(0, 10000/attackerLevel) < attackerLevel) //This will be if the player who is attacking the tile wins on the attack
        {
            if (Random.Range(0, 10000 / defenderLevel) < defenderLevel/2) //This will be if the player who is defending the tile wins on the attack
            {
                Loser = "both";
            }
            else
            {
                Loser = "defender";
                PlayerPrefs.SetInt("KillsWithAlienGrunt", PlayerPrefs.GetInt("KillsWithAlienGrunt") + 1);
            }
            partOfBattle = BattleState.PostBattle;
        }
        else if (Random.Range(0, 10000 / defenderLevel) < defenderLevel) //This will be if the player who is defending the tile wins on the attack
        {
            if (Random.Range(0, 10000/attackerLevel) < attackerLevel/2) //This will be if the player who is attcking the tile wins on the attack
            {
                Loser = "both";
            }
            else
            {
                Loser = "attacker";
            }
            partOfBattle = BattleState.PostBattle;
        }
    }

    public void SetAttackerTile(GameObject datAttacker)
    {
        attackerTile = datAttacker;
    }

    public void SetDefenderTile(GameObject datDefender)
    {
        defenderTile = datDefender;
    }
}
