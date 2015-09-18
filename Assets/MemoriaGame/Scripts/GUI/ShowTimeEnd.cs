﻿using UnityEngine;
using System.Collections;

public class ShowTimeEnd : MonoBehaviour
{
    public bool isForWin = false;
    public TweenAlpha_2 alphaT;
    public AudioClip Win;
    public AudioClip Lose;

    AudioSource _audio;

    public AudioSource audio {
        get {
        
            if (_audio == null)
                _audio = GetComponent<AudioSource> ();
            return _audio;
        }
    }

    void OnEnable ()
    {
        if (isForWin) {
            ManagerDoors.SubscribeOnVictory (StartAlpha);
            ManagerDoors.SubscribeOnVictory (Winer);
        } else {
            ManagerTime.SubscribeOnTimeGameEnd (StartAlpha);
            ManagerTime.SubscribeOnTimeGameEnd (Loser);
        }
    }

    void OnDisable ()
    {
        if (isForWin) {
            ManagerDoors.UnSubscribeOnVictory (StartAlpha);
            ManagerDoors.UnSubscribeOnVictory (Winer);
        } else {
            ManagerTime.UnSubscribeOnTimeGameEnd (StartAlpha);
            ManagerTime.UnSubscribeOnTimeGameEnd (Loser);
        }
    }

    void StartAlpha ()
    {
        alphaT.PlayForward ();
    }

    void Winer ()
    {
        audio.volume = ManagerSound.Instance.fxVolume;
        audio.clip = Win;
        audio.Play ();
    }

    void Loser ()
    {
        Invoke ("PlayAhh", alphaT.delay + 2.5f);
    }

    void PlayAhh ()
    {
        audio.volume = ManagerSound.Instance.fxVolume;
        audio.clip = Lose;
        audio.Play ();
    }
}
