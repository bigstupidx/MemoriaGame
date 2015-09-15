using UnityEngine;
using UnityEngine.UI;

using System.Collections;

public class CheckAudioGUI : MonoBehaviour
{

    public string value;
    Scrollbar slider;

    // Use this for initialization
    void OnEnable ()
    {
        slider = GetComponent<Scrollbar> ();
        slider.value = PlayerPrefs.GetFloat (value);
    }
	

}
