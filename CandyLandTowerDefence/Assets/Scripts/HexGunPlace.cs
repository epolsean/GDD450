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
            Transform childNode = this.transform.FindChild("Node(Clone)");
            renderer.material.color = Color.green;
            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(BasicGun, this.transform.position + Vector3.up * 2, Quaternion.Euler(0, 30, 0));
                Instantiate(BasicHexWall, this.transform.position, this.transform.rotation);
                tag = "SlotClosed";

                if (childNode)
                {
                    childNode.gameObject.SetActive(false);
                    GridManager.rescan = true;
                }
            }
            if(Input.GetMouseButtonDown(1))
            {
                Instantiate(BasicHexWall, this.transform.position, this.transform.rotation);
                tag = "SlotWall";
                
                if (childNode)
                {
                    childNode.gameObject.SetActive(false);
                    GridManager.rescan = true;
                }
            }
        }
        else if(tag == "SlotWall")
        {
            renderer.material.color = Color.blue;
            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(BasicGun, this.transform.position + Vector3.up*2, Quaternion.Euler(0,30,0));
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




