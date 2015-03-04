using UnityEngine;
using System.Collections;

public class AS_SpawnFX : MonoBehaviour {


    public AudioClip fx;
    public bool loop = false;
	// Use this for initialization
	void Start () {
        GetComponent<AudioSource>().loop = loop;

        if (loop == false) {
            GetComponent<AudioSource>().PlayOneShot (fx, ManagerSound.Instance.fxVolume);

            Invoke ("DestroySound", fx.length);
        } else {
            GetComponent<AudioSource>().clip = fx;
            GetComponent<AudioSource>().volume = ManagerSound.Instance.fxVolume;
            GetComponent<AudioSource>().Play ();
        }
	}

    public void DestroySound(){
    
        this.Recycle ();
    }
	
}
