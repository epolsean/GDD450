using UnityEngine;
using System.Collections;

public class HexGunPlace : MonoBehaviour {

    public GameObject BasicGun;
    public GameObject BasicHexWall;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseOver()
    {
        if (tag == "SlotOpen")
        {
            renderer.material.color = Color.green;
            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(BasicGun, this.transform.position+Vector3.up, this.transform.rotation);
                Instantiate(BasicHexWall, this.transform.position, this.transform.rotation);
                tag = "SlotClosed";
            }
            if(Input.GetMouseButtonDown(1))
            {
                Instantiate(BasicHexWall, this.transform.position, this.transform.rotation);
                tag = "SlotWall";
            }
        }
        else if(tag == "SlotWall")
        {
            renderer.material.color = Color.blue;
            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(BasicGun, this.transform.position + Vector3.up, this.transform.rotation);
                tag = "SlotClosed";
            }
        }
        else if(tag == "SlotClosed")
        {
            renderer.material.color = Color.red;
        }
    }

    void OnMouseExit()
    {
        renderer.material.color = Color.white;
    }
}
