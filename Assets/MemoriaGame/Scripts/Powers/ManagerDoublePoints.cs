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
    public int PlusScore = 2;
    float currentTime = 0;
    void Awake(){

        ManagerPause.Instance.onGamePaused.Add (new Signal ("onPaused", gameObject));
        ManagerPause.Instance.onGameResumed.Add (new Signal ("onResume", gameObject));

    }

    public void ActivePower(){
        if (isPaused || usedPower || ManagerPowers.UsingPower){
            return;
        }
        ManagerPowers.UsingPower = true;

        usedPower = true;

        currentTime = MaxTime;
        ManagerScore.Instance.SetPlusScore(PlusScore);

    }
    void DeActivePower(){

        ManagerScore.Instance.SetPlusScore(-1*PlusScore);

        ManagerPowers.UsingPower = false;
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
    [Signal]
    void onPaused(){
        isPaused = true;

    }
    [Signal]
    void onResume(){
            isPaused = false;

    }
    #endregion
}

