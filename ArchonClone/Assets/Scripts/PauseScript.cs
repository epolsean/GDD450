using UnityEngine;
using System.Collections;

public class PauseScript : MonoBehaviour {

    public GameObject PausePanel;
    public GameObject P1Pieces;
    public GameObject P2Pieces;
    public GameObject PlayerTurn;

    float startTime;
    bool startedUp;

	// Use this for initialization
	void Start () {
        startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        if (startTime + .05 < Time.time && startedUp == false)
        {
            PausePanel.SetActive(false);
            startedUp = true;
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (PausePanel.activeInHierarchy == true)
            {
                PausePanel.SetActive(false);
                P1Pieces.SetActive(true);
                P2Pieces.SetActive(true);
                PlayerTurn.SetActive(true);
                Time.timeScale = 1;
            }
            else
            {
                PausePanel.SetActive(true);
                P1Pieces.SetActive(false);
                P2Pieces.SetActive(false);
                PlayerTurn.SetActive(false);
                Time.timeScale = 0;
            }
        }
	}

    public void Resume()
    {
        PausePanel.SetActive(false);
        P1Pieces.SetActive(true);
        P2Pieces.SetActive(true);
        PlayerTurn.SetActive(true);
        Time.timeScale = 1;
    }
}
