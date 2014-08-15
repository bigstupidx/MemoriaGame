using UnityEngine;
using System.Collections;

public class ClockTimer : MonoBehaviour {


    TweenRotation tween;
	// Use this for initialization
	void Awake () {
        ManagerTime.Instance.onTimeGameStar.Add (new Signal("onTimeGameStart",gameObject));
	}
	
    [Signal]
	void onTimeGameStart () {
        tween = TweenRotation.Begin (gameObject, ManagerTime.Instance.TimeOfGame, Quaternion.identity);
        tween.to=new Vector3(0,0,-90);
       
	}

    void onPaused(){
        tween.enabled = false;

    }
    void onResume(){
        tween = TweenRotation.Begin (gameObject, ManagerTime.Instance.getCurrentTimeOfGame, Quaternion.identity);
        tween.to=new Vector3(0,0,-90);

       // tween.enabled = true;

    }
}
