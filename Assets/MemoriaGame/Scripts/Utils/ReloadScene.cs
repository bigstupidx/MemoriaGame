using UnityEngine;
using System.Collections;

public class ReloadScene : MonoBehaviour {

	// Use this for initialization
	public void Reload () {

        Application.LoadLevel (Application.loadedLevelName);

	}
	
	
}
