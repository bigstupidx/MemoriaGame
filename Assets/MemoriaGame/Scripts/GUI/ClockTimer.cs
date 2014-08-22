using UnityEngine;
using System.Collections;

public class ClockTimer : MonoBehaviour {


    TweenRotation tween;
    Vector3 currentStop;
	// Use this for initialization
	void Awake () {
        ManagerTime.Instance.onTimeGameStart.Add (new Signal("onTimeGameStart",gameObject));
        ManagerTime.Instance.onStopTime.Add (new Signal("onPaused",gameObject));
        ManagerTime.Instance.onPlayTime.Add (new Signal("onResume",gameObject));
        
        ManagerPause.Instance.onGamePaused.Add (new Signal ("onPaused", gameObject));
        ManagerPause.Instance.onGameResumed.Add (new Signal ("onResume", gameObject));
	}
	
    [Signal]
	void onTimeGameStart () {
        tween = TweenRotation.Begin (gameObject, ManagerTime.Instance.TimeOfGame, Quaternion.identity);
        tween.to=new Vector3(0,0,-90);
       
	}
    [Signal]
    public void onPaused(){
        tween.enabled = false;
        currentStop = new Vector3(0,0,Mathf.Rad2Deg *  transform.rotation.z);
    }
    [Signal]
    public void onResume(){
        tween.from = currentStop;

        tween = TweenRotation.Begin (gameObject, ManagerTime.Instance.getCurrentTimeOfGame, Quaternion.identity);
        tween.from = currentStop;
        tween.to = new Vector3(0,0,-90);

       // tween.enabled = true;

    }




}
