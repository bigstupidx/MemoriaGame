using UnityEngine;
using System.Collections;

public class CheckAudioGUI : MonoBehaviour {

    public string value;
    UISlider slider;
	// Use this for initialization
	void Awake () {
        slider =GetComponent<UISlider> ();
        slider.value = PlayerPrefs.GetFloat (value);
	}
	

}
