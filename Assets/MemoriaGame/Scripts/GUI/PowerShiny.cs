using UnityEngine;
using System.Collections;

public class PowerShiny : MonoBehaviour
{

    public AudioSource brillitoAudio;
    public Animator anim;
    PowerOff _button;

    public PowerOff button {
        get {
        
            if (_button == null)
                _button = GetComponent<PowerOff> ();
            return _button;
        }
    }

    public const float TimeRepeat = 7.0f;
    bool play = true;
    TimeCallBacks timer = new TimeCallBacks ();
    bool firstRun = true;

    void Start ()
    {
        ManagerTime.Instance.onTimeGameEnd += GameFinished;
        ManagerDoors.Instance.onVictory += GameFinished;
        firstRun = false;
        ManagerPause.SubscribeOnPauseGame (onPaused);
        ManagerPause.SubscribeOnResumeGame (onResume);
    }

    void OnEnable ()
    {
        if (!firstRun) {
            ManagerPause.SubscribeOnPauseGame (onPaused);
            ManagerPause.SubscribeOnResumeGame (onResume);
        }
    }

    void OnDisable ()
    {

        ManagerPause.UnSubscribeOnPauseGame (onPaused);
        ManagerPause.UnSubscribeOnResumeGame (onResume);
    }

    void GameFinished ()
    {
        button.isEnabled = false;
    }
    // Update is called once per frame
    void LateUpdate ()
    {
        if (button.isEnabled && !isPaused) {
        
            if (play) {

                anim.SetTrigger ("Shiny");
                brillitoAudio.volume = ManagerSound.Instance.fxVolume;
                brillitoAudio.Play ();

                play = false;
                StartCoroutine ("PlayAgain", TimeRepeat);
            }
        } 
    }

    IEnumerator PlayAgain (float time)
    {
        yield return StartCoroutine (timer.WaitPause (time));
        play = true;

    }

    #region Paused

    bool isPaused = false;

    void onPaused ()
    {
        isPaused = true;
        anim.speed = 0;
    }

    void onResume ()
    {
        isPaused = false;
        anim.speed = 1;
    }

    #endregion

}
