using UnityEngine;
using System.Collections;

public class PowerShiny : MonoBehaviour {

    public AudioSource brillitoAudio;

    public UI2DSpriteAnimation anim;
     PowerOff button;

    public const float TimeRepeat = 7.0f;
    bool play = true;
    TimeCallBacks timer  = new TimeCallBacks();
    bool firstRun = true;

    void Awake(){
        button = GetComponent<PowerOff> ();
    }
    void Start(){
        ManagerTime.Instance.onTimeGameEnd += GameFinished;
        ManagerDoors.Instance.onVictory += GameFinished;
        firstRun = false;
        ManagerPause.SubscribeOnPauseGame (onPaused);
        ManagerPause.SubscribeOnResumeGame (onResume);
    }
    void OnEnable(){
        if (!firstRun) {
            ManagerPause.SubscribeOnPauseGame (onPaused);
            ManagerPause.SubscribeOnResumeGame (onResume);
        }
    }
    void OnDisable(){

        ManagerPause.UnSubscribeOnPauseGame(onPaused);
        ManagerPause.UnSubscribeOnResumeGame(onResume);
    }
    void GameFinished(){

        button.isEnabled = false;
    }
	// Update is called once per frame
	void LateUpdate () {
        if (button.isEnabled && !isPaused) {
        
            if (play) {

                anim.Play ();
                brillitoAudio.volume = ManagerSound.Instance.fxVolume;

                brillitoAudio.Play ();
                play = false;
                Invoke ("PlayAgainIn", TimeRepeat);
            }
        } else{
            if (!play) {
                if (anim.isPlaying) {
                    anim.Pause ();
                   
                }
                CancelInvoke ("PlayAgainIn");
                PlayAgainIn ();
            }
        }
	}
    void PlayAgainIn(){

        play = true;
    }

    IEnumerator PlayAgain(float time) {
        yield return timer.WaitPause(time);
        play = true;

       // anim.ResetToBeginning ();

    }

    #region Paused
    bool isPaused = false;
    void onPaused(){
        isPaused = true;

    }
    void onResume(){
        isPaused = false;

    }
    #endregion

}
