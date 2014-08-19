﻿using UnityEngine;
using System.Collections;

public class ClockTimer : MonoBehaviour {


    TweenRotation tween;
	// Use this for initialization
	void Awake () {
        ManagerTime.Instance.onTimeGameStar.Add (new Signal("onTimeGameStart",gameObject));
        ManagerTime.Instance.onStopTime.Add (new Signal("onPaused",gameObject));
        ManagerTime.Instance.onPlayTime.Add (new Signal("onResume",gameObject));
        

	}
	
    [Signal]
	void onTimeGameStart () {
        tween = TweenRotation.Begin (gameObject, ManagerTime.Instance.TimeOfGame, Quaternion.identity);
        tween.to=new Vector3(0,0,-90);
       
	}
    [Signal]
    public void onPaused(){
        tween.enabled = false;

    }
    [Signal]
    public void onResume(){
        tween = TweenRotation.Begin (gameObject, ManagerTime.Instance.getCurrentTimeOfGame, Quaternion.identity);
        tween.to=new Vector3(0,0,-90);

       // tween.enabled = true;

    }
}
