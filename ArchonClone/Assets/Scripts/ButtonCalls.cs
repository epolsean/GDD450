using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonCalls : MonoBehaviour {

    private Button MyButton = null;
    // Use this for initialization
	void Start () {
        MyButton.onClick.AddListener(() => { PlayerTurn(); OtherTurn(); DaSwitch(); });
	}
	
	// Update is called once per frame
	public void PlayerTurn() {
        TurnStateMachine.state = TurnStateMachine.State.playerTurn;
	}

    public void OtherTurn()
    {
        TurnStateMachine.state = TurnStateMachine.State.otherTurn;
    }

    public void DaSwitch()
    {
        TurnStateMachine.state = TurnStateMachine.State.DaSwitch;
    }
}
