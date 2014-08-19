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

    [HideInInspector]
    public List<Signal> onTimeGameStar = new List<Signal>();
    [HideInInspector]
    public List<Signal> onTimeGameEnd = new List<Signal>();

    void Awake(){
        Invoke("setTimeToStart",0.1f);
    }
    void setTimeToStart(){
        currentTimeToStart = TimeToStart;

    }
    public void TimeGameStar(){

        foreach (Signal sig in onTimeGameStar) {

            sig.Invoke ();
        }
    }
    public void TimeGameEnd(){

        foreach (Signal sig in onTimeGameEnd) {

            sig.Invoke ();
        }
    }
    void Update(){
        if (!isPaused) {
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
    bool isPaused = false;
    void onPaused(){
        isPaused = true;

    }
    void onResume(){
        isPaused = false;

    }

}

