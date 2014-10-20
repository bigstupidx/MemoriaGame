using UnityEngine;
using System.Collections;

public class ManagerStopTimePower : Singleton<ManagerStopTimePower> {


    public float TimeStop = 4.0f;
    float currentTimeStop = 0;
    bool usedPower = false;
    public ClockTimer clock;

    void OnEnable(){
        ManagerPause.SubscribeOnPauseGame(onPaused);
        ManagerPause.SubscribeOnResumeGame( onResume);
    }
    void OnDisable(){

        ManagerPause.UnSubscribeOnPauseGame(onPaused);
        ManagerPause.UnSubscribeOnResumeGame(onResume);
    }

    public void ActivePower(){
        if (isPaused || usedPower ){
            return;
        }
        ManagerPowers.Instance.UsingPower = true;

        currentTimeStop = TimeStop;
        clock.Freeze ();
        ManagerTime.Instance.onStop (true);

    }
    public void DeActivePower(){
        ManagerTime.Instance.onPlay (true);
        usedPower = true;
        clock.NotFreeze ();
        ManagerPowers.Instance.UsingPower = false;
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

