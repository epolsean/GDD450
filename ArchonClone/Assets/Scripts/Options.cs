using UnityEngine;
using System.Collections;

public class Options : MonoBehaviour {

    public GameObject BackToMenuButton;
    public GameObject BackToOptionsButton;

    public GameObject ControlsPanel;
    public GameObject SettingsPanel;
    public GameObject OptionsPanel;

    public GameObject ControllerPanel;
    public GameObject LocalPanel;
    public GameObject SingleLANPanel;

    public void backToOptions()
    {
        BackToMenuButton.SetActive(true);
        BackToOptionsButton.SetActive(false);

        OptionsPanel.SetActive(true);
        ControlsPanel.SetActive(false);
        SettingsPanel.SetActive(false);
    }

    public void toControls()
    {
        BackToMenuButton.SetActive(false);
        BackToOptionsButton.SetActive(true);

        OptionsPanel.SetActive(false);
        ControlsPanel.SetActive(true);
        SettingsPanel.SetActive(false);

        LocalPanel.SetActive(true);
        ControllerPanel.SetActive(false);
        SingleLANPanel.SetActive(false);
    }

    public void toSettings()
    {
        BackToMenuButton.SetActive(false);
        BackToOptionsButton.SetActive(true);

        OptionsPanel.SetActive(false);
        ControlsPanel.SetActive(false);
        SettingsPanel.SetActive(true);
    }

    public void nextPage()
    {
        if (ControllerPanel.activeInHierarchy == true)
        {
            ControllerPanel.SetActive(false);
            LocalPanel.SetActive(true);
            SingleLANPanel.SetActive(false);
        }
        else if (LocalPanel.activeInHierarchy == true)
        {
            ControllerPanel.SetActive(false);
            LocalPanel.SetActive(false);
            SingleLANPanel.SetActive(true);
        }
        else if (SingleLANPanel.activeInHierarchy == true)
        {
            ControllerPanel.SetActive(true);
            LocalPanel.SetActive(false);
            SingleLANPanel.SetActive(false);
        }
    }

    public void previousPage()
    {
        if (ControllerPanel.activeInHierarchy == true)
        {
            ControllerPanel.SetActive(false);
            LocalPanel.SetActive(false);
            SingleLANPanel.SetActive(true);
        }
        else if (LocalPanel.activeInHierarchy == true)
        {
            ControllerPanel.SetActive(true);
            LocalPanel.SetActive(false);
            SingleLANPanel.SetActive(false);
        }
        else if (SingleLANPanel.activeInHierarchy == true)
        {
            ControllerPanel.SetActive(false);
            LocalPanel.SetActive(true);
            SingleLANPanel.SetActive(false);
        }
    }
}
