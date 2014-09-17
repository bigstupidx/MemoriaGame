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

    TweenRotation tweenRota;
    Vector3 currentStop;
	// Use this for initialization
	void Awake () {
        ManagerTime.Instance.onTimeGameStart.Add (new Signal("onTimeGameStart",gameObject));
        ManagerTime.Instance.onStopTime.Add (new Signal("onPaused",gameObject));
        ManagerTime.Instance.onPlayTime.Add (new Signal("onResume",gameObject));
        
    
	}
    void OnEnable(){
        ManagerPause.SubscribeOnPauseGame(onPaused);
        ManagerPause.SubscribeOnResumeGame( onResume);
    }
    void OnDisable(){

        ManagerPause.UnSubscribeOnPauseGame(onPaused);
        ManagerPause.UnSubscribeOnResumeGame(onResume);
    }
    void Start(){
        magicConstTime = 100.0f / ManagerTime.Instance.TimeOfGame;
    }
	
    [Signal]
	void onTimeGameStart () {
        tweenRota = TweenRotation.Begin (Arrow, ManagerTime.Instance.TimeOfGame, Quaternion.identity);
        tweenRota.to=new Vector3(0,0,-90);

        isTimeGameStart = true;
       
	}
    public void onPaused(){
        tweenRota.enabled = false;
        currentStop = new Vector3(0,0,Mathf.Rad2Deg *  Arrow.transform.rotation.z * 2.0f);
    }
    public void onResume(){
        tweenRota.from = currentStop;

        tweenRota = TweenRotation.Begin (Arrow, ManagerTime.Instance.getCurrentTimeOfGame, Quaternion.identity);
        tweenRota.from = currentStop;
        tweenRota.to = new Vector3(0,0,-90);

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
