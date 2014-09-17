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
    [HideInInspector]
    public List<Signal> onTimeGameEnd = new List<Signal>();
    public void TimeGameEnd(){

        foreach (Signal sig in onTimeGameEnd) {

            sig.Invoke ();
        }
    }



    protected override void Awake(){
        base.Awake();
        Invoke("setTimeToStart",0.1f);

      
    }

    void OnEnable(){
        ManagerPause.Instance.OnPauseGame += onPaused;
        ManagerPause.Instance.OnResumeGame += onResume;
    }
    void OnDisable(){
        ManagerPause.Instance.OnPauseGame -= onPaused;
        ManagerPause.Instance.OnResumeGame -= onResume;
    }
    void setTimeToStart(){
        currentTimeToStart = TimeToStart;

    }
        
    void Update(){
        if (!isPaused && !stopTime) {
            if (currentTimeOfGame > 0) {
                currentTimeOfGame -= Time.deltaTime;
                if (currentTimeOfGame <= 0) {
                    //Aqui hago lo q pasa cuando se pierde.

                    ManagerDoors.Instance.CanTouch = false;
                    TimeGameEnd ();

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
    /// <summary>
    /// Cuando se quiere parar el tiempo
    /// </summary>
    public void onStop(){
        stopTime = true;
        StopTimer ();
    }
    /// <summary>
    /// Cuando se quiere reanudar el tiempo
    /// </summary>
    public void onPlay(){
        stopTime = false;
        PlayTimer ();
    }
    #endregion
}

