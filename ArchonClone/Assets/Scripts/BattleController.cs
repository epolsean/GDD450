using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BattleController : MonoBehaviour {

    public GameObject attackerIcon;
    public GameObject defenderIcon;
    public GameObject vsText;

    public GameObject piecePanel1;
    public GameObject piecePanel2;
    public GameObject whoseTurn;

    GameObject attackerTile;
    GameObject defenderTile;

    string Loser;
    //Vector2 attackerIconStarPos;
    //Vector2 defenderIconStarPos;
    enum BattleState {PreBattle,Battle,PostBattle};
    BattleState partOfBattle;

    int starting = 1;

    double attackerLevel;
    double defenderLevel;

    
	// Use this for initialization
	void Start () 
    {
        switch (attackerTile.GetComponent<OnTileActions>().PieceOnTile.GetComponent<PiecePropScript>().PieceName)
        {
            case "Organic Grunt":
                attackerIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("UnitIcon_OGrunt");
                break;
            case "Organic Runner":
                attackerIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("UnitIcon_ORunner");
                break;
            case "Organic Tank":
                attackerIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("UnitIcon_OTank");
                break;
            case "Organic Scout":
                attackerIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("UnitIcon_OScout");
                break;
            case "Organic Bomber":
                attackerIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("UnitIcon_SBlank");
                break;
            case "Synthetic Grunt":
                attackerIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("UnitIcon_SGrunt");
                break;
            case "Synthetic Runner":
                attackerIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("UnitIcon_SRunner");
                break;
            case "Synthetic Tank":
                attackerIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("UnitIcon_STank");
                break;
            case "Synthetic Scout":
                attackerIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("UnitIcon_SScout");
                break;
            case "Synthetic Bomber":
                attackerIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("UnitIcon_SBlank");
                break;
        }
        switch (defenderTile.GetComponent<OnTileActions>().PieceOnTile.GetComponent<PiecePropScript>().PieceName)
        {
            case "Organic Grunt":
                defenderIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("UnitIcon_OGrunt");
                break;
            case "Organic Runner":
                defenderIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("UnitIcon_ORunner");
                break;
            case "Organic Tank":
                defenderIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("UnitIcon_OTank");
                break;
            case "Organic Scout":
                defenderIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("UnitIcon_OScout");
                break;
            case "Organic Bomber":
                defenderIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("UnitIcon_SBlank");
                break;
            case "Synthetic Grunt":
                defenderIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("UnitIcon_SGrunt");
                break;
            case "Synthetic Runner":
                defenderIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("UnitIcon_SRunner");
                break;
            case "Synthetic Tank":
                defenderIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("UnitIcon_STank");
                break;
            case "Synthetic Scout":
                defenderIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("UnitIcon_SScout");
                break;
            case "Synthetic Bomber":
                defenderIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("UnitIcon_SBlank");
                break;
        }
        attackerIcon.SetActive(true);
        defenderIcon.SetActive(true);
        //vsText.SetActive(true);
        attackerLevel = attackerTile.GetComponent<OnTileActions>().PieceOnTile.GetComponent<PiecePropScript>().PowerLevel;
        defenderLevel = defenderTile.GetComponent<OnTileActions>().TilePowerLevel*25 + defenderTile.GetComponent<OnTileActions>().PieceOnTile.GetComponent<PiecePropScript>().PowerLevel;
        
        starting = 1;
        partOfBattle = BattleState.PreBattle;
        attackerIcon.GetComponent<RectTransform>().anchoredPosition = new Vector2(GetComponent<RectTransform>().sizeDelta.x / -4, GetComponent<RectTransform>().sizeDelta.y / 2 + attackerIcon.GetComponent<RectTransform>().sizeDelta.y/2);
        defenderIcon.GetComponent<RectTransform>().anchoredPosition = new Vector2(GetComponent<RectTransform>().sizeDelta.x / 4, -1*GetComponent<RectTransform>().sizeDelta.y / 2 - defenderIcon.GetComponent<RectTransform>().sizeDelta.y/2);
	}

    void OnEnable()
    {
        switch (attackerTile.GetComponent<OnTileActions>().PieceOnTile.GetComponent<PiecePropScript>().PieceName)
        {
            case "Organic Grunt":
                attackerIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("UnitIcon_OGrunt");
                break;
            case "Organic Runner":
                attackerIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("UnitIcon_ORunner");
                break;
            case "Organic Tank":
                attackerIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("UnitIcon_OTank");
                break;
            case "Organic Scout":
                attackerIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("UnitIcon_OScout");
                break;
            case "Organic Bomber":
                attackerIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("UnitIcon_SBlank");
                break;
            case "Synthetic Grunt":
                attackerIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("UnitIcon_SGrunt");
                break;
            case "Synthetic Runner":
                attackerIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("UnitIcon_SRunner");
                break;
            case "Synthetic Tank":
                attackerIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("UnitIcon_STank");
                break;
            case "Synthetic Scout":
                attackerIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("UnitIcon_SScout");
                break;
            case "Synthetic Bomber":
                attackerIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("UnitIcon_SBlank");
                break;
        }
        switch (defenderTile.GetComponent<OnTileActions>().PieceOnTile.GetComponent<PiecePropScript>().PieceName)
        {
            case "Organic Grunt":
                defenderIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("UnitIcon_OGrunt");
                break;
            case "Organic Runner":
                defenderIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("UnitIcon_ORunner");
                break;
            case "Organic Tank":
                defenderIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("UnitIcon_OTank");
                break;
            case "Organic Scout":
                defenderIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("UnitIcon_OScout");
                break;
            case "Organic Bomber":
                defenderIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("UnitIcon_SBlank");
                break;
            case "Synthetic Grunt":
                defenderIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("UnitIcon_SGrunt");
                break;
            case "Synthetic Runner":
                defenderIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("UnitIcon_SRunner");
                break;
            case "Synthetic Tank":
                defenderIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("UnitIcon_STank");
                break;
            case "Synthetic Scout":
                defenderIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("UnitIcon_SScout");
                break;
            case "Synthetic Bomber":
                defenderIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("UnitIcon_SBlank");
                break;
        }
        attackerIcon.SetActive(true);
        defenderIcon.SetActive(true);
        //vsText.SetActive(true);
        attackerLevel = attackerTile.GetComponent<OnTileActions>().PieceOnTile.GetComponent<PiecePropScript>().PowerLevel;
        defenderLevel = defenderTile.GetComponent<OnTileActions>().TilePowerLevel*25 + defenderTile.GetComponent<OnTileActions>().PieceOnTile.GetComponent<PiecePropScript>().PowerLevel;

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
            else if(starting == 4)
            {
                /*if (Vector2.Distance(attackerIcon.GetComponent<RectTransform>().anchoredPosition, new Vector2(attackerIcon.GetComponent<RectTransform>().anchoredPosition.x, (int)-GetComponent<RectTransform>().sizeDelta.y )) > 10)
                {
                    defenderIcon.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(defenderIcon.GetComponent<RectTransform>().anchoredPosition, new Vector2(defenderIcon.GetComponent<RectTransform>().anchoredPosition.x, (int)GetComponent<RectTransform>().sizeDelta.y ), Time.deltaTime);
                    attackerIcon.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(attackerIcon.GetComponent<RectTransform>().anchoredPosition, new Vector2(attackerIcon.GetComponent<RectTransform>().anchoredPosition.x, (int)-GetComponent<RectTransform>().sizeDelta.y ), Time.deltaTime);
                }
                else
                {
                    starting++;
                }*/
            }
            else if(starting == 2)
            {
                //attackerIcon.SetActive(false);
                //defenderIcon.SetActive(false);
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
            attackerIcon.SetActive(false);
            defenderIcon.SetActive(false);
            piecePanel1.SetActive(true);
            piecePanel2.SetActive(true);
            whoseTurn.SetActive(true);
            GameObject.Find("TurnController").GetComponent<OnTurnActions>().isFighting = false;
            GameObject.Find("TurnController").GetComponent<OnTurnActions>().EndOfBattle();
            GameObject.Find("TurnController").GetComponent<OnTurnActions>().ResetController();
        }
    }

    void SimulateBattle()
    {
        Debug.Log("attacker level  : " + attackerLevel);
        Debug.Log("defender level  : " + defenderLevel);
        double ratio = 0;
        if (defenderLevel > attackerLevel)
        {
            ratio = attackerLevel / defenderLevel;
        }
        else
        {
            ratio = defenderLevel / attackerLevel;
        }
        //Add code to simulate battle and determine winner
        if (Random.Range(0, (int)(attackerLevel)) < (int)attackerLevel*ratio) //This will be if the player who is attacking the tile wins on the attack
        {
            if (Random.Range(0, (int)(defenderLevel)) < (int)defenderLevel*ratio/2) //This will be if the player who is defending the tile wins on the attack
            {
                Loser = "both";
                SpawnBasicUnits.BlackPieceCount--;
                SpawnBasicUnits.WhitePieceCount--;
                switch (attackerTile.GetComponent<OnTileActions>().PieceOnTile.GetComponent<PiecePropScript>().PieceName)
                {
                    case "Organic Grunt":
                        SpawnBasicUnits.AlienGruntCount--;
                        break;
                    case "Organic Runner":
                        SpawnBasicUnits.AlienRunnerCount--;
                        break;
                    case "Organic Tank":
                        SpawnBasicUnits.AlienTankCount--;
                        break;
                    case "Organic Scout":
                        SpawnBasicUnits.AlienScoutCount--;
                        break;
                    case "Organic Bomber":
                        SpawnBasicUnits.AlienBomberCount--;
                        break;
                    case "Synthetic Grunt":
                        SpawnBasicUnits.SynthGruntCount--;
                        break;
                    case "Synthetic Runner":
                        SpawnBasicUnits.SynthRunnerCount--;
                        break;
                    case "Synthetic Tank":
                        SpawnBasicUnits.SynthTankCount--;
                        break;
                    case "Synthetic Scout":
                        SpawnBasicUnits.SynthScoutCount--;
                        break;
                    case "Synthetic Bomber":
                        SpawnBasicUnits.SynthBomberCount--;
                        break;
                }
                switch (defenderTile.GetComponent<OnTileActions>().PieceOnTile.GetComponent<PiecePropScript>().PieceName)
                {
                    case "Organic Grunt":
                        SpawnBasicUnits.AlienGruntCount--;
                        break;
                    case "Organic Runner":
                        SpawnBasicUnits.AlienRunnerCount--;
                        break;
                    case "Organic Tank":
                        SpawnBasicUnits.AlienTankCount--;
                        break;
                    case "Organic Scout":
                        SpawnBasicUnits.AlienScoutCount--;
                        break;
                    case "Organic Bomber":
                        SpawnBasicUnits.AlienBomberCount--;
                        break;
                    case "Synthetic Grunt":
                        SpawnBasicUnits.SynthGruntCount--;
                        break;
                    case "Synthetic Runner":
                        SpawnBasicUnits.SynthRunnerCount--;
                        break;
                    case "Synthetic Tank":
                        SpawnBasicUnits.SynthTankCount--;
                        break;
                    case "Synthetic Scout":
                        SpawnBasicUnits.SynthScoutCount--;
                        break;
                    case "Synthetic Bomber":
                        SpawnBasicUnits.SynthBomberCount--;
                        break;
                }
            }
            else
            {
                Loser = "defender";
                if (defenderTile.GetComponent<OnTileActions>().PieceOnTile.tag == "White")
                {
                    SpawnBasicUnits.WhitePieceCount--;
                    switch (defenderTile.GetComponent<OnTileActions>().PieceOnTile.GetComponent<PiecePropScript>().PieceName)
                    {
                        case "Organic Grunt":
                            SpawnBasicUnits.AlienGruntCount--;
                            break;
                        case "Organic Runner":
                            SpawnBasicUnits.AlienRunnerCount--;
                            break;
                        case "Organic Tank":
                            SpawnBasicUnits.AlienTankCount--;
                            break;
                        case "Organic Scout":
                            SpawnBasicUnits.AlienScoutCount--;
                            break;
                        case "Organic Bomber":
                            SpawnBasicUnits.AlienBomberCount--;
                            break;
                        case "Synthetic Grunt":
                            SpawnBasicUnits.SynthGruntCount--;
                            break;
                        case "Synthetic Runner":
                            SpawnBasicUnits.SynthRunnerCount--;
                            break;
                        case "Synthetic Tank":
                            SpawnBasicUnits.SynthTankCount--;
                            break;
                        case "Synthetic Scout":
                            SpawnBasicUnits.SynthScoutCount--;
                            break;
                        case "Synthetic Bomber":
                            SpawnBasicUnits.SynthBomberCount--;
                            break;
                    }
                }
                else
                {
                    SpawnBasicUnits.BlackPieceCount--;
                    switch (attackerTile.GetComponent<OnTileActions>().PieceOnTile.GetComponent<PiecePropScript>().PieceName)
                    {
                        case "Organic Grunt":
                            SpawnBasicUnits.AlienGruntCount--;
                            break;
                        case "Organic Runner":
                            SpawnBasicUnits.AlienRunnerCount--;
                            break;
                        case "Organic Tank":
                            SpawnBasicUnits.AlienTankCount--;
                            break;
                        case "Organic Scout":
                            SpawnBasicUnits.AlienScoutCount--;
                            break;
                        case "Organic Bomber":
                            SpawnBasicUnits.AlienBomberCount--;
                            break;
                        case "Synthetic Grunt":
                            SpawnBasicUnits.SynthGruntCount--;
                            break;
                        case "Synthetic Runner":
                            SpawnBasicUnits.SynthRunnerCount--;
                            break;
                        case "Synthetic Tank":
                            SpawnBasicUnits.SynthTankCount--;
                            break;
                        case "Synthetic Scout":
                            SpawnBasicUnits.SynthScoutCount--;
                            break;
                        case "Synthetic Bomber":
                            SpawnBasicUnits.SynthBomberCount--;
                            break;
                    }
                }
                string playerPrefString = DetermineWhatUnitWon();
                PlayerPrefs.SetInt(playerPrefString, PlayerPrefs.GetInt(playerPrefString) + 1);
            }
            partOfBattle = BattleState.PostBattle;
        }
        else if (Random.Range(0, (int)(defenderLevel)) < (int)defenderLevel*ratio) //This will be if the player who is defending the tile wins on the attack
        {
            if (Random.Range(0, (int)(attackerLevel)) < (int)attackerLevel*ratio/2) //This will be if the player who is attcking the tile wins on the attack
            {
                Loser = "both";
                SpawnBasicUnits.BlackPieceCount--;
                SpawnBasicUnits.WhitePieceCount--;
                switch (attackerTile.GetComponent<OnTileActions>().PieceOnTile.GetComponent<PiecePropScript>().PieceName)
                {
                    case "Organic Grunt":
                        SpawnBasicUnits.AlienGruntCount--;
                        break;
                    case "Organic Runner":
                        SpawnBasicUnits.AlienRunnerCount--;
                        break;
                    case "Organic Tank":
                        SpawnBasicUnits.AlienTankCount--;
                        break;
                    case "Organic Scout":
                        SpawnBasicUnits.AlienScoutCount--;
                        break;
                    case "Organic Bomber":
                        SpawnBasicUnits.AlienBomberCount--;
                        break;
                    case "Synthetic Grunt":
                        SpawnBasicUnits.SynthGruntCount--;
                        break;
                    case "Synthetic Runner":
                        SpawnBasicUnits.SynthRunnerCount--;
                        break;
                    case "Synthetic Tank":
                        SpawnBasicUnits.SynthTankCount--;
                        break;
                    case "Synthetic Scout":
                        SpawnBasicUnits.SynthScoutCount--;
                        break;
                    case "Synthetic Bomber":
                        SpawnBasicUnits.SynthBomberCount--;
                        break;
                }
                switch (defenderTile.GetComponent<OnTileActions>().PieceOnTile.GetComponent<PiecePropScript>().PieceName)
                {
                    case "Organic Grunt":
                        SpawnBasicUnits.AlienGruntCount--;
                        break;
                    case "Organic Runner":
                        SpawnBasicUnits.AlienRunnerCount--;
                        break;
                    case "Organic Tank":
                        SpawnBasicUnits.AlienTankCount--;
                        break;
                    case "Organic Scout":
                        SpawnBasicUnits.AlienScoutCount--;
                        break;
                    case "Organic Bomber":
                        SpawnBasicUnits.AlienBomberCount--;
                        break;
                    case "Synthetic Grunt":
                        SpawnBasicUnits.SynthGruntCount--;
                        break;
                    case "Synthetic Runner":
                        SpawnBasicUnits.SynthRunnerCount--;
                        break;
                    case "Synthetic Tank":
                        SpawnBasicUnits.SynthTankCount--;
                        break;
                    case "Synthetic Scout":
                        SpawnBasicUnits.SynthScoutCount--;
                        break;
                    case "Synthetic Bomber":
                        SpawnBasicUnits.SynthBomberCount--;
                        break;
                }
            }
            else
            {
                Loser = "attacker";
                if (attackerTile.GetComponent<OnTileActions>().PieceOnTile.tag == "White")
                {
                    SpawnBasicUnits.WhitePieceCount--;
                    switch(attackerTile.GetComponent<OnTileActions>().PieceOnTile.GetComponent<PiecePropScript>().PieceName)
                    {
                        case "Organic Grunt":
                            SpawnBasicUnits.AlienGruntCount--;
                            break;
                        case "Organic Runner":
                            SpawnBasicUnits.AlienRunnerCount--;
                            break;
                        case "Organic Tank":
                            SpawnBasicUnits.AlienTankCount--;
                            break;
                        case "Organic Scout":
                            SpawnBasicUnits.AlienScoutCount--;
                            break;
                        case "Organic Bomber":
                            SpawnBasicUnits.AlienBomberCount--;
                            break;
                        case "Synthetic Grunt":
                            SpawnBasicUnits.SynthGruntCount--;
                            break;
                        case "Synthetic Runner":
                            SpawnBasicUnits.SynthRunnerCount--;
                            break;
                        case "Synthetic Tank":
                            SpawnBasicUnits.SynthTankCount--;
                            break;
                        case "Synthetic Scout":
                            SpawnBasicUnits.SynthScoutCount--;
                            break;
                        case "Synthetic Bomber":
                            SpawnBasicUnits.SynthBomberCount--;
                            break;
                    }
                }
                else
                {
                    SpawnBasicUnits.BlackPieceCount--;
                    switch (defenderTile.GetComponent<OnTileActions>().PieceOnTile.GetComponent<PiecePropScript>().PieceName)
                    {
                        case "Organic Grunt":
                            SpawnBasicUnits.AlienGruntCount--;
                            break;
                        case "Organic Runner":
                            SpawnBasicUnits.AlienRunnerCount--;
                            break;
                        case "Organic Tank":
                            SpawnBasicUnits.AlienTankCount--;
                            break;
                        case "Organic Scout":
                            SpawnBasicUnits.AlienScoutCount--;
                            break;
                        case "Organic Bomber":
                            SpawnBasicUnits.AlienBomberCount--;
                            break;
                        case "Synthetic Grunt":
                            SpawnBasicUnits.SynthGruntCount--;
                            break;
                        case "Synthetic Runner":
                            SpawnBasicUnits.SynthRunnerCount--;
                            break;
                        case "Synthetic Tank":
                            SpawnBasicUnits.SynthTankCount--;
                            break;
                        case "Synthetic Scout":
                            SpawnBasicUnits.SynthScoutCount--;
                            break;
                        case "Synthetic Bomber":
                            SpawnBasicUnits.SynthBomberCount--;
                            break;
                    }
                }
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

    public string DetermineWhatUnitWon()
    {
        string winner = "KillsWith";

        if (attackerTile.GetComponent<OnTileActions>().PieceOnTile.tag == "White")
        {
            winner += "Robot";
        }
        else
        {
            winner += "Alien";
        }

        winner += attackerTile.GetComponent<OnTileActions>().PieceOnTile.GetComponent<PiecePropScript>().unitType;
        return winner;
    }
}
