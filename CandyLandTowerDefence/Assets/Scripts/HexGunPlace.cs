using UnityEngine;
using System.Collections;

public class HexGunPlace : MonoBehaviour {

    public GameObject BasicGun;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseOver()
    {
        if (tag == "SlotOpen" || tag == "SlotWall")
        {
            renderer.material.color = Color.blue;
            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(BasicGun, this.transform.position, this.transform.rotation);
                tag = "SlotClosed";
            }
        }
    }

    void OnMouseExit()
    {
        renderer.material.color = Color.white;
    }
}
