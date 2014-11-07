//
// ManagerTime.cs
//
// Author:
//       Luis Alejandro Vieira <lavz24@gmail.com>
//
// Copyright (c) 2014 
//
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class ManagerTime : Singleton<ManagerTime>
{
    public static float TimeToStart = 5.0f;
    float currentTimeToStart = 0;
    public float getCurrentTimeToStart { get { return currentTimeToStart; }}
   
    public float TimeOfGame = 60.0f;
    float currentTimeOfGame = 0;
    public float getCurrentTimeOfGame{get{ return currentTimeOfGame; }}

    /// <summary>
    /// Funciones para cuando el tiempo comienza
    /// </summary>
    [HideInInspector]
    public List<Signal> onTimeGameStart = new List<Signal>();
    public void TimeGameStar(){

        foreach (Signal sig in onTimeGameStart) {

            sig.Invoke ();
        }
    }

    /// <summary>
    /// Funciones para cuando el tiempo se acabe
    /// </summary>
    public delegate void onTimeGameEndBroadcast();
    public event onTimeGameEndBroadcast onTimeGameEnd;

    /// <summary>
    /// Subscribes funct to OnPauseGame.
    /// </summary>
    /// <param name="funct">Funct.</param>
    public static void SubscribeOnTimeGameEnd(onTimeGameEndBroadcast funct){
        Instance.onTimeGameEnd+=funct;
    }
    /// <summary>
    /// Unsubscribe safetely funct Function to OnPauseGameDelegate
    /// </summary>
    /// <param name="funct">Function.</param>
    public static void UnSubscribeOnTimeGameEnd(onTimeGameEndBroadcast funct){
        if(Instance != null)
            Instance.onTimeGameEnd-=funct;
    }



    protected override void AwakeChild(){
        switch (ManagerDoors.numberOfPair) {

        case NumberOfPair.CincoXSeis:


            break;
        case NumberOfPair.CincoXSeisNormal:
            TimeOfGame *= 3;

            break;
        case NumberOfPair.CuatroXCuatro:
            TimeOfGame *= 2;

            break;
        }

      //  Invoke("setTimeToStart",0.1f);

      
    }
    bool firstRun = true;
    void OnEnable(){
        ManagerPause.SubscribeOnPauseGame(onPaused);
        ManagerPause.SubscribeOnResumeGame( onResume);
    }
    void OnDisable(){

        ManagerPause.UnSubscribeOnPauseGame(onPaused);
        ManagerPause.UnSubscribeOnResumeGame(onResume);
    }
    void setTimeToStart(){
        currentTimeToStart = TimeToStart;

    }
        
    void LateUpdate(){
        if(firstRun){
            firstRun = false;
            setTimeToStart ();
            return;
        }
        if (!isPaused && !stopTime) {
            if (currentTimeOfGame > 0) {
                currentTimeOfGame -= Time.deltaTime;
                if (currentTimeOfGame <= 0) {
                    //Aqui hago lo q pasa cuando se pierde.
                    currentTimeOfGame = 0;
                    ManagerDoors.Instance.CanTouch = false;
                    ManagerPause.Instance.Pause = true;
                    onTimeGameEnd ();
                    enabled = false;
                }

            } else if (currentTimeToStart > 0) {
        
                currentTimeToStart -= Time.deltaTime;
                if (currentTimeToStart <= 0) {
                   

                    currentTimeOfGame = TimeOfGame;
                    TimeGameStar ();
                    ManagerDoors.Instance.CanTouch = true;

                }
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

    #region Stop:
    /// <summary>
    /// Guarda lo signal para cuando se para el tiempo
    /// </summary>
    [HideInInspector]
    public List<Signal> onStopTime = new List<Signal>();
     void StopTimer(){

        foreach (Signal sig in onStopTime) {

            sig.Invoke ();
        }
    }
    /// <summary>
    /// Guarda los signal para cuando se da play al tiempo
    /// </summary>
    [HideInInspector]
    public List<Signal> onPlayTime = new List<Signal>();
    public void PlayTimer(){

        foreach (Signal sig in onPlayTime) {

            sig.Invoke ();
        }
    }

    /// <summary>
    /// Si el tiempo esta parado
    /// </summary>
    bool stopTime = false;
    bool isTika = false;
    /// <summary>
    /// Cuando se quiere parar el tiempo
    /// </summary>
    public void onStop(bool tika){
        if (!isTika && tika) {
            isTika = true;
        }

        if (!stopTime) {
            stopTime = true;
            StopTimer ();
        }
    
    }
    /// <summary>
    /// Cuando se quiere reanudar el tiempo
    /// </summary>
    public void onPlay(bool tika){
        if (ManagerDoors.Instance.CheckWinGame())
            return;

        if (isTika) {
            if (tika) {
        
                isTika = false;
                stopTime = false;
                PlayTimer ();
            } else if (!tika) {
            }
        } else {
            stopTime = false;
            PlayTimer ();
        }
     
    }
    #endregion
}

