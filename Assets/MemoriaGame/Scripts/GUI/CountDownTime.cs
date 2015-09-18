//
// CountDownTime.cs
//
// Author:
//       Luis Alejandro Vieira <lavz24@gmail.com>
//
// Copyright (c) 2014 
//
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent (typeof(Text))]
public class CountDownTime : MonoBehaviour
{
    Text _label;

    public Text label {
        get {
            if (_label == null)
                _label = GetComponent<Text> ();
            return _label;
        }

    }

    public AudioClip clipCount0;
    AudioSource _audio;

    public AudioSource audio {
        get {
            if (_audio == null)
                _audio = GetComponent<AudioSource> ();
            
            return _audio;
        }

    }

    public Button Pausebutton;
    public string last = "6";
    // Update is called once per frame
    void LateUpdate ()
    {
        label.text = ((int)ManagerTime.Instance.getCurrentTimeToStart).ToString ();
       
        if (label.text != last && ManagerTime.Instance.getCurrentTimeToStart >= 1) {
            audio.volume = ManagerSound.Instance.fxVolume;
            audio.Play ();
        } else if (label.text != last && ManagerTime.Instance.getCurrentTimeToStart > 0) {
            audio.clip = clipCount0;
            audio.Play ();

        }
        if (ManagerTime.Instance.getCurrentTimeToStart < 0) {
            Pausebutton.interactable = true;
            gameObject.SetActive (false);
        }
     
        last = label.text;
    }
}
