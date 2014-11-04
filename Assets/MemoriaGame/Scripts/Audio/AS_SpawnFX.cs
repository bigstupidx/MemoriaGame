using UnityEngine;
using System.Collections;

public class AS_SpawnFX : MonoBehaviour {


    public AudioClip fx;
	// Use this for initialization
	void Start () {
      //  audio.clip = ;
     //   audio.volume =;
        audio.PlayOneShot (fx, ManagerSound.Instance.fxVolume);
        Invoke ("DestroySound",fx.length);
	}

    void DestroySound(){
    
        this.Recycle ();
    }
	
}
