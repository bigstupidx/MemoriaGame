using UnityEngine;
using System.Collections;

public class SetAudioVolumen : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        bool isNotFirstTime = false;
        bool.TryParse(PlayerPrefs.GetString ("isNotFirstTime"),out isNotFirstTime);

        if (isNotFirstTime)
        {
            GetComponent<AudioSource>().volume  = PlayerPrefs.GetFloat ("bgVolume");

        }
        else
        {
            GetComponent<AudioSource>().volume   = 0.5f;
        }
       
	}
	
    public void AudioPlay(){
    
        if (!GetComponent<AudioSource>().isPlaying)
            GetComponent<AudioSource>().Play();
    }

    public bool isPlayOnEnable = false;
    void OnEnable(){
        if (isPlayOnEnable)
            GetComponent<AudioSource>().Play();
    }

}
