using UnityEngine;
using System.Collections;

public class PauseScript : MonoBehaviour {

    public GameObject PausePanel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (PausePanel.activeInHierarchy == true)
            {
                PausePanel.SetActive(false);
                Time.timeScale = 1;
            }
            else
            {
                PausePanel.SetActive(true);
                Time.timeScale = 0;
            }
        }
	}

    public void Resume()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1;
    }
}
