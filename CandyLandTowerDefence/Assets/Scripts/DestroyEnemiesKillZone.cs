using UnityEngine;
using System.Collections;

public class DestroyEnemiesKillZone : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Destroy(other.gameObject);
            UIController.livesRemaining--;
            if (UIController.livesRemaining == 0)
            {
                Application.LoadLevel("StartScreen");
            }
        }
    }
}
