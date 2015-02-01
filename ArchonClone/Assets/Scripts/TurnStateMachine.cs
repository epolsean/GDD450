using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TurnStateMachine : MonoBehaviour {

    public enum State { playerTurn, DaSwitch, otherTurn }
    public static State state;
    public static GameObject OnHoverPiece;//variable is set in TileProperties when your mouse is over a tile
    public GameObject stateText;
    public static bool fightDone = false;
    public static bool canSelectPiece = false; 

    public void Update()
    {
        if(fightDone)
        {
            fightDone = false;
            Invoke("updateTurn", 4);

        }
    }

    IEnumerator playerTurnState()
    {
        Debug.Log("PlayerTurn: Enter");
        canSelectPiece = true;
        //PiecePlaceScript.Black01Tile.GetComponent<TileProperties>().datNode.gameObject.SetActive(true);
        while (state == State.playerTurn)
        {
            SetText();
             
            yield return 0;
        }
        Debug.Log("PlayerTurn: Exit");
        if (Network.isServer)
            networkView.RPC("NextState", RPCMode.AllBuffered);
        else
            NextState();
    }

    IEnumerator DaSwitchState()
    {
        Debug.Log("DaSwitch: Enter");
        while (state == State.DaSwitch)
        {
            SetText();
            yield return 0;
        }
        Debug.Log("DaSwitch: Exit");
        if (Network.isServer)
            networkView.RPC("NextState", RPCMode.AllBuffered);
        else
            NextState();
    }

    IEnumerator otherTurnState()
    {
        Debug.Log("OtherTurn: Enter");
        canSelectPiece = true;
        while (state == State.otherTurn)
        {

            SetText();
            yield return 0;
        }
        Debug.Log("OtherTurn: Exit");
        if (Network.isServer)
            networkView.RPC("NextState", RPCMode.AllBuffered);
        else
            NextState();
    }
    
    // Use this for initialization
	void Start () {
        if(Network.isServer)
            networkView.RPC("NextState", RPCMode.AllBuffered);
        else
            NextState();
	}
	

    [RPC]
    void NextState()
    {
        string methodName = state.ToString() + "State";
        System.Reflection.MethodInfo info =
            GetType().GetMethod(methodName,
                                System.Reflection.BindingFlags.NonPublic |
                                System.Reflection.BindingFlags.Instance);
        StartCoroutine((IEnumerator)info.Invoke(this, null));
    }

    [RPC]
    void SetText()
    {
        if(TurnStateMachine.state == State.playerTurn)
        {
            stateText.GetComponent<Text>().text = "Player 1's Turn";
        }
        else if (TurnStateMachine.state == State.otherTurn)
        {
            stateText.GetComponent<Text>().text = "Player 2's Turn";
        }
        //stateText.GetComponent<Text>().text = TurnStateMachine.state.ToString();
    }

    public void updateTurn()
    {
        if (TurnStateMachine.state == TurnStateMachine.State.playerTurn)
        {
            TurnStateMachine.state = TurnStateMachine.State.otherTurn;
        }
        else
        {
            TurnStateMachine.state = TurnStateMachine.State.playerTurn;
            if (GameObject.Find("EnemyAI").activeInHierarchy)
            {
                EnemyAI.AIstate = EnemyAI.State.Idle;
            }
        }
    }
}
