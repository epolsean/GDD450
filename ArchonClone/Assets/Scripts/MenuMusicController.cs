using UnityEngine;
using System.Collections;

public class MenuMusicController : MonoBehaviour {

    private static MenuMusicController instance = null;
    public static MenuMusicController Instance
    {
        get { return instance; }
    }
    void Awake() 
    {
        if (instance != null && instance != this) 
        {
            Destroy(this.gameObject);
            return;
        } 
        else 
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    void Update()
    {
        if (Application.loadedLevelName == "TestingHexTiles" || Application.loadedLevelName == "LargeTileBoard" || Application.loadedLevelName=="TutorialTestGrid")
        {
            Destroy(this.gameObject);
        }
    }
}
