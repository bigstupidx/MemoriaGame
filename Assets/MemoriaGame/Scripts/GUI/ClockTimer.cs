using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ClockTimer : MonoBehaviour
{
    public Animator clockAnim;
    [Range (0, 100)]
    public float toYellow = 70.0f;
    bool playYellow = false;
    bool playRed = false;
    [Range (0, 100)]
    public float toExplode = 35.0f;
    float magicConstTime = 0.0f;
    bool isTimeGameStart = false;

    TweenRotation tweenRota;
    Vector3 currentStop;

    public Animator ArrowAnimator;
    public TweenScale scaleArrow;
    bool isFreezing = false;

    public AudioClip clipBefore;
    public AudioClip clipStay;
    public AudioClip clipAfter;

    public AudioClip clipRed;
    public AudioClip clipYellow;

    AudioSource _audio;

    public AudioSource audio {
    
        get {
            if (_audio == null)
                _audio = audio;
            return _audio;
        }
    }
    // Use this for initialization
    void Awake ()
    {
        ManagerTime.Instance.onTimeGameStart += onTimeGameStart;
    }

    bool firstRun = true;

    void OnEnable ()
    {
        if (!firstRun) {
            ManagerTime.Instance.onTimeGameEnd += StopAllSound;

            ManagerPause.SubscribeOnPauseGame (onPaused);
            ManagerPause.SubscribeOnResumeGame (onResume);
        }
    }

    void OnDisable ()
    {
        ManagerPause.UnSubscribeOnPauseGame (onPaused);
        ManagerPause.UnSubscribeOnResumeGame (onResume);
    }

    void Start ()
    {
        magicConstTime = 100.0f / ManagerTime.Instance.TimeOfGame;

        ManagerTime.Instance.onTimeGameEnd += StopAllSound;
        ManagerDoors.Instance.onVictory += StopAllSound;

        ManagerPause.SubscribeOnPauseGame (onPaused);
        ManagerPause.SubscribeOnResumeGame (onResume);

        ManagerTime.Instance.onStopTime += onPausedFreezee; 
        ManagerTime.Instance.onPlayTime.Add (new Signal ("onResumeFreeze", gameObject));  

        firstRun = false;

    }

    void StopAllSound ()
    {
        audio.Stop ();
    }

    void onTimeGameStart ()
    {
        tweenRota = TweenRotation.Begin (ArrowAnimator.gameObject, ManagerTime.Instance.TimeOfGame, Quaternion.identity);
        tweenRota.to = new Vector3 (0, 0, -90);

        isTimeGameStart = true;
       
    }

    public void onPaused ()
    {
        tweenRota.enabled = false;
        currentStop = new Vector3 (0, 0, Mathf.Rad2Deg * ArrowAnimator.transform.rotation.z * 2.0f);

        if (isFreezing || playRed)
            audio.Stop ();

    }

    [Signal]
    public void onPausedFreezee ()
    {
        tweenRota.enabled = false;
        currentStop = new Vector3 (0, 0, Mathf.Rad2Deg * ArrowAnimator.transform.rotation.z * 2.0f);

       
    }

    [Signal]
    public void onResumeFreeze ()
    {
        tweenRota.from = currentStop;

        tweenRota = TweenRotation.Begin (ArrowAnimator.gameObject, ManagerTime.Instance.getCurrentTimeOfGame, Quaternion.identity);
        tweenRota.from = currentStop;
        tweenRota.to = new Vector3 (0, 0, -90);

    }

    public void onResume ()
    {
        if (!isFreezing) {
            tweenRota.from = currentStop;

            tweenRota = TweenRotation.Begin (ArrowAnimator.gameObject, ManagerTime.Instance.getCurrentTimeOfGame, Quaternion.identity);
            tweenRota.from = currentStop;
            tweenRota.to = new Vector3 (0, 0, -90);
        }

        if (isFreezing || playRed)
            audio.Play ();
        // tween.enabled = true;

    }

    public void Freeze ()
    {
        if (playRed) {
            scaleArrow.enabled = false;
        } else if (playYellow) {
        }
        clockAnim.SetBool ("Frezze", true);
        ArrowAnimator.SetBool ("Frezze", true);
        isFreezing = true;

        SetAudio (clipBefore, false);

        Invoke ("AudioStayPlay", clipBefore.length);
    }

    void AudioStayPlay ()
    {
    
        SetAudio (clipStay, true);

    }

    void AudioRedPlay ()
    {

        SetAudio (clipRed, true);

    }

    void AudioYellowPlay ()
    {

        SetAudio (clipYellow, false);

    }

    public void NotFreeze ()
    {

        isFreezing = false;
        ArrowAnimator.SetBool ("Frezze", false);
        clockAnim.SetBool ("Frezze", false);

        SetAudio (clipAfter, false);

        if (playRed) {
            scaleArrow.PlayForward ();
            Invoke ("AudioRedPlay", clipAfter.length);

        } else if (playYellow) {
            Invoke ("AudioYellowPlay", clipAfter.length);

        } 
    }

    void SetAudio (AudioClip clipsito, bool loopsito)
    {
        if (audio.isPlaying) {
            audio.Pause ();
        }

        audio.volume = ManagerSound.Instance.fxVolume;
        audio.clip = clipsito;
        audio.loop = loopsito;
        audio.Play ();
    }

    void Update ()
    {
        if (isTimeGameStart && !isFreezing) {
            float value = (magicConstTime * ManagerTime.Instance.getCurrentTimeOfGame);
            if (value <= toExplode) {
                if (!clockAnim.GetBool ("Red")) {
                    scaleArrow.PlayForward ();
                    clockAnim.SetBool ("Red", true);
                    clockAnim.SetBool ("Yellow", false);
                    playRed = true;

                    AudioRedPlay ();
                }
            } else if (value <= toYellow) {
                if (!playYellow) {
                    clockAnim.SetBool ("Red", false);
                    clockAnim.SetBool ("Yellow", true);
                    playYellow = true;
                    AudioYellowPlay ();
                }
            }
        }
    }

    public void PlayFinalClock (float timerAux)
    {
        ArrowAnimator.SetBool ("Frezze", false);
        if (playRed) {
            scaleArrow.PlayForward ();
            clockAnim.SetBool ("Red", true);
            clockAnim.SetBool ("Yellow", false);

        } else if (playYellow) {
            clockAnim.SetBool ("Red", false);
            clockAnim.SetBool ("Yellow", true);
        } else {
            clockAnim.SetBool ("Red", false);
            clockAnim.SetBool ("Yellow", false);  
        }

        tweenRota.from = currentStop;

        tweenRota = TweenRotation.Begin (ArrowAnimator.gameObject, timerAux, Quaternion.identity);
        tweenRota.from = currentStop;
        tweenRota.to = new Vector3 (0, 0, -90);
    }

}


