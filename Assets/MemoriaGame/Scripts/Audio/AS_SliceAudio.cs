using UnityEngine;
using System.Collections;

public class AS_SliceAudio : MonoBehaviour {

	// Use this for initialization
	void Start () {
        ManagerDoublePoints.Instance.OnActivePower += OnActivePower;

	}
    public void OnActivePower (bool active) {
        if (active) {
            GetComponent<AudioSource>().volume = ManagerSound.Instance.fxVolume;

            GetComponent<AudioSource>().Play ();
        } else {
            GetComponent<AudioSource>().Stop ();
        }
    }

}
