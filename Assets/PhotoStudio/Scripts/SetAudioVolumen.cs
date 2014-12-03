using UnityEngine;
using System.Collections;

public class SetAudioVolumen : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        bool isNotFirstTime = false;
        bool.TryParse(PlayerPrefs.GetString ("isNotFirstTime"),out isNotFirstTime);

        if (isNotFirstTime)
        {
            audio.volume  = PlayerPrefs.GetFloat ("bgVolume");

        }
        else
        {
            audio.volume   = 0.5f;
        }
       
	}
	
    public void AudioPlay(){
    
        if (!audio.isPlaying)
            audio.Play();
    }

    public bool isPlayOnEnable = false;
    void OnEnable(){
        if (isPlayOnEnable)
            audio.Play();
    }

}
