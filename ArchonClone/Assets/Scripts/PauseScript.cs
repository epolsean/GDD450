using UnityEngine;
using System.Collections;

public class PauseScript : MonoBehaviour {

    public GameObject PausePanel;
    public GameObject P1Pieces;
    public GameObject P2Pieces;
    public GameObject PlayerTurn;
    public GameObject Slider1;
    public GameObject Slider2;
    public GameObject Slider3;
    public GameObject Canvas;

    float startTime;
    bool startedUp;
    bool Dom;

	// Use this for initialization
	void Start () {
        startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        if (startTime + .01 < Time.time && startedUp == false)
        {
            PausePanel.SetActive(false);
            startedUp = true;
            if(Canvas.GetComponent<DominationController>().enabled == true)
            {
                Dom = true;
            }
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (PausePanel.activeInHierarchy == true)
            {
                PausePanel.SetActive(false);
                P1Pieces.SetActive(true);
                P2Pieces.SetActive(true);
                PlayerTurn.SetActive(true);
                if (Dom == true)
                {
                    Slider1.SetActive(true);
                    Slider2.SetActive(true);
                    Slider3.SetActive(true);
                }
                Time.timeScale = 1;
            }
            else
            {
                PausePanel.SetActive(true);
                P1Pieces.SetActive(false);
                P2Pieces.SetActive(false);
                PlayerTurn.SetActive(false);
                if (Dom == true)
                {
                    Slider1.SetActive(false);
                    Slider2.SetActive(false);
                    Slider3.SetActive(false);
                }
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
        if (Dom == true)
        {
            Slider1.SetActive(true);
            Slider2.SetActive(true);
            Slider3.SetActive(true);
        }
        Time.timeScale = 1;
    }
}
