using UnityEngine;
using System.Collections;

public class ReloadScene : MonoBehaviour {

	// Use this for initialization
	public virtual void Reload () {

        Application.LoadLevel (Application.loadedLevelName);

	}
	
	
}
