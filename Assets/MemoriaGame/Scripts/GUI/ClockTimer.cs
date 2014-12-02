using UnityEngine;
using System.Collections;

public class ClockTimer : MonoBehaviour {
    public UI2DSprite Arrow;

    public UI2DSpriteAnimation animSpriteRed;
    public UI2DSpriteAnimation animSpriteYellow;

    public TweenScale scaleArrow;
    [Range (0,100)]
    public float toYellow = 70.0f;
    bool playYellow = false;
	bool playRed = false;
    [Range (0,100)]
    public float toExplode = 35.0f;
    float magicConstTime = 0.0f;
    bool isTimeGameStart = false;

    TweenRotation tweenRota;
    Vector3 currentStop;

    public UI2DSpriteAnimation freezeAnimation;
    public Sprite ArrowSprite;
    public Sprite ArrowFreeze;
    public Sprite BaseStateClock;
    bool isFreezing = false;

    public AudioClip clipBefore;
    public AudioClip clipStay;
    public AudioClip clipAfter;

    public AudioClip clipRed;
    public AudioClip clipYellow;
	// Use this for initialization
	void Awake () {
        ManagerTime.Instance.onTimeGameStart.Add (new Signal("onTimeGameStart",gameObject));
    //    ManagerTime.Instance.onStopTime.Add (new Signal("onPaused",gameObject));
    //    ManagerTime.Instance.onPlayTime.Add (new Signal("onResume",gameObject));
        
    
	}
    bool firstRun=true;
    void OnEnable(){
        if (!firstRun) {
            ManagerTime.Instance.onTimeGameEnd += StopAllSound;

            ManagerPause.SubscribeOnPauseGame (onPaused);
            ManagerPause.SubscribeOnResumeGame (onResume);
        }
    }
    void OnDisable(){
        ManagerPause.UnSubscribeOnPauseGame(onPaused);
        ManagerPause.UnSubscribeOnResumeGame(onResume);
    }
    void Start(){
        magicConstTime = 100.0f / ManagerTime.Instance.TimeOfGame;

        ManagerTime.Instance.onTimeGameEnd += StopAllSound;
        ManagerDoors.Instance.onVictory += StopAllSound;

        ManagerPause.SubscribeOnPauseGame (onPaused);
        ManagerPause.SubscribeOnResumeGame (onResume);

        ManagerTime.Instance.onStopTime.Add (new Signal("onPausedFreezee",gameObject)); 
        ManagerTime.Instance.onPlayTime.Add (new Signal("onResumeFreeze",gameObject));  

        firstRun = false;

    }
    void StopAllSound () {
        audio.Stop ();
    }
    [Signal]
	void onTimeGameStart () {
        tweenRota = TweenRotation.Begin (Arrow.gameObject, ManagerTime.Instance.TimeOfGame, Quaternion.identity);
        tweenRota.to=new Vector3(0,0,-90);

        isTimeGameStart = true;
       
	}
    public void onPaused(){
        tweenRota.enabled = false;
        currentStop = new Vector3(0,0,Mathf.Rad2Deg *  Arrow.transform.rotation.z * 2.0f);

        if(isFreezing || playRed)
            audio.Stop ();

    }
    [Signal]
    public void onPausedFreezee(){
        tweenRota.enabled = false;
        currentStop = new Vector3(0,0,Mathf.Rad2Deg *  Arrow.transform.rotation.z * 2.0f);

       
    }
    [Signal]
    public void onResumeFreeze(){
            tweenRota.from = currentStop;

            tweenRota = TweenRotation.Begin (Arrow.gameObject, ManagerTime.Instance.getCurrentTimeOfGame, Quaternion.identity);
            tweenRota.from = currentStop;
            tweenRota.to = new Vector3 (0, 0, -90);

    }
    public void onResume(){
        if (!isFreezing) {
            tweenRota.from = currentStop;

            tweenRota = TweenRotation.Begin (Arrow.gameObject, ManagerTime.Instance.getCurrentTimeOfGame, Quaternion.identity);
            tweenRota.from = currentStop;
            tweenRota.to = new Vector3 (0, 0, -90);
        }

        if(isFreezing || playRed)
            audio.Play ();
       // tween.enabled = true;

    }

    public void Freeze(){
        if (playRed) {
            animSpriteRed.Pause ();
            scaleArrow.enabled = false;
        } else if (playYellow) {
            animSpriteYellow.Pause ();
        }
        Arrow.sprite2D = ArrowFreeze;
        freezeAnimation.Play ();
        isFreezing = true;

        SetAudio (clipBefore,false);

        Invoke ("AudioStayPlay",clipBefore.length);
    }
    void AudioStayPlay(){
    
        SetAudio (clipStay,true);

    }
    void AudioRedPlay(){

        SetAudio (clipRed,true);

    }
    void AudioYellowPlay(){

        SetAudio (clipYellow,false);

    }
    public void NotFreeze(){

        isFreezing = false;
        freezeAnimation.Pause ();
        Arrow.sprite2D = ArrowSprite;
        SetAudio (clipAfter,false);

        if (playRed) {
            scaleArrow.PlayForward ();
            animSpriteRed.Play ();
            Invoke ("AudioRedPlay",clipAfter.length);

        } else if (playYellow) {
            animSpriteYellow.Play ();
            Invoke ("AudioYellowPlay",clipAfter.length);

        } else {
            GetComponent<UI2DSprite> ().sprite2D = BaseStateClock;
        }
            


    }

    void SetAudio(AudioClip clipsito, bool loopsito){
        if (audio.isPlaying) {
            audio.Pause ();
        }

        audio.volume = ManagerSound.Instance.fxVolume;
        audio.clip = clipsito;
        audio.loop = loopsito;
        audio.Play ();
    }

    void Update(){
        if (isTimeGameStart && !isFreezing) {
            float value = (magicConstTime * ManagerTime.Instance.getCurrentTimeOfGame);
            if (value <= toExplode) {
                if (!animSpriteRed.isPlaying) {
                    scaleArrow.PlayForward ();
                    animSpriteRed.Play ();
                    playRed = true;

                    AudioRedPlay ();
                }
            } else if (value <= toYellow) {
                if (!playYellow) {
                    animSpriteYellow.Play ();
                    playYellow = true;

                    AudioYellowPlay ();
                }
             
     
            
           
            }
        }
    }

    public void PlayFinalClock (float timerAux){
        freezeAnimation.Pause ();
        Arrow.sprite2D = ArrowSprite;
        if (playRed) {
            scaleArrow.PlayForward ();
            animSpriteRed.Play ();

        } else if (playYellow) {
            animSpriteYellow.Play ();
        } else {
            GetComponent<UI2DSprite> ().sprite2D = BaseStateClock;
        }

        tweenRota.from = currentStop;

        tweenRota = TweenRotation.Begin (Arrow.gameObject, timerAux, Quaternion.identity);
        tweenRota.from = currentStop;
        tweenRota.to = new Vector3(0,0,-90);
    }

}


