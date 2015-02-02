using UnityEngine;
using System.Collections;

public class UISoundsScript : MonoBehaviour {

    public AudioClip selectPiece;
    public AudioClip MovePiece;
    public AudioClip Fight;
    public AudioClip Error;
    public AudioClip deselectPiece; 
    
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void playSelectPiece()
    {
        this.GetComponent<AudioSource>().clip = selectPiece;
        this.audio.Play();
    }

    public void playMovePiece()
    {
        this.GetComponent<AudioSource>().clip = MovePiece;
        this.audio.Play();
    }

    public void playFight()
    {
        this.GetComponent<AudioSource>().clip = Fight;
        this.audio.Play();
    }
    public void playError()
    {
        this.GetComponent<AudioSource>().clip = Error;
        this.audio.Play();
    }
    public void playDeselect()
    {
        this.GetComponent<AudioSource>().clip = deselectPiece;
        this.audio.Play();
    }
}
