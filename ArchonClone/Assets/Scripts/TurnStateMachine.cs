using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TurnStateMachine : MonoBehaviour {

    public enum State { playerTurn, DaSwitch, otherTurn }
    public static State state;
    public static GameObject OnHoverPiece;//variable is set in TileProperties when your mouse is over a tile
    public GameObject stateText;

    IEnumerator playerTurnState()
    {
        Debug.Log("PlayerTurn: Enter");
        //PiecePlaceScript.Black01Tile.GetComponent<TileProperties>().datNode.gameObject.SetActive(true);
        while (state == State.playerTurn)
        {
            SetText();
            yield return 0;
        }
        Debug.Log("PlayerTurn: Exit");
        if (Network.isClient || Network.isServer)
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
        if (Network.isClient || Network.isServer)
            networkView.RPC("NextState", RPCMode.AllBuffered);
        else
            NextState();
    }

    IEnumerator otherTurnState()
    {
        Debug.Log("OtherTurn: Enter");
        while (state == State.otherTurn)
        {
            SetText();
            yield return 0;
        }
        Debug.Log("OtherTurn: Exit");
        if (Network.isClient || Network.isServer)
            networkView.RPC("NextState", RPCMode.AllBuffered);
        else
            NextState();
    }
    
    // Use this for initialization
	void Start () {
        if(Network.isClient || Network.isServer)
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
    void SetText()
    {
        stateText.GetComponent<Text>().text = TurnStateMachine.state.ToString();
    }
}
