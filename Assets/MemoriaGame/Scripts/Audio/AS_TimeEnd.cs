using UnityEngine;
using System.Collections;

public class AS_TimeEnd : MonoBehaviour {

	// Use this for initialization
	void Start () {
        ManagerTime.Instance.onTimeGameEnd += PlaySound;
	}
	
	// Update is called once per frame
	void PlaySound () {
        audio.Play ();
	}
}
