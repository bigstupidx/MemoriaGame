using UnityEngine;
using System.Collections;

public class AS_SpawnFX : MonoBehaviour {


    public AudioClip fx;
    public bool loop = false;
	// Use this for initialization
	void Start () {
        audio.loop = loop;

        if (loop == false) {
            audio.PlayOneShot (fx, ManagerSound.Instance.fxVolume);

            Invoke ("DestroySound", fx.length);
        } else {
            audio.clip = fx;
            audio.volume = ManagerSound.Instance.fxVolume;
            audio.Play ();
        }
	}

    public void DestroySound(){
    
        this.Recycle ();
    }
	
}
