using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HexGunPlaceV2 : MonoBehaviour {

    public GameObject BasicGun;
    public GameObject UpgradedGun;
    public GameObject BasicHexWall;
    public GameObject TowerSelectUI;

    GameObject CameraViewController;

    GameObject waveController;
    WaveSetup waveSetup;
    GameObject Canvas;

    //these two objects will remain empty until they are filled by player
    public GameObject GunOnTile;
    public GameObject TowerOnTile;

    public bool GunUpgradable; 

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
	
	}

    void OnMouseOver()
    {
        if (CameraViewController.GetComponent<CannonCameraSwitch>().isFPSCannon == false)
        {
            CameraViewController.GetComponent<CannonCameraSwitch>().Gun = null;
            if (tag == "SlotOpen")
            {
                //Transform childNode = this.transform.FindChild("Node(Clone)");
                renderer.material.color = Color.green;
                if (Input.GetMouseButtonDown(0))
                {
                    GameObject selectTower = (GameObject)Instantiate(TowerSelectUI);
                    selectTower.transform.parent = Canvas.transform;  //Fix to keep coord at zeros
                    SpawnTowerAndGun();

                }
                if (Input.GetMouseButtonDown(1))
                {
                    SpawnTower();
                }
            }
            else if (tag == "SlotWall")
            {
                renderer.material.color = Color.blue;
                if (Input.GetMouseButtonDown(0))
                {
                    SpawnGun();
                }
            }
            else if (tag == "SlotClosed")
            {
                CameraViewController.GetComponent<CannonCameraSwitch>().Gun = GunOnTile;
                if (GunUpgradable == true)
                {
                    renderer.material.color = Color.yellow;
                    if (Input.GetMouseButtonDown(0))
                    {
                        UpgradeGun();
                    }
                }
                else if (GunUpgradable == false)
                {
                    renderer.material.color = Color.red;
                }

            }
        }
    }

    void OnMouseExit()
    {
        renderer.material.color = Color.white;
    }

    public void SpawnTowerAndGun()
    {
        if (waveSetup.resource1 >= 12 && waveSetup.resource2 >= 8 && waveSetup.resource3 >= 4)
        {
            Transform childNode = this.transform.FindChild("Node(Clone)");

            GunOnTile = Instantiate(BasicGun, this.transform.position + Vector3.up * 2, Quaternion.Euler(0, 30, 0)) as GameObject;
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

    public void SpawnGun()
    {
        if (waveSetup.resource1 >= 10 && waveSetup.resource2 >= 8 && waveSetup.resource3 >= 4)
        {
            GunOnTile = Instantiate(BasicGun, this.transform.position + Vector3.up * 2, Quaternion.Euler(0, 30, 0)) as GameObject;
            waveSetup.resource1 -= 10;
            waveSetup.resource2 -= 8;
            waveSetup.resource3 -= 4;
            tag = "SlotClosed";
            GunUpgradable = true;
        }
    }

    public void UpgradeGun()
    {
        if (waveSetup.resource2 >= 8 && waveSetup.resource3 >= 4)
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




