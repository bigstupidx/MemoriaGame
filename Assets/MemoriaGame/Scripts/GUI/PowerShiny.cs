using UnityEngine;
using System.Collections;

public class PowerShiny : MonoBehaviour {

    public AudioSource brillitoAudio;

    public UI2DSpriteAnimation anim;
     PowerOff button;

    public const float TimeRepeat = 7.0f;
    bool play = true;
    TimeCallBacks timer  = new TimeCallBacks();

    void Awake(){
        button = GetComponent<PowerOff> ();
    }
	// Update is called once per frame
	void LateUpdate () {
        if (button.isEnabled) {
        
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
}
