using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BattleController : MonoBehaviour {

    public GameObject attacker;
    public GameObject defender;
    public GameObject vsText;
    //Vector2 AttackerStarPos;
    //Vector2 DefenderStarPos;

	// Use this for initialization
	void Start () {
        
        attacker.GetComponent<RectTransform>().anchoredPosition = new Vector2(GetComponent<RectTransform>().sizeDelta.x / -4, GetComponent<RectTransform>().sizeDelta.y / 2 + attacker.GetComponent<RectTransform>().sizeDelta.y/2);
        defender.GetComponent<RectTransform>().anchoredPosition = new Vector2(GetComponent<RectTransform>().sizeDelta.x / 4, -1*GetComponent<RectTransform>().sizeDelta.y / 2 - defender.GetComponent<RectTransform>().sizeDelta.y/2);
	}
	
	// Update is called once per frame
	void Update () {
        
        if (Vector2.Distance(attacker.GetComponent<RectTransform>().anchoredPosition, new Vector2(attacker.GetComponent<RectTransform>().anchoredPosition.x, (int)GetComponent<RectTransform>().sizeDelta.y / 4)) > 1)
            attacker.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(attacker.GetComponent<RectTransform>().anchoredPosition, new Vector2(attacker.GetComponent<RectTransform>().anchoredPosition.x, (int)GetComponent<RectTransform>().sizeDelta.y / 4), Time.deltaTime);
        if (Vector2.Distance(defender.GetComponent<RectTransform>().anchoredPosition,new Vector2(defender.GetComponent<RectTransform>().anchoredPosition.x, -(int)GetComponent<RectTransform>().sizeDelta.y / 4)) > 1)
            defender.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(defender.GetComponent<RectTransform>().anchoredPosition, new Vector2(defender.GetComponent<RectTransform>().anchoredPosition.x, (int)-GetComponent<RectTransform>().sizeDelta.y / 4), Time.deltaTime);
	
    }
}
