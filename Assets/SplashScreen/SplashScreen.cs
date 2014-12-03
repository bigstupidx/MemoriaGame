using UnityEngine;
using System.Collections;

// Simple Splash Screen for Unity
// No copyright asserted on this code or included files.  They may be used for any purpose.
// See http://apps.burlock.org/unity-splash-screen/ for instructions on how to use this class
// Written by Neil Burlock
// Version 1.0, 09 Jun 2014

public class SplashScreen : MonoBehaviour {
	public string loadLevelName; // Name of the level to load after the splash screen appears
	private bool isLoading = false;

	public static SplashScreen me;

    public float WaitForLoad = 3.0f;
	void Awake() {
		me = this;
	}
	
	void Start () {

        Invoke("LoadUpdate",WaitForLoad);
	}
	
	void LoadUpdate() {
		// Start loading the level on the next frame
		if (!isLoading) {
            Application.LoadLevel(me.loadLevelName);
			isLoading = true;
		}
	}	
	
	// Call from the loaded level to hide the splash
	public static void Hide() {
		if (me != null) me.gameObject.SetActive(false);
	}
}
