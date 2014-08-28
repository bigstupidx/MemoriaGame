using UnityEngine;
using System.Collections;

public class ClockTimer : MonoBehaviour {
    public GameObject Arrow;

    public UI2DSpriteAnimation animSpriteRed;
    public UI2DSpriteAnimation animSpriteYellow;

    public TweenScale scaleArrow;
    [Range (0,100)]
    public float toYellow = 70.0f;
    bool playYellow = false;

    [Range (0,100)]
    public float toExplode = 35.0f;
    float magicConstTime = 0.0f;
    bool isTimeGameStart = false;

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

    void Start(){
        magicConstTime = 100.0f / ManagerTime.Instance.TimeOfGame;
    }
	
    [Signal]
	void onTimeGameStart () {
        tween = TweenRotation.Begin (Arrow, ManagerTime.Instance.TimeOfGame, Quaternion.identity);
        tween.to=new Vector3(0,0,-90);

        isTimeGameStart = true;
       
	}
    [Signal]
    public void onPaused(){
        tween.enabled = false;
        currentStop = new Vector3(0,0,Mathf.Rad2Deg *  transform.rotation.z * 2.0f);
    }
    [Signal]
    public void onResume(){
        tween.from = currentStop;

        tween = TweenRotation.Begin (Arrow, ManagerTime.Instance.getCurrentTimeOfGame, Quaternion.identity);
        tween.from = currentStop;
        tween.to = new Vector3(0,0,-90);

       // tween.enabled = true;

    }


    void Update(){
        if (isTimeGameStart) {
            float value = (magicConstTime * ManagerTime.Instance.getCurrentTimeOfGame);
            if (value <= toExplode ) {
                if (!animSpriteRed.isPlaying) {
                    scaleArrow.PlayForward ();
                    animSpriteRed.Play ();
                }
            }else if (value <= toYellow) {
                if (!playYellow) {
                    animSpriteYellow.Play ();
                    playYellow = true;
                }
             
     
            }
           
        }
    }

}
