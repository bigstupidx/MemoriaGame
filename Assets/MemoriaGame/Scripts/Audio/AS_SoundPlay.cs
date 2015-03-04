using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AudioSource))]
public class AS_SoundPlay : MonoBehaviour {


    public void Playaudio(){
        GetComponent<AudioSource>().volume = ManagerSound.Instance.fxVolume;
        GetComponent<AudioSource>().Play ();
    }
}
