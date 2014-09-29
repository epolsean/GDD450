using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HexGunPlaceV2 : MonoBehaviour {

    public GameObject BasicGun;
    public GameObject UpgradedGun;
    public GameObject BasicHexWall;
    public GameObject TowerSelectUI;

    GameObject Canvas;

    //these two objects will remain empty until they are filled by player
    public GameObject GunOnTile;
    public GameObject TowerOnTile;

    public bool GunUpgradable; 

	// Use this for initialization
	void Start () {
        Canvas = GameObject.Find("Canvas");
        GunUpgradable = false;
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseOver()
    {
        if (tag == "SlotOpen")
        {
            //Transform childNode = this.transform.FindChild("Node(Clone)");
            renderer.material.color = Color.green;
            if (Input.GetMouseButtonDown(0))
            {
                //GameObject selectTower = (GameObject)Instantiate(TowerSelectUI);
                //selectTower.transform.parent = Canvas.transform;  //Fix to keep coord at zeros
                //TowerSelectUI.transform.SetParent(Canvas.transform,true);
                //GunOnTile = Instantiate(BasicGun, this.transform.position + Vector3.up * 2, Quaternion.Euler(0, 30, 0)) as GameObject;
                //TowerOnTile = Instantiate(BasicHexWall, this.transform.position, this.transform.rotation) as GameObject;
                //tag = "SlotClosed";
                SpawnTowerAndGun();

                /*if (childNode)
                {
                    childNode.gameObject.SetActive(false);
                    GridManager.rescan = true;
                }*/
            }
            if(Input.GetMouseButtonDown(1))
            {
                //Instantiate(BasicHexWall, this.transform.position, this.transform.rotation);
                //tag = "SlotWall";
                SpawnTower();
                
                /*if (childNode)
                {
                    childNode.gameObject.SetActive(false);
                    GridManager.rescan = true;
                }*/
            }
        }
        else if(tag == "SlotWall")
        {
            renderer.material.color = Color.blue;
            if (Input.GetMouseButtonDown(0))
            {
                //Instantiate(BasicGun, this.transform.position + Vector3.up*2, Quaternion.Euler(0,30,0));
                //tag = "SlotClosed";
                SpawnGun();
            }
        }
        else if(tag == "SlotClosed")
        {
            if(GunUpgradable == true)
            {
                renderer.material.color = Color.yellow;
                if (Input.GetMouseButtonDown(0))
                {
                    UpgradeGun();
                }
            }
            else if(GunUpgradable == false)
            {
                renderer.material.color = Color.red;
            }
            
        }
    }

    void OnMouseExit()
    {
        renderer.material.color = Color.white;
    }

    public void SpawnTowerAndGun()
    {
        Transform childNode = this.transform.FindChild("Node(Clone)"); 
       
        GunOnTile = Instantiate(BasicGun, this.transform.position + Vector3.up * 2, Quaternion.Euler(0, 30, 0)) as GameObject;
        TowerOnTile = Instantiate(BasicHexWall, this.transform.position, this.transform.rotation) as GameObject;
        tag = "SlotClosed";
        GunUpgradable = true;

        if (childNode)
        {
            childNode.gameObject.SetActive(false);
            GridManager.rescan = true;
        }
    }

    public void SpawnTower()
    {

        Transform childNode = this.transform.FindChild("Node(Clone)");
 
        TowerOnTile = Instantiate(BasicHexWall, this.transform.position, this.transform.rotation) as GameObject;
        tag = "SlotWall";

        if (childNode)
        {
            childNode.gameObject.SetActive(false);
            GridManager.rescan = true;
        }
    }

    public void SpawnGun()
    {
        GunOnTile = Instantiate(BasicGun, this.transform.position + Vector3.up * 2, Quaternion.Euler(0, 30, 0)) as GameObject;
        tag = "SlotClosed";
        GunUpgradable = true; 
    }

    public void UpgradeGun()
    {
        Debug.Log("entered UpgradeGun");
        //Destroy(GunOnTile.gameObject); //This removes the gun currently on the tile
        GunUpgradable = false;
        GunOnTile.GetComponentInChildren<BasicGunFire>().bulletSpeed += 10;
        Debug.Log("Update BulletSpeed");
    }
}




