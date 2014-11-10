using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class MenuCursorController : MonoBehaviour 
{
    GameObject eSystem;
    public GameObject firstSelected;

    public GameObject cursor;

    // Use this for initialization
    void Start()
    {
        if (Input.GetJoystickNames().Length != 0)
        {
            eSystem = GameObject.Find("EventSystem");
            eSystem.GetComponent<EventSystem>().SetSelectedGameObject(firstSelected, new BaseEventData(eSystem.GetComponent<EventSystem>()));
            gameObject.SetActive(false);
        }
        Screen.showCursor = false;

        cursor.transform.position = new Vector3(50, 50, 0);
    }


    void Update()
    {
        cursor.transform.position = new Vector3(Input.mousePosition.x + 6, Input.mousePosition.y - 6, Input.mousePosition.z);
    }
}
