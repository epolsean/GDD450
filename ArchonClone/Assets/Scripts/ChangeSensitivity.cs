using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChangeSensitivity : MonoBehaviour {

    public bool player2 = true;
    public bool x_Sens = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (player2 && x_Sens)
        {
            Player2MovementController.xSensitivity = (int)GetComponent<Slider>().value;
        }
        else if (player2 && !x_Sens)
        {
            Player2MovementController.ySensitivity = (int)GetComponent<Slider>().value;
        }
        else if (!player2 && x_Sens)
        {
            Player1MovementController.xSensitivity = (int)GetComponent<Slider>().value;
        }
        else if (!player2 && !x_Sens)
        {
            Player1MovementController.ySensitivity = (int)GetComponent<Slider>().value;
        }
	
	}

    
}
