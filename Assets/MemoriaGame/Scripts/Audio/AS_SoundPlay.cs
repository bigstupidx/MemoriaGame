using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AudioSource))]
public class AS_SoundPlay : MonoBehaviour
{

    AudioSource _audio;

    public AudioSource audio {
    
        get {

            if (_audio == null)
                _audio = GetComponent<AudioSource> ();
            return _audio;
        }

    }

    public void Playaudio ()
    {
        audio.volume = ManagerSound.Instance.fxVolume;
        audio.Play ();
    }
}
