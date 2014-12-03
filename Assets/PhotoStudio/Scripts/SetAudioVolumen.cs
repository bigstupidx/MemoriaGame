using UnityEngine;
using System.Collections;

public class SetAudioVolumen : MonoBehaviour {

	// Use this for initialization
	void OnEnable () {
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
	

}
