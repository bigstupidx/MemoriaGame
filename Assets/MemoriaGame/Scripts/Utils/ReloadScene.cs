using UnityEngine;
using System.Collections;

public class ReloadScene : MonoBehaviour {
    public GameObject loadPrefab;
	// Use this for initialization
	public virtual void Reload () {
        loadPrefab.SetActive (true);
        Application.LoadLevel (Application.loadedLevelName);

	}
	
	
}
