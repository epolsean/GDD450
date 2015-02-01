using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BattleController : MonoBehaviour {

    public GameObject attackerIcon;
    public GameObject defenderIcon;
    public GameObject vsText;

    GameObject attacker;
    GameObject defender;

    string Loser;
    bool BattleOver = false;
    //Vector2 attackerIconStarPos;
    //Vector2 defenderIconStarPos;
    enum BattleState {PreBattle,Battle,PostBattle};
    BattleState partOfBattle;

	// Use this for initialization
	void Start () 
    {
        partOfBattle = BattleState.PreBattle;
        attackerIcon.GetComponent<RectTransform>().anchoredPosition = new Vector2(GetComponent<RectTransform>().sizeDelta.x / -4, GetComponent<RectTransform>().sizeDelta.y / 2 + attackerIcon.GetComponent<RectTransform>().sizeDelta.y/2);
        defenderIcon.GetComponent<RectTransform>().anchoredPosition = new Vector2(GetComponent<RectTransform>().sizeDelta.x / 4, -1*GetComponent<RectTransform>().sizeDelta.y / 2 - defenderIcon.GetComponent<RectTransform>().sizeDelta.y/2);
	}
	
	// Update is called once per frame
	void Update () 
    {

        if (partOfBattle == BattleState.PreBattle)
        {
            if (Vector2.Distance(attackerIcon.GetComponent<RectTransform>().anchoredPosition, new Vector2(attackerIcon.GetComponent<RectTransform>().anchoredPosition.x, (int)GetComponent<RectTransform>().sizeDelta.y / 4)) > 1)
                attackerIcon.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(attackerIcon.GetComponent<RectTransform>().anchoredPosition, new Vector2(attackerIcon.GetComponent<RectTransform>().anchoredPosition.x, (int)GetComponent<RectTransform>().sizeDelta.y / 4), Time.deltaTime);
            if (Vector2.Distance(defenderIcon.GetComponent<RectTransform>().anchoredPosition, new Vector2(defenderIcon.GetComponent<RectTransform>().anchoredPosition.x, -(int)GetComponent<RectTransform>().sizeDelta.y / 4)) > 1)
                defenderIcon.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(defenderIcon.GetComponent<RectTransform>().anchoredPosition, new Vector2(defenderIcon.GetComponent<RectTransform>().anchoredPosition.x, (int)-GetComponent<RectTransform>().sizeDelta.y / 4), Time.deltaTime);
            else
                attackerIcon.SetActive(false);
                defenderIcon.SetActive(false);
                vsText.SetActive(false);
        }
        else if (partOfBattle == BattleState.Battle)
        {
            SimulateBattle();
        }
        else if (partOfBattle == BattleState.PostBattle)
        {
            if (Vector2.Distance(attackerIcon.GetComponent<RectTransform>().anchoredPosition, new Vector2(attackerIcon.GetComponent<RectTransform>().anchoredPosition.x, (int)GetComponent<RectTransform>().sizeDelta.y / 4)) > 1)
                attackerIcon.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(attackerIcon.GetComponent<RectTransform>().anchoredPosition, new Vector2(attackerIcon.GetComponent<RectTransform>().anchoredPosition.x, (int)GetComponent<RectTransform>().sizeDelta.y / 4), Time.deltaTime);
            if (Vector2.Distance(defenderIcon.GetComponent<RectTransform>().anchoredPosition, new Vector2(defenderIcon.GetComponent<RectTransform>().anchoredPosition.x, -(int)GetComponent<RectTransform>().sizeDelta.y / 4)) > 1)
                defenderIcon.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(defenderIcon.GetComponent<RectTransform>().anchoredPosition, new Vector2(defenderIcon.GetComponent<RectTransform>().anchoredPosition.x, (int)-GetComponent<RectTransform>().sizeDelta.y / 4), Time.deltaTime);
        }
    }

    void SimulateBattle()
    {
        if (BattleOver)
        {
            attackerIcon.SetActive(false);
            defenderIcon.SetActive(false);
            vsText.SetActive(false);
        }
        else
        {
            /*Add code to simulate battle and determine winner
            if (Random.Range(0, 100/attacker power) > attacker power) //This will be if the player who is attacking the tile wins on the attack
            {
                
            }
            else
            {
                
            }
            if (Random.Range(0, 100/defender power) > defender power) //This will be if the player who is defending the tile wins on the attack
            {
                
            }
            else
            {
                
            }*/
        }
    }
}
