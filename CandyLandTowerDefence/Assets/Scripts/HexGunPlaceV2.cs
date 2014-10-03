using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HexGunPlaceV2 : MonoBehaviour {

    public GameObject BasicGun;
    public GameObject UpgradedGun;
    public GameObject BasicHexWall;
    public GameObject TowerSelectUI;

    public DestroyTowerSelect DTS;

    GameObject CameraViewController;

    GameObject waveController;
    WaveSetup waveSetup;
    GameObject Canvas;

    //these two objects will remain empty until they are filled by player
    public GameObject GunOnTile;
    public GameObject TowerOnTile;

    public bool GunUpgradable;
    public static bool anyTileSelected = false;
    public bool thisTileSelected = false;

	// Use this for initialization
	void Start () {
        Canvas = GameObject.Find("Canvas");
        GunUpgradable = false;
        waveController = GameObject.FindGameObjectWithTag("WavCon");
        waveSetup = waveController.GetComponent<WaveSetup>();
        CameraViewController = GameObject.Find("FirstPersonCannonControl");
	}
	
	// Update is called once per frame
	void Update () {

        if (thisTileSelected)
        {
            renderer.material.color = Color.green;
        }
	}

    void OnMouseOver()
    {
        if (!anyTileSelected)
        {
            if (CameraViewController.GetComponent<CannonCameraSwitch>().isFPSCannon == false)
            {
                CameraViewController.GetComponent<CannonCameraSwitch>().Gun = null;
                if (tag == "SlotOpen" || tag == "SlotWall")
                {
                    //Transform childNode = this.transform.FindChild("Node(Clone)");
                    renderer.material.color = Color.green;
                    if (Input.GetMouseButtonDown(0))
                    {
                        CameraViewController.GetComponent<CannonCameraSwitch>().TileSelected = this.gameObject;
                        GameObject selectTower = (GameObject)Instantiate(TowerSelectUI);
                        selectTower.transform.parent = Canvas.transform;  //Fix to keep coord at zeros
                        selectTower.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
                        selectTower.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                        //SpawnTowerAndGun();
                        thisTileSelected = true;
                        anyTileSelected = true;

                    }
                    /*if (Input.GetMouseButtonDown(1))
                    {
                        CameraViewController.GetComponent<CannonCameraSwitch>().TileSelected = this.gameObject;
                        SpawnTower();
                        //thisTileSelected = true;
                        //anyTileSelected = true;
                    }*/
                }
                /*else if (tag == "SlotWall")
                {
                    renderer.material.color = Color.blue;
                    if (Input.GetMouseButtonDown(0))
                    {
                        CameraViewController.GetComponent<CannonCameraSwitch>().TileSelected = this.gameObject;
                        SpawnGun();
                        //thisTileSelected = true;
                        //anyTileSelected = true;
                    }
                }*/
                else if (tag == "SlotClosed")
                {
                    CameraViewController.GetComponent<CannonCameraSwitch>().Gun = GunOnTile;
                    if (GunUpgradable == true)
                    {
                        renderer.material.color = Color.yellow;
                        if (Input.GetMouseButtonDown(0))
                        {
                            CameraViewController.GetComponent<CannonCameraSwitch>().TileSelected = this.gameObject;
                            UpgradeGun(this.gameObject.name);
                            //thisTileSelected = true;
                            //anyTileSelected = true;
                        }
                    }
                    else if (GunUpgradable == false)
                    {
                        renderer.material.color = Color.red;
                    }

                }
            }
        }
    }

    void OnMouseExit()
    {
        renderer.material.color = Color.white;
    }

    public void SpawnTowerAndGun(string nameOfTower)
    {
        if (nameOfTower == "GumballTower")
        {
            if (waveSetup.resource1 >= 12 && waveSetup.resource2 >= 8 && waveSetup.resource3 >= 4)
            {
                Transform childNode = this.transform.FindChild("Node(Clone)");

                GunOnTile = Instantiate((GameObject)Resources.Load(nameOfTower), this.transform.position + Vector3.up * 2, Quaternion.Euler(0, 30, 0)) as GameObject;
                TowerOnTile = Instantiate(BasicHexWall, this.transform.position, this.transform.rotation) as GameObject;
                waveSetup.resource1 -= 12;
                waveSetup.resource2 -= 8;
                waveSetup.resource3 -= 4;
                tag = "SlotClosed";
                GunUpgradable = true;

                if (childNode)
                {
                    childNode.gameObject.SetActive(false);
                    GridManager.rescan = true;
                }
            }
        }
        else if (nameOfTower == "Catapault")
        {
            if (waveSetup.resource1 >= 4 && waveSetup.resource2 >= 10 && waveSetup.resource3 >= 10)
            {
                Transform childNode = this.transform.FindChild("Node(Clone)");

                GunOnTile = Instantiate((GameObject)Resources.Load(nameOfTower), this.transform.position + Vector3.up * 2, Quaternion.Euler(0, 30, 0)) as GameObject;
                TowerOnTile = Instantiate(BasicHexWall, this.transform.position, this.transform.rotation) as GameObject;
                waveSetup.resource1 -= 4;
                waveSetup.resource2 -= 10;
                waveSetup.resource3 -= 10;
                tag = "SlotClosed";
                GunUpgradable = true;

                if (childNode)
                {
                    childNode.gameObject.SetActive(false);
                    GridManager.rescan = true;
                }
            }
        }
        else if (nameOfTower == "Wall" && tag != "SlotWall")
        {
            SpawnTower();
        }
    }

    public void SpawnTower()
    {
        if (waveSetup.resource1 >= 2)
        {
            Transform childNode = this.transform.FindChild("Node(Clone)");

            TowerOnTile = Instantiate(BasicHexWall, this.transform.position, this.transform.rotation) as GameObject;
            waveSetup.resource1 -= 2;
            tag = "SlotWall";

            if (childNode)
            {
                childNode.gameObject.SetActive(false);
                GridManager.rescan = true;
            }
        }
    }

    public void SpawnGun(string nameOfTower)
    {
        if (nameOfTower == "GumballTower")
        {
            if (waveSetup.resource1 >= 10 && waveSetup.resource2 >= 8 && waveSetup.resource3 >= 4)
            {
                GunOnTile = Instantiate((GameObject)Resources.Load(nameOfTower), this.transform.position + Vector3.up * 2, Quaternion.Euler(0, 30, 0)) as GameObject;
                waveSetup.resource1 -= 10;
                waveSetup.resource2 -= 8;
                waveSetup.resource3 -= 4;
                tag = "SlotClosed";
                GunUpgradable = true;
            }
        }
        else if (nameOfTower == "Catapault")
        {
            if (waveSetup.resource1 >= 4 && waveSetup.resource2 >= 10 && waveSetup.resource3 >= 10)
            {
                GunOnTile = Instantiate((GameObject)Resources.Load(nameOfTower), this.transform.position + Vector3.up * 2, Quaternion.Euler(0, 30, 0)) as GameObject;
                waveSetup.resource1 -= 4;
                waveSetup.resource2 -= 10;
                waveSetup.resource3 -= 10;
                tag = "SlotClosed";
                GunUpgradable = true;
            }
        }
    }

    public void UpgradeGun(string nameOfGun)
    {
        if (waveSetup.resource2 >= 8 && waveSetup.resource3 >= 4 && nameOfGun == "GumballTower")
        {
            Debug.Log("entered UpgradeGun");
            Destroy(GunOnTile.gameObject); //This removes the gun currently on the tile
            GunOnTile = Instantiate(UpgradedGun, this.transform.position + Vector3.up * 2, Quaternion.Euler(0, 30, 0)) as GameObject;
            GunUpgradable = false;
            GunOnTile.GetComponentInChildren<BasicGunFire>().bulletSpeed += 10;
            waveSetup.resource2 -= 8;
            waveSetup.resource3 -= 4;
            Debug.Log("Update BulletSpeed");
        }
    }
}




