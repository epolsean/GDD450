using UnityEngine;
using System.Collections;

public class EndControllerScript : MonoBehaviour {

    public static bool isEnd = false; 
    
    // Use this for initialization
	void Start () {
        isEnd = false; 
	}
	
	// Update is called once per frame
	void Update () {
	    if(isEnd)
        {
            Invoke("LoadEndScene", 3);
        }
	}

    void LoadEndScene()
    {
        Application.LoadLevel("EndPlaceHolderScene");
    }
}
