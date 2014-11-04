using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AudioSource))]
public class AS_SoundPlay : MonoBehaviour {


    public void Playaudio(){
        audio.volume = ManagerSound.Instance.fxVolume;
        audio.Play ();
    }
}
