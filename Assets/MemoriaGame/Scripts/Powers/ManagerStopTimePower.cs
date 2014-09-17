using UnityEngine;
using System.Collections;

public class ManagerStopTimePower : Singleton<ManagerStopTimePower> {


    public float TimeStop = 4.0f;
    float currentTimeStop = 0;
    bool usedPower = false;


    void OnEnable(){
        ManagerPause.Instance.OnPauseGame += onPaused;
        ManagerPause.Instance.OnResumeGame += onResume;
    }
    void OnDisable(){
        ManagerPause.Instance.OnPauseGame -= onPaused;
        ManagerPause.Instance.OnResumeGame -= onResume;
    }

    public void ActivePower(){
        if (isPaused || usedPower ){
            return;
        }
        ManagerPowers.Instance.UsingPower = true;

        currentTimeStop = TimeStop;

        ManagerTime.Instance.onStop ();

    }
    public void DeActivePower(){
        ManagerTime.Instance.onPlay ();
        usedPower = true;

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

