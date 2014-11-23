using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisplayTimer : MonoBehaviour
{
    public bool volcano = false;
    public GameObject panel;
    public GameObject MoveController;

    void Start()
    {
        MoveController = GameObject.Find("MovementController");
    }

    // Update is called once per frame
    void Update()
    {
        if (MoveController.GetComponent<PawnMove>().Player02 != null && MoveController.GetComponent<PawnMove>().Player01 != null)
        {
            if (GameObject.Find("Fight") == null)
            {
                panel.GetComponent<Image>().enabled = true;
                GetComponent<Text>().enabled = true;
                if (volcano)
                {
                    if (Accelerator.countdownTimer <= 0)
                    {
                        transform.parent.gameObject.SetActive(false);
                    }
                    else
                    {
                        GetComponent<Text>().text = "Time Until Volcano Eruption: " + Accelerator.countdownTimer.ToString("0.00");
                    }
                }
                else
                {
                    if (Accelerator.countdownTimer <= 0)
                    {
                        transform.parent.gameObject.SetActive(false);
                    }
                    else
                    {
                        GetComponent<Text>().text = "Time Until Reactor Malfunction: " + Accelerator.countdownTimer.ToString("0.00");
                    }
                }

            }
        }
    }
}
