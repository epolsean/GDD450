using UnityEngine;
using System.Collections;

public class EndControllerScript : MonoBehaviour {

    public static bool isEnd = false;
    public static bool SynthVic = false;
    public static bool OrgVic = false; 
    
    // Use this for initialization
	void Start () {
        isEnd = false; 
	}
	
	// Update is called once per frame
	void Update () {
	    if(isEnd)
        {
            Invoke("LoadEndScene", 4);
        }
	}

    void LoadEndScene()
    {
        Application.LoadLevel("EndPlaceHolderScene");
        if(SynthVic)
        {
            Application.LoadLevel("SyntheticVictory");
        }
        else if(OrgVic)
        {
            Application.LoadLevel("OrganicsVictoryScreen");
        }
    }
}
