using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {


    private FMOD.Studio.EventInstance music;
    private FMOD.Studio.ParameterInstance whichMusic;


    // Use this for initialization
    void Start () {
        music = FMODUnity.RuntimeManager.CreateInstance("event:/Music");
        music.start();
		
	}
	


}
