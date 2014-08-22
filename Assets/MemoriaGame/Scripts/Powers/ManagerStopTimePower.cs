using UnityEngine;
using System.Collections;

public class ManagerStopTimePower : Singleton<ManagerStopTimePower> {


    public float TimeStop = 4.0f;
    float currentTimeStop = 0;
    bool usedPower = false;


    void Awake(){

        ManagerPause.Instance.onGamePaused.Add (new Signal ("onPaused", gameObject));
        ManagerPause.Instance.onGameResumed.Add (new Signal ("onResume", gameObject));

    }

    public void ActivePower(){
        if (isPaused || usedPower){
            return;
        }
        currentTimeStop = TimeStop;

        ManagerTime.Instance.onStop ();

    }
    public void DeActivePower(){
        ManagerTime.Instance.onPlay ();
        usedPower = true;

    }

    #region Paused
    bool isPaused = false;
    [Signal]
    void onPaused(){
        isPaused = true;

    }
    [Signal]
    void onResume(){
        isPaused = false;

    }
    #endregion


    void Update(){
        if (isPaused || usedPower)
            return ;
      
        if(currentTimeStop > 0){
            currentTimeStop -= Time.deltaTime;
            if (currentTimeStop <= 0) {
                DeActivePower ();
            }
        }
    }

}

