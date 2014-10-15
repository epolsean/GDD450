using UnityEngine;
using System.Collections;

public class GridManager : MonoBehaviour {

    public GameObject A_Star;

    public AstarPath aStarController;

    public static bool rescan = false; 
    
    // Use this for initialization
	void Start () {
        aStarController.Scan();
	}
	
	// Update is called once per frame
	void Update () {
        if (rescan == true)
        {
            aStarController.Scan();
            rescan = false;
            Debug.Log("Scan Called");
        }
	}
}
