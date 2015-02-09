using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour {

    public GameObject VolumeSlider;
	
	// Update is called once per frame
	void Update () {
        AudioListener.volume = VolumeSlider.GetComponent<Slider>().value;
	}
}
