using UnityEngine;
using System.Collections;

public class DestroyTowerSelect : MonoBehaviour {

    public GameObject HexSelected;

    public GameObject CCS;

	// Use this for initialization
	void Start () {

        CCS = GameObject.Find("FirstPersonCannonControl");
        HexSelected = CCS.GetComponent<CannonCameraSwitch>().TileSelected;
        if (this.gameObject.name == "Cancel")
        {
            this.enabled = false;
        }
        if (HexSelected.tag == "SlotWall")
        {
            HexSelected.GetComponent<HexGunPlaceV2>().SpawnTowerAndGun(this.gameObject.name);
            HexGunPlaceV2.anyTileSelected = false;
            HexSelected.GetComponent<HexGunPlaceV2>().thisTileSelected = false;
            HexSelected.transform.renderer.material.color = Color.white;
            Destroy(this.transform.parent.gameObject);
        }
        else if (HexSelected.tag == "SlotOpen")
        {
            HexSelected.GetComponent<HexGunPlaceV2>().SpawnTowerAndGun(this.gameObject.name);
            HexGunPlaceV2.anyTileSelected = false;
            HexSelected.GetComponent<HexGunPlaceV2>().thisTileSelected = false;
            HexSelected.transform.renderer.material.color = Color.white;
            Destroy(this.transform.parent.gameObject);
        }
        /*else if (HexSelected.tag == "SlotClosed")
        {
            Debug.Log("Should Upgrade");
            HexSelected.GetComponent<HexGunPlaceV2>().UpgradeGun(CCS.GetComponent<CannonCameraSwitch>().Gun.tag);
            Debug.Log(HexGunPlaceV2.anyTileSelected);
            Debug.Log(HexSelected.GetComponent<HexGunPlaceV2>().thisTileSelected);
            HexGunPlaceV2.anyTileSelected = false;
            HexSelected.GetComponent<HexGunPlaceV2>().thisTileSelected = false;
            HexSelected.transform.renderer.material.color = Color.white;
            Destroy(this.transform.gameObject);
        }*/
	}
	
	// Update is called once per frame
	void OnEnable () {
        Start();
	}
}
