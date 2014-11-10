using UnityEngine;
using System.Collections;

public class AS_BGController : MonoBehaviour {

    public AudioClip bg_01;
    public AudioClip bg_02;

    public AudioClip bg_Loser;

    public AudioClip bg_win01;
    public AudioClip bg_win02;

    public AudioClip bg_Pause;

    public AudioSource bg;
    public AudioSource bg2;

	// Use this for initialization
	void Start () {

        ChangueBGNormalReset ();


        firstRun = true;
        ManagerDoors.SubscribeOnVictory (ChangueOnVictory);
        ManagerTime.SubscribeOnTimeGameEnd (ChangueOnLoose);

        ManagerPause.SubscribeOnPauseGame (onPaused);
        ManagerPause.SubscribeOnResumeGame (onResume);
	}
	
	// Update is called once per frame
	void ChangueBGNormal () {
        bg2.loop = true;
        bg2.clip = bg_02;
        bg2.Play ();
        bg.Pause ();

	}
    void ChangueBGNormalReset () {
       // bg.Pause ();
        bg.loop = false;
        bg.clip = bg_01;
        bg.Play ();
        Invoke ("ChangueBGNormal",bg_01.length*0.95f);
    }
    void ChangueBGPause () {
        bg2.Pause ();
        bg.loop = true;
        bg.clip = bg_Pause;
        bg.Play ();


    }
    void ChangueOnLoose(){
        bg2.Pause ();
        bg.loop = true;
        bg.clip = bg_Loser;
        bg.Play ();

    }
    void ChangueOnVictory(){
        bg2.Pause ();
        bg.loop = true;
        bg.clip = bg_win01;
        bg.Play ();
        Invoke ("ChangueOnVictory02",bg_win01.length);

    }
    void ChangueOnVictory02(){
        bg.loop = true;
        bg.clip = bg_win02;
        bg.Play ();
    }

    bool firstRun = true;
    void OnEnable(){

        if (!firstRun) {
            ManagerDoors.SubscribeOnVictory (ChangueOnVictory);
            ManagerPause.SubscribeOnPauseGame (onPaused);
            ManagerPause.SubscribeOnResumeGame (onResume);
        }

    }
    void OnDisable(){
        ManagerDoors.UnSubscribeOnVictory (ChangueOnVictory);
        ManagerPause.UnSubscribeOnPauseGame(onPaused);
        ManagerPause.UnSubscribeOnResumeGame(onResume);

    }

    void onPaused(){
        ChangueBGPause ();
    }
    void onResume(){
        ChangueBGNormal ();
    }
}
