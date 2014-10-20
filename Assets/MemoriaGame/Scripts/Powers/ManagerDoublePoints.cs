//
//  ManagerDoublePoints.cs
//
//  Author:
//       Luis Alejandro Vieira <lavz24@gmail.com>
//
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ManagerDoublePoints: Singleton<ManagerDoublePoints> {

    bool usedPower = false;

    public float MaxTime = 0;
    public const int PlusScore = 1;
    float currentTime = 0;
    public delegate void onActiveBroadcast(bool activate);
    public event onActiveBroadcast OnActivePower;

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

        usedPower = true;

        currentTime = MaxTime;
        ManagerScore.Instance.SetPlusScore(PlusScore);

        if (OnActivePower != null)
            OnActivePower (true);

    }
    void DeActivePower(){

        ManagerScore.Instance.SetPlusScore(-1*PlusScore);

        ManagerPowers.Instance.UsingPower = false;

        if (OnActivePower != null)
            OnActivePower (false);
    }

    void Update(){
        if (!isPaused && currentTime> 0 ) {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0) {
                DeActivePower ();
            }
        }
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

